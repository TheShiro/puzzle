using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Modules;

public class PuzzleDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject dragged;
	private static float offset;
	private static PuzzleObject obj;
	private static RectTransform rt;

	public void OnBeginDrag (PointerEventData eventData) {
		dragged = gameObject;
		rt = dragged.GetComponent<RectTransform>();
		offset = rt.sizeDelta.x / 2;

		obj = new PuzzleObject(dragged.GetComponent<PuzzleComponent>().id);

		int pid = obj.GetParent();
		//Debug.Log(pid);
		//Debug.Log(rt.anchoredPosition3D);
		if(pid > 0) {
			obj = new PuzzleObject(pid);
			rt = obj._component.rt;
		}
		Debug.Log(obj._component.id);
	}

	public void OnDrag (PointerEventData eventData) {
		rt.anchoredPosition3D = Input.mousePosition - new Vector3(offset, offset, 0);
	}

	public void OnEndDrag(PointerEventData eventData) {
		obj.CheckScene();

		obj = null;
		rt = null;
		dragged = null;
	}
}
