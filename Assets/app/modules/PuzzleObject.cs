﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Modules {

	public class PuzzleObject : ObjectCore {

		private int id;

		/*
		* 0 - none; 1 - giver; 2 - receiver
		*/
		public int topSide;
		public int bottomSide;
		public int leftSide;
		public int rightSide;

		//constructor
		public PuzzleObject(string nameObject, Vector3 position, int id) {
			ObjectInit("Prefabs/" + nameObject);
			SetTransform(position);
			SetID(id);
			SetName("puzzle" + id);
		}

		public PuzzleObject(int id) {

		}

		public void GetObject() {
			print(_object);
		}

		private void SetID(int id) {
			this.id = id;
		}

		public int GetID() {
			return this.id;
		}

		public void SetMaterial(Texture main, Texture alpha) {
			this.SetMaterial("_MainTex", main);
			this.SetMaterial("_Alpha", alpha);
		}
	}

}