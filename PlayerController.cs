using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public Rigidbody2D myRigidbody;
	public AudioSource jumpSound;

	public bool grounded;
	public LayerMask whatIsGround;

	private Collider2D myCollider;

	private Animator myAnimator;

	private GameObject theCheckpoint;

	public static Vector3 respawnPosition;

	public DeathMenu theDeathScreen;

	public GameObject theIrishPanel;

	private GameManager theGameManager;

	private GlobalObjects theGlobalObjects;

	private Vector3 startPosition = new Vector3 (-2.3f, -0.78f, 0.0f);
	private Animator playerAnimator;

	// Use this for initialization
	void Start () 
	{
		theGlobalObjects = FindObjectOfType<GlobalObjects> ();

		myRigidbody = GetComponent<Rigidbody2D> ();
		RuntimeAnimatorController theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run5");
		if (theGlobalObjects.selectChar1) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run1");
		}
		else if (theGlobalObjects.selectChar2) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run2");
		}
		else if (theGlobalObjects.selectChar3) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run4");
		}
		else if (theGlobalObjects.selectChar4) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run3");
		}
		else if (theGlobalObjects.selectChar5) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run5");
		}
		else if (theGlobalObjects.selectChar6) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Run6");
		}

		playerAnimator = GetComponent<Animator>();
		
		playerAnimator.runtimeAnimatorController = theAnimController;

		gameObject.transform.position = startPosition;

		Vector3 position = gameObject.transform.position;
		Quaternion rotation = gameObject.transform.rotation;
		Vector3 velocity = myRigidbody.velocity;
		playerAnimator.runtimeAnimatorController = theAnimController;
		gameObject.transform.position = position;
		myRigidbody.velocity = velocity;
		transform.rotation = rotation;

		theCheckpoint = GameObject.FindGameObjectWithTag ("Checkpoint");
		respawnPosition = theCheckpoint.transform.position;

		theGameManager = FindObjectOfType<GameManager> ();

		myCollider = GetComponent<Collider2D> ();

		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

		myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);

	}

	public void MakePlayerJump(){
		grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);
		if (grounded) {
			jumpSound.Play ();
			myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpForce, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "KillPlane" || other.tag == "Spikes") 
		{
			gameObject.SetActive (false);

			theDeathScreen.gameObject.SetActive (true);
			theGameManager.deathScreenScoreText.text = theGameManager.scoreCount.ToString();
			theGameManager.SetHighScore ();
			theGameManager.GetHighScore ();
			theIrishPanel.gameObject.SetActive (false);
	
		}

		if (other.tag == "Checkpoint")
		{
			respawnPosition = other.transform.position;
		}
	}


}
