using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Deals with register the user to the database, creates the record and adds it.
 * Also deals with validation.
 */

public class register : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;

	public Dropdown dob_day;
	public Dropdown dob_month;
	public InputField dob_year;
	public Dropdown gender;
	public Dropdown gamesTime;
	public Dropdown gamesType;

	private string dob;

	public GameObject infobox;
	public Text message;

	public void createAcc() {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn .Open();

		int dobYearParse = 0;
		int.TryParse (dob_year.text, out dobYearParse);

		if (dob_year.text.Length == 4 && dobYearParse >1900 && dobYearParse <= System.DateTime.Now.Year) {
			int dDay = dob_day.value;
			int dMonth = dob_month.value;
			dob = (dDay+1).ToString() + '-' + (dMonth+1).ToString() + "-" + dob_year.text;

			globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();

			string salt = BCrypt.GenerateSalt ();
			string hashedPass = BCrypt.HashPassword (data.userPass + "r~BV2$J", salt);

			_cmd.Parameters.Add (new SqliteParameter ("@name", data.userName));
			_cmd.Parameters.Add (new SqliteParameter ("@last", data.userLast));
			_cmd.Parameters.Add (new SqliteParameter ("@email", data.userEmail));
			_cmd.Parameters.Add (new SqliteParameter ("@pass", hashedPass));
			_cmd.Parameters.Add (new SqliteParameter ("@dob", dob));
			_cmd.Parameters.Add (new SqliteParameter ("@gender", gender.value));
			_cmd.Parameters.Add (new SqliteParameter ("@gamesTime", gamesTime.value));
			_cmd.Parameters.Add (new SqliteParameter ("@gamesType", gamesType.value));

			_cmd.CommandText = "INSERT INTO `users` (firstname, lastname, email,  passwd, dob, gender, gamesTime, gamesType, tutorial) VALUES (@name, @last, @email, @pass, @dob, @gender, @gamesTime, @gamesType, 'uncompleted');";


			_cmd.ExecuteNonQuery ();
			_conn.Close ();

			data.loggedIn = false;
			data.userID = 0;
			data.userEmail = null;
			data.userName = null;
			data.userPass = null;
			data.saveData ();
			SceneManager.LoadScene (3);
		} else {
			displayMessage ("Error: Date of Birth Invalid, please check and try again.");
			_conn.Close ();
		}
	}

	private void displayMessage(string msg) {
		infobox.SetActive (true);
		message.text = msg;
	}
}
