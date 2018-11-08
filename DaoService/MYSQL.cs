using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace Dapper
{
    #region SqlHelper
    /// <summary>
    /// created_by Limeng
    /// created_at 2018-8-1 
    /// for Dapper helper
    /// </summary>
    public static class MYSQL
    {
        #region Query And Async

        public static IEnumerable<dynamic> QuerySP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }
        public async static Task<IEnumerable<dynamic>> QuerySPAsync(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }
        public static IEnumerable<dynamic> QuerySQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<IEnumerable<dynamic>> QuerySQLAsync(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public static IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }
        public async static Task<IEnumerable<T>> QuerySPAsync<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<T>(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }
        public static IEnumerable<T> QuerySQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query<T>(sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<IEnumerable<T>> QuerySQLAsync<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public static IEnumerable<object> QuerySP(Type type, string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(type, storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }
        public async static Task<IEnumerable<object>> QuerySPAsync(Type type, string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(type, storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }
        public static IEnumerable<object> QuerySQL(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Query(type, sql, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<IEnumerable<object>> QuerySQLAsync(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static object QueryFirst(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.QueryFirst(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<object> QueryFirstAsync(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static object QueryFirst<T>(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.QueryFirst<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<object> QueryFirstAsync<T>(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstAsync<T>( sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }


        public static object QueryFirstOrDefault(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.QueryFirstOrDefault(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<object> QueryFirstOrDefaultAsync(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static object QueryFirstOrDefault<T>(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<object> QueryFirstOrDefaultAsync<T>(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public static object QuerySingle(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.QuerySingle(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<object> QuerySingleAsync(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QuerySingleAsync(type, sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static object QuerySingle<T>(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.QuerySingle<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }
        public async static Task<object> QuerySingleAsync<T>(Type type, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.QuerySingleAsync<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion

        #region QueryMultiple

        #region IEnumerable<IEnumerable<dynamic>>

        public static IEnumerable<IEnumerable<dynamic>> QueryMultipleSP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple(storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
        }

        public static IEnumerable<IEnumerable<dynamic>> QueryMultipleSQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple(sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
        }

        private static IEnumerable<IEnumerable<dynamic>> QueryMultiple(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);
                List<IEnumerable<dynamic>> lists = new List<IEnumerable<dynamic>>();
                while (!gr.IsConsumed)
                    lists.Add(gr.Read());
                return lists;
            }
        }

        #endregion

        #region IEnumerable<IEnumerable<dynamic>> Async

        public async static Task<IEnumerable<IEnumerable<dynamic>>> QueryMultipleSPAsync(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return await QueryMultipleAsync(storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
        }

        public async static Task<IEnumerable<IEnumerable<dynamic>>> QueryMultipleSQLAsync(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return await QueryMultipleAsync(sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
        }

        private async static Task<IEnumerable<IEnumerable<dynamic>>> QueryMultipleAsync(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                SqlMapper.GridReader gr = await connection.QueryMultipleAsync(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);
                List<IEnumerable<dynamic>> lists = new List<IEnumerable<dynamic>>();
                while (!gr.IsConsumed)
                    lists.Add(gr.Read());
                return lists;
            }
        }

        #endregion

        #region IEnumerable<IEnumerable<T>>

        public static IEnumerable<IEnumerable<T>> QueryMultipleSP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple<T>(storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
        }

        public static IEnumerable<IEnumerable<T>> QueryMultipleSQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return QueryMultiple<T>(sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
        }

        private static IEnumerable<IEnumerable<T>> QueryMultiple<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);
                List<IEnumerable<T>> lists = new List<IEnumerable<T>>();
                while (!gr.IsConsumed)
                    lists.Add(gr.Read<T>());
                return lists;
            }
        }

        #endregion

        #region IEnumerable<IEnumerable<T>> Async

        public async static Task<IEnumerable<IEnumerable<T>>> QueryMultipleSPAsync<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return await QueryMultipleAsync<T>(storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
        }

        public async static Task<IEnumerable<IEnumerable<T>>> QueryMultipleSQLAsync<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            return await QueryMultipleAsync<T>(sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
        }

        private async static Task<IEnumerable<IEnumerable<T>>> QueryMultipleAsync<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                SqlMapper.GridReader gr = await connection.QueryMultipleAsync(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);
                List<IEnumerable<T>> lists = new List<IEnumerable<T>>();
                while (!gr.IsConsumed)
                    lists.Add(gr.Read<T>());
                return lists;
            }
        }

        #endregion
        #region Tuples

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultipleSP<T1, T2>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, object, object, object, object, object, object, object, object, object, object, object, object>(2, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultipleSQL<T1, T2>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, object, object, object, object, object, object, object, object, object, object, object, object>(2, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultipleSP<T1, T2, T3>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, object, object, object, object, object, object, object, object, object, object, object>(3, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultipleSQL<T1, T2, T3>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, object, object, object, object, object, object, object, object, object, object, object>(3, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultipleSP<T1, T2, T3, T4>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, object, object, object, object, object, object, object, object, object, object>(4, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultipleSQL<T1, T2, T3, T4>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, object, object, object, object, object, object, object, object, object, object>(4, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultipleSP<T1, T2, T3, T4, T5>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, object, object, object, object, object, object, object, object, object>(5, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultipleSQL<T1, T2, T3, T4, T5>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, object, object, object, object, object, object, object, object, object>(5, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryMultipleSP<T1, T2, T3, T4, T5, T6>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, object, object, object, object, object, object, object, object>(6, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, object, object, object, object, object, object, object, object>(6, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, object, object, object, object, object, object, object>(7, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, object, object, object, object, object, object, object>(7, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6]
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, object, object, object, object, object, object>(8, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>>(
                    (IEnumerable<T8>)lists[7]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, object, object, object, object, object, object>(8, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>>(
                    (IEnumerable<T8>)lists[7]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, object, object, object, object, object>(9, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, object, object, object, object, object>(9, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object, object, object, object>(10, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object, object, object, object>(10, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object, object, object>(11, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object, object, object>(11, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object, object>(12, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object, object>(12, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>(13, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object>(13, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>> QueryMultipleSP<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(14, storedProcedure, param, outParam, transaction, commandTimeout, CommandType.StoredProcedure, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12],
                    (IEnumerable<T14>)lists[13]
                )
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>> QueryMultipleSQL<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            IEnumerable[] lists = QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(14, sql, param, outParam, transaction, commandTimeout, CommandType.Text, connectionString);
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>, IEnumerable<T7>, Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>>(
                (IEnumerable<T1>)lists[0],
                (IEnumerable<T2>)lists[1],
                (IEnumerable<T3>)lists[2],
                (IEnumerable<T4>)lists[3],
                (IEnumerable<T5>)lists[4],
                (IEnumerable<T6>)lists[5],
                (IEnumerable<T7>)lists[6],
                new Tuple<IEnumerable<T8>, IEnumerable<T9>, IEnumerable<T10>, IEnumerable<T11>, IEnumerable<T12>, IEnumerable<T13>, IEnumerable<T14>>(
                    (IEnumerable<T8>)lists[7],
                    (IEnumerable<T9>)lists[8],
                    (IEnumerable<T10>)lists[9],
                    (IEnumerable<T11>)lists[10],
                    (IEnumerable<T12>)lists[11],
                    (IEnumerable<T13>)lists[12],
                    (IEnumerable<T14>)lists[13]
                )
            );
        }

        private static IEnumerable[] QueryMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int readCount, string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                SqlMapper.GridReader gr = connection.QueryMultiple(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: commandType);

                IEnumerable[] lists = new IEnumerable[readCount];

                if (readCount >= 1 && !gr.IsConsumed)
                {
                    lists[0] = gr.Read<T1>();
                    if (readCount >= 2 && !gr.IsConsumed)
                    {
                        lists[1] = gr.Read<T2>();
                        if (readCount >= 3 && !gr.IsConsumed)
                        {
                            lists[2] = gr.Read<T3>();
                            if (readCount >= 4 && !gr.IsConsumed)
                            {
                                lists[3] = gr.Read<T4>();
                                if (readCount >= 5 && !gr.IsConsumed)
                                {
                                    lists[4] = gr.Read<T5>();
                                    if (readCount >= 6 && !gr.IsConsumed)
                                    {
                                        lists[5] = gr.Read<T6>();
                                        if (readCount >= 7 && !gr.IsConsumed)
                                        {
                                            lists[6] = gr.Read<T7>();
                                            if (readCount >= 8 && !gr.IsConsumed)
                                            {
                                                lists[7] = gr.Read<T8>();
                                                if (readCount >= 9 && !gr.IsConsumed)
                                                {
                                                    lists[8] = gr.Read<T9>();
                                                    if (readCount >= 10 && !gr.IsConsumed)
                                                    {
                                                        lists[9] = gr.Read<T10>();
                                                        if (readCount >= 11 && !gr.IsConsumed)
                                                        {
                                                            lists[10] = gr.Read<T11>();
                                                            if (readCount >= 12 && !gr.IsConsumed)
                                                            {
                                                                lists[11] = gr.Read<T12>();
                                                                if (readCount >= 13 && !gr.IsConsumed)
                                                                {
                                                                    lists[12] = gr.Read<T13>();
                                                                    if (readCount >= 14 && !gr.IsConsumed)
                                                                    {
                                                                        lists[13] = gr.Read<T14>();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return lists;
            }
        }

        #endregion

        #endregion


        #region ExecuteScalar

        public static object ExecuteScalarSP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static object ExecuteScalarSQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public static T ExecuteScalarSP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar<T>(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExecuteScalarSQL<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.ExecuteScalar<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion


        #region ExecuteScalar Async

        public async static Task<object> ExecuteScalarSPAsync(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public async static Task<object> ExecuteScalarSQLAsync(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        public async static Task<T> ExecuteScalarSPAsync<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<T>(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public async static Task<T> ExecuteScalarSQLAsync<T>(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<T>(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion
        #region Execute

        public static int ExecuteSP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Execute(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public static int ExecuteSQL(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                connection.Open();
                return connection.Execute(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion

        #region Execute Async

        public async static Task<int> ExecuteSPAsync(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(storedProcedure, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.StoredProcedure);
            }
        }

        public async static Task<int> ExecuteSQLAsync(string sql, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, int? commandTimeout = null, string connectionString = null)
        {
            CombineParameters(ref param, outParam);

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(sql, param: (object)param, transaction: transaction, commandTimeout: GetTimeout(commandTimeout), commandType: CommandType.Text);
            }
        }

        #endregion
        #region CombineParameters

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }

        #endregion

        #region Connection String & Timeout

        private static IConfigurationRoot config;

        public static IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                    config = new ConfigurationBuilder().AddJsonFile(Directory.GetCurrentDirectory() + "\\appsettings.json").Build();
                return config;
            }
        }

        public static string GetConnectionString(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
                return Config["ConnectionStrings:MSSQL"];
            else
                return connectionString;
        }

        public static int ConnectionTimeout { get; set; }

        public static int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }

        #endregion

        #region ToEnumerable

        public delegate object ValueHandler(string columnName, Type columnType, object value);

        #endregion

        #region ToProperties

        public static IDictionary<string, object> ToProperties(this IDictionary<string, object> obj, params string[] columnNames)
        {
            return ToProperties(obj, null, columnNames);
        }

        public static IDictionary<string, object> ToProperties(this IDictionary<string, object> obj, ValueHandler getValue, params string[] columnNames)
        {
            if (columnNames != null && columnNames.Length > 0)
            {
                IDictionary<string, object> props = new Dictionary<string, object>();
                if (getValue != null)
                {
                    foreach (var pair in obj)
                    {
                        if (columnNames.Contains(pair.Key))
                            props.Add(pair.Key, getValue(pair.Key, (pair.Value != null ? pair.Value.GetType() : typeof(object)), pair.Value));
                    }
                }
                else
                {
                    foreach (var pair in obj)
                    {
                        if (columnNames.Contains(pair.Key))
                            props.Add(pair.Key, pair.Value);
                    }
                }
                return props;
            }
            else if (getValue != null)
            {
                IDictionary<string, object> props = new Dictionary<string, object>();
                foreach (var pair in obj)
                    props.Add(pair.Key, getValue(pair.Key, (pair.Value != null ? pair.Value.GetType() : typeof(object)), pair.Value));
                return props;
            }
            else
            {
                return obj;
            }
        }

        public static IDictionary<string, object> ToProperties(object obj, params string[] columnNames)
        {
            return ToProperties(obj, null, columnNames);
        }

        public static IDictionary<string, object> ToProperties(object obj, ValueHandler getValue, params string[] columnNames)
        {
            if (obj is IDictionary<string, object>)
            {
                if (getValue != null || (columnNames != null && columnNames.Length > 0))
                    return ToProperties((IDictionary<string, object>)obj, getValue, columnNames);
                else
                    return (IDictionary<string, object>)obj;
            }

            Type type = obj.GetType();

            var columns =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Select(f => new
                    {
                        ColumnName = f.Name,
                        ColumnType = f.FieldType,
                        IsField = true,
                        MemberInfo = (MemberInfo)f
                    })
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.CanRead)
                            .Where(p => p.GetGetMethod(true).IsPublic)
                            .Where(p => p.GetIndexParameters().Length == 0)
                            .Select(p => new
                            {
                                ColumnName = p.Name,
                                ColumnType = p.PropertyType,
                                IsField = false,
                                MemberInfo = (MemberInfo)p
                            })
                    )
                    .Where(c => (columnNames != null && columnNames.Length > 0 ? columnNames.Contains(c.ColumnName) : true)); // columns exist

            IDictionary<string, object> values = new Dictionary<string, object>();
            if (getValue != null)
            {
                foreach (var column in columns)
                    values.Add(column.ColumnName, getValue(column.ColumnName, column.ColumnType, (column.IsField ? ((FieldInfo)column.MemberInfo).GetValue(obj) : ((PropertyInfo)column.MemberInfo).GetValue(obj, null))));
            }
            else
            {
                foreach (var column in columns)
                    values.Add(column.ColumnName, (column.IsField ? ((FieldInfo)column.MemberInfo).GetValue(obj) : ((PropertyInfo)column.MemberInfo).GetValue(obj, null)));
            }
            return values;
        }

        public static IEnumerable<IDictionary<string, object>> ToProperties(this IEnumerable<IDictionary<string, object>> objs, params string[] columnNames)
        {
            return ToProperties(objs, null, columnNames);
        }

        public static IEnumerable<IDictionary<string, object>> ToProperties(this IEnumerable<IDictionary<string, object>> objs, ValueHandler getValue, params string[] columnNames)
        {
            if (columnNames != null && columnNames.Length > 0)
            {
                List<IDictionary<string, object>> values = new List<IDictionary<string, object>>();
                if (getValue != null)
                {
                    foreach (IDictionary<string, object> obj in objs)
                    {
                        IDictionary<string, object> props = new Dictionary<string, object>();
                        foreach (var pair in obj)
                        {
                            if (columnNames.Contains(pair.Key))
                                props.Add(pair.Key, getValue(pair.Key, (pair.Value != null ? pair.Value.GetType() : typeof(object)), pair.Value));
                        }
                        values.Add(props);
                    }
                }
                else
                {
                    foreach (IDictionary<string, object> obj in objs)
                    {
                        IDictionary<string, object> props = new Dictionary<string, object>();
                        foreach (var pair in obj)
                        {
                            if (columnNames.Contains(pair.Key))
                                props.Add(pair.Key, pair.Value);
                        }
                        values.Add(props);
                    }
                }
                return values;
            }
            else if (getValue != null)
            {
                List<IDictionary<string, object>> values = new List<IDictionary<string, object>>();
                foreach (IDictionary<string, object> obj in objs)
                {
                    IDictionary<string, object> props = new Dictionary<string, object>();
                    foreach (var pair in obj)
                        props.Add(pair.Key, getValue(pair.Key, (pair.Value != null ? pair.Value.GetType() : typeof(object)), pair.Value));
                    values.Add(props);
                }
                return values;
            }
            else
            {
                return objs;
            }
        }

        public static IEnumerable<IDictionary<string, object>> ToProperties<T>(this IEnumerable<T> objs, params string[] columnNames)
        {
            return ToProperties<T>(objs, null, columnNames);
        }

        public static IEnumerable<IDictionary<string, object>> ToProperties<T>(this IEnumerable<T> objs, ValueHandler getValue, params string[] columnNames)
        {
            if (objs is IEnumerable<IDictionary<string, object>>)
            {
                if (getValue != null || (columnNames != null && columnNames.Length > 0))
                    return ToProperties((IEnumerable<IDictionary<string, object>>)objs, getValue, columnNames);
                else
                    return (IEnumerable<IDictionary<string, object>>)objs;
            }

            Type type = typeof(T);

            var columns =
                type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Select(f => new
                    {
                        ColumnName = f.Name,
                        ColumnType = f.FieldType,
                        IsField = true,
                        MemberInfo = (MemberInfo)f
                    })
                    .Union(
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.CanRead)
                            .Where(p => p.GetGetMethod(true).IsPublic)
                            .Where(p => p.GetIndexParameters().Length == 0)
                            .Select(p => new
                            {
                                ColumnName = p.Name,
                                ColumnType = p.PropertyType,
                                IsField = false,
                                MemberInfo = (MemberInfo)p
                            })
                    )
                    .Where(c => (columnNames != null && columnNames.Length > 0 ? columnNames.Contains(c.ColumnName) : true)); // columns exist

            List<IDictionary<string, object>> values = new List<IDictionary<string, object>>();
            if (getValue != null)
            {
                foreach (var obj in objs)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (var column in columns)
                        dic.Add(column.ColumnName, getValue(column.ColumnName, column.ColumnType, (column.IsField ? ((FieldInfo)column.MemberInfo).GetValue(obj) : ((PropertyInfo)column.MemberInfo).GetValue(obj, null))));
                    values.Add(dic);
                }
            }
            else
            {
                foreach (var obj in objs)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (var column in columns)
                        dic.Add(column.ColumnName, (column.IsField ? ((FieldInfo)column.MemberInfo).GetValue(obj) : ((PropertyInfo)column.MemberInfo).GetValue(obj, null)));
                    values.Add(dic);
                }
            }
            return values;
        }

        public static IEnumerable<IDictionary<string, object>> ToProperties(this IEnumerable<object> objs, params string[] columnNames)
        {
            return ToProperties(objs, null, columnNames);
        }

        public static IEnumerable<IDictionary<string, object>> ToProperties(this IEnumerable<object> objs, ValueHandler getValue, params string[] columnNames)
        {
            if (getValue == null && (columnNames == null || columnNames.Length == 0) && objs is IEnumerable<IDictionary<string, object>>)
                return (IEnumerable<IDictionary<string, object>>)objs;

            List<IDictionary<string, object>> values = new List<IDictionary<string, object>>();
            foreach (var obj in objs)
                values.Add(ToProperties(obj, getValue, columnNames));
            return values;
        }

        #endregion
    }

    #endregion

    #region DynamicParameters Extensions

    //public static class DynamicParametersExtensions
    //{
    //    // http://msdn.microsoft.com/en-us/library/cc716729(v=vs.100).aspx
    //    static readonly Dictionary<SqlDbType, DbType?> sqlDbTypeMap = new Dictionary<SqlDbType, DbType?>
    //    {
    //        {SqlDbType.BigInt, DbType.Int64},
    //        {SqlDbType.Binary, DbType.Binary},
    //        {SqlDbType.Bit, DbType.Boolean},
    //        {SqlDbType.Char, DbType.AnsiStringFixedLength},
    //        {SqlDbType.DateTime, DbType.DateTime},
    //        {SqlDbType.Decimal, DbType.Decimal},
    //        {SqlDbType.Float, DbType.Double},
    //        {SqlDbType.Image, DbType.Binary},
    //        {SqlDbType.Int, DbType.Int32},
    //        {SqlDbType.Money, DbType.Decimal},
    //        {SqlDbType.NChar, DbType.StringFixedLength},
    //        {SqlDbType.NText, DbType.String},
    //        {SqlDbType.NVarChar, DbType.String},
    //        {SqlDbType.Real, DbType.Single},
    //        {SqlDbType.UniqueIdentifier, DbType.Guid},
    //        {SqlDbType.SmallDateTime, DbType.DateTime},
    //        {SqlDbType.SmallInt, DbType.Int16},
    //        {SqlDbType.SmallMoney, DbType.Decimal},
    //        {SqlDbType.Text, DbType.String},
    //        {SqlDbType.Timestamp, DbType.Binary},
    //        {SqlDbType.TinyInt, DbType.Byte},
    //        {SqlDbType.VarBinary, DbType.Binary},
    //        {SqlDbType.VarChar, DbType.AnsiString},
    //        {SqlDbType.Variant, DbType.Object},
    //        {SqlDbType.Xml, DbType.Xml},
    //        {SqlDbType.Udt,(DbType?)null}, // Dapper will take care of it
    //        {SqlDbType.Structured,(DbType?)null}, // Dapper will take care of it
    //        {SqlDbType.Date, DbType.Date},
    //        {SqlDbType.Time, DbType.Time},
    //        {SqlDbType.DateTime2, DbType.DateTime2},
    //        {SqlDbType.DateTimeOffset, DbType.DateTimeOffset}
    //    };

    //    public static void Add(this DynamicParameters parameter, string name, object value, SqlDbType? sqlDbType, ParameterDirection? direction, int? size)
    //    {
    //        parameter.Add(name, value, (sqlDbType != null ? sqlDbTypeMap[sqlDbType.Value] : (DbType?)null), direction, size);
    //    }

    //    public static void Add(this DynamicParameters parameter, string name, object value = null, SqlDbType? sqlDbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
    //    {
    //        parameter.Add(name, value, (sqlDbType != null ? sqlDbTypeMap[sqlDbType.Value] : (DbType?)null), direction, size, precision, scale);
    //    }

    //    public static Dictionary<string, object> Get(this DynamicParameters parameter)
    //    {
    //        return parameter.Get<object>();
    //    }

    //    // all the parameters are of the same type T
    //    public static Dictionary<string, T> Get<T>(this DynamicParameters parameter)
    //    {
    //        Dictionary<string, T> values = new Dictionary<string, T>();
    //        foreach (string parameterName in parameter.ParameterNames)
    //            values.Add(parameterName, parameter.Get<T>(parameterName));
    //        return values;
    //    }
    //}

    #endregion

}
