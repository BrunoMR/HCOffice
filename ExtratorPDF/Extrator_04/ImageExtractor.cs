using iTextSharp.text.exceptions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Extrator
{
    /// <summary>Helper class to extract images from a PDF file. Works with the most 
    /// common image types embedded in PDF files, as far as I can tell.</summary>
    /// <example>
    /// Usage example:
    /// <code>
    /// foreach (var filename in Directory.GetFiles(searchPath, "*.pdf", SearchOption.TopDirectoryOnly))
    /// {
    ///     var images = ImageExtractor.ExtractImages(filename);
    ///     var directory = Path.GetDirectoryName(filename);
    /// 
    ///     foreach (var name in images.Keys)
    ///     {
    ///         images[name].Save(Path.Combine(directory, name));
    ///     }
    /// }
    /// </code></example>
    public class PdfImageExtractor
    {
        #region Methods

        public bool PageContainsImages(PdfReader reader, int pageNumber)
        {
            PdfReaderContentParser parser = null;
            ImageRenderListener listener = null;

            try
            {
                parser = new PdfReaderContentParser(reader);
                listener = null;
                parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
                return listener.Images.Count > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                parser = null;
                listener.Dispose();
            }
        }

        public Dictionary<string, System.Drawing.Image> ExtractImages(PdfReader reader)
        {
            var images = new Dictionary<string, System.Drawing.Image>();

            var parser = new PdfReaderContentParser(reader);
            ImageRenderListener listener = null;

            for (var i = 1; i <= reader.NumberOfPages; i++)
            {
                parser.ProcessContent(i, (listener = new ImageRenderListener()));
                var index = 1;

                if (listener.Images.Count > 0)
                {
                    //Console.WriteLine("Found {0} images on page {1}.", listener.Images.Count, i);

                    foreach (var pair in listener.Images)
                    {
                        //images.Add(string.Format("{0}_Page_{1}_Image_{2}{3}",
                        //    Path.GetFileNameWithoutExtension(".pdf"), i.ToString("D4"), index.ToString("D4"), pair.Value), pair.Key);

                        images.Add
                            (
                                string.Format("{0}_{1}{2}", i.ToString("D4"), index.ToString("D4"), pair.Value),
                                pair.Key
                            );

                        index++;
                    }
                }

                listener.Dispose();
            }

            return images;
        }

        public List<System.Drawing.Image> ExtractImages(PdfReader reader, int paginaInicial)
        {
            List<System.Drawing.Image> images = null;
            PdfReaderContentParser parser = null;
            ImageRenderListener listener = null;
            int count = 0;

            try
            {
                images = new List<System.Drawing.Image>();
                parser = new PdfReaderContentParser(reader);
                listener = null;

                for (var i = paginaInicial; i <= reader.NumberOfPages; i++)
                {
                    parser.ProcessContent(i, listener = new ImageRenderListener());

                    if (listener.Images.Count > 0)
                    {
                        foreach (KeyValuePair<System.Drawing.Image, string> pair in listener.Images)
                        {
                            images.Add(pair.Key);
                            count++;
                        }
                    }

                    listener.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                parser = null;
                listener.Dispose();
            }

            return images;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public class ImageRenderListener : IRenderListener
    {
        System.Drawing.Image _drawingImage;
        PdfImageObject _image;
        Dictionary<System.Drawing.Image, string> _images = new Dictionary<System.Drawing.Image, string>();

        public Dictionary<System.Drawing.Image, string> Images
        {
            get
            {
                return _images;
            }
        }

        #region Methods

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void BeginTextBlock() { }

        public void EndTextBlock() { }

        public void RenderImage(ImageRenderInfo renderInfo)
        {
            try
            {
                _image = renderInfo.GetImage();
            }
            catch (Exception)
            {
                //Cria um objeto do tipo Bitmap
                Bitmap objBitmap = new Bitmap(200, 200);

                //Habilita o objeto bitmap para edição
                Graphics objGraphics = Graphics.FromImage(objBitmap);

                // Desneha retângulo para edição
                objGraphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 200, 200);

                //Adiciona um texto na imagem
                objGraphics.DrawString
                    (
                        "Imagem inválida",
                        new Font("Segoe UI", 14),
                        new SolidBrush(Color.White),
                        25,
                        80
                    );

                // Marca imagem gerada com extenão de imagem inválida
                this.Images.Add(objBitmap, ".inv");

                objGraphics.Dispose();
                //objBitmap.Dispose();
                return;
            }

            #region Propriedades objeto ImageRenderInfo

            //int width = Convert.ToInt32(_image.Get(PdfName.WIDTH).ToString());
            //int bitsPerComponent = Convert.ToInt32(_image.Get(PdfName.BITSPERCOMPONENT).ToString());
            //string subtype = _image.Get(PdfName.SUBTYPE).ToString();
            //int height = Convert.ToInt32(_image.Get(PdfName.HEIGHT).ToString());
            //int length = Convert.ToInt32(_image.Get(PdfName.LENGTH).ToString());

            //string colorspace = string.Empty;
            //PdfName pdfColorspace = (PdfName)_image.Get(PdfName.COLORSPACE);
            //if (pdfColorspace != null)
            //    colorspace = pdfColorspace.ToString();

            #endregion

            _drawingImage = _image.GetDrawingImage();
            PdfName filter = (PdfName)_image.Get(PdfName.FILTER);
            string extension = ".";

            if (filter != null)
            {
                if (filter == PdfName.DCTDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.JPG.FileExtension;
                }
                else if (filter == PdfName.JPXDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.JP2.FileExtension;
                }
                else if (filter == PdfName.FLATEDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.PNG.FileExtension;
                }
                else if (filter == PdfName.LZWDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.CCITT.FileExtension;
                }
                else if (filter == PdfName.CCITTFAXDECODE)
                {
                    extension += PdfImageObject.ImageBytesType.CCITT.FileExtension;
                }
            }

            this.Images.Add(_drawingImage, extension);
            _image = null;
        }

        public void RenderText(TextRenderInfo renderInfo)
        {

            string _getText = renderInfo.GetText();
            int _getTextRenderMode = renderInfo.GetTextRenderMode();
            string _toString = renderInfo.ToString();
            LineSegment _lineSegment = renderInfo.GetAscentLine();
            IList<TextRenderInfo> _list = renderInfo.GetCharacterRenderInfos();

        }

        #endregion Methods
    }
}
