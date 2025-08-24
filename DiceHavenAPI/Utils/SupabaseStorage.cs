using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DiceHaven_API.Utils
{
    public class SupabaseStorage
    {
        private readonly Supabase.Client _client;
        public SupabaseStorage(IConfiguration configuration, Supabase.Client client)
        {
            _client = client;
        }

        public async Task<string> SaveImageFromBase64(string base64Image, string filename, string fileFolder)
        {
            if (string.IsNullOrEmpty(base64Image))
                return null;

            var bucket = _client.Storage.From($"{Environment.GetEnvironmentVariable("SUPABASE_BUCKET")}/{fileFolder}");
            var fileName = $"{Guid.NewGuid()}_{filename}.png";

            var base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64Data);

            await bucket.Upload(imageBytes, fileName);
            var publicUrl = bucket.GetPublicUrl(fileName);

            return publicUrl;
        }

        public async Task DeleteFile(string fileURL, string fileFolder)
        {
            if (string.IsNullOrEmpty(fileURL))
                return;

            var bucket = _client.Storage.From(Environment.GetEnvironmentVariable("SUPABASE_BUCKET"));
            string fileName = Path.GetFileName(new Uri(fileURL).AbsolutePath);
            string path = $"{fileFolder}/{fileName}";

            await bucket.Remove(path);
        }

        public string GetFileNameFromUrl(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return null;

            return Path.GetFileName(new Uri(fileUrl).AbsolutePath);
        }


    }
}
