using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySightHandler : MonoBehaviour {

	public float		fovAngle = 120f;
	public Transform	tracker;
	
	public List <bool>			characterInSight;
	public List <float>			angleInternal;
	public List <float>			distanceToTarget;
	public List <GameObject>	characterObject;
	public List <Vector3>		characterInternalDirection;
	public List <Vector3>		lastKnownSighting;

	private CharacterSightingHandler	characterSightingHandler;
	private NavMeshAgent				nav;
	private SphereCollider				col;
	
	void Awake () {
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		characterSightingHandler = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<CharacterSightingHandler> ();

		for (int i = 0; i < characterSightingHandler.characterGameObjects.Count; i++) {
			characterInSight.Add (false);
			lastKnownSighting.Add (new Vector3(0f, 10000f, 0f));
			angleInternal.Add (0.0f);
			characterInternalDirection.Add (new Vector3(0f, 0f, 0f));
			distanceToTarget.Add (10000f);
			characterObject.Add (characterSightingHandler.characterGameObjects[i]);
		}
	}

	float DistanceToTarget (Vector3 targetPosition)
	{
		NavMeshPath path = new NavMeshPath();
		if (nav.enabled) {
			nav.CalculatePath (targetPosition, path);
		}
		
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		allWayPoints[0] = transform.position;
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		for(int i = 0; i < path.corners.Length; i++){
			allWayPoints[i + 1] = path.corners[i];
		}
		
		float pathLength = 0;
		
		for(int i = 0; i < allWayPoints.Length - 1; i++){
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}
	
	void OnTriggerStay(Collider other) {
		if (characterSightingHandler.characterGameObjects.Contains (other.gameObject)) {
			int characterEntryIndex = characterSightingHandler.characterGameObjects.IndexOf (other.gameObject);
			characterInSight [characterEntryIndex] = false;

			characterInternalDirection [characterEntryIndex] = characterSightingHandler.characterGameObjects [characterEntryIndex].transform.position - tracker.position;
			angleInternal [characterEntryIndex] = Vector3.Angle (characterInternalDirection [characterEntryIndex], transform.forward);

			RaycastHit hit;

			if (angleInternal [characterEntryIndex] < fovAngle * 0.5f) {

				if (Physics.Raycast (tracker.position, characterInternalDirection [characterEntryIndex].normalized, out hit, col.radius)) {
					if(hit.collider.gameObject.name == characterSightingHandler.characterGameObjects[characterEntryIndex].name){
						Debug.DrawRay(tracker.position, characterInternalDirection[characterEntryIndex], Color.green);

						characterInSight[characterEntryIndex] = true;
						lastKnownSighting[characterEntryIndex] = characterInternalDirection[characterEntryIndex];
					}
				}
			}
		}

		gameObject.GetComponent<AIMovementHandler> ().target = characterObject [1].transform;

	}
}
