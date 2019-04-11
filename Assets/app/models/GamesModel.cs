using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {
	public class GamesModel : ModelCore {

		public string[] GetSettingScenePuzzle() {
			db.Select(new string[] {"games.id", "puzzle.id", "puzzle.image", "games.size_x", "games.size_y"});
			db.From("games");
			db.Join("puzzle ON puzzle.id = games.puzzle_id");
			db.Where(new string[,] { {"games.is_end", "0", ""} });
			db.Order("games.id", "desc");
			db.One();

			string[,] res = db.GetResult();
			
			Debug.Log(res[0,1]);

			//settings[0] = "123";
			return new string[]{"", ""};
		} 
	}
}