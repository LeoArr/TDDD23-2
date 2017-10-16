using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour {

    Rigidbody2D myRB;
    public string nextScene = "";

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D s)
    {
        if (s.gameObject.tag == "Player")
        {
            GameObject playa = GameObject.FindWithTag("Player");
            if (playa.GetComponent<player_movement>().alive)
                playa.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
            SceneManager.LoadScene(nextScene);
        }

    }
}


