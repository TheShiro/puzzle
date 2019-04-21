using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleComponent : MonoBehaviour {

	public int id;

	/*
	* 0 - none; 1 - giver; 2 - receiver
	*/
	public int topSide;
	public int bottomSide;
	public int leftSide;
	public int rightSide;

	/*public void SetID(int id) {
		this.id = id;
	}

	public int GetID() {
		return this.id;
	}

	public string GetTypeSide() {
		return this.topSide + "" + this.bottomSide + "" + this.leftSide + "" + this.rightSide;
	}

	public int GetTypeSide(string side) {
		Debug.Log(this.topSide + "" + this.bottomSide + "" + this.leftSide + "" + this.rightSide);
		switch(side) {
			case "top": return this.topSide;
			case "bottom": return this.bottomSide;
			case "left": return this.leftSide;
			case "right": return this.rightSide;
		}

		return 0;
	}

	public void SetTopSide(int t) {
		this.topSide = t;
	}

	public void SetBottomSide(int t) {
		this.bottomSide = t;
	}

	public void SetLeftSide(int t) {
		this.leftSide = t;
	}

	public void SetRightSide(int t) {
		this.rightSide = t;
	}*/
}
