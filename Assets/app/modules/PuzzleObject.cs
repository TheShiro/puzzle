using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using Config;

namespace Modules {

	public class PuzzleObject : ObjectCore {

		private int id;

		/*
		* 0 - none; 1 - giver; 2 - receiver
		*/
		private int topSide;
		private int bottomSide;
		private int leftSide;
		private int rightSide;

		//constructor for create
		public PuzzleObject(string nameObject, Vector3 position, int id) {
			ObjectInit(C.PREFAB + nameObject);
			SetTransform(position);
			SetID(id);
			SetName("puzzle" + id);
		}
		//constructor for find
		public PuzzleObject(int id) {
			_object = GameObject.Find("puzzle" + id);
			_component = _object.GetComponent<PuzzleComponent>();
		}

		public void GetObject() {
			//print(_object);
		}

		private void SetID(int id) {
			this.id = id;
			_component = _object.GetComponent<PuzzleComponent>();
			//Debug.Log(_component);
		}

		public int GetID() {
			return this.id;
		}

		public void SetMaterial(string mainTex, string alpha) {
			PuzzleModel model = new PuzzleModel();

			//Debug.Log(alpha);

			this.SetMaterial("_MainTex", model.GetMain(mainTex));
			this.SetMaterial("_Alpha", model.GetAlpha(ref alpha));

			//Debug.Log((int)Char.GetNumericValue(alpha[0]));

			this.SetTopSide((int)Char.GetNumericValue(alpha[0]));
			this.SetBottomSide((int)Char.GetNumericValue(alpha[1]));
			this.SetLeftSide((int)Char.GetNumericValue(alpha[2]));
			this.SetRightSide((int)Char.GetNumericValue(alpha[3]));

			//this.GetTypeSide("top");
		}

		public void SetMaterialOffset(Vector2 offset) {
			this.SetMaterialOffset("_MainTex", offset);
		}

		public void SetMaterialScale(Vector2 scale) {
			this.SetMaterialScale("_MainTex", scale);
		}

		/* start block set:get for type of sides*/

		public bool SetTypeSides(string[] side, int[] type) {
			if(side.Length != type.Length) return false;

			int i = 1;
			foreach(string s in side) {
				switch(s) {
					case "top" : this.SetTopSide(type[i]); break;
					case "bottom" : this.SetBottomSide(type[i]); break;
					case "left" : this.SetLeftSide(type[i]); break;
					case "right" : this.SetRightSide(type[i]); break;
				}

				i++;
			}

			return true;
		}

		private void SetTopSide(int t) {
			_component.topSide = t;
		}

		private void SetBottomSide(int t) {
			_component.bottomSide = t;
		}

		private void SetLeftSide(int t) {
			_component.leftSide = t;
		}

		private void SetRightSide(int t) {
			_component.rightSide = t;
		}

		public string GetTypeSide() {
			//Debug.Log(_component);
			return _component.topSide + "" + _component.bottomSide + "" + _component.leftSide + "" + _component.rightSide;
		}

		public int GetTypeSide(string side) {
			//Debug.Log(_component.topSide + "" + _component.bottomSide + "" + _component.leftSide + "" + _component.rightSide);
			switch(side) {
				case "top": return _component.topSide;
				case "bottom": return _component.bottomSide;
				case "left": return _component.leftSide;
				case "right": return _component.rightSide;
			}

			return 0;
		}

		/* end block get:set for type of sides*/
	}

}