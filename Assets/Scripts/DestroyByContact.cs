using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;

	public int score;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Boundary"))
			return;
		Instantiate (explosion, transform.position,transform.rotation);
		Destroy (other.gameObject);
		Destroy (gameObject);
		if (other.CompareTag("Player")) {
			Instantiate(playerExplosion,other.transform.position,other.transform.rotation);
			GameController.GameOver ();
		}
		else GameController.AddScore (score);

	}
}
