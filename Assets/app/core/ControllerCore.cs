using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;
using Front.Views.Menu;
using Front.Views.Pause;

namespace Controller {

	public class ControllerCore : MonoBehaviour {

		protected ViewCore render;

		public ControllerCore() {
			//render = new MainView();
		}

		protected void Render(string view) {
			render = new MainView();
			render.Render();
		}


	}

}