using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using DB;

namespace Models {
	public class PuzzleModel : ModelCore {

		private static Texture LoadMain() {
			return Resources.Load("Textures/001") as Texture;
		}

		private static Texture LoadAlpha() {
			return Resources.Load("Textures/001") as Texture;
		}

		public static void GetAlpha() {
			/*DB.DB.Query("select * from alpha");
			DB.DB.All();

			DB.DB.Query("SELECT * FROM setting");
			DB.DB.All();*/

			//DBCore.Query();
			DBCore.Select();
			DBCore.From("alpha");
			DBCore.All();
		}
	}
}