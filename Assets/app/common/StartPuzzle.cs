using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class StartPuzzle : MonoBehaviour {

	void Start () {
		PuzzlesService.GeneratorPuzzles(1, 1);

		//Debug.Log(PuzzlesService.puzzleList.Count);
	}
}
