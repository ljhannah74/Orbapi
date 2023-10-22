using orbapi.DTOs;
using System.Data.SQLite;

namespace orbapi.DAL
{
    public class OrbIndexDAL
    {
        public static List<StateDTO> GetStates() 
        {
            string sql = "SELECT * FROM [State]";

            using(SQLiteConnection sqlConn = Connections.OrbConnections.OrbDevConnection)
            using(SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlConn))
            {
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Connection.Open();

                List<StateDTO> states = new List<StateDTO>();
                using(SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    while(reader.Read())
                    {
                        StateDTO state = new StateDTO
                        {
                            StateName = (string)reader["StateName"]
                        };

                        states.Add(state);
                    }
                
                sqlCmd.Connection.Close();

                return states;
            }
        }

        public static List<CountyDTO> GetCountiesByState(string stateName) 
        {
            string sql = "SELECT * FROM [County] ";
            sql += "INNER JOIN [State] ON [County].[StateId]=[State].[StateId] ";
            sql += "WHERE [State].[StateName]=@StateName";

            using(SQLiteConnection sqlConn = Connections.OrbConnections.OrbDevConnection)
            using(SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlConn))
            {
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@StateName", stateName);
                sqlCmd.Connection.Open();

                List<CountyDTO> counties = new List<CountyDTO>();
                using(SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    while(reader.Read())
                    {
                        CountyDTO county = new CountyDTO
                        {
                            CountyName = (string)reader["CountyName"]
                        };

                        counties.Add(county);
                    }
                
                sqlCmd.Connection.Close();

                return counties;
            }
        }

        public static List<OrbIndexDTO> GetOrbIndexes(string stateName, string countyName)
        {
            string sql = "SELECT * FROM [orbIndex] ";
            sql += "INNER JOIN [State] ON [County].[StateId]=[State].[StateId] ";
            sql += "INNER JOIN [County] ON [County].[CountyId]=[orbIndex].[CountyId] ";
            sql += "WHERE [State].[StateName]=@StateName ";
            sql += "AND [County].CountyName = @CountyName";

            using(SQLiteConnection sqlConn = Connections.OrbConnections.OrbDevConnection)
            using(SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlConn))
            {
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@StateName", stateName);
                sqlCmd.Parameters.AddWithValue("@CountyName", countyName);
                sqlCmd.Connection.Open();

                List<OrbIndexDTO> orbIndexes = new List<OrbIndexDTO>();
                using(SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    while(reader.Read())
                    {
                        OrbIndexDTO orbIndex = new OrbIndexDTO
                        {
                            StateName = (string)reader["StateName"],
                            CountyName = (string)reader["CountyName"],
                            IndexName = (string)reader["IndexName"],
                            Url = (string)reader["Url"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"]
                        };

                        orbIndexes.Add(orbIndex);
                    }
                
                sqlCmd.Connection.Close();

                return orbIndexes;
            }
        }

        public static List<LocalityDTO> GetLocalities(string stateName, string countyName) 
        {
            string sql = "SELECT * FROM [Locality] ";
            sql += "INNER JOIN [State] ON [County].[StateId]=[State].[StateId] ";
            sql += "INNER JOIN [County] ON [County].[CountyId]=[Locality].[CountyId] ";
            sql += "WHERE [State].[StateName]=@StateName ";
            sql += "AND [County].CountyName=@CountyName";

            using(SQLiteConnection sqlConn = Connections.OrbConnections.OrbDevConnection)
            using(SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlConn))
            {
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@StateName", stateName);
                sqlCmd.Parameters.AddWithValue("@CountyName", countyName);
                sqlCmd.Connection.Open();

                List<LocalityDTO> localities = new List<LocalityDTO>();
                using(SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    while(reader.Read())
                    {
                        LocalityDTO locality = new LocalityDTO
                        {
                            LocalityName = (string)reader["LocalityName"]
                        };

                        localities.Add(locality);
                    }
                
                sqlCmd.Connection.Close();

                return localities;
            }
        }
    }
}
