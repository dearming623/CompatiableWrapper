#region <<版本注释>>
/*-----------------------------------------------------------------
 * 作者: Ming
 * 邮箱: t8ming@live.com
 * 修改时间: 2023-12-12
 * 版本: v0.0.1
 * 
 * 
 * 修改说明: 创建odbc connection带oem配置
 * 
 * 
 * -----------------------------------------------------------------*/
#endregion

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Collections.Generic;
using MoleQ.Support.Wrapper.Common.Util;

namespace MoleQ.Market.Database
{
    /// <summary>
    /// 数据访问基础类(基于Odbc) 访问Sybase数据库
    /// 可以用户可以修改满足自己项目的需要。
    /// </summary>
    public class Odbc4SybaseHelper
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.		
        private static OdbcConnection con = null;

        public static string Dsn = "market";
        public static string Uid = "adm0";
        public static string Pwd = "systemcom";

        public static string Company = "MoleQ Inc.";
        public static string Application = "MoleQ Solutions";
        public static string Signature = "000fa55157edb8e14d818eb4fe3db41447146f1571g2a2ac0b2993b8eebd8393550f631933e5451b12b";


        public Odbc4SybaseHelper()
        {

        }

        #region  执行简单SQL语句
        public static void TestConnection()
        {
            OdbcConnection conn = GetConnInstance();
            try
            {
                bool connected = false;
                //conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    connected = true;
                    conn.Close();
                }

                if (!connected)
                    throw new Exception("Failed to connect ODBC database.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static object ExecuteScalar(string SQLString)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                using (OdbcCommand cmd = new OdbcCommand(SQLString, connection))
                {
                    try
                    {
                        //connection.Open();
                        object rows = cmd.ExecuteScalar();
                        return rows;
                    }
                    catch (OdbcException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                using (OdbcCommand cmd = new OdbcCommand(SQLString, connection))
                {
                    try
                    {
                        //connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (OdbcException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static void ExecuteSqlTran(List<string> sqls)
        {
            using (OdbcConnection conn = GetConnInstance())
            {
                //conn.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = conn;
                OdbcTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (var sql in sqls)
                    {
                        cmd.CommandText = sql.Trim();
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
                catch (OdbcException ex)
                {
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcCommand cmd = new OdbcCommand(SQLString, connection);
                OdbcParameter myParameter = new OdbcParameter("@content", OdbcType.VarChar);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    //connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (OdbcException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcCommand cmd = new OdbcCommand(strSQL, connection);
                OdbcParameter myParameter = new OdbcParameter("@fs", OdbcType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    //connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (OdbcException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                using (OdbcCommand cmd = new OdbcCommand(SQLString, connection))
                {
                    try
                    {
                        //connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (OdbcException ex)
                    {
                        connection.Close();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回OdbcDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OdbcDataReader</returns>
        public static OdbcDataReader ExecuteReader(string strSQL)
        {
            OdbcConnection connection = GetConnInstance();
            OdbcCommand cmd = new OdbcCommand(strSQL, connection);
            try
            {
                //connection.Open();
                OdbcDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                DataSet ds = new DataSet();
                try
                {
                    //connection.Open();
                    OdbcDataAdapter command = new OdbcDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (OdbcException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params OdbcParameter[] cmdParms)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (OdbcException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的OdbcParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OdbcConnection conn = GetConnInstance())
            {
                //conn.Open();
                using (OdbcTransaction trans = conn.BeginTransaction())
                {
                    OdbcCommand cmd = new OdbcCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();

                            int idx = cmdText.IndexOf("-SQL");
                            if (idx >= 0)
                            {
                                cmdText = cmdText.Substring(idx + 4);
                            }

                            OdbcParameter[] cmdParms = (OdbcParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        trans.Dispose();
                        conn.Close();
                        throw;
                    }
                    finally
                    {
                        trans.Dispose();
                        conn.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params OdbcParameter[] cmdParms)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                using (OdbcCommand cmd = new OdbcCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (OdbcException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回OdbcDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>OdbcDataReader</returns>
        public static OdbcDataReader ExecuteReader(string SQLString, params OdbcParameter[] cmdParms)
        {
            OdbcConnection connection = GetConnInstance();
            OdbcCommand cmd = new OdbcCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                OdbcDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params OdbcParameter[] cmdParms)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcCommand cmd = new OdbcCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (OdbcException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private static void PrepareCommand(OdbcCommand cmd, OdbcConnection conn, OdbcTransaction trans, string cmdText, OdbcParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OdbcParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

        #region 存储过程操作


        public static int RunProcedure(string storedProcName)
        {
            var connection = GetConnInstance();
            int res;
            OdbcCommand command = new OdbcCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            res = command.ExecuteNonQuery();
            return res;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OdbcDataReader</returns>
        public static OdbcDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            OdbcConnection connection = GetConnInstance();
            OdbcDataReader returnReader;
            //connection.Open();
            OdbcCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                DataSet dataSet = new DataSet();
                //connection.Open();
                OdbcDataAdapter sqlDA = new OdbcDataAdapter();
                var comm = BuildQueryCommand(connection, storedProcName, parameters);
                try
                {
                    sqlDA.SelectCommand = comm;
                    sqlDA.Fill(dataSet, tableName);
                    return dataSet;
                }
                catch (OdbcException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    comm.Dispose();
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// 构建 OdbcCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OdbcCommand</returns>
        private static OdbcCommand BuildQueryCommand(OdbcConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OdbcCommand command = new OdbcCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OdbcParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                int result;
                //connection.Open();
                OdbcCommand command = BuildIntCommand(connection, storedProcName, parameters);
                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                    result = (int)command.Parameters["ReturnValue"].Value;
                    //Connection.Close();
                    return result;
                }
                catch (OdbcException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 创建 OdbcCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OdbcCommand 对象实例</returns>
        private static OdbcCommand BuildIntCommand(OdbcConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OdbcCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OdbcParameter("ReturnValue",
                OdbcType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion


        //public static OdbcConnection GetOdbcConnection()
        //{
        //    return new OdbcConnection(connectionString);
        //}

        public static DataTable GetTable(string sql)
        {
            try
            {
                var connection = GetConnInstance();
                OdbcDataAdapter sda = new OdbcDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static DataTable GetTable(string sql, OdbcParameter[] sqlparams)
        {
            try
            {
                OdbcParamFix4Sybase.Rewrite(ref sql, ref sqlparams);

                var connection = GetConnInstance();
                OdbcDataAdapter sda = new OdbcDataAdapter(sql, connection);
                sda.SelectCommand.Parameters.AddRange(sqlparams);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static int ExecuteNoQuery(string sql)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcCommand cmd = new OdbcCommand(sql, connection);
                int i = -1;
                try
                {
                    i = cmd.ExecuteNonQuery();
                    return i;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public static int ExecuteNoQuery(string sql, OdbcParameter[] sqlparams)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcParamFix4Sybase.Rewrite(ref sql, ref sqlparams);
                OdbcCommand cmd = new OdbcCommand(sql, connection);
                int i = -1;
                try
                {
                    cmd.Parameters.AddRange(sqlparams);
                    i = cmd.ExecuteNonQuery();
                    return i;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }


        /// <summary>
        /// 查询数据库有无重复数据,有返回false,无返回true
        /// </summary>
        /// <param name="sql">ex. sql = "select count(*) from table"</param>
        /// <param name="sqlparams"></param>
        /// <returns></returns>
        public static bool ExecuteScalar(string sql, OdbcParameter[] sqlparams)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                int i = -1;
                OdbcParamFix4Sybase.Rewrite(ref sql, ref sqlparams);
                OdbcCommand cmd = new OdbcCommand(sql, connection);
                try
                {
                    cmd.Parameters.AddRange(sqlparams);
                    i = Convert.ToInt32(cmd.ExecuteScalar());
                    if (i > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>一定要记得关闭sdr</returns>
        public static OdbcDataReader GetReader(string sql)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcCommand cmd = new OdbcCommand(sql, connection);
                try
                {
                    OdbcDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return sdr;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>一定要记得关闭sdr</returns>
        public static OdbcDataReader GetReader(string sql, OdbcParameter[] sqlparams)
        {
            using (OdbcConnection connection = GetConnInstance())
            {
                OdbcParamFix4Sybase.Rewrite(ref sql, ref sqlparams);
                OdbcCommand cmd = new OdbcCommand(sql, connection);
                try
                {
                    cmd.Parameters.AddRange(sqlparams);
                    OdbcDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return sdr;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static void auth(OdbcConnection conn)
        {
            string auth = String.Format("SET TEMPORARY OPTION CONNECTION_AUTHENTICATION='Company={0};Application={1};Signature={2}'", Company, Application, Signature);
            using (var cmd = new OdbcCommand(auth, conn))
            {
                cmd.ExecuteNonQuery();
                Logger.Info("OdbcHelper", "Auth set oem successfully.");
            }
        }

        private static OdbcConnection GetConnInstance()
        {
            OdbcConnectionStringBuilder constr = new OdbcConnectionStringBuilder();
            constr.Add("Dsn", Dsn);
            constr.Add("Uid", Uid);
            constr.Add("Pwd", Pwd);

            if (con == null)
            {
                con = new OdbcConnection();
                con.ConnectionString = constr.ToString();
                con.Open();
                auth(con);
            }
            else if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.Close();
                con.ConnectionString = constr.ToString();
                con.Open();
                auth(con);
            }

            return con;
        }


    }

}
