using System;
using System.Collections.Generic;
using System.Xml; 
using System.Collections;
using System.Data;
using System.Configuration;

namespace MoleQ.Market.Database
{
    public static class SQLiteHelper
    {
        //public static string database = Environment.CurrentDirectory + "\\local.db";

        //private static SQLiteConnection con = null;

        //private static void CreateSQLiteConnection()
        //{
        //    SQLiteConnectionStringBuilder constr = new SQLiteConnectionStringBuilder();
        //    constr.DataSource = database;
        //    if (con == null)
        //    {
        //        con = new SQLiteConnection();
        //        con.ConnectionString = constr.ToString();  //必须以这种方式构造数据库连接字符串!!
        //        con.Open();
        //    }
        //    else if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
        //    {
        //        con.Close();
        //        con.Open();
        //    }
        //}

        //public static SQLiteConnection GetConnection()
        //{
        //    CreateSQLiteConnection();
        //    return con;
        //}

        //public static int ExecuteNoQuery(string sql)
        //{
        //    int i = -1;

        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteCommand cmd = new SQLiteCommand(sql, con);
        //        i = cmd.ExecuteNonQuery();
        //        return i;
        //    }
        //    catch (Exception ex)
        //    { 
        //        throw new Exception(ex.Message);
        //    }

        //}

        //public static int ExecuteNoQuery(string sql, SQLiteParameter[] sqlparams)
        //{
        //    int i = -1;

        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteCommand cmd = new SQLiteCommand(sql, con);
        //        cmd.Parameters.AddRange(sqlparams);
        //        i = cmd.ExecuteNonQuery();
        //        return i;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }

        //}




        ///// <summary>
        ///// 查询数据库有无重复数据,有返回false,无返回true
        ///// </summary>
        ///// <param name="sql">ex. sql = "select count(*) from table"</param>
        ///// <param name="sqlparams"></param>
        ///// <returns></returns>
        //public static bool ExecuteScalar(string sql, SQLiteParameter[] sqlparams)
        //{
        //    int i = -1;

        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteCommand cmd = new SQLiteCommand(sql, con);
        //        cmd.Parameters.AddRange(sqlparams);
        //        i = Convert.ToInt32(cmd.ExecuteScalar());
        //        if (i > 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}

        //public static DataTable GetTable(string sql)
        //{
        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, con);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        con.Close();
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}


        //public static DataTable GetTable(string sql, SQLiteParameter[] sqlparams)
        //{
        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, con);
        //        sda.SelectCommand.Parameters.AddRange(sqlparams);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        con.Close();
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}



        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns>一定要记得关闭sdr</returns>
        //public static SQLiteDataReader GetReader(string sql)
        //{
        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteCommand cmd = new SQLiteCommand(sql, con);
        //        SQLiteDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        return sdr;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns>一定要记得关闭sdr</returns>
        //public static SQLiteDataReader GetReader(string sql, SQLiteParameter[] sqlparams)
        //{
        //    try
        //    {
        //        CreateSQLiteConnection();
        //        SQLiteCommand cmd = new SQLiteCommand(sql, con);
        //        cmd.Parameters.AddRange(sqlparams);
        //        SQLiteDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        return sdr;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}



        ///// <summary>
        ///// 为执行命令准备参数
        ///// </summary>
        ///// <param name="cmd">SqlCommand 命令</param>
        ///// <param name="conn">已经存在的数据库连接</param>
        ///// <param name="trans">数据库事物处理</param>        
        ///// <param name="cmdText">Command text，T-SQL语句例如Select * from Products</param>
        ///// <param name="cmdParms">返回带参数的命令</param>
        //private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        //{

        //    //判断数据库连接状态
        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();

        //    cmd.Connection = conn;
        //    cmd.CommandText = cmdText;
        //    cmd.CommandType = CommandType.Text;
        //    //判断是否需要事物处理
        //    if (trans != null)
        //        cmd.Transaction = trans;


        //    if (cmdParms != null)
        //    {
        //        foreach (SQLiteParameter parm in cmdParms)
        //            cmd.Parameters.Add(parm);
        //    }
        //}


        //// <summary>
        ///// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库事物处理
        ///// 使用参数数组提供参数
        ///// </summary>
        ///// <remarks>
        ///// 使用示例：
        /////  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        ///// </remarks>
        ///// <param name="trans">一个存在的sql 事物处理</param>
        ///// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句，等等。)</param>
        ///// <param name="commandText">存储过程的名字或者T-SQL 语句</param>
        ///// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        ///// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        //public static int ExecuteNonQuery(SQLiteTransaction trans, string cmdText, params SQLiteParameter[] commandParameters)
        //{
        //    SQLiteCommand cmd = new SQLiteCommand();
        //    PrepareCommand(cmd, trans.Connection, trans, cmdText, commandParameters);
        //    int val = cmd.ExecuteNonQuery();
        //    cmd.Parameters.Clear();
        //    return val;
        //}


    }
}


