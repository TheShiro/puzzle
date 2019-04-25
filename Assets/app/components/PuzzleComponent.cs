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

	public RectTransform rt;

	public int parentID;
}
