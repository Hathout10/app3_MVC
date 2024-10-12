namespace app3.PL.Helper
{
    public class DocumentSetting
    {
        // 1.Upload

        public static string UploadFile(IFormFile file, string FolderName)
        {
            //1. Get location folder path
            //string FolderPath = $"F:\\شيتات\\MVC\\app3_MVC\\app3_PL\\wwwroot\\Files\\Images\\{FolderName}";

            //string FolderPath = Directory.GetCurrentDirectory() + @"wwwroot\Files\Images" + FolderName;

            string FolderPath = Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\Files" , FolderName);

            //2. Get File Name Make it Unique

            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path --> FolderPath + FileName

            string filepath =Path.Combine(FolderPath, FileName);

            // 4. save file as Stream : Data per time

           using var FileStream = new FileStream(filepath,FileMode.Create);

            file.CopyTo(FileStream);

            return FileName;


        }


        // 2. Delete

        public static void DeleteFile(string FileName, string FolderName)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\Images", FolderName,FileName);

           if(File.Exists(FilePath))
             File.Delete(FilePath);


        }

    }
}
