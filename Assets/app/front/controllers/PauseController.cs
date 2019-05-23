using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;
using Front.Models;
using Front.Controllers;
using Front.Services;
using Services;
using Models;
using Main;

namespace Front.Controllers {

	public class PauseController : ControllerCore {

		public void actionIndex() {
			GameObject canvas = GameObject.Find("Canvas");

			GameObject pm = canvas.transform.Find("Pause").gameObject;
			if(pm != null) {
				pm.SetActive(!pm.active);
				pm.GetComponent<RectTransform>().SetAsLastSibling();
			}
		}

		public void actionQuit() {
			this.save_progress();

			Application.Quit();
		}

		public void actionMenu() {
			this.save_progress();
			
			Application.LoadLevel(0);
		}

		public void actionWin() {
			Application.LoadLevel(0);
		}

		public void actionSetting(GameObject setting) {
			SettingModel model = new SettingModel();
			model.Load();

			setting.transform.Find("quality").GetChild(0).GetComponent<Dropdown>().value = (int)model.quality;
			setting.transform.Find("mouseSpeed").GetChild(0).GetComponent<Slider>().value = model.mouse;
			setting.transform.Find("soundValue").GetChild(0).GetComponent<Slider>().value = model.sound;
			setting.transform.Find("musicValue").GetChild(0).GetComponent<Slider>().value = model.music;
			setting.transform.Find("mute").GetChild(0).GetComponent<Toggle>().isOn = model.mute == 1 ? true : false;
			setting.transform.Find("fullScreen").GetChild(0).GetComponent<Toggle>().isOn = model.fullscreen == 1 ? true : false;

			setting.SetActive(!setting.active);
		}

		public void actionContinue() {

		}

		public void savedSetting(GameObject setting) {
			SettingModel model = new SettingModel();

			model.quality = setting.transform.Find("quality").GetChild(0).GetComponent<Dropdown>().value;
			model.mouse = setting.transform.Find("mouseSpeed").GetChild(0).GetComponent<Slider>().value;
			model.sound = setting.transform.Find("soundValue").GetChild(0).GetComponent<Slider>().value;
			model.music = setting.transform.Find("musicValue").GetChild(0).GetComponent<Slider>().value;
			model.mute = setting.transform.Find("mute").GetChild(0).GetComponent<Toggle>().isOn ? 1 : 0;
			model.fullscreen = setting.transform.Find("fullScreen").GetChild(0).GetComponent<Toggle>().isOn ? 1 : 0;

			model.Save();

			SettingService.SetSetting(model);

			setting.SetActive(false);
		}

		public void cancelSetting(GameObject setting) {
			this.actionSetting(setting);
		}

		private void save_progress() {
			SaveService.DeleteSave(); // clear table before save new data
			
			for(int i = 0; i < PuzzlesService.puzzleArray.Length; i++) {
				SaveService.SetSave(PuzzlesService.puzzleArray[i]);
			}

			GamesModel gm = new GamesModel();
			gm.SaveBreakGame(StartPuzzle.gamesID);
		}
	}

}