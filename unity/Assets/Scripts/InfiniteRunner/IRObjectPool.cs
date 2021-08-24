using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRObjectPool : MonoBehaviour {

    public static IRObjectPool SharedInstance;

    private List<GameObject> _pooledObstacles;

    public GameObject obstacleToPool;
    public int obstaclePoolSize;

    void Awake() {
        SharedInstance = this;
    }

    void Start() {
        _pooledObstacles = new List<GameObject>();
        for(int i = 0; i < obstaclePoolSize; i++) {
            GameObject tmpObj = Instantiate(obstacleToPool);
            tmpObj.SetActive(false);
            _pooledObstacles.Add(tmpObj);
        }
    }

    public GameObject GetPooledObstacle() {
        for(int i = 0; i < obstaclePoolSize; i++) {
            if(_pooledObstacles[i].activeInHierarchy == false) {
                return _pooledObstacles[i];
            }
        }
        return null;
    }

}
