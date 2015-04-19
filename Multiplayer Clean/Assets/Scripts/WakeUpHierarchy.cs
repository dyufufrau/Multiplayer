using UnityEngine;
using System.Collections;

public class WakeUpHierarchy : MonoBehaviour {

	public GameObject associatedWindow;

	// Use this for initialization
	void Awake () {
		transform.SetAsLastSibling ();
	}
	
	// Update is called once per frame
	void Update () {

		if (associatedWindow.activeSelf == false) {
			gameObject.SetActive(false);
		}
	
	}
}
