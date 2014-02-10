using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Galleriet.Model
{
    public class Gallery
    {
        private static readonly Regex ApprovedExtensions;
        private static string PhysicalUploadedImagesPath;
        private static readonly Regex SanitizePath;
        private static Gallery()
        {

        }
        public IEnumerable<string> GetImageNames()
        {
            return null;
        }
        public static bool ImageExists()
        {
            return false;
        }
        private bool IsValidImage()
        {
            return false;
        }
        public string SaveImage()
        {
            return "";
        }
    }
}