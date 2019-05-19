using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller;
using Front.Models;
using Services;

namespace Front.Controllers {

	public class WinController : ControllerCore {

		public void actionIndex() {
			//Debug.Log("PauseController:actionIndex");
			//this.Render("Pause");
			GameObject canvas = GameObject.Find("Canvas");//.transform.Find("Pause");

			GameObject win = canvas.transform.Find("Win").gameObject;
			//Debug.Log(menu);
			if(win != null) {
				win.SetActive(true);
				win.GetComponent<RectTransform>().SetAsLastSibling();
			}

			SaveService.DeleteSave();
		}
	}

}