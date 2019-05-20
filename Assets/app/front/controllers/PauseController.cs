using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;
using Front.Models;
using Front.Controllers;
using Services;
using Models;
using Main;

namespace Front.Controllers {

	public class PauseController : ControllerCore {

		//public GameObject pm;

		public void actionIndex() {
			//Debug.Log("PauseController:actionIndex");
			//this.Render("Pause");
			GameObject canvas = GameObject.Find("Canvas");//.transform.Find("Pause");

			GameObject pm = canvas.transform.Find("Pause").gameObject;
			//Debug.Log(menu);
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
			
			//Application.LoadLevel(0);
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

			/*Debug.Log(model.quality);
			Debug.Log(model.mouse);
			Debug.Log(model.sound);
			Debug.Log(model.music);
			Debug.Log(model.mute);
			Debug.Log(model.fullscreen);*/

			setting.SetActive(!setting.active);

			SettingService ss = new SettingService();
			ss.SetSetting();
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

			/*Debug.Log(model.quality);
			Debug.Log(model.sound);
			Debug.Log(model.mute);*/
			setting.SetActive(false);
		}

		public void cancelSetting(GameObject setting) {
			/*SettingModel model = new SettingModel();

			setting.SetActive(false);*/
			this.actionSetting(setting);
		}

		private void save_progress() {
			SaveService.DeleteSave(); // clear table for save new data
			
			for(int i = 0; i < PuzzlesService.puzzleArray.Length; i++) {
				SaveService.SetSave(PuzzlesService.puzzleArray[i]);
			}

			GamesModel gm = new GamesModel();
			gm.SaveBreakGame(StartPuzzle.gamesID);
		}
	}

}