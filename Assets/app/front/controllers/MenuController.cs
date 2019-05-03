using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;

public class MenuController : ControllerCore {

	public void actionMain() {
		this.Render("Main");
	}

	public void actionTheme() {
		this.Render("Theme");
	}

	public void actionPuzzle() {
		this.Render("Puzzle");
	}
}
