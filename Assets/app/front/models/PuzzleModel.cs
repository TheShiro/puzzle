using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Front.Models {

	public class PuzzleModel : ModelCore {

		public int id;
		public int set_id;
		public string name;
		public string image;
		public int low;
		public int mid;
		public int high;

		public void GetByName(string name) {
			db.Select(new string[] {"`id`", "`set_id`", "`name`", "`image`", "`low`", "`mid`", "`high`"});
			db.From("puzzle");
			db.Where(new string[,] { {"image", name, "text"} });
			db.One();

			this.init(db.GetResult());
		}

		private void init(string[,] res) {
			Int32.TryParse(res[0,0], out this.id);
			Int32.TryParse(res[0,1], out this.set_id);
			this.name = res[0,2];
			this.image = res[0,3];
			Int32.TryParse(res[0,4], out this.low);
			Int32.TryParse(res[0,5], out this.mid);
			Int32.TryParse(res[0,6], out this.high);
		}
	}

}