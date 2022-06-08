using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IFileStorageService
    {
        public Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType);
        public Task DeleteFile(string fileRoute, string containerName);
        public Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType);
    }
}
