using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SatronusNextResource.Models
{
    public class FileManagement
    {
        public static bool IsError(string file)
        {
            return file == null || file.Length == 0;
        }
        public static bool FileValidation(string path)
        {
            return !File.Exists(path);
        }
        public static void DirectoryValidation(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}