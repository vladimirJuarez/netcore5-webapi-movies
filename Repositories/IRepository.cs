using System.Collections.Generic;
using System.Threading.Tasks;
using back_end.Entidades;

namespace back_end.Repositories
{
    public interface IRepository
    {
        Task<Genre> GetById(int id);
        List<Genre> Get();
    }
}