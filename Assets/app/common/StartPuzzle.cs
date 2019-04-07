using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Services;

public class StartPuzzle : MonoBehaviour {

	void Start () {
		PuzzleService.GeneratorPuzzles(2, 2);
	}
}
