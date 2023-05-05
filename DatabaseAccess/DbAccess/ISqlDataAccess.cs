using Microsoft.Extensions.Configuration;

namespace DatabaseAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task Save<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}