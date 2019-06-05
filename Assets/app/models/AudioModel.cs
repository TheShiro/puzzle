using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;

namespace Models {

	public class AudioModel : ModelCore {

		public string[] GetMusic(int ind) {
			db.Select(new string[] {"name"});
			db.From("audio");
			db.Where(new string[,] { {"sector", ind + "", ""} });
			db.Order("random", "()");
			db.All();

			string[,] res = db.GetResult();

			Debug.Log("///////" + res[0,0]);
			string[] res_out = new string[res.Length];
			for(int i = 0; i < res.Length; i++) {
				res_out[i] = res[i,0];
			}

			if(res_out.Length > 0) return res_out;

			return null;
		}

		public AudioClip[] GetList(int ind) {
			string[] s = GetMusic(ind);

			if(s.Length > 0) {
				AudioClip[] clips = new AudioClip[s.Length];

				for(int i = 0; i < s.Length; i++) {
					clips[i] = Resources.Load(C.MUSIC + s[i], typeof(AudioClip)) as AudioClip;
				}

				return clips;
			}

			return null;
		}
	}

}