﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Helpers
{
    public static class ConvertTo
    {
        public static byte[] DocumentToByteArray(HttpPostedFileBase archive)
        {
            Stream fs = archive.InputStream;
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            byte[] fileContent = binaryReader.ReadBytes(archive.ContentLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();

            return fileContent;
        }

        public static byte[] ImageToByteArray(HttpPostedFileBase archive)
        {
            Image img = Image.FromStream(archive.InputStream, true, true);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}