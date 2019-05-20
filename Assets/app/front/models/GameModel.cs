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
			db.Values(new string[] {"" + pid, "0", "0", "0", "0", "" + size});
			db.Go();
		}

		public bool CheckBreakGame() {
			db.Select(new string[] {"is_break", "is_end"});
			db.From("games");
			db.Order("games.id", "desc");
			db.One();
			string[,] res = db.GetResult();

			if(res[0,0] == "1" && res[0,1] == "0") {
				return true;
			}

			return false;
		}
	}

}