using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Services;

public class StartPuzzle : MonoBehaviour {

	void Start () {
		PuzzleService.GeneratorPuzzles(1, 1);
		//PuzzleObject puzzle = new PuzzleObject("puzzle", new Vector3(0,0,0));

		//puzzle.Passport();
		//puzzle.GetObject();
	}
}
