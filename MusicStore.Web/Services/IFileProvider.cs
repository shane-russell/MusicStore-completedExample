namespace MusicStore.Web.Services
{
    public interface IFileProvider
    {
        byte[] GetFileBytes(string relativePath);
    }
}
