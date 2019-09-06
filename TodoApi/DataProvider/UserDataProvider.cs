using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using User.DataProvider;
using TodoApi.Models;

namespace User.DataProvider
{
    public class UserDataProvider : IUserDataProvider{

        private readonly string connectionString = "Data Source=(local);Initial Catalog=TestApiDb;Persist Security Info=True;User ID=sa;Password=admindamsa02";

        //private SqlConnection sqlConnection;

        public async Task AddUser(Users user){
            using (var sqlConnection = new SqlConnection(connectionString)){
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@FullName", user.FullName);
                dynamicParameters.Add("@Password", user.Password);

                await sqlConnection.ExecuteAsync(
                    "spAddUser",
                    dynamicParameters,
                    commandType : CommandType.StoredProcedure);
            }
        }
        public async Task DeleteUser(int UserId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", UserId);
                await sqlConnection.ExecuteAsync(
                    "spDeleteUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Users> GetUser(int UserId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserId", UserId);
                return await sqlConnection.QuerySingleOrDefaultAsync<Users>(
                    "spGetUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetUsers()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Users>(
                    "spGetUsers",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }
// 
        public async Task UpdateUser(Users user)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", user.UserName);
                dynamicParameters.Add("@Password", user.Password);
                await sqlConnection.ExecuteAsync(
                    "spUpdateUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}