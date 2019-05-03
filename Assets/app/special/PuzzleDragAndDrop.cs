using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Modules;
using Services;
//using System.Runtime.InteropServices;

public class PuzzleDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	//[DllImport("user32.dll")]
	//static extern bool SetCursorPos(int X, int Y);

	public static GameObject dragged;
	private static float offset;
	private static PuzzleObject obj;
	private static RectTransform rt;
	public bool enabled = true;

	public void OnBeginDrag (PointerEventData eventData) {
		if(enabled) {
			dragged = gameObject;
			rt = dragged.GetComponent<RectTransform>();
			offset = rt.sizeDelta.x / 2;

			obj = new PuzzleObject(dragged.GetComponent<PuzzleComponent>().id);

			int pid = obj.GetParent();

			if(pid > 0) {
				obj = new PuzzleObject(pid);
				rt = obj._component.rt;
			}

			enabled = GameObject.Find("puzzle" + obj.GetID()).GetComponent<PuzzleDragAndDrop>().enabled;
		}
	}

	public void OnDrag (PointerEventData eventData) {
		if(enabled) {
			rt.anchoredPosition3D = Input.mousePosition - new Vector3(offset, offset, 0);
		}
	}

	public void OnEndDrag(PointerEventData eventData) {
		if(enabled) {
			//obj.CheckScene();
			//PuzzlesService.CheckAllPiece();

			if(PuzzlesService.CheckBackPlace(obj.GetID())) {
				enabled = !enabled;
			} else {
				obj.CheckScene();
				PuzzlesService.CheckAllPiece();
			}

			obj = null;
			rt = null;
			dragged = null;
		}
	}
}
