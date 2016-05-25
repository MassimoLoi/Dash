using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dash
{
    internal class MsSqlHelper : IDisposable
    {
        public SqlConnection SqlConn;

        private SqlDataAdapter sqlAdp;

        public DataTable Data { get; set; }

        public string DataBase { get; set; }

        public List<string> FieldList { get; set; }

        public string PassWord { get; set; }

        public SqlDataReader Reader { get; set; }

        public string Server { get; set; }

        public string UserName { get; set; }

        private string connectionstring;

        public MsSqlHelper()
        {
            connectionstring = ConfigurationManager.ConnectionStrings["dashConnectionString"].ToString();
        }

        public void Dispose()
        {
            if (Data != null)
                Data.Dispose();

            if (sqlAdp != null)
                sqlAdp.Dispose();

            SqlConn.Close();
            SqlConn.Dispose();
        }

        public void ExecuteCommand(string command,
            string server = "",
            string database = "",
            string username = "",
            string password = "")
        {
            if (server != "")
                this.Server = server;

            if (database != "")
                this.DataBase = database;

            if (username != "")
                this.UserName = username;

            if (password != "")
                this.PassWord = password;

            try
            {
                if (OpenDataConnection())
                {
                    using (SqlCommand sqlCmd = new SqlCommand(command))
                    {
                        sqlCmd.Connection = SqlConn;
                        sqlCmd.CommandTimeout = 30000;
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTableSchema(string tableName)
        {
            DataTable schema = null;

            try
            {
                if (OpenDataConnection())
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select top 1 * from " + tableName))
                    {
                        sqlCmd.Connection = SqlConn;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        schema = reader.GetSchemaTable();

                        this.FieldList = new List<string>();

                        for (int i = 0; i < schema.Rows.Count; i++)
                        {
                            this.FieldList.Add(schema.Rows[i]["ColumnName"].ToString());
                        }

                        reader.Close();
                        return schema;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return schema;
        }

        public bool isTablePresent(string tableName)
        {
            string execString = "IF OBJECT_ID('dbo." + tableName + "', 'U') IS NOT NULL " +
                                "Select '1' as Result " +
                                "else " +
                                "select '0' as Result ";

            OpenDataTable(execString);

            return (Data.Rows[0]["Result"].ToString() == "1");
        }

        public bool OpenDataConnection()
        {
            bool connect = false;
            bool success = true;

            if (SqlConn == null)
                connect = true;
            else
            {
                if ((SqlConn.State == System.Data.ConnectionState.Broken) || (SqlConn.State == System.Data.ConnectionState.Closed))
                {
                    connect = true;
                }
            }

            if (connect)
            {
                //SqlConn = new SqlConnection("Data Source=" + Server + ";Initial Catalog=" + DataBase + ";Persist Security Info=True;Pooling=false;Connect Timeout=60000;User ID=" + UserName + ";Pwd=" + PassWord + ";");
                SqlConn = new SqlConnection(this.connectionstring);

                try
                {
                    SqlConn.Open();
                }
                catch (System.Exception ex)
                {
                    success = false;
                }
            }

            return success;
        }

        public bool OpenDataReader(
            string selectString,
            string server = "",
            string database = "",
            string username = "",
            string password = "")
        {
            if (server != "")
                this.Server = server;

            if (database != "")
                this.DataBase = database;

            if (username != "")
                this.UserName = username;

            if (password != "")
                this.PassWord = password;

            if (OpenDataConnection())
            {
                using (SqlCommand sqlCmd = new SqlCommand(selectString))
                {
                    sqlCmd.CommandTimeout = 600000;

                    sqlCmd.Connection = SqlConn;

                    this.Reader = sqlCmd.ExecuteReader();
                }

                return this.Reader.HasRows;
            }
            else
            {
                return false;
            }
        }

        public bool OpenDataTable(
            string selectString,
            string server = "",
            string database = "",
            string username = "",
            string password = "")
        {
            if (server != "")
                this.Server = server;

            if (database != "")
                this.DataBase = database;

            if (username != "")
                this.UserName = username;

            if (password != "")
                this.PassWord = password;

            if (OpenDataConnection())
            {
                if (this.sqlAdp != null)
                {
                    this.sqlAdp.Dispose();
                    this.Data.Dispose();
                }

                this.Data = new DataTable();
                this.sqlAdp = new SqlDataAdapter(selectString, SqlConn);

                sqlAdp.Fill(this.Data);
            }

            return true;
        }

        public void Update()
        {
            using (SqlCommandBuilder sqlCmd = new SqlCommandBuilder(sqlAdp))
            {
                sqlAdp.Update(this.Data);
            }
        }
    }
}