using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {
	public class GamesModel : ModelCore {

		public string[] GetSettingScenePuzzle() {
			db.Select(new string[] {"games.id", "puzzle.id", "puzzle.image", "game_size.x", "game_size.y"});
			db.From("games");
			db.Join("puzzle ON puzzle.id = games.puzzle_id");
			db.Join("game_size ON game_size.id = games.size_id");
			db.Where(new string[,] { {"games.is_end", "0", ""} });
			db.Order("games.id", "desc");
			db.One();

			string[,] res = db.GetResult();

			return new string[]{res[0,0], res[0,1], res[0,2], res[0,3], res[0,4]};
		} 
	}
}