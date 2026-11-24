using MySql.Data.MySqlClient;

namespace akademikusok;

public class Adatbazis
{
    private readonly string connectionstring;

    public Adatbazis()
    {
        this.connectionstring = "server=localhost;database=akademikusok;user=root;password=";
    }

    public List<Tag> Tagok { get; } = new List<Tag>();

    public List<Tagsag> Tagsagok { get; } = new List<Tagsag>();

    public void Feltolt()
    {
        Dictionary<int, Tag> index = new Dictionary<int, Tag>();

        try
        {
            using (MySqlConnection conn = new MySqlConnection(this.connectionstring))
            {
                conn.Open();
                string query = "SELECT * FROM tag";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            Tag t = new Tag(
                                id,
                                reader.GetString("nev"),
                                reader.GetString("nem"),
                                Egesz(reader, "szuletett"),
                                Egesz(reader, "elhunyt"),
                                Szoveg(reader, "identitas")
                            );
                            Tagok.Add(t);
                            index[id] = t;
                        }
                    }
                }

                query = "SELECT * FROM tagsag";
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tagsag ts = new Tagsag(
                                reader.GetInt32("id"),
                                index[reader.GetInt32("tagid")],
                                reader.GetString("tipus"),
                                reader.GetInt32("ev")
                            );
                            Tagsagok.Add(ts);
                        }
                    }
                }
            }
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            Environment.Exit(0);
        }
        catch (Exception)
        {
            Console.WriteLine();
            Environment.Exit(0);
        }
    }

    private int? Egesz(MySqlDataReader reader, string oszlop)
    {
        int i = reader.GetOrdinal(oszlop);
        return reader.IsDBNull(i) ? null : reader.GetInt32(i);
    }

    private string Szoveg(MySqlDataReader reader, string oszlop)
    {
        int i = reader.GetOrdinal(oszlop);
        return reader.IsDBNull(i) ? "" : reader.GetString(i);
    }
}
