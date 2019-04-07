using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.UI;
using DB_config;

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
				path = Application.dataPath + DBConfig.DB_NAME;
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

			_command = new SqliteCommand(_query, _connect);
			_reader = _command.ExecuteReader();
		}

		public static void Select(string[] s) {
			_query = "select";

			foreach(string select in s) {
				_query += " " + select + ",";
			}

			_query = _query.Remove(_query.Length - 1);
			_query += " ";
		}

		public static void Select() {
			_query = "select * ";
		}

		public static void From(string table) {
			_query += " from " + table + " ";
		}

		public static void Where(string[,] s) {
			_query += " where";

			for(int i = 0; i < s.GetLength(0); i++) {

				//Debug.Log(s[i,0] + " = " + s[i,1]);

				_query += " " + s[i,0] + " = " + s[i,1] + ",";
			}

			_query = _query.Remove(_query.Length - 1);
		}

		public static void Order(string order) {
			_query += " order by " + order + " ";
		}

		public static void Group(string group) {
			_query += " group by " + group + " ";
		}

		public static void Limit(string limit) {
			_query += " limit " + limit;
		}

		public static void One() {
			SetConnection();

			Query();

			_result = new string[1];

			Debug.Log(_reader[0]);
		}

		public static void All() {
			SetConnection();

			Query();

			Debug.Log(_query);

			//while(_reader.Read()) {
				//Debug.Log(_reader[1]);
			//}
		}
		
	}

}