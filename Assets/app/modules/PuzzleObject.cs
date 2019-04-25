using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using Config;
using Main;

namespace Modules {

	public class PuzzleObject : ObjectCore {

		//private int id;

		/*
		* 0 - none; 1 - giver; 2 - receiver
		*/
		/*private int topSide;
		private int bottomSide;
		private int leftSide;
		private int rightSide;*/

		private Image _mat;
		public PuzzleComponent _component;
		//private RectTransform _rt;

		//constructor for create
		public PuzzleObject(string nameObject, Vector3 position, int id, float koeff_scale) {
			ObjectInit(C.PREFAB + nameObject);
			_component = _object.GetComponent<PuzzleComponent>();
			TransformInit();
			AttachToCanvas();
			SetTransform(position);
			SetScale(koeff_scale);
			SetID(id);
			SetName("puzzle" + id);
			MaterialInit();
		}
		//constructor for find
		public PuzzleObject(int id) {
			_object = GameObject.Find("puzzle" + id);
			_component = _object.GetComponent<PuzzleComponent>();
			TransformInit();
		}

		public void GetObject() {
			//print(_object);
		}

		private void SetID(int id) {
			_component.id = id;
		}

		public int GetID() {
			return _component.id;
		}

		private void SetScale(float koeff) {
			_component.rt.sizeDelta *= koeff;
		}

		//start redefinition
		private void MaterialInit() {
			_mat = _object.GetComponent<Image>();
			_mat.material = new Material(Shader.Find("Puzzle/puzzle"));
		}

		private void TransformInit() {
			_component.rt = _object.GetComponent<RectTransform>();
		}

		public void SetTransform(Vector3 pos) {
			_component.rt.anchoredPosition3D = pos;
		}

		public Vector3 GetTransform() {
			return _component.rt.anchoredPosition3D;
		}
		//end redefinition

		private void AttachToCanvas() {
			_component.rt.parent = GameObject.Find("Canvas").GetComponent<RectTransform>();
		}

		public void SetMaterial(string mainTex, string alpha) {
			PuzzleModel model = new PuzzleModel();

			_mat.material.SetTexture("_MainTex", model.GetMain(mainTex));
			_mat.material.SetTexture("_Alpha", model.GetAlpha(ref alpha));

			this.SetTopSide(Int32.Parse("" + alpha[0]));
			this.SetBottomSide(Int32.Parse("" + alpha[1]));
			this.SetLeftSide(Int32.Parse("" + alpha[2]));
			this.SetRightSide(Int32.Parse("" + alpha[3]));
		}

		public void SetMaterialOffset(Vector2 offset) {
			//this.SetMaterialOffset("_MainTex", offset);
			_mat.material.SetTextureOffset("_MainTex", offset);
		}

		public void SetMaterialScale(Vector2 scale) {
			//this.SetMaterialScale("_MainTex", scale);
			_mat.material.SetTextureScale("_MainTex", scale);
		}

		/* start block set:get for type of sides*/

		public bool SetTypeSides(string[] side, int[] type) {
			if(side.Length != type.Length) return false;

			int i = 1;
			foreach(string s in side) {
				switch(s) {
					case "top" : this.SetTopSide(type[i]); break;
					case "bottom" : this.SetBottomSide(type[i]); break;
					case "left" : this.SetLeftSide(type[i]); break;
					case "right" : this.SetRightSide(type[i]); break;
				}

				i++;
			}

			return true;
		}

		private void SetTopSide(int t) {
			_component.topSide = t;
		}

		private void SetBottomSide(int t) {
			_component.bottomSide = t;
		}

		private void SetLeftSide(int t) {
			_component.leftSide = t;
		}

		private void SetRightSide(int t) {
			_component.rightSide = t;
		}

		public string GetTypeSide() {
			return _component.topSide + "" + _component.bottomSide + "" + _component.leftSide + "" + _component.rightSide;
		}

		public int GetTypeSide(string side) {
			switch(side) {
				case "top": return _component.topSide;
				case "bottom": return _component.bottomSide;
				case "left": return _component.leftSide;
				case "right": return _component.rightSide;
			}

			return 0;
		}

		public int GetParent() {
			return _component.parentID;
		}

		public void SetParent(int id) {
			_component.parentID = id;
		}

		/* end block get:set for type of sides*/

		public void CheckScene() {
			if(_component.topSide > 0) {
				if(this.CheckDistanceTop()) {
					Debug.Log("TRUE1");
					ConnectTop();
				}
			}

			if(_component.bottomSide > 0) {
				if(this.CheckDistanceBottom()) {
					Debug.Log("TRUE2");
					ConnectBottom();
				}
			}

			if(_component.leftSide > 0) {
				if(this.CheckDistanceLeft()) {
					Debug.Log("TRUE3");
					ConnectLeft();
				}
			}

			if(_component.rightSide > 0) {
				if(this.CheckDistanceRight()) {
					Debug.Log("TRUE4");
					ConnectRight();
				}
			}
		}

		private bool CheckDistanceTop() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() - StartPuzzle.sizeX); //Debug.Log("TOP");

			_component.rt.parent = _oside._component.rt; 

			if(_component.rt.anchoredPosition3D.x > -(_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.x < (_component.rt.sizeDelta.x * 0.1f)  
				&& _component.rt.anchoredPosition3D.y < -(_component.rt.sizeDelta.x * 0.5f) && _component.rt.anchoredPosition3D.y > -(_component.rt.sizeDelta.x * 0.7f)) {
				return true; 
			}

			this.AttachToCanvas();
			return false;
		}

		private bool CheckDistanceBottom() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() + StartPuzzle.sizeX); //Debug.Log("BOTTOM");

			_component.rt.parent = _oside._component.rt; 

			if(_component.rt.anchoredPosition3D.x > -(_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.x < (_component.rt.sizeDelta.x * 0.1f) 
					&& _component.rt.anchoredPosition3D.y < (_component.rt.sizeDelta.x * 0.7f) && _component.rt.anchoredPosition3D.y > (_component.rt.sizeDelta.x * 0.5f)) {
				return true; 
			}

			this.AttachToCanvas();
			return false;
		}

		private bool CheckDistanceLeft() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() - 1); //Debug.Log("LEFT");

			_component.rt.parent = _oside._component.rt; 

			if(_component.rt.sizeDelta.x > (_component.rt.sizeDelta.x * 0.5f) && _component.rt.sizeDelta.x < (_component.rt.sizeDelta.x * 0.7f) 
					&& _component.rt.sizeDelta.y < (_component.rt.sizeDelta.x * 0.1f) && _component.rt.sizeDelta.y > -(_component.rt.sizeDelta.x * 0.1f)) {
				return true; 
			}

			this.AttachToCanvas();
			return false;
		}

		private bool CheckDistanceRight() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() + 1); //Debug.Log("RIGHT");

			_component.rt.parent = _oside._component.rt; 

			if(_component.rt.sizeDelta.x > -(_component.rt.sizeDelta.x * 0.7f) && _component.rt.sizeDelta.x < -(_component.rt.sizeDelta.x * 0.5f) 
					&& _component.rt.sizeDelta.y < (_component.rt.sizeDelta.x * 0.1f) && _component.rt.sizeDelta.y > -(_component.rt.sizeDelta.x * 0.1f)) {
				return true; 
			}

			this.AttachToCanvas();
			return false;
		}

		private void ConnectTop() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() - StartPuzzle.sizeX);

			//_component.rt.parent = _oside._component.rt;
			this.SetTransform(new Vector3(0, -_component.rt.sizeDelta.x * 0.61f, 0));
			this.SetParent(_oside.GetParent() > 0 ? _oside.GetParent() : this.GetID() - StartPuzzle.sizeX);
		}

		private void ConnectBottom() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() + StartPuzzle.sizeX);

			//_component.rt.parent = _oside._component.rt;
			this.SetTransform(new Vector3(0, _component.rt.sizeDelta.x * 0.61f, 0));
			this.SetParent(_oside.GetParent() > 0 ? _oside.GetParent() : this.GetID() + StartPuzzle.sizeX);
		}

		private void ConnectLeft() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() - 1);

			//_component.rt.parent = _oside._component.rt;
			this.SetTransform(new Vector3(_component.rt.sizeDelta.x * 0.61f, 0, 0));
			this.SetParent(_oside.GetParent() > 0 ? _oside.GetParent() : this.GetID() - 1);
		}

		private void ConnectRight() {
			PuzzleObject _oside = new PuzzleObject(this.GetID() + 1);

			//_component.rt.parent = _oside._component.rt;
			this.SetTransform(new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0));
			this.SetParent(_oside.GetParent() > 0 ? _oside.GetParent() : this.GetID() + 1);
		}
	}

}