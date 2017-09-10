using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour {

	public GameObject reloadBar;

	public GameObject projectile;
	private GameObject activeProjectile;

	LineRenderer line;
	private Vector3 lineStartPos;
	private Vector3 lineEndPos;

	public float fireSpeed;
	public Vector3 firstPos;
	public Vector3 lastPos;

	Quaternion rotation;

	int count;
	int maxIterations;

	float timeLeft = 0.0f;
	bool needToReload;

	void Awake(){
		rotation = transform.rotation;
	}

	void Start(){

		needToReload = true;

		line = GetComponent<LineRenderer> ();
		line.startWidth = 0.1f;
		line.endWidth = 0.1f;
	}

	void Update(){


		if (needToReload == true)
		{	
			timeLeft -= Time.deltaTime;
		}

		if (timeLeft < 0.0f) {
			
			needToReload = false;
			reloadBar.GetComponent<Animator>().Play("New State");
			timeLeft = 2.0f;
		}

		line.SetPosition (0, lineStartPos);

		RaycastHit hit;
		if (Physics.Raycast (transform.position, firstPos - lastPos, out hit, 1 << LayerMask.NameToLayer ("Reflect"))) {
			
			lineEndPos = (firstPos - lastPos);

		} else {
			
			lineEndPos = (firstPos - lastPos);

		}
		line.SetPosition (1, lineEndPos);

	}

	void LateUpdate(){
		transform.rotation = rotation;
	}

	void OnMouseUp(){
		if (activeProjectile == null) {
			activeProjectile = Instantiate (projectile, transform.position, transform.rotation);
		} else if (activeProjectile != null) {
			Destroy (activeProjectile);
			activeProjectile = Instantiate (projectile, transform.position, transform.rotation);
		}

		line.enabled = false;
		lastPos = Input.mousePosition;

		if (needToReload == false) {
			activeProjectile.GetComponent<Rigidbody> ().velocity = (firstPos - lastPos).normalized * fireSpeed;
			needToReload = true;
			reloadBar.GetComponent<Animator> ().Play ("Reload");
		} else {
			GameObject.Find ("Tick").GetComponent<AudioSource>().Play();
		}
	}

	void OnMouseDrag()
	{
		lastPos = Input.mousePosition;
		line.enabled = true;
	}

	void OnMouseDown(){

			firstPos = Input.mousePosition;
			lineStartPos = new Vector3 (0.0f, 0.0f, 0.0f);
			line.SetPosition (0, lineStartPos);
	}
		
}
