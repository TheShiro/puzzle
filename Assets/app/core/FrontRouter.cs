using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Front.Controllers;

namespace Router {

	public class FrontRouter {

		public void Route(string controller, string action) {
			Debug.Log("route");

			switch(controller) {
				case "pause" : PauseStart(action); break;
			}
		}

		private void PauseStart(string action) {
			PauseController pc = new PauseController();

			switch(action) {
				case "index" : pc.actionIndex(); break;
				case "quit" : pc.actionQuit(); break;
				case "menu" : pc.actionMenu(); break;
				case "setting" : pc.actionSetting(); break;
				case "continue" : pc.actionContinue(); break;
			}
		}
	}

}