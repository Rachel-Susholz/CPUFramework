using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CPUFramework

{
    public class SQLUtility
    {
        public static string ConnectionString = "";

        public static SqlCommand GetSqlCommand(string sprocname)
        {
            SqlCommand cmd;
            using (SqlConnection conn = new(SQLUtility.ConnectionString))
            {
                cmd = new(sprocname, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
            }
            return cmd;
        }

        public static DataTable GetDataTable(SqlCommand cmd)
        {
            Debug.Print("-------" + Environment.NewLine + cmd.CommandText);
            DataTable dt = new();
            using (SqlConnection conn = new(SQLUtility.ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            SetAllColumnsAllowNull(dt);
            return dt;
        }
        
        public static DataTable GetDataTable(string sqlstatement)
        {
            return GetDataTable(new SqlCommand(sqlstatement));
        }
        public static DataTable ExecuteSQL(string sqlstatrment)
        {
            
            return GetDataTable(sqlstatrment);
            
        }
        private static void SetAllColumnsAllowNull(DataTable dt)
        {
            foreach(DataColumn c in dt.Columns)
            {
                c.AllowDBNull = true;
            } 
        }

        public static void DebugPrintDataTable(DataTable dt)
        {
            foreach(DataRow r in dt.Rows)
            {
                foreach(DataColumn c in dt.Columns)
                {
                    Debug.Print(c.ColumnName + " = " + r[c.ColumnName].ToString());
                } 
            }
        }
        
    }
}
