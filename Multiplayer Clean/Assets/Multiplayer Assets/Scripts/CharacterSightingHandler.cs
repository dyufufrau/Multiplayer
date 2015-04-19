using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ActiveTurn {
	PlayerTurn,
	EnemyTurn
};

public class CharacterSightingHandler : MonoBehaviour {

	public List <GameObject> characterGameObjects;
	public Vector3 resetPosition = new Vector3 (0f, 10000f, 0f);
	public ActiveTurn currentTurn = ActiveTurn.PlayerTurn;

	void Awake() {
		characterGameObjects = GameObject.FindGameObjectsWithTag (Tags.player).ToList();
	}
}

