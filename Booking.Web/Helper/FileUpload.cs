namespace Booking.Web.Helper;

public class FileUpload
{
    /// <summary>
    /// Uploads a file to a subdirectory of wwwroot, creates the directory if missing.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="webRootPath"></param>
    /// <param name="targetFolder"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<string> UploadFileAsync(IFormFile file, string targetFolder, CancellationToken token)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file), "No file was uploaded.");

        if (file.Length == 0)
            throw new InvalidOperationException("Uploaded file is empty.");

        string? filename = Path.GetFileName(file.FileName);

        if (string.IsNullOrEmpty(filename)) throw new InvalidOperationException("Uploaded file does not have file name.");

        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", targetFolder);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string originalExtension = Path.GetExtension(filename) ?? string.Empty;
        string safeFileName = Path.GetFileNameWithoutExtension(filename) ?? string.Empty;
        string finalFileName = $"{safeFileName.Replace(" ", "_")}_{Guid.NewGuid()}{originalExtension}";
        string fullPath = Path.Combine(folderPath, finalFileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream, token);

        // Return relative web path (slashes for URL)
        return Path.Combine("/", targetFolder, finalFileName).Replace(" ", "%").Replace("\\", "/");
    }
}
