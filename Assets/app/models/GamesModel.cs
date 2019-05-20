using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {
	public class GamesModel : ModelCore {

		public string[] GetSettingScenePuzzle() {
			db.Select(new string[] {"games.id", "puzzle.id", "puzzle.image", "game_sizes.x", "game_sizes.y", "games.size_id", "games.is_break"});
			db.From("games");
			db.Join("puzzle ON puzzle.id = games.puzzle_id");
			db.Join("game_sizes ON game_sizes.id = games.size_id");
			db.Where(new string[,] { {"games.is_end", "0", ""} });
			db.Order("games.id", "desc");
			db.One();

			string[,] res = db.GetResult();

			return new string[]{res[0,0], res[0,1], res[0,2], res[0,3], res[0,4], res[0,5], res[0,6]};
		}

		public void SaveEndGame(int gid) {
			db.Update("games");
			db.Set(new string[,] { {"is_end", "1"} });
			db.Where(new string[,] { {"id", "" + gid, ""} });
			db.Go();
		}

		public void SaveBreakGame(int gid) {
			db.Update("games");
			db.Set(new string[,] { {"is_break", "1"} });
			db.Where(new string[,] { {"id", "" + gid, ""} });
			db.Go();
		}
	}
}