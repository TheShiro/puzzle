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
				path = Application.dataPath + "/pzl.bytes";
				_connect = new SqliteConnection("Data Source=" + path);
				_command = new SqliteCommand(_connect);
				_connect.Open();

				if(_connect.State == ConnectionState.Open) {
					Debug.Log("Connected");
				}
			} 

			return _connect;
		}

		public void Query() {
			SetConnection();

			Debug.Log(_query);
			_command = new SqliteCommand(_query, _connect);
			//Debug.Log(_command);
			_reader = _command.ExecuteReader();
			//Debug.Log(_reader);
		}

		public void Select(string[] s) {
			_query = "select";

			foreach(string select in s) {
				_query += " " + select + ",";
			}

			_query = _query.Remove(_query.Length - 1);
			_query += " ";
		}

		public void Select() {
			_query = "Select * ";
		}

		public void Select(string sql) {
			_query = sql;
		}

		public void From(string table) {
			_query += " from " + table + " ";
		}

		public void Where(string[,] s) {
			_query += " where";

			for(int i = 0; i < s.GetLength(0); i++) {
				string smb = (s[i,2] == "text") ? "like" : "=";
				_query += " '" + s[i,0] + "' " + smb + " '" + s[i,1] + "',";
			}

			_query = _query.Remove(_query.Length - 1);
		}

		public void Order(string order) {
			_query += " order by " + order + " ";
		}

		public void Group(string group) {
			_query += " group by " + group + " ";
		}

		public void Limit(string limit) {
			_query += " limit " + limit;
		}

		public void One() {
			SetConnection();

			Limit("1");
			Query();

			while(_reader.Read()) {
				//Debug.Log(_reader[]);
				if(_reader[6] == null) {
					Debug.Log("is out");
				}
			}
		}

		public void All() {
			SetConnection();

			Query();

			//Debug.Log(_reader);

			while(_reader.Read()) {
				Debug.Log(_reader[1]);
			}
		}

		private int GetLengthReader() {
			//ищем в запросе колечество запятых между select и from
			//к этому количеству прибавляем 1 - это и будет количество столбцов
			return 0;
		}
	}

}