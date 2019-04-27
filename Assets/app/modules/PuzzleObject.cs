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

		public void SetParent(PuzzleObject par) {
			_component.parentID = par._component.id;
			_component.rt.parent = par._component.rt;
		}

		/* end block get:set for type of sides*/

		public void CheckScene() {
			int[] arr_id = new int[4];
			arr_id[0] = (_component.topSide > 0) ? this.GetID() - StartPuzzle.sizeX : 0;
			arr_id[1] = (_component.bottomSide > 0) ? this.GetID() + StartPuzzle.sizeX : 0;
			arr_id[2] = (_component.leftSide > 0) ? this.GetID() - 1 : 0;
			arr_id[3] = (_component.rightSide > 0) ? this.GetID() + 1 : 0;


			for(int i = 0; i < 4; i++) {
				if(arr_id[i] == 0) continue; // go to next iteration

				PuzzleObject _oside = new PuzzleObject(arr_id[i]);

				if(!this.CheckDistance(_oside, i)) {
					this.AttachToCanvas();
					//Debug.Log(i + " false");
					continue; // go to next iteration
				}

				//Debug.Log(i + " true " + arr_id[i]);

				this.Connect(_oside, i);
				this.SetParent(_oside.GetParent() > 0 ? _oside.GetParent() : arr_id[i]);

				PuzzleObject par = new PuzzleObject(this.GetParent());
				_component.rt.parent = par._component.rt; 
				AttachToParent(par);

				break; // left from cycle
			}
		}

		public bool CheckDistance(PuzzleObject _oside, int side) {
			_component.rt.parent = _oside._component.rt; 

			Debug.Log("local" + _component.rt.anchoredPosition3D);

			//Debug.Log("global" + Camera.main.WorldToViewportPoint (_oside._component.rt.anchoredPosition3D));
			//Debug.Log("global" + Camera.main.WorldToViewportPoint (_component.rt.anchoredPosition3D));

			Debug.Log("global" + Camera.main.WorldToViewportPoint(new PuzzleObject(1)._component.rt.anchoredPosition3D));
			Debug.Log("global" + Camera.main.WorldToViewportPoint(new PuzzleObject(8)._component.rt.anchoredPosition3D));

			switch(side) {
				case 0 : return CheckDistanceTop(); 
				case 1 : return CheckDistanceBottom(); 
				case 2 : return CheckDistanceLeft(); 
				case 3 : return CheckDistanceRight(); 
			}

			//_child.AttachToCanvas();
			return false;
		}

		//do every child's attached to top parent
		private void AttachToParent(PuzzleObject par) {
			int c = _component.rt.childCount;

			if(c > 0) {
				for(int j = 0; j < c; j++) {
					this.Child(j).SetParent(par);
				}
			}
		}

		private bool CheckDistanceTop() {
			if(_component.rt.anchoredPosition3D.x > -(_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.x < (_component.rt.sizeDelta.x * 0.1f)  
				&& _component.rt.anchoredPosition3D.y < -(_component.rt.sizeDelta.x * 0.5f) && _component.rt.anchoredPosition3D.y > -(_component.rt.sizeDelta.x * 0.7f)) {
				return true; 
			}
			return false;
		}

		private bool CheckDistanceBottom() {
			if(_component.rt.anchoredPosition3D.x > -(_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.x < (_component.rt.sizeDelta.x * 0.1f) 
					&& _component.rt.anchoredPosition3D.y < (_component.rt.sizeDelta.x * 0.7f) && _component.rt.anchoredPosition3D.y > (_component.rt.sizeDelta.x * 0.5f)) {
				return true; 
			}
			return false;
		}

		private bool CheckDistanceLeft() {
			//Debug.Log((_component.rt.sizeDelta.x * 0.7f) + " > " + _component.rt.sizeDelta.x + " > " + (_component.rt.sizeDelta.x * 0.5f));

			if((_component.rt.sizeDelta.x * 0.7f) > _component.rt.anchoredPosition3D.x && _component.rt.anchoredPosition3D.x > (_component.rt.sizeDelta.x * 0.5f)) { // axis X
				if(_component.rt.anchoredPosition3D.y < (_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.y > -(_component.rt.sizeDelta.x * 0.1f)) { // axis Y
					return true; 
				}
			}
			return false;
		}

		private bool CheckDistanceRight() {
			//Debug.Log(-(_component.rt.sizeDelta.x * 0.7f) + " < " + _component.rt.anchoredPosition3D.x + " < " + -(_component.rt.sizeDelta.x * 0.5f));

			if(-(_component.rt.sizeDelta.x * 0.7f) < _component.rt.anchoredPosition3D.x && _component.rt.anchoredPosition3D.x < -(_component.rt.sizeDelta.x * 0.5f)) { // axis X
				if(_component.rt.anchoredPosition3D.y < (_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.y > -(_component.rt.sizeDelta.x * 0.1f)) {  // axis Y
					return true; 
				}
			}
			return false;
		}

		private void Connect(PuzzleObject _oside, int side) {
			//this.SetParent(_oside);

			switch(side) {
				case 0 : this.SetTransform(new Vector3(0, -_component.rt.sizeDelta.x * 0.61f, 0)); break;
				case 1 : this.SetTransform(new Vector3(0, _component.rt.sizeDelta.x * 0.61f, 0)); break;
				case 2 : this.SetTransform(new Vector3(_component.rt.sizeDelta.x * 0.61f, 0, 0)); break;
				case 3 : this.SetTransform(new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0)); break;
			}
		}

		public PuzzleObject Child(int i) {
			GameObject c = _component.rt.GetChild(i).gameObject;

			int id = c.GetComponent<PuzzleComponent>().id;

			//Debug.Log("child id" + id);

			return new PuzzleObject(id);
		}
	}

}