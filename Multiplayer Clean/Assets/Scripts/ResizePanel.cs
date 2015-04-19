using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler {

	public Vector2 minSize;
	public Vector2 maxSize;

	public RectTransform dragArea;

	private RectTransform rectTransform;
	private RectTransform rectTransformSelf;
	private Vector2 currentPointerPosition;
	private Vector2 previousPointerPosition;

	void Awake () {
		rectTransform = transform.parent.GetComponent<RectTransform> ();
		rectTransformSelf = transform.GetComponent<RectTransform> ();

		if (maxSize.x == 0 || maxSize.x > dragArea.sizeDelta.x) {
			maxSize.x = dragArea.sizeDelta.x;
		}

		if (maxSize.y == 0 || maxSize.y > dragArea.sizeDelta.y) {
			maxSize.y = dragArea.sizeDelta.y;
		}
	}

	public void OnPointerDown (PointerEventData data) {
		rectTransform.SetAsLastSibling ();
		RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, data.position, data.pressEventCamera, out previousPointerPosition);
	}

	public void OnDrag (PointerEventData data) {

		if (rectTransform == null) {
			return;
		}

		Vector2 sizeDelta = rectTransform.sizeDelta;

		RectTransformUtility.ScreenPointToLocalPointInRectangle (rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
		Vector2 resizeValue = currentPointerPosition - previousPointerPosition;
		
		sizeDelta += new Vector2 (resizeValue.x, -resizeValue.y);
		sizeDelta = new Vector2 (Mathf.Clamp (sizeDelta.x, minSize.x, maxSize.x),
		                         Mathf.Clamp (sizeDelta.y, minSize.y, maxSize.y));

		rectTransform.sizeDelta = sizeDelta;
		previousPointerPosition = currentPointerPosition;

	}

}
