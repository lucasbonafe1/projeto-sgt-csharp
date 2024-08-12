using Dapper;
using SGT.Domain.Repositories;
using SGT.Infrastructure.Data;
using SGT.Domain.Entities;
using System.Data;
using Npgsql;

namespace SGT.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = DbConfig.ConnectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<UserEntity> Add(UserEntity entity)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"INSERT INTO users (name, email, password) VALUES (@name, @email, @password) RETURNING userId;";
                var id = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = id;

                return entity;
            }
        }

        public async Task<IEnumerable<UserEntity?>> GetAll()
        {
            using (var connection = CreateConnection())
            {
                var sql = @"SELECT * FROM users";

                return await connection.QueryAsync<UserEntity>(sql);
            }
        }

        public async Task<UserEntity?> GetById(int id)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"SELECT * FROM users WHERE userId = @Id";

                return await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { Id = id });
            }
        }

        //FIXME: REVER LÓGICA
        public async Task<IEnumerable<TaskEntity?>> GetTasksByUserId(int id)
        {
            using (var connection = CreateConnection())
            { 
                var sql = @"SELECT * FROM Tasks WHERE userId = @Id";

                return await connection.QueryAsync<TaskEntity>(sql, new { Id = id });
            }
        }

        public async Task<UserEntity?> Update(UserEntity entity, int id)
        {
            // TODO: ADD TRY E CATCH DEPOIS
            var existingUser = await GetById(id);

            if (existingUser == null)
            {
                return null;
            }

            using (var connection = CreateConnection())
            {
                var sql = @"
                            UPDATE users
                            SET name = @name,
                                email = @email,
                                password = @password
                            WHERE userId = @Id";

                entity.Id = id;

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    name = entity.Name,
                    email = entity.Email,
                    password = entity.Password,
                    Id = id
                });

                if (affectedRows == 0)
                {
                    return null;
                }

                return entity;
            }
        }

        // TODO: Implementar Delete Lógico
        public async Task Delete(UserEntity entity)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"DELETE FROM users WHERE userId = @Id";
                await connection.ExecuteAsync(sql, new { Id = entity.Id });
            }
        }

    }
}
