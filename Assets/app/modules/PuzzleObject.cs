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

		//constructor
		public PuzzleObject(string nameObject, Vector3 position, int id) {
			ObjectInit(C.PREFAB + nameObject);
			SetTransform(position);
			SetID(id);
			SetName("puzzle" + id);
		}

		public PuzzleObject(int id) {

		}

		public void GetObject() {
			//print(_object);
		}

		private void SetID(int id) {
			this.id = id;
		}

		public int GetID() {
			return this.id;
		}

		public void SetMaterial(string mainTex, string alpha) {
			PuzzleModel model = new PuzzleModel();

			Debug.Log(alpha);
			this.SetTopSide((int)alpha[0]);
			this.SetBottomSide((int)alpha[1]);
			this.SetLeftSide((int)alpha[2]);
			this.SetRightSide((int)alpha[3]);

			this.SetMaterial("_MainTex", model.GetMain(mainTex));
			this.SetMaterial("_Alpha", model.GetAlpha(alpha));
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
			this.topSide = t;
		}

		private void SetBottomSide(int t) {
			this.bottomSide = t;
		}

		private void SetLeftSide(int t) {
			this.leftSide = t;
		}

		private void SetRightSide(int t) {
			this.rightSide = t;
		}

		public int GetTypeSide(out int top, out int bottom, out int left, out int right) {
			top = this.topSide;
			bottom = this.bottomSide;
			left = this.leftSide;
			right = this.rightSide;

			return 0;
		}

		public int GetTypeSide(string side) {
			switch(side) {
				case "top": return this.topSide;
				case "bottom": return this.bottomSide;
				case "left": return this.leftSide;
				case "right": return this.rightSide;
			}

			return 0;
		}

		/* end block get:set for type of sides*/
	}

}