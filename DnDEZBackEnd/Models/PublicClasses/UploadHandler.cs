using DnDEZBackend.Models.SQLObjects;

namespace DnDEZBackend.Models.Public_Classes
{
    public class UploadHandler
    {
        private DnDezdbContext dbContext = new DnDezdbContext();
        public Image getImage(IFormFile file, string folderName)
        {
            string[] validExtensions = [".jpg", ".png", ".jpeg", ".gif", ".webp", ".avif", ".svg", ".jfif"];
            string FileExtension = Path.GetExtension(file.FileName);

            if(!validExtensions.Contains(FileExtension))
            {
                return null;
            }

            long size = file.Length;
            if(size > (1024 *  1024 * 5)) 
            {
                return null;
            }

            string fileName = Guid.NewGuid().ToString() + FileExtension;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Images\\{folderName}");

            using FileStream stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create);
            file.CopyTo(stream);

            Image newImage = new Image()
            {
                ImageId = 0,
                ImagePath = Path.Combine($"Images\\{folderName}", fileName)
            };

            dbContext.Images.Add(newImage);
            dbContext.SaveChanges();

            return newImage;
        }
    }
}
