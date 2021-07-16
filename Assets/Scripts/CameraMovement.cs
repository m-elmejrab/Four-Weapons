using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform player;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update () {
        transform.position =  player.transform.position + new Vector3(0, 0, -2);
        //transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0,0,-2), 10f * Time.deltaTime);
        transform.LookAt(player);
    }
}
