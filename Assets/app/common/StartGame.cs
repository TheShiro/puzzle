using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using Router;
using Front.Models;
using Front.Services;

namespace Main {

	public class StartGame : MonoBehaviour {

		void Awake() {
			GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
		}

		void Start () {
			Settings();
		}

		private void Settings() {
			SettingModel model = new SettingModel();

			model.Load();

			SettingService.SetSetting(model);
		}

	}

}