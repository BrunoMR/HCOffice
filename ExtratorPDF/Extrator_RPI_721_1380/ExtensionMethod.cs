using System;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Extrator
{
    public static class ExtensionMethod
    {
        public static String GetDescription(this Enum item)
        {
            Type tipo = item.GetType();
            FieldInfo fi = tipo.GetField(item.ToString());

            DescriptionAttribute[] atributos =
                fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (atributos.Length > 0)
                return atributos[0].Description;
            else
                return String.Empty;
        }

        public static String FormatTime(this TimeSpan time)
        {
            string retorno =
                string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);

            return retorno;
        }

        public static String FormatRPIDate(this DateTime date)
        {
            string retorno =
                string.Format("{0:D4}-{1:D2}-{2:D2}", date.Year, date.Month, date.Day);

            return retorno;
        }

        public static ImageFormat GetImageFormat(this Image img)
        {
            if (img.RawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            else if (img.RawFormat.Equals(ImageFormat.Jpeg))
                return ImageFormat.Jpeg;
            if (img.RawFormat.Equals(ImageFormat.Bmp))
                return ImageFormat.Bmp;
            else if (img.RawFormat.Equals(ImageFormat.Emf))
                return ImageFormat.Emf;
            else if (img.RawFormat.Equals(ImageFormat.Exif))
                return ImageFormat.Exif;
            else if (img.RawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Gif;
            else if (img.RawFormat.Equals(ImageFormat.Icon))
                return ImageFormat.Icon;
            else if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
                return ImageFormat.MemoryBmp;
            else if (img.RawFormat.Equals(ImageFormat.Tiff))
                return ImageFormat.Tiff;
            else if (img.RawFormat.Equals(ImageFormat.Wmf))
                return ImageFormat.Wmf;
            else
                return ImageFormat.Png;

        }

        public static string TextoSemMascara(this MaskedTextBox _mask)
        {
            _mask.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            String retString = _mask.Text;
            _mask.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

            return retString;
        }
    }
}
