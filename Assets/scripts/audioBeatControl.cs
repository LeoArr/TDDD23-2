using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioBeatControl : MonoBehaviour {
	float bpm;
	AudioSource audioSource;
	public AudioClip tune2;
	//SecondsPerBeat
	private float spb;
	//the current % beat accuracy
	public float beatValue;
	public float percentDone;
	public float percentLimit = 0.95f;
	//----------for visualization
	float scaleF = 0.2f;
	private GameObject circle;

	void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		bpm = 120.0f;
		spb = 60.0f/bpm;
		audioSource = GetComponent<AudioSource>();
		//----------for visualization
		circle = GameObject.Find("circle");
		//audioSource.clip = tune2;
		//audioSource.Play();
	}

	// Update is called once per frame
	void Update () {
		//float elapsed = ((float)audioSource.timeSamples)/((float)audioSource.clip.samples);
		//beatValue = Mathf.Abs(Mathf.Sin(Mathf.PI*((elapsed%0.25f)/0.25f - 0.5f)));
		float time = audioSource.time;
		percentDone = time/audioSource.clip.length;
		float temp = Mathf.Abs(Mathf.Sin(Mathf.PI*((time%spb)/spb - 0.5f)));
		beatValue = temp;


		//----------for visualization
		float s = Mathf.Max(0.12f,scaleF * (beatValue));
		circle.transform.localScale = new Vector3(s, s, s);
	}

	//	abs(time%SecondsPerBeat)/SecondsPerBeat - 0.5) är en nice
	//	ekvation för att få saker att röra sig i takt.
	//abs(cos( (x%spb) * pi ))

	//perfekta ekvationen för att räkna ut när man är på beat
	//abs ( sin (((x%spb)/spb - 0.5) * pi) ) - snäll
	//abs ( tan ( (x%0.5)/0.5 - 0.5 ) * pi) - elak

}
