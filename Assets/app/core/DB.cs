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
		private static string[,] _result;

		private static string _query;
		private static int selectCount;

		private static string path;
		//public Text text;

		private static SqliteConnection SetConnection() {
			//_connect = null;
			if(_connect == null) {
				path = Application.dataPath + DBConfig.DB_NAME;
				_connect = new SqliteConnection("Data Source=" + path);
				_command = new SqliteCommand(_connect);
				_connect.Open();

				if(_connect.State == ConnectionState.Open) {
					//Debug.Log("Connected DB");
				}
			} 

			return _connect;
		}

		public void Query() {
			SetConnection();

			Debug.Log("Query = \"" + _query + "\"");
			_command = new SqliteCommand(_query, _connect);
			//Debug.Log(_command);
			_reader = _command.ExecuteReader();
			//Debug.Log(_reader);

			//_connect.Close();
		}

		public void Select(string[] s) {
			_query = "select";

			selectCount = 0;
			foreach(string select in s) {
				_query += " " + select + ",";

				selectCount++;
			}

			_query = _query.Remove(_query.Length - 1);
			_query += " ";
		}

		public void Insert(string table) {
			_query = "insert into ";
			_query += table;
			_query += " ";
		}

		public void Update(string table) {
			_query = "update ";
			_query += table;
			_query += " ";
		}

		public void Set(string[,] s) {
			_query += " set ";

			for(int i = 0; i < s.GetLength(0); i++) {
				//_query += (s[i,2] == "text") ?  " '" + s[i,0] + "' like '" + s[i,1] + "'," : " " + s[i,0] + " = " + s[i,1] + " and";
				_query += " " + s[i,0] + " = '" + s[i,1] + "',";//this.smb_decode(s[i,2], s[i,0], s[i,1]) + " ,";
			}

			_query = _query.Remove(_query.Length - 1);
		}

		public void From(string table) {
			_query += " from " + table + " ";
		}

		public void Join(string join) {
			_query += " join " + join + " ";
		}

		public void Where(string[,] s) {
			_query += " where";

			for(int i = 0; i < s.GetLength(0); i++) {
				//_query += (s[i,2] == "text") ?  " '" + s[i,0] + "' like '" + s[i,1] + "'," : " " + s[i,0] + " = " + s[i,1] + " and";
				_query += this.smb_decode(s[i,2], s[i,0], s[i,1]) + " and";
			}

			_query = _query.Remove(_query.Length - 3);
		}

		public void Order(string order, string type) {
			_query += " order by " + order + " " + type + " ";
		}

		public void Group(string group) {
			_query += " group by " + group + " ";
		}

		public void Limit(string limit) {
			_query += " limit " + limit;
		}

		public void One() {
			//SetConnection();

			Limit("1");
			Query();

			//Debug.Log("Count of field = " + selectCount);

			_result = new string[1,selectCount];

			for(int i = 0; i < selectCount; i++) {
				_result[0,i] = _reader[i].ToString();
			}

			_connect.Close();
    		_command.Dispose();
    		_connect = null;
		}

		public string[,] GetResult() {
			return _result;
		}

		public void All() {
			//SetConnection();

			Query();

			Debug.Log(_reader);
			_result = new string[6,1];
			int i = 0;
			while(_reader.Read()) {
				//Debug.Log(_reader[0]);
				_result[i,0] = _reader[0].ToString();
				i++;
			}

			_connect.Close();
    		_command.Dispose();
    		_connect = null;
		}

		public void Go() {
			//SetConnection();
			Query();

			_connect.Close();
    		_command.Dispose();
    		_connect = null;
		}

		private string smb_decode(string smb, string field, string val) {
			switch(smb) {
				case "text": return " '" + field + "' like '" + val + "' ";
				case "not": return " " + field + " <> " + val + " ";
				default: return " " + field + " = " + val + " ";
			}
		}
	}

}