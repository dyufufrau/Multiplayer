using UnityEngine;
using System.Collections;
using InControl;

public class PlayerHandler : MonoBehaviour {

	public GameObject[] players;

	private InputDevice controller1;
	private Camera cameraStatus;

	private int currentObject = 0;

	// Use this for initialization
	void Start () {
		controller1 = InputManager.ActiveDevice;
		cameraStatus = Camera.main;
//		players[0].GetComponent<MovementHandler>().isControllable = true;
		cameraStatus.GetComponent<ThirdPersonCamera>().followMe = players[0].transform.FindChild ("Tracker");
	}
}
