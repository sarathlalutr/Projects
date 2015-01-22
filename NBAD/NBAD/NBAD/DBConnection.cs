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

        internal DataTable getDesignationAll()
        {
            var allData = new DataTable();
            try
            {
                var cmd = new SqlCommand("usp_DesignationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }

        internal DataTable getBranchAll()
        {
            var allData = new DataTable();
            try
            {
                var cmd = new SqlCommand("usp_BranchSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }

        internal DataTable getDepartmentAll()
        {
            var allData = new DataTable();
            try
            {
                var cmd = new SqlCommand("usp_DepartmentSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }

        internal DataTable getLocationAll()
        {
            var allData = new DataTable();
            try
            {
                var cmd = new SqlCommand("usp_LocationSelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }

        internal int InsertBranch(string branchId, string branchName)
        {
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_tblBranchInsert";
            cmd.Parameters.Add("@BranchID", branchId);
            cmd.Parameters.Add("@BranchName", branchName);
            cmd.Connection = con;
            int rs = cmd.ExecuteNonQuery();

            con.Close();
            return rs;
        }

        internal DataTable GetAllDetails(string procedureName)
        {
            var allData = new DataTable();
            //SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            try
            {
                var cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }

        internal DataTable GetDetailsWithId(string id,string procedureName,string parameter)
        {
            var allData = new DataTable();
            SqlParameter param;
            //SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            try
            {
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                param = new SqlParameter(parameter, id);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;

                cmd.Parameters.Add(param);
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }

        internal void updateBranch(string branchId, string branchName, string branchEntryId)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("usp_tblBranchUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BranchEntryId", SqlDbType.Int).Value = branchEntryId;
            cmd.Parameters.Add("@BranchID", branchId);
            cmd.Parameters.Add("@BranchName", branchName);
         
            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        internal string deletewithId(string BranchEntryId,string procedureName,string parameter)
        {
            string res = "";
            try
            {
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(parameter, SqlDbType.Int).Value = BranchEntryId;
                res = "" + cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                con.Close();
                res = ex.Number.ToString();
            }
            return res;
        }

        internal object GetSearchDetails(string procedureName, string parameter, string searchString)
        {
            var allData = new DataTable();
            SqlParameter param;
            //SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            try
            {
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var cmd = new SqlCommand(procedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                param = new SqlParameter(parameter, searchString);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;

                cmd.Parameters.Add(param);
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return allData;
        }
    }
}