using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Config;

namespace Models {

	public class SaveModel : ModelCore {

		public int id;
		public int obj_id;
		public int enabled;
		public string posx;
		public string posy;
		public int parent = 0;

		public void Save() {
			db.Insert("saves");
			db.Values(new string[] {"" + obj_id, "" + enabled, posx, posy, "" + parent});
			db.Go();
		}

		public void Loading() {
			db.Select(new string[] {"obj_id", "enabled", "posx", "posy", "parent"});
			db.From("saves");
			db.Where(new string[,] { {"obj_id", this.id + "", ""} });
			db.One();

			this.init(db.GetResult());
		}

		public void Clear() {
			db.Delete("saves");
			db.Go();
		}

		private void init(string[,] res) {
			Int32.TryParse(res[0,0], out this.obj_id);
			Int32.TryParse(res[0,1], out this.enabled);
			this.posx = res[0,2];
			this.posy = res[0,3];
			Int32.TryParse(res[0,4], out this.parent);
		}
	}

}