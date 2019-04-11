﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Models;

namespace Services {

	public static class PuzzlesService {

		public static List<PuzzleObject> puzzleList = new List<PuzzleObject>();

		public static void GeneratorPuzzles(int x, int y, string image) {
			//horizontal
			for(int i = 0; i < x; i++) {
				//vertical
				for(int j = 0; j < y; j++) {
					//formula getting id of puzzle
					int id = j + x * i + 1;
					PuzzleObject puzzle = new PuzzleObject("puzzle", new Vector3(i,0,j), id);
					//print("puzzle " + puzzle.GetID());
					puzzleList.Add(puzzle);

					//puzzleTexture = LoadMain();
					//puzzleTexture = LoadMain();
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
	}

}