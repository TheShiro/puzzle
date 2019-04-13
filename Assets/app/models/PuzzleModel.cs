using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {
	public class PuzzleModel : ModelCore {

		public Texture GetMain(string tex) {
			return Resources.Load(C.TEXTURE + tex) as Texture;
		}

		private Texture LoadAlpha(string mask) {
			return Resources.Load(C.ALPHA + mask) as Texture;
		}

		public Texture GetAlpha() {
			/*db.Select();
			db.From("alpha");
			db.All();*/

			return this.LoadAlpha("Edge");
		}


		//example method with out parameter
		private int test(out int x) {
			x = 10;
			//code
			return 1;
		}
	}
}