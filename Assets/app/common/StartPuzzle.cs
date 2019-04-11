using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

public class StartPuzzle : MonoBehaviour {

	public int gamesID;
	public int puzzleID;

	void Start () {
		string[] gameSettings = PuzzlesService.GetSettings();
		gamesID = Int32.Parse(gameSettings[0]);
		puzzleID = Int32.Parse(gameSettings[1]);

		PuzzlesService.GeneratorPuzzles(Int32.Parse(gameSettings[3]), Int32.Parse(gameSettings[4]), gameSettings[2]);

		//Debug.Log(PuzzlesService.puzzleList.Count);
	}
}
