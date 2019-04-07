using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules {

	public class PuzzleObject : ObjectCore {

		private int id;

		//constructor
		public PuzzleObject(string nameObject, Vector3 position, int id) {
			ObjectInit(nameObject);
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

		public PuzzleObject SetMaterial() {
			
		}
	}

}