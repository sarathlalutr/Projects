using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Libraries;

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
                    catch(Exception ex)
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

        //internal int InsertBranch(string branchId, string branchName)
        //{
        //    if (con.State.Equals(ConnectionState.Closed))
        //    {
        //        con.Open();
        //    }
        //    var cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "usp_tblBranchInsert";
        //    cmd.Parameters.Add("@BranchID", branchId);
        //    cmd.Parameters.Add("@BranchName", branchName);
        //    cmd.Connection = con;
        //    int rs = cmd.ExecuteNonQuery();

        //    con.Close();
        //    return rs;
        //}
        internal int InsertBranch(string branchId, string branchName,string inTime,string outTime,string userId,DateTime dtInfo)
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
            cmd.Parameters.Add("@scheduleInTime", inTime);
            cmd.Parameters.Add("@scheduleOutTime", outTime);
            cmd.Parameters.Add("@UserId", userId);
            cmd.Parameters.Add("@dtInfo", dtInfo);
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

        //internal void updateBranch(string branchId, string branchName, string branchEntryId)
        //{
        //    string status = "";
        //    if (con.State.Equals(ConnectionState.Closed))
        //    {
        //        con.Open();
        //    }
        //    var cmd = new SqlCommand("usp_tblBranchUpdate", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@BranchEntryId", SqlDbType.Int).Value = branchEntryId;
        //    cmd.Parameters.Add("@BranchID", branchId);
        //    cmd.Parameters.Add("@BranchName", branchName);
         
        //    //con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
        internal void updateBranch(string branchId, string branchName,string intime,string outTime, string branchEntryId)
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
            cmd.Parameters.Add("@scheduleInTime", intime);
            cmd.Parameters.Add("@scheduleOutTime", outTime);
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

        internal int InsertDepartment(string departmentName, string userId, DateTime dtInfo)
        {
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_tblDepartmentInsert";

            cmd.Parameters.Add("@DepartmentName", departmentName);
            cmd.Parameters.Add("@UserId", userId);
            cmd.Parameters.Add("@dtInfo", dtInfo);
            cmd.Connection = con;
            int rs = cmd.ExecuteNonQuery();

            con.Close();
            return rs;
        }

        internal void updateDepartment(string departmentName, string departmentID)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("usp_tblDepartmentUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = departmentID;

            cmd.Parameters.Add("@DepartmentName", departmentName);

            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        internal int InsertDesignation(string designation, string userId, DateTime dtInfo)
        {
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_tblDesignationInsert";

            cmd.Parameters.Add("@DesignationName", designation);

            cmd.Parameters.Add("@UserId", userId);
            cmd.Parameters.Add("@dtInfo", dtInfo);
            cmd.Connection = con;
            int rs = cmd.ExecuteNonQuery();

            con.Close();
            return rs;
        }

        internal void updateDesignation(string designation, string designationID)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("usp_tblDesignationUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DesignationId", SqlDbType.Int).Value = designationID;

            cmd.Parameters.Add("@DesignationName", designation);

            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        internal int InsertLocation(string location ,string userId,DateTime dtInfo)
        {
            int rs=0;
            try
            {
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_tblLocationInsert";

                cmd.Parameters.Add("@LocationName", location);
                     cmd.Parameters.Add("@UserId", userId);
            cmd.Parameters.Add("@dtInfo", dtInfo);
                cmd.Connection = con;
                 rs = cmd.ExecuteNonQuery();

                con.Close();
               
            }
            catch (Exception)
            {
                
            }
            return rs;
        }

        internal void updateLocation(string location, string LocationID)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("usp_tblLocationUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationID;

            cmd.Parameters.Add("@LocationName", location);

            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        internal int InsertDescription(string description, string userId, DateTime dtInfo)
        {
            int rs = 0;
            try
            {
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_tblDescriptionInsert";

                cmd.Parameters.Add("@Description", description);
                cmd.Parameters.Add("@UserId", userId);
                cmd.Parameters.Add("@dtInfo", dtInfo);
                cmd.Connection = con;
                rs = cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception)
            {

            }
            return rs;
        }

        internal void updateDescription(string description, string descriptionID)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("tblDescriptionUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DescriptionId", SqlDbType.Int).Value = descriptionID;

            cmd.Parameters.Add("@Description", description);

            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //internal int insertDetails(string empid, string empname, string gender, string desigantion,
        //    string description, string branch, string department, string swipeIntime, string swipeInLocation, string swipeOutTime, 
        //    string swipeOutLocation)
        //{
        //    if (con.State.Equals(ConnectionState.Closed))
        //    {
        //        con.Open();
        //    }
        //    var cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "usp_tblAccessInsertNew";
        //    cmd.Parameters.Add("@EmployeeId", empid);
        //    cmd.Parameters.Add("@EmployeeName", empname);
        //    cmd.Parameters.Add("@Gender", gender);
        //    cmd.Parameters.Add("@Designation", desigantion);
        //    cmd.Parameters.Add("@Description", description);
        //    cmd.Parameters.Add("@Branch", branch);
        //    cmd.Parameters.Add("@Department", department);
        //    cmd.Parameters.Add("@SwipeInLocation", swipeInLocation);
        //    cmd.Parameters.Add("@SwipeInTime", swipeIntime);
        //    cmd.Parameters.Add("@SwipeOutTime", swipeOutTime);
        //    cmd.Parameters.Add("@SwipeOutLocation", swipeOutLocation);
        //    cmd.Connection = con;
        //    int rs = cmd.ExecuteNonQuery();

        //    con.Close();
        //    return rs;
        //}
        internal int insertDetails(string empid, string empname, string gender, string desigantion,
            string description, string branch, string department, string swipeIntime, string swipeInLocation,string readerType,string approvedby,string aprovedDate,string userId,DateTime dtInfo)
        {
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_tblAccessInsertNew";
            cmd.Parameters.Add("@EmployeeId", empid);
            cmd.Parameters.Add("@EmployeeName", empname);
            cmd.Parameters.Add("@Gender", gender);
            cmd.Parameters.Add("@Designation", desigantion);
            cmd.Parameters.Add("@Description", description);
            cmd.Parameters.Add("@Branch", branch);
            cmd.Parameters.Add("@Department", department);
            cmd.Parameters.Add("@SwipeInLocation", swipeInLocation);
            cmd.Parameters.Add("@SwipeInTime", swipeIntime);
            cmd.Parameters.Add("@ReaderType", readerType);
            cmd.Parameters.Add("@AprovedBy", approvedby);
            cmd.Parameters.Add("@AprovedDate", aprovedDate);
            	
	       cmd.Parameters.Add("@UserId", userId);
            cmd.Parameters.Add("@dtInfo", dtInfo);
            cmd.Connection = con;
            int rs = cmd.ExecuteNonQuery();

            con.Close();
            return rs;
        }
        internal DateTime covertDateTime(string dateIn)
        {
            string NewFormat = "";
            var date = new DateTime();
            try
            {
                //string[] date1 = dateIn.Split('/');
                //NewFormat = date1[1] + "/" + date1[0] + "/" + date1[2];
                date = DateTime.ParseExact(dateIn.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
            }
            return date;
        }
        internal  DateTime ConvertDate(string dateIn)
        {
            string NewFormat = "";
            var date = new DateTime();
            try
            {
                //string[] date1 = dateIn.Split('/');
                //NewFormat = date1[1] + "/" + date1[0] + "/" + date1[2];
                date = DateTime.ParseExact(dateIn.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
            }
            return date;
        }
        internal int InsertBranchTiming(string branchId, string sunIn, string sunOut, string monIn, string monOut, string tueIn, string tueOut,
            string wedIn, string wedOut, string thursIn, string thursOut, string friIn, string friOut, string satIn, string satOut,string userId,DateTime dtInfo)
        {
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_tblBranchTimingInsert";
            cmd.Parameters.Add("@BranchEntryID", branchId);
            cmd.Parameters.Add("@sunIn", sunIn);
            cmd.Parameters.Add("@sunOut", sunOut);
            cmd.Parameters.Add("@monIn", monIn);
            cmd.Parameters.Add("@monOut", monOut);
            cmd.Parameters.Add("@tuesIn", tueIn);
            cmd.Parameters.Add("@tuesOut", tueOut);
            cmd.Parameters.Add("@wedIn", wedIn);
            cmd.Parameters.Add("@wedOut", wedOut);
            cmd.Parameters.Add("@thursIn", thursIn);
            cmd.Parameters.Add("@thursOut", thursOut);
            cmd.Parameters.Add("@friIn", friIn);
            cmd.Parameters.Add("@friOut", friOut);
            cmd.Parameters.Add("@satIn", satIn);
            cmd.Parameters.Add("@satOut", satOut);
            cmd.Parameters.Add("@UserId", userId);
            cmd.Parameters.Add("@dtInfo", dtInfo);
            
            cmd.Connection = con;
            int rs = cmd.ExecuteNonQuery();

            con.Close();
            return rs;
        }
        internal void updateBranchTiming(string branchId, string sunIn, string sunOut, string monIn, string monOut, string tueIn, string tueOut,
            string wedIn, string wedOut, string thursIn, string thursOut, string friIn, string friOut, string satIn, string satOut,string branchTimingEntryId)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("usp_tblBranchTimingUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BranchTimeEntryID", SqlDbType.Int).Value = branchTimingEntryId;
            //cmd.Parameters.Add("@BranchEntryID", branchId);
            //cmd.Parameters.Add("@BranchID", branchId);
            cmd.Parameters.Add("@SundayIn", sunIn);
            cmd.Parameters.Add("@SundayOut", sunOut);
            cmd.Parameters.Add("@MondayIn", monIn);
            cmd.Parameters.Add("@MondayOut", monOut);
            cmd.Parameters.Add("@TuesdayIn", tueIn);
            cmd.Parameters.Add("@TuesdayOut", tueOut);
            cmd.Parameters.Add("@WednesdayIn", wedIn);
            cmd.Parameters.Add("@WednesdayOut", wedOut);
            cmd.Parameters.Add("@ThursdayIn", thursIn);
            cmd.Parameters.Add("@ThursdayOut", thursOut);
            cmd.Parameters.Add("@FridayIn", friIn);
            cmd.Parameters.Add("@FridayOut", friOut);
            cmd.Parameters.Add("@SaturdayIn", satIn);
            cmd.Parameters.Add("@SaturdayOut", satOut);
            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        internal void updateDetails(string empid, string empname, string gender, string desigantion,
            string description, string branch, string department, string swipeIntime, string swipeInLocation, string readerType, string approvedby, string aprovedDate,string NBADId)
        {
            string status = "";
            if (con.State.Equals(ConnectionState.Closed))
            {
                con.Open();
            }
            var cmd = new SqlCommand("usp_tblAccess_ManualEntryUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@NBADId", SqlDbType.Int).Value = NBADId;
            cmd.Parameters.Add("@EmployeeId", empid);
            cmd.Parameters.Add("@EmployeeName", empname);
            cmd.Parameters.Add("@Gender", gender);
            cmd.Parameters.Add("@Designation", desigantion);
            cmd.Parameters.Add("@Description", description);
            cmd.Parameters.Add("@Branch", branch);
            cmd.Parameters.Add("@Department", department);
            cmd.Parameters.Add("@Location", swipeInLocation);
            cmd.Parameters.Add("@AccessTime", swipeIntime);
            cmd.Parameters.Add("@ReaderType", readerType);
            cmd.Parameters.Add("@ApprovedBy", approvedby);
            cmd.Parameters.Add("@ApprovedDate", aprovedDate);
         
            //con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



        internal void insertLog(string action, string item, string userId, DateTime dateTime)
        {
            int rs = 0;
            try
            {
                if (con.State.Equals(ConnectionState.Closed))
                {
                    con.Open();
                }
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_tblLogInsert";
                cmd.Parameters.Add("@Action", action);
                cmd.Parameters.Add("@Item", item);
                cmd.Parameters.Add("@UserId", userId);
                cmd.Parameters.Add("@EntryDate", dateTime);
                cmd.Connection = con;
                rs = cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception)
            {

            }
            
        }
    }
}