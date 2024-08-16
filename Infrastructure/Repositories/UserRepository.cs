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
                var sql = @"INSERT INTO users (name, phone_number, email, password, account_creation_date) VALUES (@Name, @PhoneNumber, @Email, @Password, @AccountCreationDate) RETURNING user_id;";
                var id = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = id;

                return entity;
            }
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            using (var connection = CreateConnection())
            {
                var sql = @"SELECT user_id AS Id, name, phone_number as PhoneNumber, email, account_creation_date as AccountCreationDate FROM users WHERE deleted_at IS null";

                return await connection.QueryAsync<UserEntity>(sql);
            }
        }

        public async Task<UserEntity?> GetById(int id)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"
                            SELECT u.user_id as Id, u.name, u.phone_number as PhoneNumber, u.email, u.password, u.account_creation_date as AccountCreationDate,
                                   t.task_id as Id, t.title, t.description, t.start_date as StartDate, t.end_date as EndDate, t.status, t.user_id as UserId
                            FROM users u
                            LEFT JOIN tasks t ON u.user_id = t.user_id AND t.deleted_at IS null
                            WHERE u.user_id = @Id AND u.deleted_at IS null";

                // cria um dicionário que vai armazenar o usuário e suas tarefas.
                var userDictionary = new Dictionary<int, UserEntity>();


                // executa a consulta async e mapeia os resultados para as entidades UserEntity e TaskEntity.
                var result = await connection.QueryAsync<UserEntity, TaskEntity, UserEntity>(
                    sql,
                    (user, task) => //map
                    {

                        // verifica presença do user no dicionário.
                        if (!userDictionary.TryGetValue(user.Id, out var userEntity))
                        {

                            // Se não estiver, cria uma nova entrada no dicionário.
                            userEntity = user;
                            userEntity.Tasks = new List<TaskEntity>();
                            // adiciona o usuário com a lista de tasks ao dicionário.
                            userDictionary.Add(user.Id, userEntity);
                        }

                        if (task != null)
                        {
                            userEntity.Tasks.Add(task);
                        }

                        return userEntity;
                    },
                    splitOn: "Id", // define onde ocorre a divisão entre User e Task
                    param: new { Id = id }
                );

                return userDictionary.Values.FirstOrDefault(); 
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
                            SET name = @Name,
                                phone_number = @PhoneNumber,
                                email = @Email
                            WHERE user_id = @Id";

                entity.Id = id;

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    name = entity.Name,
                    phoneNumber = entity.PhoneNumber,
                    email = entity.Email,
                    Id = id
                });

                if (affectedRows == 0)
                {
                    return null;
                }

                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                string sqlQuery = @"UPDATE users 
                                SET deleted_at = NOW() 
                                WHERE user_id = @Id";

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    return await connection.ExecuteAsync(sqlQuery, new { Id = id }) > 0;
                }
            }
            catch (PostgresException error)
            {
                throw new ApplicationException("Erro ao efetuar a query sql " + error);
            }
        }
    }
}
