using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Front.Models;
using Config.Setting;

namespace Front.Services {

	public static class SettingService {

		public static SettingModel sm;

		public static void SetSetting(SettingModel model) {
			SetMusicSetting(model.music);
			SetSoundSetting(model.sound);
			SetMuteSetting(model.mute);
			SetMouseSetting(model.mouse);
			SetResolutionSetting(model.quality);
			SetScreenSetting(model.fullscreen);
		}

		private static void SetMusicSetting(float v) {
			GameObject.FindWithTag("Music").gameObject.GetComponent<AudioSource>().volume = v * Setting.MUSIC;
		}

		private static void SetSoundSetting(float v) {
			GameObject[] m = GameObject.FindGameObjectsWithTag("Sound");

			foreach(GameObject s in m) {
				s.GetComponent<AudioSource>().volume = v * Setting.SOUND;
			}
		}

		private static void SetMuteSetting(float v) {
			bool mute = false;
			switch(v + "") {
				case "0" : mute = false; break;
				case "1" : mute = true; break;
			}

			GameObject[] m = GameObject.FindGameObjectsWithTag("Sound");

			foreach(GameObject s in m) {
				s.GetComponent<AudioSource>().mute = mute;
			}

			GameObject.FindWithTag("Music").gameObject.GetComponent<AudioSource>().mute = mute;
		}

		private static void SetMouseSetting(float v) {
			Vector2 sensitivity = new Vector2(v, v);
			Vector2 mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivity.x, Input.GetAxisRaw("Mouse Y") * sensitivity.y);
		}

		private static void SetResolutionSetting(float v) {
			switch(v + "") {
				case "0" : Screen.SetResolution(1920, 1080, true); break;
				case "1" : Screen.SetResolution(1280, 720, true); break;
				case "2" : Screen.SetResolution(640, 480, true); break;
			}
		}

		private static void SetScreenSetting(float v) {
			Screen.fullScreen = v > 0 ? true : false;
		}
	}
}