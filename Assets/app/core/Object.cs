using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCore : ScriptableObject {

	private Transform _transfrorm;
	private MeshRenderer _mesh;

	//protected GameObject prefab;
	protected GameObject _object;

	//special component for save object setting
	protected PuzzleComponent _component;

	//initialization
	public void ObjectInit(string nameObject) {
		//_object = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
		_object = Instantiate(Resources.Load(nameObject)) as GameObject;
		TransformInit();
		MaterialInit();
	}

	private void TransformInit() {
		_transfrorm = _object.transform;
	}

	private void MaterialInit() {
		_mesh = _object.GetComponent<MeshRenderer>();
	}

	protected Vector3 GetTranform() {
		return _transfrorm.position;
	}

	/* SetTransform start */
	protected void SetTransform(float x) {
		_transfrorm.position = new Vector3(x, _transfrorm.position.y, _transfrorm.position.z);
	}

	protected void SetTransform(float x, float y) {
		_transfrorm.position = new Vector3(x, y, _transfrorm.position.z);
	}

	protected void SetTransform(float x, float y, float z) {
		_transfrorm.position = new Vector3(x, y, z);
	}

	protected void SetTransform(Vector3 pos) {
		_transfrorm.position = pos;
	}
	/* SetTransform end */

	/* AddTransform start */
	protected void AddTransform(float x) {
		_transfrorm.position = new Vector3(_transfrorm.position.x + x, _transfrorm.position.y, _transfrorm.position.z);
	}

	protected void AddTransform(float x, float y) {
		_transfrorm.position = new Vector3(_transfrorm.position.x + x, _transfrorm.position.y + y, _transfrorm.position.z);
	}

	protected void AddTransform(float x, float y, float z) {
		_transfrorm.position = new Vector3(_transfrorm.position.x + x, _transfrorm.position.y + y, _transfrorm.position.z + z);
	}
	/* AddTransform end */

	protected void SetName(string name) {
		_object.name = name;
	}

	protected void SetMaterial(string channel, Texture tex) {
		_mesh.materials[0].SetTexture(channel, tex);
	}

	protected void SetMaterialScale(string channel, Vector2 scale) {
		_mesh.materials[0].SetTextureScale(channel, scale);
	}

	protected void SetMaterialOffset(string channel, Vector2 offset) {
		_mesh.materials[0].SetTextureOffset(channel, offset);
	}

	protected void Passport() {
		//print("class ObjectBase");
	}
}
