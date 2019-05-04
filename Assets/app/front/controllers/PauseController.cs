using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Front.Controllers {

	public class PauseController : ControllerCore {

		public void actionIndex() {
			Debug.Log("PauseController:actionIndex");
			this.Render("Pause");
		}

		public void actionQuit() {

		}

		public void actionMenu() {

		}

		public void actionSetting() {
			this.Render("Setting");
		}

		public void actionContinue() {

		}

		
	}

}