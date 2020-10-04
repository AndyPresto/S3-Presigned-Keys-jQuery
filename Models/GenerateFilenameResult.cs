using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3_Presigned_Keys_jQuery.Models
{
    public class GenerateFilenameResult
    {
        public string PresignedUrl { get; set; }
        public string FileName { get; set; }
    }
}
