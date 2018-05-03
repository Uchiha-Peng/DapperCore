using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DapperCore.Models
{
    public class DbContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(User model)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = "Insert into User (Id,UserName,Url,Age) Values(@Id,@UserName,@Url,@Age)";
                return conn.Execute(sql, model);
            }
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOne(User model)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", model.Id);
                dp.Add("@UserName", model.UserName);
                dp.Add("@Url", model.Url);
                dp.Add("@Age", model.Age);
                return conn.Execute("User", dp, null, null, CommandType.TableDirect);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Edit(User model)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = "update User set UserName=@UserName,Url=@Url,Age=@Age where Id=@Id";
                return conn.Execute(sql, model);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = "Delete from User where Id=" + Id;
                return conn.Execute(sql);
            }
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <returns></returns>
        public User SelectUser(int Id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var sql = "Select Id,UserName,Url,Age from User where Id=" + Id;
                return conn.QueryFirstOrDefault<User>(sql);
            }
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<User> SelectAll()
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var sql = "Select Id,UserName,Url,Age from User";
                return conn.Query<User>(sql).ToList();
            }
        }

        /// <summary>
        /// 登录并添加Log
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="userLogModel"></param>
        //public void Logoin(User userModel, LoginLog userLogModel)
        //{
        //    using (conn)
        //    {
        //        IDbTransaction tran = conn.BeginTransaction();
        //        try
        //        {
        //            string query = "Update User set Key='测试' where ID=@ID";//更新一条记录
        //            conn.Execute(query, userModel, tran, null, null);

        //            query = "insert into UserLoginLog (userId,CreateTime) value (@userId,@CreateTime)";//删除一条记录
        //            conn.Execute(query, userLogModel, tran, null, null);

        //            //提交
        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            //提交错误
        //            //回滚事务
        //            tran.Rollback();
        //        }
        //    }
        //}

        /// <summary>
        /// 执行无参数存储过程 返回列表
        /// </summary>
        /// <returns></returns>
        //private IEnumerable<User> ExecuteStoredProcedureNoParms()
        //{
        //    using (IDbConnection con = conn)
        //    {
        //        var userList = new List<User>();
        //        userList = con.Query<User>("QueryRoleNoParms",
        //                                null,
        //                                null,
        //                                true,
        //                                null,
        //                                CommandType.StoredProcedure).ToList();
        //        return userList;
        //    }
        //}

        /// <summary>
        /// 执行无参数存储过程 返回int
        /// </summary>
        /// <returns></returns>
        private int ExecutePROC()
        {
            using (IDbConnection con = new MySqlConnection(ConnectionString))
            {
                return con.Execute("QueryRoleWitdParms", null, null, null, CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ExecutePROC(User model)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ID", "1");
            dp.Add("@msg", "", DbType.String, ParameterDirection.Output);
            using (IDbConnection con = new MySqlConnection(ConnectionString))
            {
                con.Execute("Proc", dp, null, null, CommandType.StoredProcedure);
                string roleName = dp.Get<string>("@msg");
                return roleName;
            }
        }
    }
}