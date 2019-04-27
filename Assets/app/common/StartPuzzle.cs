using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;

namespace Main {

public class StartPuzzle : MonoBehaviour {

	public static int gamesID;
	public static int puzzleID;

	public static int sizeX;
	public static int sizeY;

	void Start () {
		string[] gameSettings = GameService.GetSettings();
		gamesID = Int32.Parse(gameSettings[0]);
		puzzleID = Int32.Parse(gameSettings[1]);
		sizeX = Int32.Parse(gameSettings[3]);
		sizeY = Int32.Parse(gameSettings[4]);

		PuzzlesService.GeneratorPuzzles(sizeX, sizeY, gameSettings[2]);


	}
}

}