using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusic_script : MonoBehaviour {
	public AudioSource audioSource;
	private bool activated = false;
	private bool waiting = false;
	private audioBeatControl abc;
	public AudioClip newTune;
	// Use this for initialization
	void Start () {
		abc = audioSource.GetComponent<audioBeatControl>();
	}

	// Update is called once per frame
	void Update () {
		if (waiting && abc.percentDone > abc.percentLimit) {
			audioSource.clip = newTune;
			audioSource.Play();
			waiting = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (activated) return;
		if(other.CompareTag("Player")) {
			waiting = true;
			GetComponent<BoxCollider2D>().enabled = false;
   	}
  }
}
