using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {
	public class PuzzleModel : ModelCore {

		private static Texture LoadMain() {
			return Resources.Load(C.TEXTURE + "001") as Texture;
		}

		private static Texture LoadAlpha() {
			return Resources.Load(C.TEXTURE + "001") as Texture;
		}

		public void GetAlpha() {
			db.Select();
			db.From("alpha");
			db.All();

			//db.get();
		}
	}
}