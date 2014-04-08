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
            ApprovedExtensions = new Regex("^.*.(jp[e]?g|gif|png)$", RegexOptions.IgnoreCase);
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
        }
        public IEnumerable<string> GetImageNames()
        {
            var images = Directory.GetFiles(PhysicalUploadedImagesPath);
            var list = new List<string>(50);
            FileInfo info;
            foreach (var image in images)
            {
                info = new FileInfo(image);
                list.Add(info.Name);
            }
            list.TrimExcess();
            list.Sort();
            return list.AsReadOnly();
        }
        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagesPath, name));
        }
        private bool IsValidImage(Image image)
        {
            return (image.RawFormat.Guid == ImageFormat.Gif.Guid || image.RawFormat.Guid == ImageFormat.Jpeg.Guid || image.RawFormat.Guid == ImageFormat.Png.Guid);
        }
        public string SaveImage(Stream stream, string fileName)
        {
            var image = Image.FromStream(stream);
            if (IsValidImage(image))
            {
                fileName = SanitizePath.Replace(fileName, "");
                var i = 0;
                while (ImageExists(fileName))
                {
                    fileName = string.Format("{0}({1}){2}", Path.GetFileNameWithoutExtension(fileName), i++, Path.GetExtension(fileName));
                }
                var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
                image.Save(Path.Combine(PhysicalUploadedImagesPath, fileName));
                thumbnail.Save(Path.Combine(PhysicalUploadedImagesPath, "Thumbs", fileName));
                return fileName;
            }
            throw new ArgumentException("Fel MIME-typ.");
        }
    }
}