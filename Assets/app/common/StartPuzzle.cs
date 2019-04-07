using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Services;

public class StartPuzzle : MonoBehaviour {

	void Start () {
		PuzzlesService.GeneratorPuzzles(2, 2);

		//Debug.Log(PuzzlesService.puzzleList.Count);
	}
}
