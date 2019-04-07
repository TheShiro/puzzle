using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;

namespace Services {

	public class PuzzleService : MonoBehaviour {

		public static void GeneratorPuzzles(int x, int y) {
			PuzzleObject puzzle = new PuzzleObject("puzzle", new Vector3(0,0,0));
		}
	}

}