using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFARINANGOS5
{
    public class FileAcessHelper
    {
        public static string GetLocalFilePatch(string filePatch)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filePatch);            
        }
    }
}
