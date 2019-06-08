using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cash {

	public class CashLoad : MonoBehaviour {

		public GameObject puzzle;
		public GameObject back;

		//get by type
		public GameObject GetObject(string type) {
			switch(type) {
				case "puzzle_UI" : return puzzle;
				case "puzzle_UI_back" : return back;

				default : return puzzle;
			}
		}
	}

}