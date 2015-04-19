using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class MovementHandler : MonoBehaviour {
	
	public float playerRange = 5f;
	public float playerSpeed = 1f;
	public bool isControllable = false;

	private float internalMovementPotential;
	private float internalIssuedCommands;
	private bool movementEnded = false;
	private Vector3 velocity;
	private float velocityVector;
	private Vector3 previousPosition;

	private Image movementSlider;
	private InputDevice controller1;
	private Transform cameraTransform;
	private Transform cameraPivot;
	private CharacterController characterController;
	private Transform thisTransform;

	float movementInternal;
	
	// Use this for initialization
	void Start () {
		previousPosition = new Vector3 (transform.position.x, 0, transform.position.z);
		internalMovementPotential = playerRange;
		controller1 = InputManager.ActiveDevice;
		characterController = GetComponent<CharacterController> ();
		thisTransform = GetComponent<Transform> ();
		cameraTransform = Camera.main.transform;
		movementSlider = GameObject.Find ("AP Bar").GetComponent<Image> ();
	}

	void FaceRelativeToCamera() {

		Vector3 horizontalVelocity = new Vector3 (0, 0, 0);
		Vector3 movement = cameraTransform.TransformDirection (new Vector3 (controller1.LeftStick.X, 0, controller1.LeftStick.Y));
		float summedSticks;
		summedSticks = Mathf.Abs(controller1.LeftStick.X) + Mathf.Abs(controller1.LeftStick.Y);

		if (movementEnded == false) {
			movement.y = 0;
			horizontalVelocity = characterController.velocity;
			horizontalVelocity.y = 0;
		} else {

			if (summedSticks > 0.75) {
				movement.y = 0;
				thisTransform.forward = movement.normalized;
				horizontalVelocity.y = 0;
			}
		}
		
		if (horizontalVelocity.magnitude > 0.1f) {
			thisTransform.forward = movement.normalized;
		}
	}

	void Update () {
		if (isControllable == true) {
			velocityVector = (new Vector3 (transform.position.x, 0, transform.position.z) - previousPosition).magnitude / Time.deltaTime;
			previousPosition = new Vector3 (transform.position.x, 0, transform.position.z);

			Vector3 movement = cameraTransform.TransformDirection (new Vector3 (controller1.LeftStick.X, 0, controller1.LeftStick.Y));
			float controllerVector = Mathf.Clamp (Mathf.Abs (controller1.LeftStick.X) + Mathf.Abs (controller1.LeftStick.Y), 0f, 1f);

			movement.y = 0;
			movement.Normalize ();

			if (movementEnded == false && characterController.isGrounded) {

				if (internalMovementPotential < 0.001f) {
					movementEnded = true;
				}

				movement *= (playerSpeed * controllerVector) * Time.deltaTime;


			} else {
				movement = new Vector3 (0, movement.y, 0);
			}

			//Handles how full the slider UI element is and also only updates the character velocity while grounded
			if (characterController.isGrounded) {
				velocity = new Vector3 (characterController.velocity.x, 0, characterController.velocity.z);
				movementSlider.fillAmount = (internalMovementPotential -= ((Time.deltaTime * velocityVector)) * playerSpeed) / playerRange;
			}

			characterController.Move (movement);
			FaceRelativeToCamera ();


			//DEBUG CODE; REMOVE LATER
			if (controller1	.Action1.IsPressed) {
				internalMovementPotential = playerRange;
				movementEnded = false;
			}
		}
	}
}