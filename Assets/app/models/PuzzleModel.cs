using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;

namespace Models {
	public class PuzzleModel : ModelCore {

		private static Texture LoadMain() {
			return Resources.Load("Textures/001") as Texture;
		}

		private static Texture LoadAlpha() {
			return Resources.Load("Textures/001") as Texture;
		}
	}
}