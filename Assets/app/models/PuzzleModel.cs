using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {
	public class PuzzleModel : ModelCore {

		private static Texture LoadMain() {
			return Resources.Load(C.TEXTURE + "001") as Texture;
		}

		private static Texture LoadAlpha() {
			return Resources.Load(C.TEXTURE + "001") as Texture;
		}

		public string GetAlpha() {
			db.Select();
			db.From("alpha");
			db.All();

			return "";
		}

		public string GetSettingScenePuzzle() {
			string query = "SELECT puzzle.id, puzzle.image, games.size_x, games.size_y "
				+ "FROM games "
				+ "JOIN puzzle ON puzzle.id = games.puzzle_id "
				+ "WHERE games.is_end = 0 "
				+ "ORDER BY games.id DESC";

			db.Select(query);
			db.One();

			return "";
		} 
	}
}