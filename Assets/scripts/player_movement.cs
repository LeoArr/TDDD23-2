using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour {
	//movement variables
	public float maxSpeed;
	private bool facingRight;

  //Status variables
  public bool alive = true;

	//Score results
	public GameObject good;
	public GameObject great;
	public GameObject perfect;

	//jumping variables
	private bool onGround = false;
	//to see if we should bounce down
	private bool hitHead = false;
	public float hitCheckRadius = 0.005f;
	public LayerMask groundLayer;
	//check feet & head
	public Transform groundCheck;
	public Transform headCheck;
	public float jumpHeight;
	//To control jump height
	private int jumpControlFrames;
	public int jumpControlFramesMax;
	private float jumpControlLimit = 0.1f;
	private float jumpOnBeatFactor;
	//jump cancel mechanic
	private bool hasDived = false;
	//Components
	Rigidbody2D myRB;
	Animator myAnim;

	//respawn
	public Vector3 spawnPoint;

	//beat controller
	audioBeatControl beatController;

	//score n shit
	scorepoints_script sps;
	public bool onThePill = false;
	private float timeOnThePill = 0.0f;
	// Use this for initialization
	void Start () {
			spawnPoint = transform.position;
			myRB = GetComponent<Rigidbody2D>();
			myAnim = GetComponent<Animator>();
			sps = GameObject.FindWithTag("ScorePoints").GetComponent<scorepoints_script>();
			beatController = GameObject.FindWithTag("BeatController").GetComponent<audioBeatControl>();
			facingRight = true;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//borde vänsta på att ljudklippet spelas klart sedan starta om!
		if (myRB.position.y < -20) {
			//if (beatController.percentDone > beatController.percentLimit) {
				//string scene = SceneManager.GetActiveScene().name;
				//SceneManager.LoadScene(scene);
				transform.position = spawnPoint;
				myRB.position = spawnPoint;
				myRB.velocity = new Vector2(0, 0);
				alive = true;
				foreach (Collider2D c in GetComponents<Collider2D>()) {
					c.enabled = true;
				}
		}
		//fall av ifall man är död
		if (!alive) {
			foreach (Collider2D c in GetComponents<Collider2D>()) {
				c.enabled = false;
			}
			return;
		}
		//horizontal movement
		float moveInput = Input.GetAxis("Horizontal");
		myRB.velocity = new Vector2(moveInput * maxSpeed, myRB.velocity.y);
		flipSprite(moveInput);

		//update if we are colliding either at feet or head
		if (myRB.velocity.y <= jumpControlLimit)
			onGround = Physics2D.OverlapCircle(groundCheck.position, hitCheckRadius, groundLayer);
		if (myRB.velocity.y <= jumpControlLimit)
			hitHead = Physics2D.OverlapCircle(headCheck.position, hitCheckRadius, groundLayer);
		//jump control
		jumpHeightControl();
		//Animation
		myAnim.SetFloat("speed", Mathf.Abs(myRB.velocity.x));
		myAnim.SetFloat("verticalSpeed", myRB.velocity.y);
		myAnim.SetBool("isGrounded", onGround);
  }

	void jumpHeightControl() {
		//if we jump up into something, cancel the jump
		if (hitHead && jumpControlFrames > 0) {
			jumpControlFrames = 0;
			hasDived = true;
			myRB.velocity = new Vector2(myRB.velocity.x, 0);
			return;
		}
		//holding down jump button will make jump higher
		//jumpControlFrames regulates how high
		if (!onGround && Input.GetAxis("Vertical") > 0 && jumpControlFrames > 0) {
			myRB.velocity = new Vector2(myRB.velocity.x, jumpHeight + jumpOnBeatFactor*3 + jumpControlFrames/8);
		} else if (!onGround && Input.GetAxis("Vertical") == 0 ) {
			jumpControlFrames = 0;
		}
		jumpControlFrames--;
		//'dive' mechanic
		if (!onGround && Input.GetAxis("Vertical") < 0 && !hasDived) {
			hasDived = true;
			myRB.velocity = new Vector2(myRB.velocity.x, Mathf.Min(-jumpHeight/2, myRB.velocity.y));
		}
		if (jumpControlFrames < 0 || onGround) {
			jumpControlFrames = 0;
		}
	}

	public void tookPill() {
		onThePill = true;
		timeOnThePill = 6.0f;
	}

	void updatePill() {
		if (onThePill) {
			timeOnThePill -= Time.deltaTime;
		}
		if (timeOnThePill <= 0.0f) {
			onThePill = false;
			timeOnThePill = 0.0f;
		} else {
			onThePill = true;
		}
	}

	void Update() {
		updatePill();
		if (!alive) return;
		if (onGround && Input.GetAxis("Vertical") > 0 && myRB.velocity.y <= jumpControlLimit) {
			onGround = false;
			hasDived = false;
			jumpControlFrames = jumpControlFramesMax;
			myRB.velocity = new Vector2(myRB.velocity.x, jumpHeight);
			if (beatController.beatValue > 0.8) {
				jumpOnBeatFactor = beatController.beatValue;
				GetComponent<AudioSource>().Play();
				Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
				GameObject sRes = perfect;
				if (beatController.beatValue < 0.95) {
					sRes = great;
					if (beatController.beatValue < 0.9)
						sRes = good;
				}
				Instantiate(sRes, pos, Quaternion.identity);
				addPoints((int)((beatController.beatValue + sps.getCombo()) * 100.0f));
				sps.increaseCombo(beatController.beatValue/5.0f);
			} else
				jumpOnBeatFactor = 0;
			}
	}

	void addPoints(int points) {
		scorepoints_script sps = GameObject.FindWithTag("ScorePoints").GetComponent<scorepoints_script>();
		sps.scorepoints += points;
	}

	void flipSprite(float moveInput) {
		if (facingRight && moveInput < 0) {
			flipX();
			facingRight = !facingRight;
		} else if (!facingRight && moveInput > 0) {
			flipX();
			facingRight = !facingRight;
		}
	}
	void flipX() {
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;
	}
}
