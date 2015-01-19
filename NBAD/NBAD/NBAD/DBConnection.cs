using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NBAD
{
    public class DBConnection
    {
        public static SqlConnection con = null;

        public DBConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NBADConnString"].ToString();
                con = new SqlConnection(connectionString);
                con.Open();
            }
            catch (Exception ex)
            {
            }
        }


        internal DataTable insertTODB(System.Data.DataTable dt)
        {
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            SqlParameter[] parameters =
            {
                new SqlParameter("@EmployeeImportList", dt) {SqlDbType = SqlDbType.Structured}
            };

            using (
                DataTable table = DBConnection.ExecuteSelectCommand("usp_ImportEmployeeToDb",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        internal static DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType, SqlParameter[] param)
        {
            var table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["NBADConnString"].ToString()))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(param);
                    cmd.CommandTimeout = 0;
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                    catch
                    {
                        con.Close();
                        throw;
                    }
                }
            }

            return table;
        }
    }
}