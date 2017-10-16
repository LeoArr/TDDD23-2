using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg1_movement : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag("Player").GetComponent<player_movement>().alive) {
			float m_y = (player.transform.position.y + 2.0f) * 0.9f;
			transform.position = new Vector3(player.transform.position.x, m_y, 0);
		}
	}
}
