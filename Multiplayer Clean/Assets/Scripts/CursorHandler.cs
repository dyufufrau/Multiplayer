using UnityEngine;
using System.Collections;

public class CursorHandler : MonoBehaviour {

	void Update () {
		Cursor.visible = false;
		transform.position = Input.mousePosition;
	}

}
