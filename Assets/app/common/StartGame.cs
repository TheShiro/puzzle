using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using Router;
using Front.Models;
using Front.Services;

namespace Main {

	public class StartGame : MonoBehaviour {

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