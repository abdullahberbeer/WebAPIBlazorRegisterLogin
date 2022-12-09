namespace WebAPI.Helper
{
    public interface IUploadHelper
    {
         Task<string> UploadFile(string avatar);
    }
}
