using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Libraries
{
   public static class DatabaseManager
    {
        private static readonly string CONNECTION_STRING =
             ConfigurationManager.ConnectionStrings["NBADConnString"].ToString();

        #region Stored Procedure

        // This function will be used to execute R(CRUD) operation of parameterless commands
        internal static DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType)
        {
            DataTable table = null;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["NBADConnString"].ToString()))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            table = new DataTable();
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

        // This function will be used to execute R(CRUD) operation of parameterized commands
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

        // This function will be used to execute R(CRUD) operation of parameterized commands
        internal static DataSet ExecuteSelectCommand(string CommandName, CommandType cmdType, SqlParameter[] param, string dataTableName)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("PageCount");

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
                            da.Fill(ds, dataTableName);
                            dt.Columns.Add("PageCount");
                            dt.Rows.Add();
                            dt.Rows[0][0] = cmd.Parameters["@PageCount"].Value;
                            ds.Tables.Add(dt);
                        }
                    }
                    catch
                    {
                        con.Close();
                        throw;
                    }
                }
            }

            return ds;
        }

        // This function will be used to execute CUD(CRUD) operation of parameterized commands
        internal static bool ExecuteNonQuery(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int status = 0;

            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        status = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        con.Close();
                        throw;
                    }
                }
            }

            return (status > 0);
        }

        internal static bool ExecuteNonQuery(string CommandName, CommandType cmdType)
        {
            int status = 0;

            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    //cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        status = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        con.Close();
                        throw;
                    }
                }
            }

            return (status > 0);
        }


        internal static int ExecuteNonQuery1(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int status = 0;

            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        status = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        con.Close();
                        status = ex.Number;
                        //throw;
                    }
                }
            }

            return status;
        }
#endregion

    }
}
