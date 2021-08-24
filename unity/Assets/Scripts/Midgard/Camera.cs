using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform player;

    void Update() {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

}