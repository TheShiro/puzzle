using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class StartPuzzle : MonoBehaviour {

	public int gamesID;
	public int puzzleID;

	void Start () {
		string[] gameSettings = PuzzlesService.GetSettings();
		PuzzlesService.GeneratorPuzzles(1, 1);

		//Debug.Log(PuzzlesService.puzzleList.Count);
	}
}
