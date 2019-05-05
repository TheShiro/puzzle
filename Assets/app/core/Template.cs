using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Template : MonoBehaviour {

	public void menu_button(GameObject b) {
		if(b.transform.GetChild(0).GetComponent<Text>().color == new Color(0.000f, 0.784f, 0.624f, 1.000f)) {
			b.transform.GetChild(0).GetComponent<Text>().color = new Color(1.000f, 1.000f, 1.000f, 1.000f);
		} else {
			b.transform.GetChild(0).GetComponent<Text>().color = new Color(0.000f, 0.784f, 0.624f, 1.000f);
		}
	}
}
