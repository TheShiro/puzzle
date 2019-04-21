using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject dragged;
	private static float offset;

	public void OnBeginDrag (PointerEventData eventData) {
		dragged = gameObject;
		//RectTransform rt = dragged.GetComponent<RectTransform>();
		//rt.sizeDelta = new Vector2(100, 100);
	}

	public void OnDrag (PointerEventData eventData) {
		transform.position = Input.mousePosition - new Vector3(100, 100, 0);
	}

	public void OnEndDrag(PointerEventData eventData) {
		dragged = null;
	}
}
