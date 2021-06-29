using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IDataAccess
    {
        string ConnectionStringName { get; set; }
        Task<T> Get<T, U>(string sql, U parameters);
        Task<List<T>> GetAll<T, U>(string sql, U parameters);
        Task Insert<T>(string sql, T parameters);
    }
}