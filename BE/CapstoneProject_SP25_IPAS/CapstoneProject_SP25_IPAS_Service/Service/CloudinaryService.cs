﻿using CapstoneProject_SP25_IPAS_Common.Utils;
using CapstoneProject_SP25_IPAS_Service.IService;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<bool> DeleteImageByUrlAsync(string url)
        {
            // Trích xuất publicId từ URL
            var publicId = GetPublicIdFromUrl(url);

            if (string.IsNullOrEmpty(publicId))
            {
                throw new Exception("Invalid URL or publicId could not be extracted.");
            }

            // Xác định các tham số xoá ảnh
            var deletionParams = new DeletionParams(publicId);

            // Thực hiện lệnh xoá trên Cloudinary
            var result = await _cloudinary.DestroyAsync(deletionParams);

            // Kiểm tra kết quả trả về
            if (result.Result == "ok")
            {
                return true; // Đã xóa thành công
            }
            else if (result.Result == "not found")
            {
                // File không tồn tại
                throw new Exception("File not found on Cloudinary.");
            }
            else
            {
                // Xóa thất bại do lý do khác
                throw new Exception($"Deletion failed: {result.Error?.Message ?? "Unknown error"}");
            }
        }

        public async Task<bool> DeleteVideoByUrlAsync(string url)
        {
            // Trích xuất publicId từ URL
            var publicId = GetPublicIdFromUrl(url);

            if (string.IsNullOrEmpty(publicId))
            {
                throw new Exception("Invalid URL or publicId could not be extracted.");
            }

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Video
            };

            // Thực hiện lệnh xoá trên Cloudinary
            var result = await _cloudinary.DestroyAsync(deletionParams);

            // Kiểm tra kết quả trả về
            if (result.Result == "ok")
            {
                return true; // Đã xóa thành công
            }
            else if (result.Result == "not found")
            {
                // File không tồn tại
                throw new Exception("File not found on Cloudinary.");
            }
            else
            {
                // Xóa thất bại do lý do khác
                throw new Exception($"Deletion failed: {result.Error?.Message ?? "Unknown error"}");
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string? folder)
        {
            var uploadResult = new ImageUploadResult();
            var uploadParams = new ImageUploadParams();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    if (folder != null)
                    {
                        uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, stream),

                            // Chỉ định thư mục lưu trữ
                            Folder = folder,

                        };
                    }
                    else
                    {
                       uploadParams = new ImageUploadParams
                       {
                            File = new FileDescription(file.FileName, stream),
                       };
                    }

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            // Kiểm tra kết quả upload và trả về URL
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString(); // URL an toàn qua HTTPS
            }
            else
            {
                throw new Exception($"Image upload failed: {uploadResult.Error?.Message}");
            }
        }

        public async Task<string> UploadVideoAsync(IFormFile file, string? folder)
        {
            long maxSizeInBytes = 100 * 1024 * 1024;
            var uploadVideoParams = new VideoUploadParams();
            if (file.Length > maxSizeInBytes)
            {
                throw new Exception($"File size exceeds the limit of {maxSizeInBytes} MB");
            }

            if(folder != null)
            {
               uploadVideoParams = new VideoUploadParams
               {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = folder,
               };
            }
            else
            {
                uploadVideoParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                };
            }

            var uploadResult = await _cloudinary.UploadAsync(uploadVideoParams);
            if(uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            else
            {
                throw new Exception($"Video upload failed: {uploadResult.Error?.Message}");
            }
           
        }

        private string GetPublicIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("URL cannot be null or empty", nameof(url));

            try
            {
                // Lấy phần đường dẫn của URL sau domain
                var uri = new Uri(url);
                var path = uri.AbsolutePath;

                // Loại bỏ phần domain và các tiền tố upload (cho hình ảnh hoặc video)
                string pathWithoutPrefix = path
                    .Replace("/image/upload/", "")   // Xử lý với ảnh
                    .Replace("/video/upload/", "")  // Xử lý với video
                    .Replace("/upload/", "");        // Xử lý chung cho cả hình ảnh và video

                // Loại bỏ phần "v[version]" nếu có (phiên bản ảnh/video)
                string[] pathParts = pathWithoutPrefix.Split('/');
                string pathWithoutVersion = string.Join("/", pathParts.Skip(2));

                // Loại bỏ phần mở rộng và trả về publicId đầy đủ (bao gồm thư mục và tài nguyên)
                string publicId = pathWithoutVersion.Split('.')[0];

                return publicId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to extract publicId from URL", ex);
            }
        }

    }
}
