using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrator_29032005
{
    public static class Util
    {
        public static string FileSizeFormat(this long lSize)
        {
            double size = lSize;
            int index = 0;
            for (; size > 1024; index++)
                size /= 1024;
            return size.ToString("0.000 " + new[] { "B", "KB", "MB", "GB", "TB" }[index]);
        }
    }
}
