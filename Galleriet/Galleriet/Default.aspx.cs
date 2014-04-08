using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;
using System.IO;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        private Gallery _gallery;
        private Gallery Gallery
        {
            get
            {
                return _gallery ?? (_gallery = new Gallery());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var image = Request.QueryString["image"];
            if (image != null)
            {
                //show image
            }
            if (Request.QueryString["uploaded"] == "true")
            {
                //show success message
            }
            else if (Request.QueryString["uploaded"] == "false")
            {
                //show fail message
            }
        }

        protected void UploadImage_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (ImageUploader.HasFile)
                {
                    try
                    {
                        var imageName = Gallery.SaveImage(ImageUploader.FileContent, ImageUploader.FileName);
                        Response.Redirect("Default.aspx?image=" + imageName + "&uploaded=true");
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("Default.aspx?uploaded=false");
                    }
                }

            }
        }
    }
}