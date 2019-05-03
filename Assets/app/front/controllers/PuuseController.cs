using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

namespace Front.Controllers {

	public class PuuseController : ControllerCore {

		public void actionPause() {
			this.Render("Pause");
		}
	}

}