using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models {

	public class AudioModel : ModelCore {

		public string[] GetMusic(int ind) {
			db.Select(new string[] {"name"});
			db.From("audio");
			db.Where(new string[,] { {"sector", ind + "", "text"} });

			string[,] res = db.GetResult();

			return null;
		}

		public AudioClip[] GetList(int ind) {

			return null;
		}
	}

}