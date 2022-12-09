using WebAPI.Models;

namespace WebAPI.Helper
{
    public class UploadHelper : IUploadHelper
    {
        //public async Task<string> UploadFilee(byte[] bytes, string fileName)
        //{
        //    string uploadsFolder = Path.Combine("Images", fileName);
        //    Stream stream = new MemoryStream(bytes);
        //    using (var ms = new FileStream(uploadsFolder, FileMode.Create))
        //    {
        //        await stream.CopyToAsync(ms);

        //    }
        //    return uploadsFolder;

        //}
        public async Task<string> UploadFile(string avatar)
        {
            if (!string.IsNullOrWhiteSpace(avatar))
            {
                byte[] imgBytes = Convert.FromBase64String(avatar);
                string filesName = $"{Guid.NewGuid()}_{DateTime.Now.ToShortDateString().ToString().Trim()}.jpeg";
                string uploadsFolder = Path.Combine("Images", filesName);
                Stream stream = new MemoryStream(imgBytes);
                using (var ms = new FileStream(uploadsFolder, FileMode.Create))
                {
                    await stream.CopyToAsync(ms);

                }
                return uploadsFolder;
            }
            else
            {
                return "Resim yüklenirken hata oluştu";
            }

            
          
        }

        //public async Task<string> UploadFile(byte[] bytes, string fileName)
        //{
        //    string uploadsFolder = Path.Combine("Images", fileName);
        //    Stream stream = new MemoryStream(bytes);
        //    using (var ms = new FileStream(uploadsFolder, FileMode.Create))
        //    {
        //        await stream.CopyToAsync(ms);

        //    }
        //    return uploadsFolder;
        //}

    }
}
