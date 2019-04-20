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

		public Texture GetAlpha(string all_side) {
			int count = 0;

			if(all_side[0] != 3) count++;
			if(all_side[1] != 3) count++;
			if(all_side[2] != 3) count++;
			if(all_side[3] != 3) count++;

			Debug.Log("count = " + count);

			db.Select(new string[] {"mask_name"});
			db.From("alpha");
			db.Where(new string[,] { {"top", "0", ""}, {"left", "0", ""} });
			db.Order("random", "()");
			db.One();

			string[,] res = db.GetResult();

			return this.LoadAlpha(res[0,0]);
		}


		//example method with out parameter
		private int test(out int x) {
			x = 10;
			//code
			return 1;
		}
	}
}