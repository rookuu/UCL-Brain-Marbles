using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class adminPanel : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;
	IDataReader _reader;

	int numberOfStages = 0;
	public int currentLevel;

	public GameObject infobox;
	public Text infotext;

	int uniqueMarbles;
	public int[] marbleids;
	public int slotBeingEdited = -1;

	public Button saveMarbleBtn, saveLevelBtn;

	public Dropdown leveldropdown;
	public InputField scoreTarget, timeLimit, missScore, marblesOnScreen;

	public GameObject horizLayout, marbleUI;
	public GameObject levelpanel, marblepanel;

	public Dropdown shape, sprite, script;
	public Toggle fake;
	public InputField min, max, speed, size, rot, chance, score;
	public Image marble;

	public GameObject circle;
	public GameObject triangle;
	public GameObject tear;
	public GameObject oval;

	public Sprite circlepure;
	public Sprite circleblue;
	public Sprite circlegreen;
	public Sprite circlered;

	public Sprite ovalpure;
	public Sprite ovalblue;
	public Sprite ovalgreen;
	public Sprite ovalred;

	public Sprite tearpure;
	public Sprite tearblue;
	public Sprite teargreen;
	public Sprite tearred;

	public Sprite trianglepure;
	public Sprite triangleblue;
	public Sprite trianglegreen;
	public Sprite trianglered;

	[Space(10)]
	[Header("Marble 1")]
	public GameObject Marble1;
	public Sprite sprite1;
	public bool fake1;
	public int speed1;
	public string movementScript1;
	public int maxNodes1;
	public int minNodes1;
	public float size1;
	public int rotation1;
	public int relativeChance1;
	public int scoreChange1;

	[Space(5)]
	[Header("Marble 2")]
	public GameObject Marble2;
	public Sprite sprite2;
	public bool fake2;
	public int speed2;
	public string movementScript2;
	public int maxNodes2;
	public int minNodes2;
	public float size2;
	public int rotation2;
	public int relativeChance2;
	public int scoreChange2;

	[Space(5)]
	[Header("Marble 3")]
	public GameObject Marble3;
	public Sprite sprite3;
	public bool fake3;
	public int speed3;
	public string movementScript3;
	public int maxNodes3;
	public int minNodes3;
	public float size3;
	public int rotation3;
	public int relativeChance3;
	public int scoreChange3;

	[Space(5)]
	[Header("Marble 4")]
	public GameObject Marble4;
	public Sprite sprite4;
	public bool fake4;
	public int speed4;
	public string movementScript4;
	public int maxNodes4;
	public int minNodes4;
	public float size4;
	public int rotation4;
	public int relativeChance4;
	public int scoreChange4;

	[Space(5)]
	[Header("Marble 5")]
	public GameObject Marble5;
	public Sprite sprite5;
	public bool fake5;
	public int speed5;
	public string movementScript5;
	public int maxNodes5;
	public int minNodes5;
	public float size5;
	public int rotation5;
	public int relativeChance5;
	public int scoreChange5;

	[Space(5)]
	[Header("Marble 6")]
	public GameObject Marble6;
	public Sprite sprite6;
	public bool fake6;
	public int speed6;
	public string movementScript6;
	public int maxNodes6;
	public int minNodes6;
	public float size6;
	public int rotation6;
	public int relativeChance6;
	public int scoreChange6;

	// Use this for initialization
	void Start () {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();

		_cmd.CommandText = "SELECT * FROM `stages`;";
		_reader = _cmd.ExecuteReader ();

		while (_reader.Read ()) {
			numberOfStages++;

			leveldropdown.options.Add (new Dropdown.OptionData() {text="Level " + _reader["stageid"]});
		}

		_conn.Close ();

		marbleids = new int[6];

		saveMarbleBtn.onClick.AddListener (delegate {
			saveMarble(slotBeingEdited);	
		});


		leveldropdown.value = -1;	
		leveldropdown.onValueChanged.AddListener (delegate {
			currentLevel = leveldropdown.value + 1;
			loadLevelParameters();
		});

		leveldropdown.value = 0;
		currentLevel = 1;
		loadLevelParameters ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadLevelParameters() {
		_conn = new SqliteConnection (_dbName);
		_cmd = _conn.CreateCommand ();
		_conn.Open ();

		_cmd.Parameters.Add(new SqliteParameter ("@stageid", currentLevel));

		_cmd.CommandText = "SELECT `levelid` FROM `stages` WHERE `stageid`=@stageid;";
		_reader = _cmd.ExecuteReader ();
		_reader.Read ();

		int levelid = (int)_reader ["levelid"];

		_cmd.Parameters.Add (new SqliteParameter ("@levelid", levelid));
		_cmd.CommandText = "SELECT * FROM `levels` WHERE `levelid`=@levelid;";

		_reader = _cmd.ExecuteReader ();
		_reader.Read ();

		scoreTarget.text = _reader ["scoretarget"].ToString();
		timeLimit.text = _reader ["timelimit"].ToString();
		missScore.text = _reader ["negativeclick"].ToString();
		marblesOnScreen.text = _reader ["marblesonscreen"].ToString();

		uniqueMarbles = (int)_reader ["uniquemarbles"];

		if (  uniqueMarbles == 1) {
			int id1 = (int)_reader ["marble1"];
			_conn.Close ();

			loadMarbleData (id1, 1);
		} else if (  uniqueMarbles == 2) {
			int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"];
			_conn.Close ();

			loadMarbleData (id1, 1);
			loadMarbleData (id2, 2);
		} else if (  uniqueMarbles == 3) {
			int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"];
			_conn.Close ();

			loadMarbleData (id1, 1);
			loadMarbleData (id2, 2);
			loadMarbleData (id3, 3);
		} else if (  uniqueMarbles == 4) {
			int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"], id4 = (int)_reader ["marble4"];
			_conn.Close ();

			loadMarbleData (id1, 1);
			loadMarbleData (id2, 2);
			loadMarbleData (id3, 3);
			loadMarbleData (id4, 4);
		} else if (  uniqueMarbles == 5) {
			int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"], id4 = (int)_reader ["marble4"], id5 = (int)_reader["marble5"];
			_conn.Close ();

			loadMarbleData (id1, 1);
			loadMarbleData (id2, 2);
			loadMarbleData (id3, 3);
			loadMarbleData (id4, 4);
			loadMarbleData (id5, 5);
		} else if (  uniqueMarbles == 6) {
			int id1 = (int)_reader ["marble1"], id2 = (int)_reader ["marble2"], id3 = (int)_reader ["marble3"], id4 = (int)_reader ["marble4"], id5 = (int)_reader["marble5"], id6 = (int)_reader["marble6"];
			_conn.Close ();

			loadMarbleData (id1, 1);
			loadMarbleData (id2, 2);
			loadMarbleData (id3, 3);
			loadMarbleData (id4, 4);
			loadMarbleData (id5, 5);
			loadMarbleData (id6, 6);
		}
		rebuildLayout ();
	}

	public void Save() {
		_conn = new SqliteConnection (_dbName);
		_cmd = _conn.CreateCommand ();
		_conn.Open ();

		//New Level, Assign Stage to Level
		//SELECT * FROM levels ORDER BY levelid DESC LIMIT 1;
		_cmd.Parameters.Clear();
		_cmd.Parameters.Add (new SqliteParameter ("@scoreTarget", scoreTarget.text));
		_cmd.Parameters.Add (new SqliteParameter ("@timeLimit", timeLimit.text));
		_cmd.Parameters.Add (new SqliteParameter ("@missClick", missScore.text));
		_cmd.Parameters.Add (new SqliteParameter ("@marblesOnScreen", marblesOnScreen.text));
		_cmd.Parameters.Add (new SqliteParameter ("@uniqueMarbles", uniqueMarbles));
		_cmd.Parameters.Add (new SqliteParameter ("@1", checkMarbleIds(0)));
		_cmd.Parameters.Add (new SqliteParameter ("@2", checkMarbleIds(1)));
		_cmd.Parameters.Add (new SqliteParameter ("@3", checkMarbleIds(2)));
		_cmd.Parameters.Add (new SqliteParameter ("@4", checkMarbleIds(3)));
		_cmd.Parameters.Add (new SqliteParameter ("@5", checkMarbleIds(4)));
		_cmd.Parameters.Add (new SqliteParameter ("@6", checkMarbleIds(5)));

		_cmd.CommandText = "INSERT INTO `levels` (scoretarget, timelimit, negativeclick, marblesonscreen, uniquemarbles, marble1, marble2, marble3, marble4, marble5, marble6) VALUES (@scoreTarget, @timeLimit, @missClick, @marblesOnScreen, @uniqueMarbles, @1, @2, @3, @4, @5, @6);";
		_cmd.ExecuteNonQuery ();
	
		_cmd.CommandText = "SELECT * FROM levels ORDER BY levelid DESC LIMIT 1;";
		_reader = _cmd.ExecuteReader ();
		_reader.Read ();

		int newLevelId = (int) _reader ["levelid"];

		_cmd.Parameters.Add (new SqliteParameter ("@stageid", currentLevel));
		_cmd.Parameters.Add (new SqliteParameter ("@levelid", newLevelId));

		_cmd.CommandText = "UPDATE `stages` SET levelid=@levelid WHERE stageid=@stageid;";
		_cmd.ExecuteNonQuery ();

		loadLevelParameters ();

		_conn.Close ();

		infobox.SetActive (true);
		infotext.text = "Level Saved Successfully.";
	}

	void loadMarbleData(int marbleid, int slot) {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();
		_cmd.Parameters.Add (new SqliteParameter ("@marbleid", marbleid));
		_cmd.CommandText = "SELECT * FROM `marbles` WHERE `marbleid`=@marbleid";
		_reader = _cmd.ExecuteReader ();

		marbleids [slot - 1] = marbleid;

		if (_reader.Read ()) {
			if (slot == 1) {
				  Marble1 = findShape ((string)_reader ["shape"]);
				  sprite1 = findSprite ((string)_reader ["sprite"]);
				  fake1 = findFake ((string)_reader ["fake"]);
				  speed1 = (int)_reader ["speed"];
				  movementScript1 = (string)_reader ["script"];
				  maxNodes1 = (int)_reader ["maxNodes"];
				  minNodes1 = (int)_reader ["minNodes"];
				  size1 = (int)_reader ["size"];
				  rotation1 = (int)_reader ["rotation"];
				  relativeChance1 = (int)_reader ["relchance"];
				  scoreChange1 = (int)_reader ["score"];
			} else if (slot == 2) {
				  Marble2 = findShape ((string)_reader ["shape"]);
				  sprite2 = findSprite ((string)_reader ["sprite"]);
				  fake2 = findFake ((string)_reader ["fake"]);
				  speed2 = (int)_reader ["speed"];
				  movementScript2 = (string)_reader ["script"];
				  maxNodes2 = (int)_reader ["maxNodes"];
				  minNodes2 = (int)_reader ["minNodes"];
				  size2 = (int)_reader ["size"];
				  rotation2 = (int)_reader ["rotation"];
				  relativeChance2 = (int)_reader ["relchance"];
				  scoreChange2 = (int)_reader ["score"];
			} else if (slot == 3) {
				  Marble3 = findShape ((string)_reader ["shape"]);
				  sprite3 = findSprite ((string)_reader ["sprite"]);
				  fake3 = findFake ((string)_reader ["fake"]);
				  speed3 = (int)_reader ["speed"];
				  movementScript3 = (string)_reader ["script"];
				  maxNodes3 = (int)_reader ["maxNodes"];
				  minNodes3 = (int)_reader ["minNodes"];
				  size3 = (int)_reader ["size"];
				  rotation3 = (int)_reader ["rotation"];
				  relativeChance3 = (int)_reader ["relchance"];
				  scoreChange3 = (int)_reader ["score"];
			} else if (slot == 4) {
				  Marble4 = findShape ((string)_reader ["shape"]);
				  sprite4 = findSprite ((string)_reader ["sprite"]);
				  fake4 = findFake ((string)_reader ["fake"]);
				  speed4 = (int)_reader ["speed"];
				  movementScript4 = (string)_reader ["script"];
				  maxNodes4 = (int)_reader ["maxNodes"];
				  minNodes4 = (int)_reader ["minNodes"];
				  size4 = (int)_reader ["size"];
				  rotation4 = (int)_reader ["rotation"];
				  relativeChance4 = (int)_reader ["relchance"];
				  scoreChange4 = (int)_reader ["score"];
			} else if (slot == 5) {
				  Marble5 = findShape ((string)_reader ["shape"]);
				  sprite5 = findSprite ((string)_reader ["sprite"]);
				  fake5 = findFake ((string)_reader ["fake"]);
				  speed5 = (int)_reader ["speed"];
				  movementScript5 = (string)_reader ["script"];
				  maxNodes5 = (int)_reader ["maxNodes"];
				  minNodes5 = (int)_reader ["minNodes"];
				  size5 = (int)_reader ["size"];
				  rotation5 = (int)_reader ["rotation"];
				  relativeChance5 = (int)_reader ["relchance"];
				  scoreChange5 = (int)_reader ["score"];
			} else if (slot == 6) {
				  Marble6 = findShape ((string)_reader ["shape"]);
				  sprite6 = findSprite ((string)_reader ["sprite"]);
				  fake6 = findFake ((string)_reader ["fake"]);
				  speed6 = (int)_reader ["speed"];
				  movementScript6 = (string)_reader ["script"];
				  maxNodes6 = (int)_reader ["maxNodes"];
				  minNodes6 = (int)_reader ["minNodes"];
				  size6 = (int)_reader ["size"];
				  rotation6 = (int)_reader ["rotation"];
				  relativeChance6 = (int)_reader ["relchance"];
				  scoreChange6 = (int)_reader ["score"];
			}

			_conn.Close ();
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
		} else if (sprite == "circlegreen") {
			return circlegreen;
		} else if (sprite == "circlered") {
			return circlered;
		} else if (sprite == "ovalpure") {
			return ovalpure;
		} else if (sprite == "ovalblue") {
			return ovalblue;
		} else if (sprite == "ovalgreen") {
			return ovalgreen;
		} else if (sprite == "ovalred") {
			return ovalred;
		} else if (sprite == "tearpure") {
			return tearpure;
		} else if (sprite == "tearblue") {
			return tearblue;
		} else if (sprite == "teargreen") {
			return teargreen;
		} else if (sprite == "tearred") {
			return tearred;
		} else if (sprite == "trianglepure") {
			return trianglepure;
		} else if (sprite == "triangleblue") {
			return triangleblue;
		} else if (sprite == "trianglegreen") {
			return trianglegreen;
		} else if (sprite == "trianglered") {
			return trianglered;
		}

		// More Sprites need to be added here!

		return circlepure; //FailSafe;
	}

	bool findFake (string fake) {
		if (fake == "yes") {
			return true;
		} else {
			return false;
		}
	}

	Color statusColour(bool isFake) {
		if (isFake == true) {
			return Color.red;
		} else {
			return Color.green;
		}
	}

	public void displayMarbleData (int id) {
		levelpanel.SetActive (false);
		marblepanel.SetActive (true);

		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();
		_cmd.Parameters.Add (new SqliteParameter ("@marbleid", id));
		_cmd.CommandText = "SELECT * FROM `marbles` WHERE `marbleid`=@marbleid";
		_reader = _cmd.ExecuteReader ();
		_reader.Read ();

		shape.captionText.text = _reader ["shape"].ToString();
		sprite.captionText.text = _reader ["sprite"].ToString();
		marble.sprite = findSprite ((string)_reader ["sprite"]);


		if (findFake ((string)_reader ["fake"])) {
			fake.isOn = true;
		} else {
			fake.isOn = false;
		}

		speed.text = _reader ["speed"].ToString();
		script.captionText.text  = _reader ["script"].ToString();
		max.text = _reader ["maxNodes"].ToString();
		min.text = _reader ["minNodes"].ToString();
		size.text = _reader ["size"].ToString();
		rot.text = _reader ["rotation"].ToString();
		chance.text = _reader ["relchance"].ToString();
		score.text = _reader ["score"].ToString();
	}

	void rebuildLayout() {
		uniqueMarbles = 0;
		for (int i = 0; i < marbleids.Length; i++) {
			if (marbleids [i] != 0) {
				uniqueMarbles++;
			}
		}

		GameObject newUIMarble;

		foreach (Transform child in horizLayout.transform) {
			GameObject.Destroy (child.gameObject);
		}

		if (uniqueMarbles >= 1) {
			marbleUI.GetComponent<Image> ().color = statusColour (fake1);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = sprite1;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);
			newUIMarble.GetComponent<Button> ().onClick.AddListener (delegate {
				openMarbleEdit(1);
			});
		} 
		if (uniqueMarbles >= 2) {
			marbleUI.GetComponent<Image> ().color = statusColour (fake2);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = sprite2;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);
			newUIMarble.GetComponent<Button> ().onClick.AddListener (delegate {
				openMarbleEdit(2);
			});
		} 
		if (uniqueMarbles >= 3) {
			marbleUI.GetComponent<Image> ().color = statusColour (fake3);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = sprite3;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);
			newUIMarble.GetComponent<Button> ().onClick.AddListener (delegate {
				openMarbleEdit(3);
			});
		}
		if (uniqueMarbles >= 4) {
			marbleUI.GetComponent<Image> ().color = statusColour (fake4);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = sprite4;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);
			newUIMarble.GetComponent<Button> ().onClick.AddListener (delegate {
				openMarbleEdit(4);
			});
		}
		if (uniqueMarbles >= 5) {
			marbleUI.GetComponent<Image> ().color = statusColour (fake5);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = sprite5;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);
			newUIMarble.GetComponent<Button> ().onClick.AddListener (delegate {
				openMarbleEdit(5);
			});
		}
		if (uniqueMarbles >= 6) {
			marbleUI.GetComponent<Image> ().color = statusColour (fake6);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = sprite6;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);
			newUIMarble.GetComponent<Button> ().onClick.AddListener (delegate {
				openMarbleEdit(6);
			});
		}

		LayoutRebuilder.MarkLayoutForRebuild (horizLayout.transform as RectTransform);
	}

	void saveMarble(int slot) {
		// HERE
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();

		_cmd.Parameters.Add (new SqliteParameter ("@shape", shape.captionText.text));
		_cmd.Parameters.Add (new SqliteParameter ("@sprite", sprite.captionText.text));

		string fakeStatus;

		if (fake.isOn) {
			fakeStatus = "yes";
		} else {
			fakeStatus = "no";
		}

		_cmd.Parameters.Add (new SqliteParameter ("@fake", fakeStatus));
		_cmd.Parameters.Add (new SqliteParameter ("@script", script.captionText.text));
		_cmd.Parameters.Add (new SqliteParameter ("@max", max.text));
		_cmd.Parameters.Add (new SqliteParameter ("@min", min.text));
		_cmd.Parameters.Add (new SqliteParameter ("@speed", speed.text));
		_cmd.Parameters.Add (new SqliteParameter ("@size", size.text));
		_cmd.Parameters.Add (new SqliteParameter ("@rot", rot.text));
		_cmd.Parameters.Add (new SqliteParameter ("@chance", chance.text));
		_cmd.Parameters.Add (new SqliteParameter ("@score", score.text));

		_cmd.CommandText = "INSERT INTO `marbles` (shape, sprite,  fake, script, maxNodes, minNodes, speed, size, rotation, relchance, score) VALUES (@shape, @sprite, @fake, @script, @max, @min, @speed, @size, @rot, @chance, @score);";
		_cmd.ExecuteNonQuery ();


		_cmd.CommandText = "SELECT * FROM `marbles` ORDER BY marbleid DESC LIMIT 1;";
		_reader = _cmd.ExecuteReader ();
		_reader.Read ();

		marbleids [slot - 1] = (int)_reader ["marbleid"];

		if (slot == 1) {
			Marble1 = findShape ((string)_reader ["shape"]);
			sprite1 = findSprite ((string)_reader ["sprite"]);
			fake1 = findFake ((string)_reader ["fake"]);
			speed1 = (int)_reader ["speed"];
			movementScript1 = (string)_reader ["script"];
			maxNodes1 = (int)_reader ["maxNodes"];
			minNodes1 = (int)_reader ["minNodes"];
			size1 = (int)_reader ["size"];
			rotation1 = (int)_reader ["rotation"];
			relativeChance1 = (int)_reader ["relchance"];
			scoreChange1 = (int)_reader ["score"];
		} else if (slot == 2) {
			Marble2 = findShape ((string)_reader ["shape"]);
			sprite2 = findSprite ((string)_reader ["sprite"]);
			fake2 = findFake ((string)_reader ["fake"]);
			speed2 = (int)_reader ["speed"];
			movementScript2 = (string)_reader ["script"];
			maxNodes2 = (int)_reader ["maxNodes"];
			minNodes2 = (int)_reader ["minNodes"];
			size2 = (int)_reader ["size"];
			rotation2 = (int)_reader ["rotation"];
			relativeChance2 = (int)_reader ["relchance"];
			scoreChange2 = (int)_reader ["score"];
		} else if (slot == 3) {
			Marble3 = findShape ((string)_reader ["shape"]);
			sprite3 = findSprite ((string)_reader ["sprite"]);
			fake3 = findFake ((string)_reader ["fake"]);
			speed3 = (int)_reader ["speed"];
			movementScript3 = (string)_reader ["script"];
			maxNodes3 = (int)_reader ["maxNodes"];
			minNodes3 = (int)_reader ["minNodes"];
			size3 = (int)_reader ["size"];
			rotation3 = (int)_reader ["rotation"];
			relativeChance3 = (int)_reader ["relchance"];
			scoreChange3 = (int)_reader ["score"];
		} else if (slot == 4) {
			Marble4 = findShape ((string)_reader ["shape"]);
			sprite4 = findSprite ((string)_reader ["sprite"]);
			fake4 = findFake ((string)_reader ["fake"]);
			speed4 = (int)_reader ["speed"];
			movementScript4 = (string)_reader ["script"];
			maxNodes4 = (int)_reader ["maxNodes"];
			minNodes4 = (int)_reader ["minNodes"];
			size4 = (int)_reader ["size"];
			rotation4 = (int)_reader ["rotation"];
			relativeChance4 = (int)_reader ["relchance"];
			scoreChange4 = (int)_reader ["score"];
		} else if (slot == 5) {
			Marble5 = findShape ((string)_reader ["shape"]);
			sprite5 = findSprite ((string)_reader ["sprite"]);
			fake5 = findFake ((string)_reader ["fake"]);
			speed5 = (int)_reader ["speed"];
			movementScript5 = (string)_reader ["script"];
			maxNodes5 = (int)_reader ["maxNodes"];
			minNodes5 = (int)_reader ["minNodes"];
			size5 = (int)_reader ["size"];
			rotation5 = (int)_reader ["rotation"];
			relativeChance5 = (int)_reader ["relchance"];
			scoreChange5 = (int)_reader ["score"];
		} else if (slot == 6) {
			Marble6 = findShape ((string)_reader ["shape"]);
			sprite6 = findSprite ((string)_reader ["sprite"]);
			fake6 = findFake ((string)_reader ["fake"]);
			speed6 = (int)_reader ["speed"];
			movementScript6 = (string)_reader ["script"];
			maxNodes6 = (int)_reader ["maxNodes"];
			minNodes6 = (int)_reader ["minNodes"];
			size6 = (int)_reader ["size"];
			rotation6 = (int)_reader ["rotation"];
			relativeChance6 = (int)_reader ["relchance"];
			scoreChange6 = (int)_reader ["score"];
		}

		rebuildLayout ();
		marblepanel.SetActive (false);
		levelpanel.SetActive (true);
	}

	void openMarbleEdit(int slot) {
		displayMarbleData (marbleids [slot - 1]);
		slotBeingEdited = slot;
	}

	public void discardMarbleEdit() {
		marblepanel.SetActive (false);
		levelpanel.SetActive (true);
	}

	public void updatePic() {
		marble.sprite = findSprite (sprite.captionText.text);
	}

	string checkMarbleIds(int index) {
		if (marbleids [index] == 0) {
			return null;
		} else {
			return marbleids[index].ToString();
		}
	}

	public void NewMarble() {
		slotBeingEdited = horizLayout.transform.childCount + 1;
		levelpanel.SetActive (false);
		marblepanel.SetActive (true);
	}

	public void deleteMarble() {
		marbleids [slotBeingEdited - 1] = 0;

		System.Array.Sort (marbleids);
		System.Array.Reverse(marbleids);
		rebuildLayout ();

		marblepanel.SetActive (false);
		levelpanel.SetActive (true);

	}
}