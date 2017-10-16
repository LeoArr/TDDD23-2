using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pill_script : MonoBehaviour {
	private float cycle_cur_time = 0.0f;
	private float cycle_max_time = 2.0f*Mathf.PI;
	private float orig_y;
	// Use this for initialization
	void Start () {
		orig_y = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		cycle_cur_time += Time.deltaTime;
		if (cycle_cur_time >= cycle_max_time)
			cycle_cur_time = 0.0f;

		float y_float = Mathf.Sin(cycle_cur_time);
		transform.position = new Vector3(transform.position.x, orig_y + y_float, 0);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			GetComponent<AudioSource>().Play();
			other.gameObject.GetComponent<player_movement>().tookPill();
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
   	}
  }
}
