using Dapper;
using DapperNightProject.Context;
using DapperNightProject.Dtos.EmployeeDtos;

namespace DapperNightProject.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DapperContext _context;

        public EmployeeService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            string query = "INSERT INTO TblEmployee (Name, Surname, Salary) VALUES " +
                "(@name, @surname, @salary)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", createEmployeeDto.Name);
            parameters.Add("@surname", createEmployeeDto.Surname);
            parameters.Add("@salary", createEmployeeDto.Salary);
            var con = _context.CreateConnection();
            await con.ExecuteAsync(query, parameters);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            string query = "DELETE FROM TblEmployee WHERE EmployeeId = @p1";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", id);
            var conn = _context.CreateConnection();
            await conn.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultEmployeeDto>> GetAllEmployeesAsync()
        {
            string query = "SELECT * FROM TblEmployee";
            var conn = _context.CreateConnection();
            var values = await conn.QueryAsync<ResultEmployeeDto>(query);
            return values.ToList();
        }

        public async Task<GetEmployeeByIdDto> GetEmployeeByIdAsync(int id)
        {
            string query = "SELECT * FROM TblEmployee WHERE EmployeeId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var conn = _context.CreateConnection();
            var values = await conn.QueryFirstAsync<GetEmployeeByIdDto>(query, parameters);
            return values;
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto)
        {
            string query = "UPDATE TblEmployee SET Name = @name, Surname = @surname, Salary = @salary " +
                "WHERE EmployeeId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@name", updateEmployeeDto.Name);
            parameters.Add("@surname", updateEmployeeDto.Surname);
            parameters.Add("@salary", updateEmployeeDto.Salary);
            parameters.Add("@id", updateEmployeeDto.EmployeeId);
            var conn = _context.CreateConnection();
            await conn.ExecuteAsync(query, parameters);
        }
    }
}
