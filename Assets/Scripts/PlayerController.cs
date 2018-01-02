using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xmin,xmax,zmin,zmax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform spawnPoint;

	public float fireRate = 0.5f;
	private float lastFire = 0.0f;

	AudioSource audio;
	Rigidbody rigidbody;

	void Start() {
		audio = GetComponent<AudioSource>();
		rigidbody = GetComponent<Rigidbody> ();
	}

	//Before every frame that requires physics changes
	void FixedUpdate() {
		//get input for movement
		float moveHoriz = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHoriz, 0.0f, moveVert);

		//set velocity based on that input
		rigidbody.velocity = movement * speed;

		//keep it in a certain area
		rigidbody.position = new Vector3 (
			Mathf.Clamp (rigidbody.position.x, boundary.xmin, boundary.xmax),
			0.0f,
			Mathf.Clamp (rigidbody.position.z, boundary.zmin, boundary.zmax)
		);

		//bank the ship when it moves L/R
		rigidbody.rotation = Quaternion.Euler(0.0f,0.0f, rigidbody.velocity.x * -tilt);
	}

	//Before every frame
	void Update() {
		//If fired and it has been long enough
		if (Input.GetButton (KeyCode.Space.ToString()) && Time.time > lastFire + fireRate) {
			lastFire = Time.time; //set last fire to this time
			Instantiate(shot,spawnPoint); //spawn a new bolt
			audio.Play ();
		}
	}
}
