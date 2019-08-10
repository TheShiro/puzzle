using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

public class Assets : MonoBehaviour {

	private int start = 0;

	void Start () {
		StartCoroutine("loading");
	}

	IEnumerator loading() {
		if(start == 0) {
			start = 1;
			yield return new WaitForSeconds(6);
		}

		Application.LoadLevel(C._GAME_MENU_);
	}
}
