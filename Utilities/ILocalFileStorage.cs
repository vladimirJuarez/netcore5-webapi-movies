using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace back_end.Utilities
{
    public interface ILocalFileStorage
    {
        Task DeleteFile(string path, string container);
        Task<string> EditFile(string path, string container, IFormFile file);
        Task<string> SaveFile(string container, IFormFile file);
        
    }
}