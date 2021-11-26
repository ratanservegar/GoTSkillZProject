using GoTSkillZ.DataUtilities.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GoTSkillZ.DataUtilities.Core.Attribute;


namespace GoTSkillZ.DataUtilities.Core.Services
{
    public class DbUtility : IDbUtility
    {

        private readonly string _connectionString;

        public DbUtility()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString;
        }


        public int CheckIfExists(string tableName, string columnName, string value)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("CheckIfExists", conn))
            {
                try
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    cmd.Parameters.AddWithValue("@Value", value);
                    var result = cmd.ExecuteScalar();
                    return (int)result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public  DataSet RunStoredProcedure(string storeProc, List<SqlParameterDTO> parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(storeProc, con))
                {
                    var returnDataSet = new DataSet();
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 480;

                        if (parameters != null)
                            if (parameters.Any())
                                foreach (var parameter in parameters)
                                    cmd.Parameters.AddWithValue(parameter.Parameter, parameter.Value);

                        con.Open();
                        var adp = new SqlDataAdapter(cmd);
                        adp.Fill(returnDataSet);
                        con.Close();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }

                    return returnDataSet;
                }
            }
        }

        public  DataTable RunSqlQuery(string query)
        {
            using (var con =
                new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    var dtData = new DataTable();
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 480;
                        con.Open();
                        var adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtData);
                        con.Close();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }

                    return dtData;
                }
            }
        }

    }
}
