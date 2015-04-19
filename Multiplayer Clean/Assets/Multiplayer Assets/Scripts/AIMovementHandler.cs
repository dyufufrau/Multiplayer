using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIMovementHandler : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;

	public float playerRange = 5f;
	public float playerSpeed = 1f;

	private float internalMovementPotential;
	private float internalIssuedCommands;
	private bool movementEnded = false;
	private Vector3 velocity;
	private float velocityVector;
	private Vector3 previousPosition;
	
	private Image movementSlider;
	private Transform cameraTransform;
	private Transform cameraPivot;
	private Transform thisTransform;
	private CharacterController characterController;

	public enum Direction {
		Moving, Patrolling, Idle, RunningForCover, Cover, Dead, Alert
	};

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		previousPosition = new Vector3 (transform.position.x, 0, transform.position.z);
		internalMovementPotential = playerRange;
		thisTransform = GetComponent<Transform> ();
		cameraTransform = Camera.main.transform;
		movementSlider = GameObject.Find ("AP Bar").GetComponent<Image> ();
		characterController = GetComponent<CharacterController> ();

		if (target == null) {
			target = gameObject.transform;
		}
	}


	// Update is called once per frame
	void Update () {

		if (characterController.isGrounded) {
			agent.SetDestination (target.position);
			velocityVector = (new Vector3 (transform.position.x, 0, transform.position.z) - previousPosition).magnitude / Time.deltaTime;
			previousPosition = new Vector3 (transform.position.x, 0, transform.position.z);

			movementSlider.fillAmount = (internalMovementPotential -= ((Time.deltaTime * velocityVector)) * playerSpeed) / playerRange;

			if (internalMovementPotential < 0.01f) {
				agent.Stop ();
			}

			Debug.Log("grounded");
		}
	}
}
