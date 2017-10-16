using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_script : MonoBehaviour {
	private bool drive = false;
	private Vector3 playerPosition;
	public Rigidbody2D myRB;
	private float speed = 4.0f;
	// Use this for initialization
	void Start () {
		playerPosition = GameObject.FindWithTag("Player").transform.position;
	}

	// Update is called once per frame
	void Update () {
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
