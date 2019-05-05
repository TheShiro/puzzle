using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;
using Front.Models;

namespace Front.Controllers {

	public class PauseController : ControllerCore {

		//public GameObject pm;

		public void actionIndex() {
			Debug.Log("PauseController:actionIndex");
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
			Application.Quit();
		}

		public void actionMenu() {
			Application.LoadLevel(0);
		}

		public void actionSetting(GameObject setting) {

			setting.SetActive(!setting.active);
			//this.Render("Setting");
		}

		public void actionContinue() {

		}

		public void savedSetting(GameObject setting) {

		}

		public void cancelSetting(GameObject setting) {
			setting.SetActive(false);
		}
	}

}