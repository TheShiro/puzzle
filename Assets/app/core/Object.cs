using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCore : MonoBehaviour {

	private Transform _transfrorm;

	//protected GameObject prefab;
	protected GameObject _object;

	//constructor
	public ObjectCore(string nameObject) {
		//_object = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
		_object = Instantiate(Resources.Load(nameObject)) as GameObject;
	}

	private void TransformInit() {
		_transfrorm = gameObject.transform;
	}

	public Vector3 GetTranform() {
		return _transfrorm.position;
	}

	/* SetTransform start */
	public void SetTransform(float x) {
		_transfrorm.position = new Vector3(x, _transfrorm.position.y, _transfrorm.position.z);
	}

	public void SetTransform(float x, float y) {
		_transfrorm.position = new Vector3(x, y, _transfrorm.position.z);
	}

	public void SetTransform(float x, float y, float z) {
		_transfrorm.position = new Vector3(x, y, z);
	}

	public void SetTransform(Vector3 pos) {
		_transfrorm.position = new Vector3(pos.x, pos.y, pos.z);
	}
	/* SetTransform end */

	/* AddTransform start */
	public void AddTransform(float x) {
		_transfrorm.position = new Vector3(_transfrorm.position.x + x, _transfrorm.position.y, _transfrorm.position.z);
	}

	public void AddTransform(float x, float y) {
		_transfrorm.position = new Vector3(_transfrorm.position.x + x, _transfrorm.position.y + y, _transfrorm.position.z);
	}

	public void AddTransform(float x, float y, float z) {
		_transfrorm.position = new Vector3(_transfrorm.position.x + x, _transfrorm.position.y + y, _transfrorm.position.z + z);
	}
	/* AddTransform end */

	public void Passport() {
		print("class ObjectBase");
	}
}
