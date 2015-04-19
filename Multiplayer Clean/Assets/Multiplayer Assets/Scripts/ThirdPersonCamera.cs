using UnityEngine;
using System.Collections;
using InControl;
using System.Linq;

public class ThirdPersonCamera : MonoBehaviour {

	public Vector2 cameraSpeed = new Vector2 (2.5f, 1.2f);
	public Vector2 cameraYLimits = new Vector2 (-20f, 80f);
	public Vector2 minAndMaxDistance = new Vector2 (0.5f, 2.0f);
	public float distance = 2.0f;
	public float maxDistance = 2.5f;
	public Transform followMe;
	public bool resetCamera = true;
	public LayerMask layerMask;

	private float x = 0f;
	private float y = 0f;
	private float velocityVector;
	private Vector3 previousPosition;

	private InputDevice controller1;
	
	void Start () {
		controller1 = InputManager.ActiveDevice;
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
	}
	
	public float ClampAngle (float angle, float min, float max) {
		if (angle < -360f) {
			angle += 360f;
		} else if (angle > 360f) {
			angle -= 360f;
		}
		
		return Mathf.Clamp (angle, min, max);
	}

	public void TerrainDetection () {
		Ray myRay = new Ray (followMe.transform.position, -transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (myRay, out hit, minAndMaxDistance.y, layerMask)) {
			distance = hit.distance;
		} else {
			distance = minAndMaxDistance.y;
		}
	}

	void LateUpdate () {
		velocityVector = (new Vector3 (followMe.transform.position.x, 0, followMe.transform.position.z) - previousPosition).magnitude / Time.deltaTime;
		previousPosition = new Vector3 (followMe.transform.position.x, 0, followMe.transform.position.z);

		Quaternion rotation = Quaternion.Euler (y, x, 0f);
		Vector3 position = rotation * new Vector3 (0.0f, 0.0f, -distance) + followMe.position;

		x += controller1.RightStick.X * cameraSpeed.x;
		y -= controller1.RightStick.Y * cameraSpeed.y;
		
		y = ClampAngle(y, cameraYLimits.x, cameraYLimits.y);

		if (resetCamera == true) {
			x = followMe.transform.eulerAngles.y;
			y = 0;
			
			resetCamera = false;
		}

		TerrainDetection ();

		transform.rotation = rotation;
		transform.position = position;
	}
}