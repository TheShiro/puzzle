using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;
using Front.Models;

namespace Front.Controllers {

	public class MenuController : ControllerCore {

		public void actionTheme(GameObject pack) {
			pack.SetActive(!pack.active);
		}

		public void actionPuzzle() {
			this.Render("Puzzle");
		}

		public void actionStart() {
			//this.Render("Puzzle");
		}

		public void actionAchievement() {
			//this.Render("Puzzle");
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

			/*Debug.Log(model.quality);
			Debug.Log(model.mouse);
			Debug.Log(model.sound);
			Debug.Log(model.music);
			Debug.Log(model.mute);
			Debug.Log(model.fullscreen);*/

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
	}

}