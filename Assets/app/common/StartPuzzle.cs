using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using Router;

namespace Main {

	public class StartPuzzle : MonoBehaviour {

		public static int gamesID;
		public static int puzzleID;

		public static int sizeX;
		public static int sizeY;

		public int size;
		public int is_break;

		void Start () {
			string[] gameSettings = GameService.GetSettings();
			gamesID = Int32.Parse(gameSettings[0]);
			puzzleID = Int32.Parse(gameSettings[1]);
			sizeX = Int32.Parse(gameSettings[3]);
			sizeY = Int32.Parse(gameSettings[4]);
			this.size = Int32.Parse(gameSettings[5]);
			this.is_break = Int32.Parse(gameSettings[6]);

			PuzzlesService.GeneratorPuzzles(sizeX, sizeY, gameSettings[2]);

			if(is_break == 1) {
				for(int i = 0; i < PuzzlesService.puzzleArray.Length; i++) {
					SaveService.GetSave(PuzzlesService.puzzleArray[i]);
				}
			}

			StartCoroutine("finished");
		}

		IEnumerator finished() {
			while(!PuzzlesService.Assemble()) {
				yield return new WaitForSeconds(3);
			}

			//to win
			Debug.Log("you win");

			GameEnd(gamesID, puzzleID);
		}

		private void GameEnd(int gid, int pid) {
			GameService.GameEnd(gamesID);
			PuzzlesService.GameEnd(puzzleID, size);

			FrontRouter r = new FrontRouter();
			r.Route("win");
		}
	}

}