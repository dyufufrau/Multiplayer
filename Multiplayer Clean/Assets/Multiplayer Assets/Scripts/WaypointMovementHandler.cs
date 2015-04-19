using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointMovementHandler : MonoBehaviour {

	public GameObject actor;
	public GameObject masterWaypoint;
	private Transform actorTarget;
	public GameObject[] waypoints;
	private int currentWaypoint = 0;


	// Use this for initialization
	void Start () {
		actor.GetComponent<AIMovementHandler> ().target = actorTarget;
	}
	
	// Update is called once per frame
	void Update () {
		actor.GetComponent<AIMovementHandler> ().target = waypoints[currentWaypoint].transform;
		float distance = Vector3.Distance (new Vector3 (waypoints[currentWaypoint].transform.position.x , 0, waypoints[currentWaypoint].transform.position.z),
		                                   new Vector3 (actor.transform.position.x , 0, actor.transform.position.z));

		//actor.GetComponent<Transform> ().LookAt (waypoints [currentWaypoint].transform.position);

		if (distance < 0.01f) {
			currentWaypoint++;
		}

		if (currentWaypoint == waypoints.Length){
			currentWaypoint = 0;
		}
	}
}
