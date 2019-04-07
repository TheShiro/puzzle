using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;

namespace Services {

	public class PuzzlesService : MonoBehaviour {

		public static List<PuzzleObject> puzzleList = new List<PuzzleObject>();

		public static void GeneratorPuzzles(int x, int y) {
			//horizontal
			for(int i = 0; i < x; i++) {
				//vertical
				for(int j = 0; j < y; j++) {
					//formula getting id of puzzle
					int id = j + x * i + 1;
					PuzzleObject puzzle = new PuzzleObject("puzzle", new Vector3(i,j,0), id);
					//print("puzzle " + puzzle.GetID());
					puzzleList.Add(puzzle);

					puzzle.SetMaterial();
				}
			}
		}
	}

}