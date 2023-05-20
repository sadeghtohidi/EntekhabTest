using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rasad.Common.Utilities
{
    public class UplodedFile
    {
       
        public async Task<string> jsonInsertFiles(IFormFile file, string filepath)
        {
            var FolderName = Path.Combine("wwwroot", filepath);
            var PathToSave = Path.Combine(Directory.GetCurrentDirectory(), FolderName);

            var newFileName = "";
            newFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').ToLower();
            var fullPath = Path.Combine(PathToSave, newFileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return newFileName;
        }
      
    }
}