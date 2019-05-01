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

		public void SetParentLocal(int id) {
			_component.parentID = id;
		}

		public void SetParentLocal(PuzzleObject par) {
			_component.parentID = par._component.id;
			_component.rt.SetParent(par._component.rt);
		}

		public void SetParentForChilds(PuzzleObject par, Vector3 offset) {
			/*_component.parentID = par._component.id;
			_component.rt.parent = par._component.rt;*/
			this.SetTransform(_component.rt.anchoredPosition3D + offset);
			this.SetParentLocal(par);
			Debug.Log("HERE! Connected childs");
			Debug.Log("offset" + offset);

			int c = _component.rt.childCount;

			Debug.Log("puzzle " + this.GetID() + " have " + c + " childs, parent " + par.GetID());

			if(c > 0) {
				for(int j = 0; j < c; j++) {
					//Debug.Log("child key " + j);
					this.Child(0).SetParentLocal(par);
				}
			}

			//this.SetParent(par);
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
					CheckDistanceForChild(i);
					continue; // go to next iteration
				}

				//Debug.Log(i + " true " + arr_id[i]);

				this.Connect(_oside, i);
				this.SetParentLocal(_oside.GetParent() > 0 ? _oside.GetParent() : arr_id[i]);

				PuzzleObject par = new PuzzleObject(this.GetParent());
				_component.rt.parent = par._component.rt; 
				AttachToParent(par);

				break; // left from cycle
			}
		}

		public bool CheckDistance(PuzzleObject _oside, int side) {
			_component.rt.parent = _oside._component.rt; 

			//Debug.Log("local" + _oside._component.id);

			//Debug.Log("global" + Camera.main.WorldToViewportPoint (_oside._component.rt.anchoredPosition3D));
			//Debug.Log("global" + Camera.main.WorldToViewportPoint (_component.rt.anchoredPosition3D));

			//Debug.Log("global" + Camera.main.WorldToViewportPoint(new PuzzleObject(1)._component.rt.anchoredPosition3D));
			//Debug.Log("global" + Camera.main.WorldToViewportPoint(new PuzzleObject(8)._component.rt.anchoredPosition3D));

			switch(side) {
				case 0 : return CheckDistanceTop(Camera.main.WorldToViewportPoint(_component.rt.anchoredPosition3D), Camera.main.WorldToViewportPoint(_oside._component.rt.anchoredPosition3D)); 
				case 1 : return CheckDistanceBottom(Camera.main.WorldToViewportPoint(_component.rt.anchoredPosition3D), Camera.main.WorldToViewportPoint(_oside._component.rt.anchoredPosition3D)); 
				case 2 : return CheckDistanceLeft(Camera.main.WorldToViewportPoint(_component.rt.anchoredPosition3D), Camera.main.WorldToViewportPoint(_oside._component.rt.anchoredPosition3D)); 
				case 3 : return CheckDistanceRight(Camera.main.WorldToViewportPoint(_component.rt.anchoredPosition3D), Camera.main.WorldToViewportPoint(_oside._component.rt.anchoredPosition3D)); 
			}

			return false;
		}

		//do every child's attached to top parent
		private void AttachToParent(PuzzleObject par) {
			int c = _component.rt.childCount;

			Debug.Log("puzzle " + this.GetID() + " have " + c + " childs, parent " + par.GetID());

			if(c > 0) {
				for(int j = 0; j < c; j++) {
					//Debug.Log("child key " + j);
					this.Child(0).SetParentLocal(par);
				}
			}
		}

		private void CheckDistanceForChild(int side) {
			int c = _component.rt.childCount;
			int[] child_id;
			int[] fin_id;
			if(c > 0) {
				child_id = new int[c+1];
				fin_id = new int[c+1];

				for(int j = 0; j < c; j++) {
					child_id[j] = this.Child(j).GetID();
					fin_id[j] = this.Child(j).GetID();
				}

				child_id[c] = this.GetID();
				fin_id[c] = this.GetID();

				Debug.Log("test");
				switch(side) {
					case 0 : Debug.Log("TOP"); break;
					case 1 : Debug.Log("BOTTOM"); break;
					case 2 : Debug.Log("LEFT"); break;
					case 3 : Debug.Log("RIGHT"); break;
				}

				for(int j = 0; j <= c; j++) {
					int del;
					switch(side) {
						case 0 : del = child_id[j] - StartPuzzle.sizeX; break;
						case 1 : del = child_id[j] + StartPuzzle.sizeX; break;
						case 2 : del = child_id[j] - 1; break;
						case 3 : del = child_id[j] + 1; break;
						default : del = 0; break;
					}

					//Debug.Log("in " + child_id[j] + " del " + del);

					for(int k = 0; k <= c; k++) {
						if(child_id[k] == del) {
							//Debug.Log("delete " + child_id[j]);
							fin_id[j] = 0;
						}
					}

				}

				fin_id[c] = 0;
				for(int j = 0; j <= c; j++) {
					if(fin_id[j] == 0) continue;
					
					/*Debug.Log("-------------------------------------------------------");
					Debug.Log("out " + fin_id[j]);*/

					PuzzleObject temp = new PuzzleObject(fin_id[j]);

					switch(side) {
						case 0 : if(temp.GetTypeSide("top") != 0) test_check(side, temp, new PuzzleObject(fin_id[j] - StartPuzzle.sizeX)); break;
						case 1 : if(temp.GetTypeSide("bottom") != 0) test_check(side, temp, new PuzzleObject(fin_id[j] + StartPuzzle.sizeX)); break;
						case 2 : if(temp.GetTypeSide("left") != 0) test_check(side, temp, new PuzzleObject(fin_id[j] - 1)); break;
						case 3 : if(temp.GetTypeSide("right") != 0) test_check(side, temp, new PuzzleObject(fin_id[j] + 1)); break;
					}

					Debug.Log("-------------------------------------------------------");
				}
			}
		}

		private void test_check(int side, PuzzleObject one, PuzzleObject two/*, PuzzleObject tp*/) {
			Vector3 sub = one._component.rt.anchoredPosition3D;

			PuzzleObject two_par = null;

			Vector3 master;
			Vector3 master_par = new Vector3(0,0,0);
			master = two._component.rt.anchoredPosition3D;

			// if parent id > 0 then used parent of master piece to calculate global position
			if(two.GetParent() > 0) {
				two_par = new PuzzleObject(two.GetParent());
				master_par = two_par._component.rt.anchoredPosition3D;

				//Debug.Log("two_par" + one);
			}

			//position parent of sub piece
			PuzzleObject par = new PuzzleObject(one.GetParent());
			Vector3 top = par._component.rt.anchoredPosition3D;

			Debug.Log("id" + one.GetID());
			//Debug.Log("pid " + two.GetParent() + " --- two par " + (two_par._component.id ? two_par._component.id : "333"));
			Debug.Log("top" + top);
			Debug.Log("sub" + sub);
			Debug.Log("sub global" + (top + sub));
			Debug.Log("master" + master + "mid" + two.GetID());
			Debug.Log("master par" + master_par);
			Debug.Log("master global" + (master_par + master));

			Vector3 result = (master_par + master) - (top + sub);
			Debug.Log("result" + result);

			if(side == 0) {
				if((_component.rt.sizeDelta.x * 0.55f) < result.y && result.y < (_component.rt.sizeDelta.x * 0.7f)) {
					if((_component.rt.sizeDelta.x * 0.1f) > result.x && result.x > -(_component.rt.sizeDelta.x * 0.1f)) {
						Debug.Log("true");
						/*one.SetParentLocal(two);

						this.SetParentForChilds(two, (new Vector3(0, -_component.rt.sizeDelta.x * 0.61f, 0) - one._component.rt.anchoredPosition3D));
						one.SetTransform(one._component.rt.anchoredPosition3D + (new Vector3(0, -_component.rt.sizeDelta.x * 0.61f, 0) - one._component.rt.anchoredPosition3D));*/

						if(two.GetParent() > 0) {
							this.ConnectChild(one, two_par, result + new Vector3(0, -_component.rt.sizeDelta.x * 0.61f, 0));
						} else {
							this.ConnectChild(one, two, result + new Vector3(0, -_component.rt.sizeDelta.x * 0.61f, 0));
						}
					}
				}
			}

			if(side == 1) {
				if(-(_component.rt.sizeDelta.x * 0.55f) > result.y && result.y > -(_component.rt.sizeDelta.x * 0.7f)) {
					if((_component.rt.sizeDelta.x * 0.1f) > result.x && result.x > -(_component.rt.sizeDelta.x * 0.1f)) {
						Debug.Log("true");
						/*one.SetParent(two);

						this.SetParentForChilds(two, (new Vector3(0, _component.rt.sizeDelta.x * 0.61f, 0) - one._component.rt.anchoredPosition3D));
						one.SetTransform(one._component.rt.anchoredPosition3D + (new Vector3(0, _component.rt.sizeDelta.x * 0.61f, 0) - one._component.rt.anchoredPosition3D));*/

						if(two.GetParent() > 0) {
							this.ConnectChild(one, two_par, result + new Vector3(0, _component.rt.sizeDelta.x * 0.61f, 0));
						} else {
							this.ConnectChild(one, two, result + new Vector3(0, _component.rt.sizeDelta.x * 0.61f, 0));
						}
					}
				}
			}

			if(side == 2) {
				if(-(_component.rt.sizeDelta.x * 0.55f) > result.x && result.x > -(_component.rt.sizeDelta.x * 0.7f)) {
					if((_component.rt.sizeDelta.x * 0.1f) > result.y && result.y > -(_component.rt.sizeDelta.x * 0.1f)) {
						Debug.Log("true");
						/*one.SetParent(two);

						this.SetParentForChilds(two, new Vector3(_component.rt.sizeDelta.x * 0.61f, 0, 0) - one._component.rt.anchoredPosition3D);
						one.SetTransform(one._component.rt.anchoredPosition3D + (new Vector3(_component.rt.sizeDelta.x * 0.61f, 0, 0) - one._component.rt.anchoredPosition3D));*/

						if(two.GetParent() > 0) {
							this.ConnectChild(one, two_par, result + new Vector3(_component.rt.sizeDelta.x * 0.61f, 0, 0));
						} else {
							this.ConnectChild(one, two, result + new Vector3(_component.rt.sizeDelta.x * 0.61f, 0, 0));
						}
					}
				}
			}

			if(side == 3) {
				if((_component.rt.sizeDelta.x * 0.55f) < result.x && result.x < (_component.rt.sizeDelta.x * 0.7f)) {
					if((_component.rt.sizeDelta.x * 0.1f) > result.y && result.y > -(_component.rt.sizeDelta.x * 0.1f)) {
						Debug.Log("true");
						if(two.GetParent() > 0) {
							/*one.SetParent(two_par);

							this.SetParentForChilds(two_par, (new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0) - one._component.rt.anchoredPosition3D));
							one.SetTransform(one._component.rt.anchoredPosition3D + (new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0) - one._component.rt.anchoredPosition3D));*/
							this.ConnectChild(one, two_par, result + new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0));
						} else {
							/*one.SetParent(two);

							this.SetParentForChilds(two, (new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0) - one._component.rt.anchoredPosition3D));
							one.SetTransform(one._component.rt.anchoredPosition3D + (new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0) - one._component.rt.anchoredPosition3D));*/

							this.ConnectChild(one, two, result + new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0));
						}
					}
				}
			}
		}

		private void ConnectChild(PuzzleObject one, PuzzleObject two, Vector3 pos) {
			one.SetParentLocal(two);

			this.SetParentForChilds(two, pos);
			//Debug.Log(new Vector3(-_component.rt.sizeDelta.x * 0.61f, 0, 0) + "pos" + pos);
			one.SetTransform(one._component.rt.anchoredPosition3D + pos);
		}

		private bool CheckDistanceTop(Vector3 sub, Vector3 master) {
			// lod logic
			if(_component.rt.anchoredPosition3D.x > -(_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.x < (_component.rt.sizeDelta.x * 0.1f)  
				&& _component.rt.anchoredPosition3D.y < -(_component.rt.sizeDelta.x * 0.5f) && _component.rt.anchoredPosition3D.y > -(_component.rt.sizeDelta.x * 0.7f)) {
				return true; 
			}
			/*Debug.Log("sub" + sub);
			Debug.Log("master" + master);
			Debug.Log(master - sub);*/
			// new logic
			/*if( < -sub.x && sub.x < ) {
				if() {

				}
			}*/
			return false;
		}

		private bool CheckDistanceBottom(Vector3 sub, Vector3 master) {
			/*Debug.Log("sub" + sub);
			Debug.Log("master" + master);
			Debug.Log(master - sub);*/

			if(_component.rt.anchoredPosition3D.x > -(_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.x < (_component.rt.sizeDelta.x * 0.1f) 
					&& _component.rt.anchoredPosition3D.y < (_component.rt.sizeDelta.x * 0.7f) && _component.rt.anchoredPosition3D.y > (_component.rt.sizeDelta.x * 0.5f)) {
				return true; 
			}
			return false;
		}

		private bool CheckDistanceLeft(Vector3 sub, Vector3 master) {
			/*Debug.Log("sub" + sub);
			Debug.Log("master" + master);
			Debug.Log(master - sub);*/

			//Debug.Log((_component.rt.sizeDelta.x * 0.7f) + " > " + _component.rt.sizeDelta.x + " > " + (_component.rt.sizeDelta.x * 0.5f));

			if((_component.rt.sizeDelta.x * 0.7f) > _component.rt.anchoredPosition3D.x && _component.rt.anchoredPosition3D.x > (_component.rt.sizeDelta.x * 0.5f)) { // axis X
				if(_component.rt.anchoredPosition3D.y < (_component.rt.sizeDelta.x * 0.1f) && _component.rt.anchoredPosition3D.y > -(_component.rt.sizeDelta.x * 0.1f)) { // axis Y
					return true; 
				}
			}
			return false;
		}

		private bool CheckDistanceRight(Vector3 sub, Vector3 master) {
			/*Debug.Log("sub" + sub);
			Debug.Log("master" + master);
			Debug.Log(master - sub);*/

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

		public PuzzleObject Child(int k) {
			GameObject c = _component.rt.GetChild(k).gameObject;

			int id = c.GetComponent<PuzzleComponent>().id;

			//Debug.Log("child id" + id);

			return new PuzzleObject(id);
		}
	}

}