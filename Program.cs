using Npgsql;

class Program
{
    public static void Main()
    {
        string cs = "Server=localhost;Port=5432;Database=testdb;User Id=appperfect";
        using (NpgsqlConnection conn = new NpgsqlConnection(cs))
        {
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            Console.WriteLine("Connected");

            String character = Console.ReadLine();
            switch (character)
            {
                case "r":
                    ReadTable(cmd);
                    break;
                case "i":
                    InsertValues(cmd);
                    break;
                case "u":
                    UpdateValues(cmd);
                    break;
                case "d":
                    DeleteValues(cmd);
                    break;
            }
        }
    }

    static void ReadTable(NpgsqlCommand cmd)
    {
        var data = new List<string>();
        cmd.CommandText = @"SELECT * FROM users ORDER BY id ASC";
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var rowData = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    rowData += reader[i].ToString() + " ";
                }
                data.Add(rowData);
            }
        }
        Console.WriteLine(string.Join("\n", data));
    }
    static void InsertValues(NpgsqlCommand cmd)
    {
        cmd.CommandText = "INSERT INTO users (name, email, age) VALUES ('Virat','virat@appperfect.com',23);";
        cmd.ExecuteNonQuery();
        Console.WriteLine("Inserted values");
    }

    static void UpdateValues(NpgsqlCommand cmd)
    {
        cmd.CommandText = "UPDATE users SET age = 55 WHERE name='John'";
        cmd.ExecuteNonQuery();
    }

    static void DeleteValues(NpgsqlCommand cmd)
    {
        cmd.CommandText = "DELETE from users WHERE name='John'";
        cmd.ExecuteNonQuery();
    }
}