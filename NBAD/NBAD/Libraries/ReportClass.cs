using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Libraries
{
   public class ReportClass
    {

        public DataTable DesigantionReport(string designation)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@designation", designation)
               
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_ReportDesignation",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }
        public DataTable BranchReport(string branch)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@branch", branch)
               
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_ReportBranch",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        public DataTable DepartmentReport(string department)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@department", department)
               
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_ReportDepartment",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        public DataTable LocationReport(string location)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@location", location)
               
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_ReportLocation",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        //public DataTable allSwipeReport(DateTime fromdate, DateTime todate,string empid)
        //{
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@DateFrom", fromdate)
        //        , new SqlParameter("@DateTo", todate)
        //        ,new SqlParameter("@employeeId", empid)
        //    };

        //    //Lets get the user
        //    using (
        //        DataTable table = DatabaseManager.ExecuteSelectCommand("allSwipeReportFinal_New",
        //            CommandType.StoredProcedure, parameters))

        //        //check if any record exist or not
        //        if (table.Rows.Count > 0)
        //        {
        //            return table;
        //        }
        //        else
        //            return null;
        //}

        public DataTable allSwipeReport(DateTime fromdate, DateTime todate,string bankId, string empid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom", fromdate)
                , new SqlParameter("@DateTo", todate)
                ,new SqlParameter("@Bankid", bankId)
                ,new SqlParameter("@employeeId", empid)
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("allSwipeReportFinal_New",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }


        public DataTable GetDetailsWithId(string empID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@employeeID", empID)
               
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("employeeSelectwithId",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }
        public DataTable GetDetailsWithIdManualEntry(string empID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@employeeID", empID)
               
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("employeeSelectManualEntrywithId",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        public DataTable manualEntryReport(DateTime fromdate, DateTime todate,string branchID,string empId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@DateFrom", fromdate)
                , new SqlParameter("@DateTo", todate),
                 new SqlParameter("@Bankid", branchID)
                 , new SqlParameter("@employeeID", empId)
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("ManualEntryReportFinal_New",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        public DataTable WorkedDayrReport(DateTime fromdate, DateTime todate, string empId,string bankID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Bankid", bankID)
                , new SqlParameter("@employeeID", empId)
                ,new SqlParameter("@DateFrom", fromdate)
                , new SqlParameter("@DateTo", todate)
                 
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("WorkedDayrReport_New",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }


        public DataTable punctualityReport(DateTime fromdate, string bankId,string empid)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Bankid", bankId)
                ,new SqlParameter("@employeeId", empid)
                ,new SqlParameter("@DateFrom", fromdate)
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("PunctualityrReport_Final",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }

        public DataTable LogReport(DateTime fromdate, DateTime todate)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@fromDate", fromdate)
                , new SqlParameter("@toDate", todate)
                
            };

            //Lets get the user
            using (
                DataTable table = DatabaseManager.ExecuteSelectCommand("usp_LogReport",
                    CommandType.StoredProcedure, parameters))

                //check if any record exist or not
                if (table.Rows.Count > 0)
                {
                    return table;
                }
                else
                    return null;
        }
    }
}
