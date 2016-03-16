using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;

public class loadLevelData : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;
	IDataReader _reader;

	public GameObject circle;
	public GameObject triangle;
	public GameObject tear;
	public GameObject oval;

	public Sprite circlepure;
	public Sprite circleblue;

	int stageid = 1, levelid;

	void Awake() {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();

		_cmd.Parameters.Add(new SqliteParameter ("@stageid", stageid));
		_cmd.CommandText = "SELECT * FROM `stages` WHERE `stageid`=@stageid";
		_reader = _cmd.ExecuteReader ();

		if (_reader.Read ()) {
			levelid = (int)_reader ["levelid"];
		
			_cmd.Parameters.Add (new SqliteParameter ("@levelid", levelid));
			_cmd.CommandText = "SELECT * FROM `levels` WHERE `levelid`=@levelid";
			_reader = _cmd.ExecuteReader ();

			if (_reader.Read ()) {
				levelController levelData = GameObject.Find ("levelController").GetComponent<levelController> ();
				levelData.scoreTarget = (int)_reader ["scoretarget"];
				levelData.timeLimit = (int)_reader ["timelimit"];
				levelData.badClickScore = (int)_reader ["negativeclick"];

				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.marblesOnScreen = (int)_reader ["marblesonscreen"];
				marbleData.uniqueMarbles = (int)_reader ["uniquemarbles"];

				if (marbleData.uniqueMarbles == 1) {
					int id1 = (int)_reader ["marble1"];
					_conn.Close ();

					loadMarbleData (id1, 1);
				} else if (marbleData.uniqueMarbles == 2) {
					int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"];
					_conn.Close ();

					loadMarbleData (id1, 1);
					loadMarbleData (id2, 2);
				} else if (marbleData.uniqueMarbles == 3) {
					int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"];
					_conn.Close ();

					loadMarbleData (id1, 1);
					loadMarbleData (id2, 2);
					loadMarbleData (id3, 3);
				} else if (marbleData.uniqueMarbles == 4) {
					int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"], id4 = (int)_reader ["marble4"];
					_conn.Close ();

					loadMarbleData (id1, 1);
					loadMarbleData (id2, 2);
					loadMarbleData (id3, 3);
					loadMarbleData (id4, 4);
				} else if (marbleData.uniqueMarbles == 5) {
					int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"], id4 = (int)_reader ["marble4"], id5 = (int)_reader["marble5"];
					_conn.Close ();

					loadMarbleData (id1, 1);
					loadMarbleData (id2, 2);
					loadMarbleData (id3, 3);
					loadMarbleData (id4, 4);
					loadMarbleData (id5, 5);
				} else if (marbleData.uniqueMarbles == 6) {
					int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"], id4 = (int)_reader ["marble4"], id5 = (int)_reader["marble5"], id6 = (int)_reader["marble6"];
					_conn.Close ();

					loadMarbleData (id1, 1);
					loadMarbleData (id2, 2);
					loadMarbleData (id3, 3);
					loadMarbleData (id4, 4);
					loadMarbleData (id5, 5);
					loadMarbleData (id6, 6);
				}
			}
		}
	}

	void loadMarbleData(int marbleid, int slot) {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();
		_cmd.Parameters.Add (new SqliteParameter ("@marbleid", marbleid));
		_cmd.CommandText = "SELECT * FROM `marbles` WHERE `marbleid`=@marbleid";
		_reader = _cmd.ExecuteReader ();

		if (_reader.Read ()) {
			if (slot == 1) {
				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.Marble1 = findShape ((string)_reader ["shape"]);
				marbleData.sprite1 = findSprite ((string)_reader ["sprite"]);
				marbleData.fake1 = findFake ((string)_reader ["fake"]);
				marbleData.speed1 = (int)_reader ["speed"];
				marbleData.movementScript1 = (string)_reader ["script"];
				marbleData.size1 = (int)_reader ["size"];
				marbleData.rotation1 = (int)_reader ["rotation"];
				marbleData.relativeChance1 = (int)_reader ["relchance"];
				marbleData.scoreChange1 = (int)_reader ["score"];
			} else if (slot == 2) {
				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.Marble2 = findShape ((string)_reader ["shape"]);
				marbleData.sprite2 = findSprite ((string)_reader ["sprite"]);
				marbleData.fake2 = findFake ((string)_reader ["fake"]);
				marbleData.speed2 = (int)_reader ["speed"];
				marbleData.movementScript2 = (string)_reader ["script"];
				marbleData.size2 = (int)_reader ["size"];
				marbleData.rotation2 = (int)_reader ["rotation"];
				marbleData.relativeChance2 = (int)_reader ["relchance"];
				marbleData.scoreChange2 = (int)_reader ["score"];
			} else if (slot == 3) {
				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.Marble3 = findShape ((string)_reader ["shape"]);
				marbleData.sprite3 = findSprite ((string)_reader ["sprite"]);
				marbleData.fake3 = findFake ((string)_reader ["fake"]);
				marbleData.speed3 = (int)_reader ["speed"];
				marbleData.movementScript3 = (string)_reader ["script"];
				marbleData.size3 = (int)_reader ["size"];
				marbleData.rotation3 = (int)_reader ["rotation"];
				marbleData.relativeChance3 = (int)_reader ["relchance"];
				marbleData.scoreChange3 = (int)_reader ["score"];
			} else if (slot == 4) {
				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.Marble4 = findShape ((string)_reader ["shape"]);
				marbleData.sprite4 = findSprite ((string)_reader ["sprite"]);
				marbleData.fake4 = findFake ((string)_reader ["fake"]);
				marbleData.speed4 = (int)_reader ["speed"];
				marbleData.movementScript4 = (string)_reader ["script"];
				marbleData.size4 = (int)_reader ["size"];
				marbleData.rotation4 = (int)_reader ["rotation"];
				marbleData.relativeChance4 = (int)_reader ["relchance"];
				marbleData.scoreChange4 = (int)_reader ["score"];
			} else if (slot == 5) {
				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.Marble5 = findShape ((string)_reader ["shape"]);
				marbleData.sprite5 = findSprite ((string)_reader ["sprite"]);
				marbleData.fake5 = findFake ((string)_reader ["fake"]);
				marbleData.speed5 = (int)_reader ["speed"];
				marbleData.movementScript5 = (string)_reader ["script"];
				marbleData.size5 = (int)_reader ["size"];
				marbleData.rotation5 = (int)_reader ["rotation"];
				marbleData.relativeChance5 = (int)_reader ["relchance"];
				marbleData.scoreChange5 = (int)_reader ["score"];
			} else if (slot == 6) {
				marbleController marbleData = GameObject.Find ("marbleController").GetComponent<marbleController> ();
				marbleData.Marble6 = findShape ((string)_reader ["shape"]);
				marbleData.sprite6 = findSprite ((string)_reader ["sprite"]);
				marbleData.fake6 = findFake ((string)_reader ["fake"]);
				marbleData.speed6 = (int)_reader ["speed"];
				marbleData.movementScript6 = (string)_reader ["script"];
				marbleData.size6 = (int)_reader ["size"];
				marbleData.rotation6 = (int)_reader ["rotation"];
				marbleData.relativeChance6 = (int)_reader ["relchance"];
				marbleData.scoreChange6 = (int)_reader ["score"];
			}
		}
	}

	GameObject findShape (string shape) {
		if (shape == "circle") {
			return circle;
		} else if (shape == "triangle") {
			return triangle;
		} else if (shape == "tear") {
			return tear;
		} else {
			return oval;
		}
	}

	Sprite findSprite (string sprite) {
		if (sprite == "circlepure") {
			return circlepure;
		} else if (sprite == "circleblue") {
			return circleblue;
		}

		return circlepure; //temp
	}

	bool findFake (string fake) {
		if (fake == "yes") {
			return true;
		} else {
			return false;
		}
	}
}
