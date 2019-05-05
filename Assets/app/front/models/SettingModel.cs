using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Front.Models {

	public class SettingModel : ModelCore {

		public float quality;
		public float mouse;
		public float sound;
		public float music;
		public float mute;
		public float fullscreen;

		public void Save() {
			db.Update("setting");
			db.Set(new string[,] { {"value", this.quality.ToString()} });
			db.Where(new string[,] { {"id", "1", ""} });
			db.Go();

			db.Update("setting");
			db.Set(new string[,] { {"value", this.mouse.ToString()} });
			db.Where(new string[,] { {"id", "2", ""} });
			db.Go();

			db.Update("setting");
			db.Set(new string[,] { {"value", this.sound.ToString()} });
			db.Where(new string[,] { {"id", "3", ""} });
			db.Go();

			db.Update("setting");
			db.Set(new string[,] { {"value", this.music.ToString()} });
			db.Where(new string[,] { {"id", "4", ""} });
			db.Go();

			db.Update("setting");
			db.Set(new string[,] { {"value", this.mute.ToString()} });
			db.Where(new string[,] { {"id", "5", ""} });
			db.Go();

			db.Update("setting");
			db.Set(new string[,] { {"value", this.fullscreen.ToString()} });
			db.Where(new string[,] { {"id", "6", ""} });
			db.Go();
		}

		public void Load() {
			db.Select(new string[] {"`value`"});
			db.From("setting");
			db.Order("id", "asc");
			db.All();

			string[,] res = db.GetResult();

			this.quality = float.Parse(res[0,0]);
			this.mouse = float.Parse(res[1,0]);
			this.sound = float.Parse(res[2,0]);
			this.music = float.Parse(res[3,0]);
			this.mute = float.Parse(res[4,0]);
			this.fullscreen = float.Parse(res[5,0]);
		}
	}

}