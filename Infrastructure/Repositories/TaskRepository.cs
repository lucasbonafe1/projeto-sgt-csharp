using Dapper;
using SGT.Domain.Repositories;
using SGT.Domain.Entities;
using SGT.Infrastructure.Data;
using Npgsql;
using System.Data;

namespace SGT.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository()
        {
            _connectionString = DbConfig.ConnectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<TaskEntity> Add(TaskEntity entity)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"INSERT INTO tasks 
                                (title, description, duration_in_days, creation_date, end_date, status) 
                            VALUES 
                                (@title, @description, @duration_in_days, @creation_date, @end_date, @status)
                            RETURNING taskId;";

                var id = await connection.QuerySingleAsync<int>(sql, entity);
                entity.Id = id;

                return entity;
            }
        }

        public async Task<IEnumerable<TaskEntity?>> GetAll()
        {
            using (var connection = CreateConnection())
            {
                var sql = @"SELECT * FROM tasks";

                return await connection.QueryAsync<TaskEntity>(sql);
            }
        }

        public async Task<TaskEntity?> GetById(int id)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"SELECT * FROM tasks WHERE taskId = @Id";

                return await connection.QueryFirstOrDefaultAsync<TaskEntity>(sql, new { Id = id });
            }
        }

        public async Task<TaskEntity?> Update(TaskEntity entity, int id)
        {
            var existingTask = await GetById(id);

            if (existingTask == null)
            {
                return null;
            }

            using (var connection = CreateConnection())
            {
                var sql = @"
                            UPDATE tasks 
                            SET title = @title, 
                                description = @description, 
                                duration_in_days = @duration_in_days, 
                                creation_date = @creation_date, 
                                end_date = @end_date, 
                                status = @status  
                            WHERE taskId = @Id";

                entity.Id = id;

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    title = entity.Title,
                    description = entity.Description,
                    duration_in_days = entity.DurationInDays,
                    creation_date = entity.CriationDate,
                    end_date = entity.EndDate,
                    status = entity.Status,
                    Id = id
                });

                if (affectedRows == 0)
                {
                    return null;
                }

                return entity;
            }
        }

        public async Task Delete(TaskEntity entity)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"DELETE FROM tasks WHERE taskId = @Id";
                await connection.ExecuteAsync(sql, new { Id = entity.Id });
            }
        }
    }
}
