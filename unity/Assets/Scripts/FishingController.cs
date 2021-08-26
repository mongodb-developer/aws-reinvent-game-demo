using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour
{

    void Start() {
        for(int i = 0; i < 5; i++) {
            GameObject fish = FObjectPool.SharedInstance.GetPooledFish();
            if(fish != null) {
                fish.SetActive(true);
            }
        }
    }

    void Update() {

    }

}