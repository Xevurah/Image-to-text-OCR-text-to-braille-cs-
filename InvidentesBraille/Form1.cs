using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;

namespace InvidentesBraille
{
    public partial class Form1 : Form
    {
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
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Image Files(*.png; *.jpg; *.bmp; *.gif)|*.png; *.jpg; *.bmp; *.gif";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
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
                MessageBox.Show("Imagen Cargada Correctamente", "Exito!", MessageBoxButtons.OK);
                pictureBox1.Image = Image.FromFile(filePath);

                var testImagePath = filePath;
                try
                {
                    using (var engine = new TesseractEngine(@"./tessdata", "spa", EngineMode.Default))
                    {
                        using (var img = Pix.LoadFromFile(testImagePath))
                        {
                            using (var page = engine.Process(img))
                            {
                                var text = page.GetText().TrimEnd();
                                for(int i = 0; i < 79; i++)
                                {
                                    Console.WriteLine("indice de foreach: "+ i);
                                    Console.WriteLine("valor: " + braille2D[i, 0]);
                                    Console.WriteLine("remplazo: " + braille2D[i, 1]);
                                    text = text.Replace(braille2D[i, 0], braille2D[i, 1]);
                                }
                                textBox1.AppendText(text);
                            } 
                        }
                    }
                }
                catch (Exception err)
                {
                    Trace.TraceError(err.ToString());
                    Console.WriteLine("Unexpected Error: " + err.Message);
                    Console.WriteLine("Details: ");
                    Console.WriteLine(err.ToString());
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
