using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_movement : MonoBehaviour {

    Rigidbody2D myRB;
    Animator myAnim;

    private float playerPositionX;
    bool facingLeft = true;
    public float speed = -5;
    bool drive = false;
    Vector3 playerPosition;
    //camera things
    //ta bort ifall vi inte behöver förstöra object


    // Use this for initialization
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        playerPositionX = GameObject.FindWithTag("Player").transform.position.x;

    }
     

    // Update is called once per frame
    void Update()
    {

        if (drive)
            myRB.velocity = new Vector2(speed, myRB.velocity.y);
        else
            playerPosition = GameObject.FindWithTag("Player").transform.position;


        if (Vector3.Distance(playerPosition, myRB.transform.position) < 20)
        {
            drive = true;
        }


    }


    void OnCollisionEnter2D(Collision2D s)
    {
        if (s.gameObject.tag == "Player")
        {
            GameObject playa = GameObject.FindWithTag("Player");
            if (playa.GetComponent<player_movement>().alive)
                playa.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
            playa.GetComponent<player_movement>().alive = false;
        }

    }
}


