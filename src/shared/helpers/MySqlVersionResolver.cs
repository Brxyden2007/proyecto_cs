using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace proyecto_cs;
public class MySqlVersionResolver
{
    public static Version DetectVersion(string connectionString)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var raw = conn.ServerVersion;
        var clean = raw.Split('-')[0];
        return Version.Parse(clean);
    }
}
