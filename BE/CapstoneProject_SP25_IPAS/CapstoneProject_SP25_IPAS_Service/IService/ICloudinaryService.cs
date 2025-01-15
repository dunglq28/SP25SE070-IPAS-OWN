using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.IService
{
    public interface ICloudinaryService
    {
        public Task<string> UploadImageAsync(IFormFile file, string? folder);
        public Task<bool> DeleteImageByUrlAsync(string url);

        public Task<string> UploadVideoAsync(IFormFile file, string? folder);
        public Task<bool> DeleteVideoByUrlAsync(string url);
    }
}
