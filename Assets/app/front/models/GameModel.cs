using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Front.Models {

	public class GameModel : ModelCore {

		public int id;
		public int puzzle_id;
		public int is_break;
		public int is_end;
		public string start_time;
		public string end_time;
		public int size_id;

		public void End() {

		}

		public void Break() {
			
		}

		public void Set(int pid, int size) {
			db.Insert("games");
			db.Set(new string[,] { {"puzzle_id", "" + pid}, {"size_id", "" + size} });
			db.Go();
		}
	}

}