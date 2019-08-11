using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using Migrations;

public class Assets : MonoBehaviour {

	private int start = 0;

	void Start () {
		//check unapplied migrations
		migrations();

		//StartCoroutine("loading");
	}

	IEnumerator loading() {
		if(start == 0) {
			start = 1;
			yield return new WaitForSeconds(6);
		}

		Application.LoadLevel(C._GAME_MENU_);
	}

	private void migrations() {
		string nspace = "Migtations";

		var q = from t in Assembly.GetExecutingAssembly().GetTypes()
		        where t.Namespace == nspace
		        select t;
		q.ToList().ForEach(t => Debug.Log(t));

		Debug.Log(q.ToList());
	}
}
