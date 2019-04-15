using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Models;

namespace Services {

	public static class GameService {

		public static string[] GetSettings() {
			GamesModel gm = new GamesModel();

			string[] settings = gm.GetSettingScenePuzzle();
			//gm.GetSettingScenePuzzle();

			return settings;
		}
	}

}