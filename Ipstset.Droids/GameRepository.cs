using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dapper;

namespace Ipstset.Droids
{
    public class GameRepository
    {
        private string _connection;
        public GameRepository(string connection)
        {
            _connection = connection;
        }
        //get last game result
        public GameResult GetGameResult(string gameId)
        {
            GameResult result = null;



            return result;
        }
        //jsonconvert.serializeobject(result)
        public GameResult GetLastGame(string userId)
        {
            GameResult result = null;
            var sql = "Select top 1 JsonText from tblGameResult where PlayerId = @userId order by GameResultId desc";
            var json = "";
            using (var sqlConnection = new SqlConnection(_connection))
            {
                sqlConnection.Open();
                json = sqlConnection.Query<string>(sql, new { userId }).FirstOrDefault();
                sqlConnection.Close();
            }

            //convert json string to GameResult
            if(!string.IsNullOrEmpty(json))
                result = JsonConvert.DeserializeObject<GameResult>(json);

            return result;
        }

        public List<CardInfo> GetCardInfos()
        {
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("select * from tblCardInfo", con);
                cmd.CommandType = System.Data.CommandType.Text;

                var cards = new List<CardInfo>();
                using (con)
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new CardInfo
                        {
                            CardInfoId = Convert.ToInt32(reader["CardInfoId"]),
                            Name = reader["Name"].ToString(),
                            Image = reader["Image"].ToString()
                        });
                    }
                    con.Close();
                }

                return cards;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //insert game result
        public bool InsertGameResult(GameResult gameResult)
        {
            var sql = "Insert into tblGameResult (GameId,PlayerId,JsonText,DateCreated) " +
                      "values (@gameId,@playerId,@json,GETDATE())";

            using (var sqlConnection = new SqlConnection(_connection))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.Execute(sql, new
                    {
                        gameId = gameResult.Id,
                        playerId = gameResult.Player.Id,
                        json = JsonConvert.SerializeObject(gameResult),
                        date = DateTime.Now
                    });

                    sqlConnection.Close();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }

        
    }
}
