using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entekhab.Common.Utilities
{
    public static class SaveFile
    {
        public async static Task<string> CkEditorAsync(IFormFile file)
        {
            if (file != null)
            {

                //string Max_id = Guid.NewGuid().ToString();
                //FileInfo fi = new FileInfo(file.FileName);
                //string file_new = Max_id.ToString() + fi.Extension;

                try
                {
                    Random rnd = new Random();
                    int id = rnd.Next(1000, 9999);
                    var title = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileName = title.Replace(" ", "-") + "_" + id.ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/FileUpload/ckeditor" ,
                                fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var url = $"{"/FileUpload/ckeditor/"}{fileName}";

                    return url;

                }
                catch (Exception)
                {

                    return null;
                }
            }
            else
            {
                return null;
            }

            
        }
        public async static Task<string> BlogAsync(IFormFile file, int id,string title)
        {
            return (await SaveAsync(file, id, "Blog",title));
        }
        public async static Task<string> ProductAsync(IFormFile file, int id,string title)
        {
            return (await SaveAsync(file, id, "Product",title));
        }
        public async static Task<string> CategoryAsync(IFormFile file, int id, string title)
        {
            return (await SaveAsync(file, id, "Category", title));
        }
        public async static Task<string> SaveAsync(IFormFile file,int id, string subdirectory,string title)
        {
            if (file != null  && !string.IsNullOrEmpty(title))
            {

                //string Max_id = Guid.NewGuid().ToString();
                //FileInfo fi = new FileInfo(file.FileName);
                //string file_new = Max_id.ToString() + fi.Extension;

                try
                {

                var fileName = title.Replace(" ", "-") + "_" + id.ToString()  + Path.GetExtension(file.FileName);
                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/FileUpload/" + subdirectory,
                            fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;

                }
                catch (Exception)
                {

                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
