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

    public string realmAppId = "demogames-sdyhk";

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
            } catch (Exception ex) {
                Debug.LogError(ex);
                return ex.Message;
            }
            return "";
        }
        return "Email / Password Required!";
    }

    public async void Logout() {
        await _realmUser.LogOutAsync();
        _realm.Dispose();
    }

    public async Task<string> Register(string name, string email, string password) {
        if(name != "" && email != "" && password != "") {
            try {
                _realmApp = App.Create(new AppConfiguration(realmAppId) {
                    MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
                });
                await _realmApp.EmailPasswordAuth.RegisterUserAsync(email, password);
                await Login(email, password);
                PlayerModel player = CreateCurrentPlayer(_realmUser.Id, name, email);
                return "";
            } catch (Exception ex) {
                Debug.LogError(ex);
                return ex.Message;
            }
        }
        return " All Fields Required!";
    }

    public string GetAuthId() {
        return _realmUser != null ? _realmUser.Id : "";
    }

    public string GetAuthEmail() {
        return _realmUser != null ? _realmUser.Profile.Email : "";
    }

    public PlayerModel CreateCurrentPlayer(string id, string name, string email) {
        PlayerModel player = _realm.Find<PlayerModel>(id);
        if(player == null) {
            _realm.Write(() => {
                player = _realm.Add(new PlayerModel(id, name, email));
            });
        }
        return player;
    }

    public PlayerModel GetCurrentPlayer() {
        PlayerModel player = _realm.Find<PlayerModel>(_realmUser.Id);
        if(player == null) {
            _realm.Write(() => {
                player = _realm.Add(new PlayerModel(_realmUser.Id, _realmUser.Profile.Email));
            });
        }
        return player;
    }

    public void IncreaseChangeStreamsScore(int currentScore) {
        PlayerModel player = GetCurrentPlayer();
        if(currentScore > player.Games.ChangeStreams.HighScore) {
            _realm.Write(() => {
                player.Games.ChangeStreams.HighScore = currentScore;
            });
        }
    }

    public void IncreaseChangeStreamsPlayCount() {
        PlayerModel player = GetCurrentPlayer();
        _realm.Write(() => {
            player.Games.ChangeStreams.TotalPlays++;
        });
    }

    public void SetTotalScore(int score) {
        PlayerModel player = GetCurrentPlayer();
        _realm.Write(() => {
            player.TotalScore = score;
        });
    }

    public void IncreaseFishingPlayCount() {
        PlayerModel player = GetCurrentPlayer();
        _realm.Write(() => {
            player.Games.Fishing.TotalPlays++;
        });
    }

    public void IncreaseFishingScore(int currentScore) {
        PlayerModel player = GetCurrentPlayer();
        if(currentScore > player.Games.Fishing.HighScore) {
            _realm.Write(() => {
                player.Games.Fishing.HighScore = currentScore;
            });
        }
    }

    public void IncreaseForestScrollerPlayCount() {
        PlayerModel player = GetCurrentPlayer();
        _realm.Write(() => {
            player.Games.ForestScroller.TotalPlays++;
        });
    }

    public void IncreaseForestScrollerScore(int currentScore) {
        PlayerModel player = GetCurrentPlayer();
        if(currentScore > player.Games.ForestScroller.HighScore) {
            _realm.Write(() => {
                player.Games.ForestScroller.HighScore = currentScore;
            });
        }
    }

    public void UpdatePositionInMidgard(float x, float y) {
        PlayerModel player = GetCurrentPlayer();
        _realm.Write(() => {
            player.X = (double) x;
            player.Y = (double) y;
        });
    }

}