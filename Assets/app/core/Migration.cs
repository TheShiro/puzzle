using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Migration {

	protected string GetMigrationName() {
		return this.GetType().Name;
	}

	public bool check() {
		string mig = this.GetMigrationName();

		Debug.Log(mig);

		return false;
	}

}
