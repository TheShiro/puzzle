using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;
using Front.Models;
using Front.Controllers;
using Front.Services;
using Config;
//using Models;

namespace Front.Controllers {

	public class MenuController : ControllerCore {

		public void actionTheme(GameObject pack) {
			pack.SetActive(!pack.active);
		}

		public void actionPuzzle(int id) {
			GameObject pp = GameObject.Find("Canvas").transform.Find("main").Find("PuzzlePanel").gameObject;
			GameObject list = GameObject.Find("Canvas").transform.Find("main").Find("PuzzlePanel").Find("puzzleList").gameObject;
			GameObject pzl = GameObject.Find("Canvas").transform.Find("main").Find("PuzzlePanel").Find("puzzleList").Find("" + id).gameObject;
			//Debug.Log(pzl);

			for(int i = 0; i < list.transform.childCount; i++) {
				list.transform.GetChild(i).gameObject.SetActive(false);
			}
			
			pp.SetActive(true);
			pzl.SetActive(true);
		}

		public void actionStart(string name) {
			GameObject size = GameObject.Find("Canvas").transform.Find("main").Find("PuzzlePanel").Find("size").gameObject;
			Debug.Log(size.GetComponent<Dropdown>().value);
			int sz = size.GetComponent<Dropdown>().value + 1;

			GameModel model = new GameModel();
			PuzzleModel pm = new PuzzleModel();

			pm.GetByName(name);

			if(sz == 3 && pm.mid == 0) {
				GameObject warning = GameObject.Find("Canvas").transform.Find("main").Find("PuzzlePanel").Find("Warning").gameObject;
				warning.SetActive(true);
			} else {
				model.Set(pm.id, sz);
				Application.LoadLevel(C._GAME_PLAY_);
			}
		}

		public void actionAchievement(GameObject panel) {
			panel.SetActive(!panel.active);
		}

		public void actionQuit() {
			Application.Quit();
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

		public void actionContinue(GameObject error) {
			GameModel model = new GameModel();
			if(model.CheckBreakGame()) {
				Application.LoadLevel(C._GAME_PLAY_);
			} else {
				error.SetActive(true);
			}
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
	}

}