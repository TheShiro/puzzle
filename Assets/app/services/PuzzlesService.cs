using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Models;

namespace Services {

	public static class PuzzlesService {

		public static PuzzleObject[,] puzzleArray;

		public static void GeneratorPuzzles(int x, int y, string image) {
			puzzleArray = new PuzzleObject[y,x];
			//horizontal
			for(int i = 0; i < y; i++) {
				//vertical
				for(int j = 0; j < x; j++) {
					//formula getting id of puzzle
					int id = j + (x * i) + 1;
 
					PuzzleObject puzzle = new PuzzleObject("puzzle", new Vector3((j * 9),0,(-i * 9)), id);


					puzzle.SetMaterial(image); 
				}
			}
		}

		public static string[] GetSettings() {
			GamesModel gm = new GamesModel();

			string[] settings = gm.GetSettingScenePuzzle();
			//gm.GetSettingScenePuzzle();

			return settings;
		}

		private static int GetMatrixPosition() {
			//...
			return 1;
		}
	}

}