using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score_result_script : MonoBehaviour {
	private float time;
	private float endTime = 75.0f;
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		time = 0;
		spriteRenderer = GetComponent<SpriteRenderer>();
		transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x,
																		 transform.position.y + 0.02f,
																		 transform.position.z);
		time++;
		float factor = time/endTime;
		transform.localScale = new Vector3(factor + 0.1f, factor + 0.1f, factor + 0.1f);
		Color tmp = spriteRenderer.color;
		tmp.a = 1.0f-factor*factor;
		spriteRenderer.color = tmp;
		if (time >= endTime) {
			Destroy(gameObject);
		}
	}

	
}
