using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.UI;

namespace DB {

	public class DBCore {

		private static SqliteConnection _connect;
		private static SqliteCommand _command;
		private static SqliteDataReader _reader;
		private static string[] _result;

		private static string _query;

		private static string path;
		//public Text text;

		private static SqliteConnection SetConnection() {
			if(_connect == null) {
				path = Application.dataPath + "/pzl.bytes.db";
				_connect = new SqliteConnection("URI=file:" + path);
				_connect.Open();

				if(_connect.State == ConnectionState.Open) {
					Debug.Log("Connected");
				}
			} 

			return _connect;
		}

		public static void Query() {
			SetConnection();

			/*_command = new SqliteCommand(query, _connect);
			_reader = _command.ExecuteReader();*/

			//_query = query;
		}

		public static void Select(string[] s) {
			_query = "select ";

			foreach(string select in s) {
				_query += select + ", ";
			}

			_query = _query.Remove(_query.Length - 2);
			_query += " ";
		}

		public static void Select() {
			_query = "select * ";
		}

		public static void From(string table) {
			_query += " from " + table + " ";
		}

		public static void Where(string[,] s) {
			_query += " where ";
		}

		public static void One() {
			SetConnection();

			_result = new string[1];

			Debug.Log(_reader[0]);
		}

		public static void All() {
			SetConnection();

			Debug.Log(_query);

			//while(_reader.Read()) {
				//Debug.Log(_reader[1]);
			//}
		}
		
	}

}