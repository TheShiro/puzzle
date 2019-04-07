using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules {

	public class PuzzleObject : ObjectCore {

		//constructor
		public PuzzleObject(string nameObject, Vector3 position) : base(nameObject) {
			SetTransform(position);
		}

		public void GetObject() {
			print(_object);
		}
	}

}