using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Models;
using Main;

namespace Services {

	public static class SaveService {

		public static void GetSave(PuzzleObject obj) {
			SaveModel sm = new SaveModel();

			sm.id = obj._component.id;
			sm.Loading();

			Debug.Log(sm.obj_id);

			if(sm.obj_id > 0) {
				obj._component.rt.gameObject.GetComponent<PuzzleDragAndDrop>().enabled = sm.enabled == 1 ? true : false;
				if(sm.parent > 0) obj.SetParentLocal(new PuzzleObject(sm.parent));
				obj.SetTransform(new Vector3(float.Parse(sm.posx), float.Parse(sm.posy), 0.0f));
			}
		}

		public static void SetSave(PuzzleObject obj) {
			SaveModel sm = new SaveModel();

			sm.obj_id = obj._component.id;
			sm.enabled = obj._component.rt.gameObject.GetComponent<PuzzleDragAndDrop>().enabled == true ? 1 : 0;
			sm.posx = obj._component.rt.anchoredPosition3D.x + "";
			sm.posy = obj._component.rt.anchoredPosition3D.y + "";
			sm.parent = obj.GetParent();

			sm.Save();
		}

		public static void DeleteSave() {
			SaveModel sm = new SaveModel();
			sm.Clear();
		}
	}

}