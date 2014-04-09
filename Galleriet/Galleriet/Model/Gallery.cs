using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Galleriet.Model
{
    public class Gallery
    {
        private static readonly Regex ApprovedExtensions;
        private static string PhysicalUploadedImagesPath;
        private static readonly Regex SanitizePath;
        static Gallery()
        {
            ApprovedExtensions = new Regex("^.*.(jp[e]?g|gif|png)$", RegexOptions.IgnoreCase); //tillåtna filändelser
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images"); //bildmappen
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars))); //ta bort ogiltiga tecken
        }
        public IEnumerable<string> GetImageNames()
        {
            var images = Directory.GetFiles(PhysicalUploadedImagesPath); //hämta bilderna från bildmappen
            var list = new List<string>(50); //skapa en lista som kommer innehålla bilderna
            FileInfo info;
            foreach (var image in images) //gå igenom varje bild
            {
                info = new FileInfo(image);
                list.Add(info.Name); //lägg till bildnamnet i listan
            }
            list.TrimExcess(); //ta bort tomma platser från listan
            list.Sort(); //sortera listan
            return list.AsReadOnly(); //returnera listan
        }
        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagesPath, name)); //finns bilden?
        }
        private bool IsValidImage(Image image)
        {
            return (image.RawFormat.Guid == ImageFormat.Gif.Guid || image.RawFormat.Guid == ImageFormat.Jpeg.Guid || image.RawFormat.Guid == ImageFormat.Png.Guid); //har bilden rätt mime-typ?
        }
        public string SaveImage(Stream stream, string fileName)
        {
            var image = Image.FromStream(stream); //hämta bilden
            if (IsValidImage(image)) //om bilden är giltig
            {
                fileName = SanitizePath.Replace(fileName, "");
                var i = 2;
                var copyName = fileName; //vad kopian kommer att heta
                while (ImageExists(copyName)) //om det redan finns en kopia som heter så
                {
                    copyName = string.Format("{0}({1}){2}", Path.GetFileNameWithoutExtension(fileName), i++, Path.GetExtension(fileName)); //lägg på ett nummer inom parantes
                }
                var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero); //skapa tumnagelbild
                image.Save(Path.Combine(PhysicalUploadedImagesPath, copyName)); //spara bilden
                thumbnail.Save(Path.Combine(PhysicalUploadedImagesPath, "Thumbs", copyName)); //spara tumnagelbilden
                return copyName; //returnera bildnamnet
            }
            throw new ArgumentException("Fel MIME-typ."); //bilden var inte giltig
        }
    }
}