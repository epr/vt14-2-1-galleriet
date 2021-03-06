﻿using System;
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
                return _gallery ?? (_gallery = new Gallery()); //lazy initialisation av galleriet
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var image = Request.QueryString["image"];
            if (image != null) //om det finns en bild i länken, visa den i originalformat
            {
                Original.ImageUrl = string.Format("~/Images/{0}", image);
                Original.Visible = true;
            }
            if (Request.QueryString["uploaded"] == "true") //om uppladdningen lyckades, visa meddelande
            {
                SuccessMessage.Text = string.Format("Bilden '{0}' har sparats.", image);
                MessageHolder.Visible = true;
            }
            else if (Request.QueryString["uploaded"] == "false") //om uppladdningen misslyckades, visa felmeddelande
            {
                CustomValidator1.IsValid = false;
            }
        }

        protected void UploadImage_Click(object sender, EventArgs e)
        {
            if (IsValid) //om allt är korrekt
            {
                if (ImageUploader.HasFile) //och om det finns en vald bild
                {
                    try
                    {
                        var imageName = Gallery.SaveImage(ImageUploader.FileContent, ImageUploader.FileName); //spara bilden
                        Response.Redirect("Default.aspx?image=" + imageName + "&uploaded=true", false); //lägg bildnamnet i länken
                    }
                    catch
                    {
                        Response.Redirect("Default.aspx?uploaded=false", false); //om uppladdningen misslyckades
                    }
                }

            }
        }

        public IEnumerable<string> ImageRepeater_GetData()
        {
            return Gallery.GetImageNames(); //hämta bildnamn till repeatern
        }

        protected void ImageRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e) //markera den valda tumnagelbilden
        {
            var image = Request.QueryString["image"];
            if (image != null)
            {
                if (e.Item.DataItem.ToString() == image)
                {
                    var l = (HyperLink)e.Item.FindControl("ThumbLink");
                    l.Attributes.Add("class", "selected");
                }
            }
        }
    }
}