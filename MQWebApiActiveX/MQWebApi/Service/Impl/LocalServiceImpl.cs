using System;
using System.Collections.Generic;
using System.Text;
using MoleQ.Support.Wrapper.Common.Util;
using MoleQ.Market.Database;
using MQWebApi.Service.Impl;
using MQWebApi.Database;
using MQWebApi;

namespace MoleQ.Wrapper.WebApi.Service.Impl
{
    public class LocalServiceImpl : BaseService, LocalService
    {
        private const string TAG = "LocalServiceImpl";

        public LocalServiceImpl(string connString)
            : base(connString)
        {
        }

        public LocalServiceImpl(string dsn, string uid, string pwd)
        {
            //SybaseHelper.connectionString = string.Format("Dsn={0};Uid={1};Pwd={2}", dsn, uid, pwd);
            Odbc4SybaseHelper.Dsn = dsn;
            Odbc4SybaseHelper.Uid = uid;
            Odbc4SybaseHelper.Pwd = pwd;
        }

        //public bool Save4(int id, string txt)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("INSERT INTO ");
        //    sb.Append("dba.t_data");
        //    sb.Append("( c_data ) ");
        //    sb.Append("VALUES (");
        //    sb.AppendFormat("'{0}'", txt);
        //    sb.Append(")");

        //    string sql = sb.ToString();
        //    int affectRow = -1;
        //    try
        //    {
        //        affectRow = SybaseHelper.ExecuteSql(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(TAG, ex.ToString());
        //    }
        //    return affectRow > 0;

        //}

        //public void RemoveAll4()
        //{
        //    string sql = "TRUNCATE TABLE dba.t_data";
        //    try
        //    {
        //        SybaseHelper.ExecuteSql(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(TAG, ex.ToString());
        //    }

        //}

        //public bool Save3(int id, string txt)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("INSERT INTO ");
        //    sb.Append("t_data");
        //    sb.Append("( c_data ) ");
        //    sb.Append("VALUES (");
        //    sb.AppendFormat("'{0}'", txt);
        //    sb.Append(")");

        //    string sql = sb.ToString();
        //    int affectRow = -1;
        //    try
        //    {
        //        SQLiteHelper.ExecuteNoQuery(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(TAG, ex.ToString());
        //    }
        //    return affectRow > 0;

        //}

        //public void RemoveAll3()
        //{
        //    string sql = "TRUNCATE TABLE t_data";
        //    try
        //    {
        //        SQLiteHelper.ExecuteNoQuery(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(TAG, ex.ToString());
        //    }

        //}

        public bool Save(int id, string txt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append("DBA.t_webapi_data ");
            sb.Append("( wd_no, wd_data ) ");
            sb.Append("VALUES (");
            sb.AppendFormat(" {0},", id);
            sb.AppendFormat("'{0}'", txt);
            sb.Append(")");

            string sql = sb.ToString();
            int affectRow = -1;
            try
            {
                affectRow = Odbc4SybaseHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex.ToString());
            }
            return affectRow > 0;

        }

        public void RemoveAll()
        {
            //string sql = "DELETE FROM t_data_receive WHERE id > 0 ";
            //string sql = "TRUNCATE TABLE t_webapi_data";
            //try
            //{
            //    SQLiteHelper.ExecuteNoQuery(sql);
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(TAG, ex.ToString());
            //}

        }


        public bool Update(int id, string txt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 覆盖记录，先insert记录，再根据ROWCOUNT判断后是否 update记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool SaveOrUpdate(int id, string content)
        {
            content = DBUtil.FixableContent(content);
            string insertSql = "INSERT INTO DBA.t_webapi_data ( wd_no, wd_data ) VALUES ({0},'{1}')";
            string updateSql = "UPDATE DBA.t_webapi_data SET wd_data ='{0}' WHERE wd_no = {1} ";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(updateSql, content, id);
            sb.Append("\r\n IF @@ROWCOUNT = 0 \r\n");
            sb.AppendFormat(insertSql, id, content);
            //sb.Append(";");

            string sql = sb.ToString();
            int affectRow = -1;
            try
            {
                affectRow = Odbc4SybaseHelper.ExecuteNoQuery(sql);
                Logger.Info(TAG, "Save or update executed. Base on rowcount.");
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex.ToString());
            }
            return affectRow > 0;
        }

        /// <summary>
        /// 覆盖记录，使用IF ELSE来判断执行insert还是update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool SaveOrUpdate2(int id, string content)
        {
            content = DBUtil.FixableContent(content);
            string condition = "SELECT 1 FROM DBA.t_webapi_data WHERE wd_no = {0}";
            string insertSql = "INSERT INTO DBA.t_webapi_data ( wd_no, wd_data ) VALUES ({0},'{1}')";
            string updateSql = "UPDATE DBA.t_webapi_data SET wd_data ='{0}' WHERE wd_no = {1} ";

            StringBuilder sb = new StringBuilder();
            sb.Append("IF NOT EXISTS (");
            sb.AppendFormat(condition, id);
            sb.Append(")");
            sb.Append(" BEGIN ");
            sb.AppendFormat(insertSql, id, content);
            sb.Append(" END ");
            sb.Append(" ELSE ");
            sb.Append(" BEGIN ");
            sb.AppendFormat(updateSql, content, id);
            sb.Append(" END ");

            string sql = sb.ToString();
            int affectRow = -1;
            try
            {
                affectRow = Odbc4SybaseHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex.ToString());
            }
            return affectRow > 0;
        }

        public bool RemoveOrInsert(int id, string str)
        {
            string deleteSql = "DELETE DBA.t_webapi_data WHERE wd_no = {0} ";
            string insertSql = "INSERT INTO DBA.t_webapi_data ( wd_no, wd_data ) VALUES ({0},'{1}')";

            bool isOk;
            List<string> sqls = new List<string>();
            sqls.Add(string.Format(deleteSql, id));
            sqls.Add(string.Format(insertSql, id, str));

            try
            {
                Odbc4SybaseHelper.ExecuteSqlTran(sqls);
                isOk = true;
                Logger.Info(TAG, "Delete has been executed, then insert record.");
            }
            catch (Exception ex)
            {
                isOk = false;
                Logger.Error(TAG, ex.ToString());
            }
            return isOk;

        }


        #region LocalService 成员


        public bool Remove(int id)
        {
            string deleteSql = "DELETE DBA.t_webapi_data WHERE wd_no = {0} ";
            string sql = string.Format(deleteSql, id);
            int affectRow = -1;
            try
            {
                affectRow = Odbc4SybaseHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex.ToString());
            }
            return affectRow >= 0;

        }

        public bool Insert(int id, string str)
        {
            string insertSql = "INSERT INTO DBA.t_webapi_data ( wd_no, wd_data ) VALUES ({0},'{1}')";
            string sql = string.Format(insertSql, id, str);
            int affectRow = -1;
            try
            {
                affectRow = Odbc4SybaseHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex.ToString());
            }
            return affectRow > 0;
        }

        #endregion
    }
}
