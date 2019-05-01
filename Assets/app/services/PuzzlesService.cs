using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Models;

namespace Services {

	public static class PuzzlesService {

		public static PuzzleObject[] puzzleArray;

		public static void GeneratorPuzzles(int x, int y, string image) {
			puzzleArray = new PuzzleObject[y*x];
			//horizontal
			for(int i = 0; i < y; i++) {
				//vertical
				for(int j = 0; j < x; j++) {
					//formula getting id of puzzle
					int id = j + (x * i) + 1;
 
					PuzzleObject puzzle = new PuzzleObject("puzzle_UI", new Vector3(GeneratePX(), GeneratePZ(), 0), id, Scale(x));

					//save object
					puzzleArray[id-1] = puzzle;

					//Debug.Log("start id = " + id + " / x=" + x + " / y=" + y);

					puzzle.SetMaterial(image, GetMatrixPosition(id, x, y)); 
					//float off = 1.0f / (float)y * 0.32f;
					float off_x = 1.0f / (float)x * 0.32f;
					float off_y = 1.0f / (float)y * 0.32f;
					puzzle.SetMaterialOffset(new Vector2((1.0f / (float)x * (float)j) - off_x, (1.0f / (float)y * ((float)y - 1 -(float)i)) - off_y));
					float scale_x = 1.0f / (float)x * 0.609f;
					puzzle.SetMaterialScale(new Vector2((1.0f / (float)x) + scale_x, (1.0f / (float)y) / 0.609f));
				}
			}
		}

		private static string GetMatrixPosition(int id, int x, int y) {
			PuzzleObject obj;

			int top = SearchTopSide(id, x);
			int bottom = SearchBottomSide(id, x, y);
			int left = SearchLeftSide(id, x);
			int right = SearchRightSide(id, x);

			if(top == 3) {
				obj = new PuzzleObject(id - x);
				string b_side = obj.GetTypeSide();
				//Debug.Log("top " + b_side);
				top = (b_side[1] == '1') ? 2: 1;
			}

			if(left == 3) {
				obj = new PuzzleObject(id - 1);
				string r_side = obj.GetTypeSide();
				//Debug.Log("left " + r_side);
				left = (r_side[3] == '1') ? 2: 1;
			}

			return top + "" + bottom + "" + left + "" + right;
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

		//Generate position on x
		private static float GeneratePX() {
			//Random rand = new Random();
			return Random.Range(0, Screen.width * 0.15f);
		}

		//Generate position on z
		private static float GeneratePZ() {
			//Random rand = new Random();
			return Random.Range(Screen.height * 0.1f, Screen.height * 0.8f);
		}

		private static float Scale(int x) {
			switch(x) {
				case 7 : return 1.0f;
				case 10 : return 0.8f;
				case 15 : return 0.5f;
				default: return 1.0f;
			}
		}

		public static void CheckAllPiece() {
			
		}
	}

}