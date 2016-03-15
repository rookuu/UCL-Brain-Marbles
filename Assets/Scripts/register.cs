using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class register : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;

	public Text firstName;
	public Text email;
	public Text password;
	public Text passwordVerify;

	public void createAcc() {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn .Open();

		_cmd.Parameters.Add(new SqliteParameter ("@name", firstName.text));
		_cmd.Parameters.Add(new SqliteParameter ("@email", email.text));
		_cmd.Parameters.Add(new SqliteParameter ("@pass", password.text));

		_cmd.CommandText = "INSERT INTO `users` (firstname, email,  passwd) VALUES (@name, @email, @pass);";


		_cmd.ExecuteNonQuery();
	}
}
