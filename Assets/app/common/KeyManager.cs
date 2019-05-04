using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Router;

public class KeyManager : MonoBehaviour {

	private FrontRouter r;

	public int inPlay = 0;

	private void Start() {
		r = new FrontRouter();
	}
	
	private void Update () {
		if(inPlay == 1) {
			if(Input.GetKeyDown(KeyCode.Escape)) {
				Debug.Log("press escape");

				r.Route("pause", "index");
			}
		}
	}

}
