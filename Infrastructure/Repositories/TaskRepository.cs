using Dapper;
using SGT.Domain.Interfaces;
using SGT.Domain.Entities;
using SGT.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SGT.Domain.Repositories
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
            return new SqlConnection(_connectionString);
        }

        public async Task<TaskEntity> Add(TaskEntity entity)
        {
            using (var connection = CreateConnection())
            {
                var sql = @"INSERT INTO tasks 
                                (title, description, duration_in_days, criation_date, end_date, status) 
                            VALUES 
                                (@title, @description, @duration_in_days, @criation_date, @end_date, @status);";

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
            // TODO: ADD TRY E CATCH DEPOIS
            var existingUser = await GetById(id);

            if (existingUser == null)
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
                                criation_date = @criacao_date, 
                                end_date = @end_date, 
                                status = @status  
                            WHERE taskId = @Id";

                entity.Id = id;

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    title = entity.Title,
                    description = entity.Description,
                    duration_in_days = entity.DurationInDays,
                    criacao_date = entity.CriationDate,
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

        //TODO: Delete Lógico
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
