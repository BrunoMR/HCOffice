using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Extrator_29032005
{
    public class Teste
    {
        public void TestarLeituraPorCoordenada()
        {
            RPI rpi = new RPI();
            rpi.DocPdf.Carregar(@"C:\Users\Carol\Documents\_RPIS\Novos\RPI_TESTE.pdf", 1, rpi.PathRepositorio);

            for (int pageNumber = 1; pageNumber <= rpi.DocPdf.PdfReader.NumberOfPages; pageNumber++)
            {
                int llx = 0;
                int urx = 600;
                //int lly = 795;
                int ury = 800;


                // Percorre linhas do documento
                for (int lly = 795; lly > 0; lly -= 5)
                {
                    RenderFilter[] filters = new RenderFilter[1];
                    LocationTextExtractionStrategy regionFilter = new LocationTextExtractionStrategy();

                    //Rectangle(float llx, float lly, float urx, float ury)
                    //filters[0] = new RegionTextRenderFilter(new Rectangle(31, 15, 356, 1224));

                    filters[0] = new RegionTextRenderFilter(new iTextSharp.text.Rectangle(llx, lly, urx, ury));

                    FilteredTextRenderListener strategy = new FilteredTextRenderListener(regionFilter, filters);
                    string result = PdfTextExtractor.GetTextFromPage(rpi.DocPdf.PdfReader, pageNumber, strategy);

                    //result =
                    //    Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(result)));

                    //decrement upper right Y
                    ury -= 5;
                }

            }

            //return result;
        }

        private static void AdjustPdfContent(PdfReader reader, Func<string, string> adjustingFunc)
        {
            int pageCount = reader.NumberOfPages;

            //Loop through each page
            for (int i = 1; i <= pageCount; i++)
            {
                //Get the current page
                PdfDictionary pageDictionary = reader.GetPageN(i);

                //Get all of the annotations for the current page
                PdfArray annots = pageDictionary.GetAsArray(PdfName.ANNOTS);

                //Make sure we have something
                if ((annots == null) || (annots.Length == 0))
                {
                    continue;
                }

                //Loop through each annotation

                foreach (PdfObject annotation in annots.ArrayList)
                {
                    //Convert the itext-specific object as a generic PDF object
                    PdfDictionary annotationDictionary = (PdfDictionary)PdfReader.GetPdfObject(annotation);

                    //Make sure this annotation has a link
                    if (!annotationDictionary.Get(PdfName.SUBTYPE).Equals(PdfName.LINK))
                    {
                        continue;
                    }

                    //Make sure this annotation has an ACTION
                    if (annotationDictionary.Get(PdfName.A) == null)
                    {
                        continue;
                    }

                    //Get the ACTION reference for the current annotation, each action is an pdfobject
                    PdfObject a = annotationDictionary.Get(PdfName.A);

                    if (a.IsIndirect())
                    {
                        // Get the indirect reference
                        PdfIndirectReference indirectRef = (PdfIndirectReference)a;

                        // Get the GoToR type object which is at the document level
                        PdfDictionary goToR = (PdfDictionary)reader.GetPdfObject(indirectRef.Number);

                        // Get the FileSpec object which is at the document level
                        PdfObject f = goToR.Get(PdfName.F);

                        if (f == null || !f.IsIndirect())
                        {
                            continue;
                        }

                        PdfObject fileSpecObject = reader.GetPdfObject(((PdfIndirectReference)f).Number);

                        if (!fileSpecObject.IsDictionary())
                        {
                            continue;
                        }

                        PdfDictionary fileSpec = (PdfDictionary)fileSpecObject;

                        //This is the link text
                        //PdfObject ufObject = fileSpec.Get(PdfName.UF);
                        //string ufValue = (ufObject == null) ? string.Empty : ufObject.ToString();

                        //Now the link value (the actual file reference
                        string fValue = fileSpec.Get(PdfName.F).ToString(); //This is the file reference
                        //Utils.LogMsg("Found 'indirect' file reference {0}", fValue);

                        //Tweak the value as dictated by the caller's adjusting function
                        var adjustedValue = adjustingFunc(fValue);
                        if (adjustedValue != fValue)
                        {
                            //Utils.LogMsg("\tReplacing with value {0}", adjustedValue);
                            fileSpec.Put(PdfName.F, new PdfString(adjustedValue));
                        }
                    }
                }
            }
        }

        public List<string> ExtrairProcessosXML(string pathRepositorio)
        {
            OpenFileDialog ofdXML = new OpenFileDialog();

            #region Localizar arquivo XML

            ofdXML.InitialDirectory = pathRepositorio;
            ofdXML.Filter = "txt files (*.xml)|*.xml";
            ofdXML.FilterIndex = 2;
            ofdXML.RestoreDirectory = false;

            if (ofdXML.ShowDialog() == DialogResult.Cancel)
                return null;

            #endregion

            XDocument root = XDocument.Load(ofdXML.FileName);

            List<string> list =
                root.Descendants("processo").Select(p => (string)p.FirstAttribute).ToList();

            return list;

        }

        public void TestarLinq()
        {
            List<Pagina> listaPaginas = new List<Pagina>();
            Processo processo;
            Pagina pagina;
            Imagem imagem;

            #region Cria páginas

            pagina = new Pagina(1);

            processo = new Processo { Pagina = 1, Numero = "000000001" };
            pagina.Processos.Add(processo);

            imagem = new Imagem { Pagina = 1, Nome = "00000001_01.jpg" };
            pagina.Imagens.Add(imagem);

            processo = new Processo { Pagina = 1, Numero = "000000002" };
            pagina.Processos.Add(processo);

            imagem = new Imagem { Pagina = 1, Nome = "00000001_02.jpg" };
            pagina.Imagens.Add(imagem);

            listaPaginas.Add(pagina);

            pagina = new Pagina(2);

            processo = new Processo { Pagina = 2, Numero = "000000001" };
            pagina.Processos.Add(processo);

            imagem = new Imagem { Pagina = 2, Nome = "00000002_01.jpg" };
            pagina.Imagens.Add(imagem);

            listaPaginas.Add(pagina);

            pagina = new Pagina(3);

            processo = new Processo { Pagina = 3, Numero = "000000001" };
            pagina.Processos.Add(processo);

            imagem = new Imagem { Pagina = 3, Nome = "00000003_01.jpg" };
            pagina.Imagens.Add(imagem);

            listaPaginas.Add(pagina);

            #endregion

            List<Processo> listproc = new List<Processo>();

            // Percorre páginas
            foreach (Pagina pag in listaPaginas)
            {
                // Extrai lista com todos os processos
                foreach (Processo proc in pag.Processos)
                    listproc.Add(proc);
            }


            //Monta grupos por número de processo onde haja mais que 1
            IEnumerable<IGrouping<string, Processo>> grupo =
                listproc.GroupBy(x => x.Numero)
                        .Where(x => x.Count() > 1);

            List<Processo> listRepetidos = new List<Processo>();

            // Percorre grupos 
            foreach (var groupProc in grupo)
            {
                string chaveGrupo = groupProc.Key;

                //Recupera processos do grupo
                foreach (Processo proc in groupProc)
                    listRepetidos.Add(proc);
            }

        }

        public void TestarRegex()
        {
            Regex _rgxCabecalho
                = new Regex(@"RPI[\s][0-9]{4}", RegexOptions.Compiled);

            // dd/mm/yyyy
            Regex _dataValida
                = new Regex
                    (
                        @"(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))",
                        RegexOptions.Compiled
                    );

            Cabecalho cabecalho = new Cabecalho();
            string entrada = "MARCAS  -  RPI 2312 de 28/04/2015   14 ";

            Match _match = _rgxCabecalho.Match(entrada);
            //Match _match = _dataValida.Match(entrada);

            if (_match.Success)
            {
                //saida.NumeroRPI = "0000";
                //saida.Data = DateTime.Now;

            }

        }

        public void CarregarImagem()
        {
            string path = @"C:\Users\Carol\Documents\1786\Imagens";

            Bitmap bmp = new Bitmap(Path.Combine(path, "00000072_07.tiff"));

            // Rotacionar verticalmente a imagem
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            bmp.Save(Path.Combine(path, "00000072_07_ROTACIONADO.tiff"));
        }

        public void GerarRelatorio()
        {
            StreamWriter writer = null;

            bool _renomeouImagensExtraidas = false;
            string filePath = @"C:\Users\Carol\Documents\Relatório geral.txt";

            if (File.Exists(filePath))
                writer = File.AppendText(filePath);
            else
                writer = File.CreateText(filePath);

            // Excreve no arquivo
            if (_renomeouImagensExtraidas)
            {
                writer.WriteLine
                    (
                        string.Format("RPI {0} - Sim", DateTime.Now.ToLongTimeString())
                    );
            }
            else
            {
                writer.WriteLine
                    (
                        string.Format("RPI {0} - Não", DateTime.Now.ToLongTimeString())
                    );
            }

            writer.Close();
            writer.Dispose();
        }

        public void PdImgFlatDecode()
        {
            string path = @"C:\Users\Carol\Documents\_RPIS\Novos\marcas2235.pdf";

            //PdfReader reader = new PdfReader(path);

            //// We assume that there's a single large picture on the first page
            //PdfDictionary page = reader.GetPageN(1);
            //PdfDictionary resources = page.GetAsDict(PdfName.RESOURCES);
            //PdfDictionary xobjects = resources.GetAsDict(PdfName.XOBJECT);
            //PdfName imgName = xobjects.GetKeys().iterator().next();

            //PRStream imgStream = (PRStream)xobjects.GetAsStream(imgName);
            //imgStream.SetData(PdfReader.GetStreamBytesRaw(imgStream), true);
            //PdfArray array = new PdfArray();
            //array.add(PdfName.FLATEDECODE);
            //array.add(PdfName.DCTDECODE);
            //imgStream.put(PdfName.FILTER, array);
            //PdfStamper stamper = new PdfStamper(reader, new FileOutputStream(dest));
            //stamper.close();
            //reader.close();
        }
    }
}
