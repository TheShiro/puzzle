using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Migrations {

	public class Start : Migration {

		private string GetMigrationName() {
			return this.GetType().Name;
		} 
	}

}