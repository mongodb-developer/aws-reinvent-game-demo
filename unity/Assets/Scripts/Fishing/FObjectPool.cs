using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FObjectPool : MonoBehaviour {

    public static FObjectPool SharedInstance;

    private List<GameObject> _pooledFish;

    public GameObject fishToPool;
    public int fishPoolSize;

    void Awake() {
        SharedInstance = this;
        _pooledFish = new List<GameObject>();
        for(int i = 0; i < fishPoolSize; i++) {
            GameObject tmpObj = Instantiate(fishToPool);
            tmpObj.SetActive(false);
            _pooledFish.Add(tmpObj);
        }
    }

    void Start() {
        
    }

    public GameObject GetPooledFish() {
        for(int i = 0; i < fishPoolSize; i++) {
            if(_pooledFish[i].activeInHierarchy == false) {
                return _pooledFish[i];
            }
        }
        return null;
    }

}
