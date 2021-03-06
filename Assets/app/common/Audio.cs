﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

namespace Main {

	public class Audio : MonoBehaviour {

		public AudioSource audio;
		public AudioClip[] clip;
		public int i = 0;
		public int collection = 0;

		void Start() {
			AudioModel model = new AudioModel();
			clip = model.GetList(collection);
		}
		
		void Update () {
			if(!audio.isPlaying) {
				if(clip[i] != null) {
					audio.clip = clip[i];
					audio.Play();
				}
				i++;
				if(i > clip.Length) {
					i = 0;
				}
			}
		}
	}

}