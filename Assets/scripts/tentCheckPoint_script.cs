using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentCheckPoint_script : MonoBehaviour {
	bool activated = false;
	public Sprite activesprite;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (activated) return;

   	if(other.CompareTag("Player")) {
			activated = true;
     	other.GetComponent<player_movement>().spawnPoint = transform.position;
			GetComponent<AudioSource>().Play();
			sr.sprite = activesprite;
   	}
  }
}
