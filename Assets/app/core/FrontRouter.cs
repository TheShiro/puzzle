using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Front.Controllers;

namespace Router {

	public class FrontRouter {

		public void Route(string controller, string action) {
			//Debug.Log("route");

			switch(controller) {
				case "pause" : PauseStart(action); break;
				case "win" : WinStart(action); break;
			}
		}

		public void Route(string controller) {
			//Debug.Log("route");

			switch(controller) {
				case "pause" : PauseStart("index"); break;
				case "win" : WinStart("index"); break;
			}
		}

		private void PauseStart(string action) {
			PauseController pc = new PauseController();

			switch(action) {
				case "index" : pc.actionIndex(); break;
				case "quit" : pc.actionQuit(); break;
				case "menu" : pc.actionMenu(); break;
				//case "setting" : pc.actionSetting(); break;
				case "continue" : pc.actionContinue(); break;
			}
		}

		private void WinStart(string action) {
			WinController wc = new WinController();

			switch(action) {
				case "index" : wc.actionIndex(); break;
			}
		}
	}

}