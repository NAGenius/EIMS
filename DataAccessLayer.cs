using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace EIMS
{
    internal class DataAccessLayer
    {
        static string connstr = ConfigurationManager.AppSettings["DBconn"];

        public static int ExecuteNonQuery(string query)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }

        public static DataTable ExecuteQuery(string query)
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        // 调用存储函数
        public static MySqlParameter CallStoredFunction(string functionName, MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure; // 存储函数也使用 StoredProcedure 类型
                    cmd.CommandText = functionName;

                    // 添加参数
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    // 添加返回值参数
                    MySqlParameter returnValue = new MySqlParameter("@ReturnValue", MySqlDbType.Int32);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // 执行存储函数
                    cmd.ExecuteNonQuery();

                    // 获取返回值
                    return returnValue;
                }
            }
        }
    }
}
