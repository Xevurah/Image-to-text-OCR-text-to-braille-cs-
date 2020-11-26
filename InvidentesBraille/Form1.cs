using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tesseract;
using Image = System.Drawing.Image;

namespace InvidentesBraille
{
    public partial class Form1 : Form
    {
        public StringBuilder sb = new StringBuilder();
        public Form1()
        {
            InitializeComponent();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            char[,] braille2D = new char[,] {
                
                //{ " el ", " ⠮ " },
                //{ " y ", " ⠯ " },
                //{ " de ", " ⠷ " },
                //{ " con ", " ⠾ " },
                //{ " en ", " ⠔ " },
                //{ " o ", " ⠖ " },
                //{ " para ", " ⠿ " },
                { ',', '⠠' },
                { '/', '⠌' },
                { '?', '⠹' },
                { '.', '⠨' },
                { 'á', '⠁' },
                { 'é', '⠑' },
                { 'í', '⠊' },
                { 'ó', '⠕' },
                { 'ú', '⠥' },
                { 'a', '⠁' },
                { 'b', '⠃' },
                { 'c', '⠉' },
                { 'd', '⠙' },
                { 'e', '⠑' },
                { 'f', '⠋' },
                { 'g', '⠛' },
                { 'h', '⠓' },
                { 'i', '⠊' },
                { 'j', '⠚' },
                { 'k', '⠅' },
                { 'l', '⠇' },
                { 'm', '⠍' },
                { 'ñ', '⠝' },
                { 'n', '⠝' },
                { 'o', '⠕' },
                { 'p', '⠏' },
                { 'q', '⠟' },
                { 'r', '⠗' },
                { 's', '⠎' },
                { 't', '⠞' },
                { 'u', '⠥' },
                { 'v', '⠧' },
                { 'w', '⠺' },
                { 'x', '⠭' },
                { 'y', '⠽' },
                { 'z', '⠵' },
                { 'A', '⠁' },
                { 'Á', '⠁' },
                { 'B', '⠃' },
                { 'C', '⠉' },
                { 'D', '⠙' },
                { 'E', '⠑' },
                { 'É', '⠑' },
                { 'F', '⠋' },
                { 'G', '⠛' },
                { 'H', '⠓' },
                { 'I', '⠊' },
                { 'Í', '⠊' },
                { 'J', '⠚' },
                { 'K', '⠅' },
                { 'L', '⠇' },
                { 'M', '⠍' },
                { 'N', '⠝' },
                { 'O', '⠕' },
                { 'Ó', '⠕' },
                { 'P', '⠏' },
                { 'Q', '⠟' },
                { 'R', '⠗' },
                { 'S', '⠎' },
                { 'T', '⠞' },
                { 'U', '⠥' },
                { 'Ú', '⠥' },
                { 'V', '⠧' },
                { 'W', '⠺' },
                { 'X', '⠭' },
                { 'Y', '⠽' },
                { 'Z', '⠵' },
                { '#', '⠼' },
                { '1', '⠂' },
                { '2', '⠆' },
                { '3', '⠲' },
                { '4', '⠲' },
                { '5', '⠢' },
                { '6', '⠖' },
                { '7', '⠶' },
                { '8', '⠦' },
                { '9', '⠔' },
                { '0', '⠴' },
                { ' ', ' ' }
            };


            //var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                sb.Append("se crea openfiledialog\n");
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Image Files(*.png; *.jpg; *.bmp; *.gif)|*.png; *.jpg; *.bmp; *.gif";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sb.Append("se abre openfile dialog\n");
                    //Get the path of specified file
                    filePath = openFileDialog1.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog1.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }


            if (filePath != null && filePath != "")
            {
                //sb.Append("se valida el filepath\n");
                MessageBox.Show("Imagen Cargada Correctamente", "Exito!", MessageBoxButtons.OK);
                pictureBox1.Image = Image.FromFile(filePath);

                var testImagePath = filePath;
                try
                {
                    //sb.Append("entra en try de tesseract\n");
                    using (var engine = new TesseractEngine(@"./tessdata", "spa", EngineMode.Default))
                    {
                        //sb.Append("entra en tesseract engine\n");
                        using (var img = Pix.LoadFromFile(testImagePath))
                        {
                            //sb.Append("usa pix para loadfromimage\n");
                            using (var page = engine.Process(img))
                            {
                                //sb.Append("hace engine process para path de imagen\n");
                                var text = page.GetText().TrimEnd();
                                //sb.Append("justo antes del for para replace\n");
                                for (int i = 0; i < 79; i++)
                                {
                                    //Console.WriteLine("indice de foreach: " + i);
                                    //Console.WriteLine("valor: " + braille2D[i, 0]);
                                    //Console.WriteLine("remplazo: " + braille2D[i, 1]);
                                    text = text.Replace(braille2D[i, 0], braille2D[i, 1]);
                                }
                                textBox1.Text = text;
                                var txtInput = Path.GetTempPath() + "log.txt";
                                File.WriteAllText(txtInput, text);
                                //sb.Append("se termina de escribir el output en un path temporal\n");
                                //File.AppendAllText("logger.txt", //sb.ToString());
                                //sb.Clear();
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    //sb.Append("Se encontro algun error durante el try tesseract\n"+ err.ToString()+"\n");
                    Trace.TraceError(err.ToString());
                    Console.WriteLine("Unexpected Error: " + err.Message);
                    Console.WriteLine("Details: ");
                    Console.WriteLine(err.ToString());
                    //File.AppendAllText("logger.txt", //sb.ToString());
                    //sb.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF Files|*.pdf";
            dlg.FilterIndex = 0;

            string fileName = string.Empty;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var txtOutput = dlg.FileName;

                //sb.Append("Entra al envio de PDF\n");
                //  public static final String FONT = "resources/fonts/Cardo-Regular.ttf";
                //  public static final String TEXT = "The Cardo family of fonts supports this character: \u2609";
                //
                //  public void createPdf(String dest) throws IOException, DocumentException {
                //  Document document = new Document();
                //  PdfWriter.getInstance(document, new FileOutputStream(dest));
                //  document.open();
                //  BaseFont bf = BaseFont.createFont(FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                //  Font f = new Font(bf, 12);
                //  Paragraph p = new Paragraph(TEXT, f);
                //  document.add(p);
                //  document.close();
                //}

                BaseFont bf = BaseFont.CreateFont("font/EversonMono.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font f = new Font(bf, 12);

                var txtInput = Path.GetTempPath() + "log.txt";
                //Read the Data from Input File
                StreamReader rdr = new StreamReader(txtInput);
                //sb.Append("inicia streamreader\n");
                //Create a New instance on Document Class
                Document doc = new Document();
                //Create a New instance of PDFWriter Class for Output File
                try
                {
                    //sb.Append("hace try de filestream\n");
                    PdfWriter.GetInstance(doc, new FileStream(txtOutput, FileMode.Create));
                }
                catch (Exception err)
                {
                    //sb.Append("hubo error de filestream\n");
                }
                //Open the Document
                doc.Open();
                //sb.Append("abre instancia de doc\n");
                //Add the content of Text File to PDF File
                doc.Add(new Paragraph(rdr.ReadToEnd(), f));
                //sb.Append("agrega font a doc\n");
                //Close the Document
                doc.Close();
                //sb.Append("cierra instancia de doc\n");
                //Open the Converted PDF File
                Process.Start(txtOutput);
                rdr.Close();
                //sb.Append("abre pdf\n");
                //File.Delete(txtInput);
                //sb.Append("borra txt\n");
                //File.AppendAllText("logger.txt", //sb.ToString());
                //sb.Clear();
            }
        }
    }
}
