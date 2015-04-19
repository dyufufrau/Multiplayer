using UnityEngine;
using System.Collections;

public class GravityHandler : MonoBehaviour {

	private CharacterController characterController;
	private Vector3 velocity;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
	
		characterController = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (characterController.isGrounded) {
			velocity = new Vector3 (characterController.velocity.x, 0, characterController.velocity.z);
		} else {
			velocity.y += Physics.gravity.y * Time.deltaTime;
		}

		movement += Physics.gravity;
		characterController.Move (movement);
	}
}
