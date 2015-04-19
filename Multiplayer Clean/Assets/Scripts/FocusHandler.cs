using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FocusHandler : MonoBehaviour, IPointerDownHandler {

	public void OnPointerDown (PointerEventData data) {
		transform.SetAsLastSibling ();
	}
}
