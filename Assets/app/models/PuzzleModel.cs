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

		public Texture GetAlpha(ref string all_side) {
			string[,] param = this.ParseSides(all_side);
			

			db.Select(new string[] {"mask_name", "top", "bottom", "left", "right"});
			db.From("alpha");
			db.Where(param);
			db.Order("random", "()");
			db.One();

			string[,] res = db.GetResult();

			all_side = res[0,1] + res[0,2] + res[0,3] + res[0,4];

			return this.LoadAlpha(res[0,0]);
		}

		private int CountSide(string all_side) {
			int count = 0;

			if(all_side[0] != '3') count++;
			if(all_side[1] != '3') count++;
			if(all_side[2] != '3') count++;
			if(all_side[3] != '3') count++;

			return count;
		}

		private string[,] ParseSides(string all_side) {
			//int count = this.CountSide(all_side);

			string[,] param = new string[4,3];

			//int pos = 0;
			for(int i = 0; i < 4; i++) {
				if(all_side[i] != '3') {
					param[i,0] = this.IncToText(i);
					param[i,1] = "" + all_side[i];
					param[i,2] = "";

					//pos++;
				} else {
					param[i,0] = this.IncToText(i);
					param[i,1] = "0";
					param[i,2] = "not";

					//pos++;
				}
			}

			return param;
		}

		private string IncToText(int i) {
			switch(i) {
				case 0 : return "top";
				case 1 : return "bottom";
				case 2 : return "left";
				case 3 : return "right";
			}

			return "";
		}
	}
}