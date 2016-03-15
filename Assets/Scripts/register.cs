using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

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

	public void createAcc() {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn .Open();

		dob_day.value += 1;
		dob_month.value += 1;
		dob = dob_day.value.ToString() + '-' + dob_month.value.ToString() + "-" + dob_year.text;

		globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();

		_cmd.Parameters.Add(new SqliteParameter ("@name", data.userName));
		_cmd.Parameters.Add(new SqliteParameter ("@email", data.userEmail));
		_cmd.Parameters.Add(new SqliteParameter ("@pass", data.userPass));
		_cmd.Parameters.Add(new SqliteParameter ("@dob", dob));
		_cmd.Parameters.Add(new SqliteParameter ("@gender", gender.value));
		_cmd.Parameters.Add(new SqliteParameter ("@gamesTime", gamesTime.value));
		_cmd.Parameters.Add(new SqliteParameter ("@gamesType", gamesType.value));

		_cmd.CommandText = "INSERT INTO `users` (firstname, email,  passwd, dob, gender, gamesTime, gamesType) VALUES (@name, @email, @pass, @dob, @gender, @gamesTime, @gamesType);";


		_cmd.ExecuteNonQuery();
	}
}
