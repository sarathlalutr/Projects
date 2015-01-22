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
    }
}
