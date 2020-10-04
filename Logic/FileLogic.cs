using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S3_Presigned_Keys_jQuery.Logic
{
    public class FileLogic
    {
        public static string CalculateMimeType(string extension)
        {
            var mimetypes = GetAcceptedMimetypes();

            extension = Path.GetExtension(extension);

            var matchedMimeType = mimetypes
                .Where(m => m.Extension.ToLower() == extension.ToLower())
                .FirstOrDefault();

            if (matchedMimeType != null)
            {
                return matchedMimeType.Mimetype;
            }
            else
            {
                return null;
            }
        }

        public static List<Models.AcceptedMimetypes> GetAcceptedMimetypes()
        {
            List<Models.AcceptedMimetypes> mimetypes = new List<Models.AcceptedMimetypes>();

            mimetypes.Add(new Models.AcceptedMimetypes
            {
                Extension = ".mp4",
                Mimetype = "video/mp4"
            });

            mimetypes.Add(new Models.AcceptedMimetypes
            {
                Extension = ".webm",
                Mimetype = "video/webm"
            });

            mimetypes.Add(new Models.AcceptedMimetypes
            {
                Extension = ".mkv",
                Mimetype = "video/x-matroska"
            });

            return mimetypes;
        }
    }
}
