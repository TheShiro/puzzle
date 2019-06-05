using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using Router;
using Front.Models;
using Front.Services;
using Config;

namespace Main {

	public class StartPuzzle : MonoBehaviour {

		public static int gamesID;
		public static int puzzleID;

		public static int sizeX;
		public static int sizeY;

		public int size;
		public int is_break;

		void Awake() {
			GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
		}

		void Start () {
			Settings();
			string[] gameSettings = GameService.GetSettings();
			gamesID = Int32.Parse(gameSettings[0]);
			puzzleID = Int32.Parse(gameSettings[1]);
			sizeX = Int32.Parse(gameSettings[3]);
			sizeY = Int32.Parse(gameSettings[4]);
			this.size = Int32.Parse(gameSettings[5]);
			this.is_break = Int32.Parse(gameSettings[6]);

			//формируем пазл в сцене
			PuzzlesService.GeneratorPuzzles(sizeX, sizeY, gameSettings[2]);

			//Если загруженый пазл был прерван, то загружаем его сохранение
			if(is_break == 1) {
				for(int i = 0; i < PuzzlesService.puzzleArray.Length; i++) {
					SaveService.GetSave(PuzzlesService.puzzleArray[i]);
				}
			}

			StartCoroutine("finished");
		}

		IEnumerator finished() {
			//Проверка собран ли пазл
			while(!PuzzlesService.Assemble()) {
				yield return new WaitForSeconds(3);
			}

			GameEnd(gamesID, puzzleID);
		}

		private void GameEnd(int gid, int pid) {
			GameService.GameEnd(gamesID);
			PuzzlesService.GameEnd(puzzleID, size);

			//устанавливаем готовое изображение
			GameObject.Find("Canvas").transform.Find("Win").Find("Image").gameObject.GetComponent<Image>().material = Resources.Load(C.MATERIALS + "101", typeof(Material)) as Material;

			FrontRouter r = new FrontRouter();
			r.Route("win");
		}

		private void Settings() {
			SettingModel model = new SettingModel();

			model.Load();

			SettingService.SetSetting(model);
		}
	}

}