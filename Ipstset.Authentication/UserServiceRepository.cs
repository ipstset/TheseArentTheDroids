using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Ipstset.Authentication
{
    public class UserServiceRepository: IUserServiceRepository
    {
        private string _connection;

        public UserServiceRepository(string connection)
        {
            _connection = connection;
        }

        public int SaveUserIdentity(UserIdentity userIdentity)
        {
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("ipstset_InsertUserIdentity", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdentityToken", userIdentity.IdentityToken));
                cmd.Parameters.Add(new SqlParameter("@SessionID", userIdentity.SessionId));
                cmd.Parameters.Add(new SqlParameter("@DomainUserID", userIdentity.DomainUserId));
                cmd.Parameters.Add(new SqlParameter("@TraceToken", userIdentity.TraceToken));
                cmd.Parameters.Add(new SqlParameter("@DateCreated", userIdentity.DateCreated));
                cmd.Parameters.Add(new SqlParameter("@DateExpired", userIdentity.DateExpired));
                cmd.Parameters.Add(new SqlParameter("@UserData", userIdentity.UserDataJson));

                var id = 0;
                using (con)
                {
                    con.Open();
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

                return id;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public UserIdentity GetUserIdentity(string identityToken)
        {
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("ipstset_GetUserIdentity", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@IdentityToken", identityToken));

                var identity = new UserIdentity();
                using (con)
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        identity.Id = Convert.ToInt32(reader["UserIdentityID"]);
                        identity.IdentityToken = reader["IdentityToken"].ToString();
                        identity.SessionId = reader["SessionID"].ToString();
                        identity.TraceToken = reader["TraceToken"].ToString();
                        identity.DomainUserId = Convert.ToInt32(reader["DomainUserID"]);
                        identity.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                        identity.DateExpired = Convert.ToDateTime(reader["DateExpired"]);
                        var json = reader["UserData"].ToString();
                        if(!String.IsNullOrEmpty(json))
                         identity.UserData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string,string>>(json);
                    }
                    con.Close();
                }

                return identity;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
