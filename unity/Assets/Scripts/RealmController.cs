using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using MongoDB.Bson;

public class RealmController : MonoBehaviour {

    public static RealmController Instance;

    public string realmAppId = "reinvent-kvqqn";

    private Realm _realm;
    private App _realmApp;
    private User _realmUser;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void OnDisable() {
        if(_realm != null) {
            _realm.Dispose();
        }
    }

    public async Task<string> Login(string email, string password) {
        if(email != "" && password != "") {
            _realmApp = App.Create(new AppConfiguration(realmAppId) {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            try {
                _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                _realm = await Realm.GetInstanceAsync(new SyncConfiguration(email, _realmUser));
                await _realm.GetSession().WaitForDownloadAsync();
            } catch (ClientResetException clientResetEx) {
                if(_realm != null) {
                    _realm.Dispose();
                }
                clientResetEx.InitiateClientReset();
            }
            return _realmUser.Id;
        }
        return "";
    }

    public async Task<string> Register(string name, string email, string password) {
        if(name != "" && email != "" && password != "") {
            try {
                var document = new BsonDocument {
                    { "name", name },
                    { "email", email },
                    { "password", password }
                };
                _realmApp = App.Create(new AppConfiguration(realmAppId) {
                    MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
                });
                await _realmApp.EmailPasswordAuth.RegisterUserAsync(email, password);
                return await Login(email, password);
            } catch (Exception ex) {
                Debug.Log(ex);
            }
        }
        return "";
    }

    public string GetAuthId() {
        return _realmUser != null ? _realmUser.Id : "";
    }

    public string GetAuthEmail() {
        return _realmUser != null ? _realmUser.Profile.Email : "";
    }

    public Player GetCurrentPlayer() {
        Player player = _realm.Find<Player>(_realmUser.Id);
        if(player == null) {
            _realm.Write(() => {
                player = _realm.Add(new Player(_realmUser.Id, _realmUser.Profile.Email));
            });
        }
        return player;
    }

}