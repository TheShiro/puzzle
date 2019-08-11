using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Migrations {

	public class Asia : Migration {

		private string GetMigrationName() {
			return this.GetType().Name;
		} 
	}

}