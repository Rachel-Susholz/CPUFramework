﻿using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CPUFramework

{
    public class SQLUtility
    {
        public static string ConnectionString = "";
        
        public static DataTable GetDataTable(string sqlstatement)
        {
            DataTable dt = new();
            SqlConnection conn = new();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            SqlCommand cmd = new();
            cmd.Connection = conn;
            cmd.CommandText = sqlstatement;
            var dr = cmd.ExecuteReader();
            dt.Load(dr);
            SetAllColumnsAllowNull(dt);
            return dt;
        }
        public static void ExecuteSQL(string sqlstatrment)
        {
            GetDataTable(sqlstatrment);
        }
        private static void SetAllColumnsAllowNull(DataTable dt)
        {
            foreach(DataColumn c in dt.Columns)
            {
                c.AllowDBNull = true;
            } 
        }

        /*public static void DebugPrintDataTable(DataTable dt)
        {
            foreach(DataRow r in dt.Rows)
            {
                foreach(DataColumn c in dt.Columns)
                {
                    Debug.Print(c.ColumnName + " = " + r[c.ColumnName].ToString());
                } 
            }
        }
        */
    }
}
