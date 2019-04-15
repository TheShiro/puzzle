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

					Debug.Log("start id = " + id + " / x=" + x + " / y=" + y);
					GetMatrixPosition(id, x, y);

					puzzle.SetMaterial(image); 
				}
			}
		}

		private static int GetMatrixPosition(int id, int x, int y) {
			Debug.Log("top " + SearchTopSide(id, x));
			Debug.Log("bottom " + SearchBottomSide(id, x, y));
			Debug.Log("left " + SearchLeftSide(id, x));
			Debug.Log("right " + SearchRightSide(id, x));
			//...
			return 1;
		}

		// 3 means it need take attribute top piece
		private static int SearchTopSide(int id, int x) {
			if(id <= x) return 0;

			return 3;
		}

		// 3 means it need to random set bottom attribute
		private static int SearchBottomSide(int id, int x, int y) {
			if(((double)id + (double)x) / (double)x > y) return 0;

			return 3;
		}

		// 3 means it need take attribute left piece
		private static int SearchLeftSide(int id, int x) {
			if(id > x) {
				double res = ((double)id - 1.0) / (double)x;
				if(res == (int)res) {
					return 0;
				}
			} 

			if(id == 1) return 0;
			

			return 3;
		}

		// 3 means it need to random set right attribute
		private static int SearchRightSide(int id, int x) {
			if(id > x) {
				double res = (double)id / (double)x;
				if(res == (int)res) {
					return 0;
				}
			}

			if(id == x) return 0;

			return 3;
		}
	}

}