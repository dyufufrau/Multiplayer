using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DoubleClickHandler : MonoBehaviour, IPointerClickHandler {

	public GameObject iconAssociation;
	public GameObject startMenuAssociation;

	int clickCounter;

	public void OnPointerClick(PointerEventData eventData) {

		clickCounter = eventData.clickCount;
		
		if (clickCounter == 2) {

			if (iconAssociation.activeSelf == false) {
				iconAssociation.SetActive(true);
				startMenuAssociation.SetActive(true);
				iconAssociation.transform.SetAsLastSibling ();
				startMenuAssociation.transform.SetAsLastSibling ();
			}

		}
		
	}

}
