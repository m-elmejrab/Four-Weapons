using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Player Died");
        if (coll.gameObject.tag == "Player")
        {
            GameManager.instance.GameOver();
        }
    }
}
