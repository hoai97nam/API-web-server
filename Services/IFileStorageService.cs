using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IFileStorageService
    {
        public Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute);
        public Task DeleteFile(string fileRoute, string containerName);
        public Task<string> Save(byte[] content, string extension, string containerName);
    }
}
