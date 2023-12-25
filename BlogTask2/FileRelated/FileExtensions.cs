namespace BlogTask2.FileRelated
{

    public static class PathConstants
    {
        public const string BlogImagesPath = "wwwroot/blogimages";
    }

    public static class FileExtensions
    {

        public static async Task<string> SaveFileAndReturnNameAsync(this IFormFile file,string path,IWebHostEnvironment env)
        {
            var root = env.ContentRootPath;

            var folderPath = Path.Combine(root, path);
            var fileName = Guid.NewGuid().ToString().Replace("-","")+file.FileName;

            if (Directory.Exists(folderPath))
            { 
                var newFile = File.Create(Path.Combine(folderPath, fileName));
                await file.CopyToAsync(newFile);
                newFile.Close();
            }

            return fileName;
        }


    }
}
