using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 1, 0);
	}

	// Update is called once per frame
	void LateUpdate () {
        //gör så att kameran enbart rör sig ifall spelaren lever
    if (GameObject.FindWithTag("Player").GetComponent<player_movement>().alive) {
      transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10) + offset;
  	}
	}
}
