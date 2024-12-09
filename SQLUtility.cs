﻿using System.Data;
using System.Data.SqlClient;

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
            return dt;
        }
    }
}
