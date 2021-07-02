using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Persistence.Interfaces;

namespace Persistence
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<T> Get<T, U>(string sql, U parameters)
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            return data;
        }

        public async Task<List<T>> GetAll<T, U>(string sql, U parameters)
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            var data = await connection.QueryAsync<T>(sql, parameters);
            return data.ToList();
        }

        public async Task Insert<T>(string sql, T parameters)
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync(sql, parameters);
        }

        public async Task Delete<T>(string sql, T parameters)
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync(sql, parameters);
        }

        public async Task Update<T>(string sql, T parameters)
        {
            var connectionString = _config.GetConnectionString(ConnectionStringName);
            using IDbConnection connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}