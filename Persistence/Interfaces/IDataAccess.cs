using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IDataAccess
    {
        string ConnectionStringName { get; set; }
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
    }
}