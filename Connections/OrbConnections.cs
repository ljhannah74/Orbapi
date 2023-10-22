using System.Data.SQLite;

namespace Connections
{
    public class OrbConnections
    {
        static Dictionary<string, SqlConnectionInfo> SqlDataSources { get; set; }

        static OrbConnections()
        {
            SqlDataSources = new Dictionary<string, SqlConnectionInfo>();

            SqlDataSources["DEV"] = new SqlConnectionInfo
            {
                DataSource = "ORB_DATABASE.db"
            };
        }

        public static SQLiteConnection OrbDevConnection => GetSqlConnection("DEV");

        private static SQLiteConnection GetSqlConnection(string dbName, int? maxPoolSize = null)
        {
            var connInfo = SqlDataSources[dbName];

            return new SQLiteConnection($"Data Source={connInfo.DataSource};Version=3;FailIfMissing=True;");
        }
    }
}