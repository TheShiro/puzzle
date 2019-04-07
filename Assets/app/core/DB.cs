using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.UI;

namespace DB {

	public class DB : MonoBehaviour {

		private static SqliteConnection _connect;
		private static SqliteCommand _command;
		private static SqliteDataReader _reader;
		private static string[] _result;

		private string path;
		//public Text text;

		public void SetConnection() {
			path = Application.dataPath + "/pzl.bytes";
			_connect = new SqliteConnection("URI=file:" + path);
			_connect.Open();

			if(_connect.State == ConnectionState.Open) {
				print("Connected");
			}
		}

		public void Query(string query) {
			_command = new SqliteCommand(query, _connect);
			_reader = _command.ExecuteReader();
		}

		public void One() {
			//_result
		}
		
	}

}