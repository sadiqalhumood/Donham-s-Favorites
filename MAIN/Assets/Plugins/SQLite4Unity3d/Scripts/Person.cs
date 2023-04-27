using SQLite4Unity3d;
using TMPro;

public class Person  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Person: Id={0}, Name={1},  Surname={2}]", Id, Username, Password);
	}
}
