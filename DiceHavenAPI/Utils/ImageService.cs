using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

public class ImageService
{
    private readonly string _imageDirectory;

    public ImageService(IConfiguration configuration)
    {
        _imageDirectory = configuration["ImageStorage:Path"];

        if (!Directory.Exists(_imageDirectory))
        {
            Directory.CreateDirectory(_imageDirectory);
        }
    }

    public string? SaveImageFromBase64(string base64Image)
    {
        try
        {
            if (string.IsNullOrEmpty(base64Image))
                return null;
            var base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);

            byte[] imageBytes = Convert.FromBase64String(base64Data);

            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            string fileName = $"image_{timestamp}.png";

            string filePath = Path.Combine(_imageDirectory, fileName);

            File.WriteAllBytes(filePath, imageBytes);

            return filePath;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao salvar a imagem", ex);
        }
    }

    public string? GetImageAsBase64(string relativeOrFullPath)
    {
        try
        {
            if (string.IsNullOrEmpty(relativeOrFullPath))
                return null;
            string fullPath = Path.IsPathRooted(relativeOrFullPath)
                ? relativeOrFullPath
                : Path.Combine(_imageDirectory, relativeOrFullPath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Arquivo de imagem não encontrado.", fullPath);

            byte[] imageBytes = File.ReadAllBytes(fullPath);

            string base64 = Convert.ToBase64String(imageBytes);

            string mimeType = GetMimeType(fullPath);
            return $"data:{mimeType};base64,{base64}";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao converter a imagem para base64.", ex);
        }
    }

    public bool DeleteImage(string relativeOrFullPath)
    {
        try
        {
            if (string.IsNullOrEmpty(relativeOrFullPath))
                return false;

            string fullPath = Path.IsPathRooted(relativeOrFullPath)
                ? relativeOrFullPath
                : Path.Combine(_imageDirectory, relativeOrFullPath);

            if (!File.Exists(fullPath))
                return false;

            File.Delete(fullPath);
            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Erro ao excluir a imagem.", ex);
        }
    }


    private string GetMimeType(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLower();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            _ => "application/octet-stream"
        };
    }
}
