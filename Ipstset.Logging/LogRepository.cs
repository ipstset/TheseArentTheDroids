﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.Logging
{
    public class LogRepository : ILogRepository
    {
        private string _connection;

        public LogRepository(string connection)
        {
            _connection = connection;
        }

        public bool Save(LogEntry logEntry)
        {
            try
            {
                var con = new SqlConnection(_connection);
                var cmd = new SqlCommand("ipstset_SaveLogEntry", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LogDate", logEntry.LogDate));
                cmd.Parameters.Add(new SqlParameter("@DomainUserID", logEntry.DomainUserId));
                cmd.Parameters.Add(new SqlParameter("@Parameters", logEntry.ParametersJson));
                cmd.Parameters.Add(new SqlParameter("@LogEntryType", logEntry.Type.ToString()));
                cmd.Parameters.Add(new SqlParameter("@SessionID", logEntry.SessionId));

                var results = 0;
                using (con)
                {
                    con.Open();
                    results = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (results == 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
