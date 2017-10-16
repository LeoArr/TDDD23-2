using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cop_movement : MonoBehaviour {

    Rigidbody2D myRB;
    Animator myAnim;

    private float move =-1;
    private float playerPositionX;
    bool facingLeft = true;
    bool alive = true;
    public float hitCheckRadius = 0.005f;

    //transforms, kroppsdelar etc
    public Transform head;
    public Transform ledgeCheck;
    public Transform body;

    //föra att jämföra mot spealre och terräng
    public LayerMask player;
    public LayerMask ground;

    //camera things
    private Camera cam;
    private Plane[] planes;
    public Collider2D col;
    public AudioSource audioSource;
    public AudioClip scream;
    public AudioClip fall;

    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        playerPositionX = GameObject.FindWithTag("Player").transform.position.x;

        //init camera stuff
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        col = GetComponent<Collider2D>();

    }

    void engage(float playerPositionX) {
    if (playerPositionX > this.transform.position.x && facingLeft)
    {
        flipX();
        move = -1;
        facingLeft = false;
    }
    else if (playerPositionX > this.transform.position.x && !facingLeft)
    {
        move = 1;
    }
    else if (playerPositionX < this.transform.position.x && !facingLeft)
    {
        flipX();
        move = 1;
        facingLeft = true;
    }
    else if (playerPositionX < this.transform.position.x && facingLeft)
    {
        move = -1;
        facingLeft = true;
    }

    if (Physics2D.OverlapCircle(head.position, hitCheckRadius, player) && alive) {
        alive = false;
        print("ENEMY KILLED");
        audioSource.clip = scream;
        audioSource.Play();
        audioBeatControl beatController = GameObject.FindWithTag("BeatController").GetComponent<audioBeatControl>();
        addPoints((int)(beatController.beatValue * 100.0f));
        myRB.constraints = RigidbodyConstraints2D.None;
            foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
            body.GetComponent<Collider2D>().enabled = false;

        }
    myRB.velocity = new Vector2(move, myRB.velocity.y);
  }

  void addPoints(int points) {
		scorepoints_script sps = GameObject.FindWithTag("ScorePoints").GetComponent<scorepoints_script>();
		sps.scorepoints += points;
	}

	// Update is called once per frame
	void Update () {
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;

        if (!GeometryUtility.TestPlanesAABB(planes, col.bounds) && !alive) {
            //Destroy(this.gameObject);
            //tar bort object när dom faller utanför skärmen
            //Kanske använda pooling istälelt?
        }

        if (Vector3.Distance(playerPosition, myRB.transform.position) < 4.0f && alive) {
          engage(playerPosition.x);
          myAnim.speed = 3.0f;

        } else {
            saunter();
            myAnim.speed = 1.0f;
        }
    }

    void saunter() {
      if (!Physics2D.OverlapCircle(ledgeCheck.position, hitCheckRadius, ground)) {
        facingLeft = !facingLeft;
        flipX();
      }
      if (facingLeft) {
        myRB.velocity = new Vector2(-1, myRB.velocity.y);
      } else {
        myRB.velocity = new Vector2(1, myRB.velocity.y);
      }
    }

    void flipX()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    void OnCollisionEnter2D(Collision2D s)
    {
        if (s.gameObject.tag == "Player")
        {
            GameObject playa = GameObject.FindWithTag("Player");
            if (playa.GetComponent<player_movement>().alive)
                playa.GetComponent<Rigidbody2D>().AddForce(transform.up * 300);
            playa.GetComponent<player_movement>().alive = false;
            audioSource.clip = fall;
  					audioSource.Play();
        }
        else if (s.gameObject.tag == "Car") {
            myRB.AddForce(transform.up * 300);
        }

    }
}
