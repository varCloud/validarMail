using Entidades;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilerias;
using Excel = Microsoft.Office.Interop.Excel;
namespace MarcaExcel
{
    public partial class Form1 : Form
    {

        EmailAddressAttribute checkMail = null;
        StreamWriter archivo = null;
        public Form1()
        {
            InitializeComponent();
            checkMail = new EmailAddressAttribute();

            fileArchivoErrores.Filter = fileArchivoMail.Filter = fileDialog.Filter = "CSV files (*.csv)|*.csv|Excel (.xls)|*.xls|All files (*.*)|*.*";
            fileArchivoErrores.Title = fileArchivoMail.Title = fileDialog.Title = "";
            fileArchivoErrores.FileName = fileArchivoMail.FileName = fileDialog.FileName = "";
            this.backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            this.backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            this.backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            this.backgroundWorker1.WorkerReportsProgress = true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {            
            validaMail((e.Argument as List<Persona>));
            MessageBox.Show("Proceso Completo ..!!");
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.txtLIst.Text += " Proceso Terminado ...!! ";
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var item = (e.UserState as Persona);
            this.txtLIst.Text += "Registro: " + e.ProgressPercentage.ToString() + " correo: " + item.correo + " estatus correo: " + item.estatusCorreo+"\r\n";
        }

 
        Regex reg = new Regex("[^a-zA-Z0-9 ]");
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    var csv = new ExcelQueryFactory(fileDialog.FileName);
                    var Personas = from c in csv.Worksheet<Persona>()
                                   select c;
                    archivo = Utilerias.ValidaMail.CrearArchivoCSV("Archivos_Validados", Path.GetFileNameWithoutExtension(this.fileDialog.FileName) + "_validado");
                    var line = string.Format("{0},{1},{2},{3},{4}", "id", "nombre", "telefono", "correo", "municipio");
                    archivo.WriteLine(line);
                    archivo.Flush();
                    this.backgroundWorker1.RunWorkerAsync(Personas.Cast<Persona>().ToList());
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al CSV: " + ex.Message);
            }
        }

        public string RemoveDiacritics(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                text = text.Normalize(NormalizationForm.FormD);
                var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
                return new string(chars).Normalize(NormalizationForm.FormC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        bool invalid = false;
        public bool IsValidEmail(string strIn)
        {

            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();
            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException ex)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        private void Dividir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    var csv = new ExcelQueryFactory(fileDialog.FileName);
                    var Personas = from c in csv.Worksheet<Persona>()
                                   select c;

                    EscribriCSV(Personas.Cast<Persona>().ToList());
                    MessageBox.Show("Proceso Completo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error recorrer el excel");
            }
        }

        public StreamWriter CrearArchivoCSV(int indice, string path = "")
            
        {
            StreamWriter sw = null;
            try
            {
                path = string.IsNullOrEmpty(path) ? "Prospectos" : path;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string file = Path.Combine(path, path + "_" + indice + ".csv");
                if (File.Exists(file))
                    File.Delete(file);
                sw = new StreamWriter(file);
                sw.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al CSV");
            }
            return sw;
        }

        private void btnArchivoMail_Click(object sender, EventArgs e)
        {
            this.fileArchivoMail.ShowDialog();
        }

        public void validaMail(List<Persona> Personas)
        {
            try
            {
                int indiceRegistro = 0;
                int indiceProceso = 0;
                int indicellave = 0;
                int indiceRegistroTotal = 0;
                string[] apiKey = ConfigurationManager.AppSettings["apikey"].Replace(" ", "").Replace("\r\n","").Split(',');
                foreach (Persona item in Personas)
                {

                    try
                    {
                        ResponseValidaCorreo r = ValidaMail.ValidaCorreo(item.correo, apiKey[indicellave]);
                        item.estatusCorreo = r.result.result.ToString();
                        if (r.result.result.ToString().Equals("ok"))
                        {
                            indiceRegistro++;
                            if (!item.nombre.IsNormalized(NormalizationForm.FormD))
                            {
                                item.nombre = RemoveDiacritics(reg.Replace(item.nombre.Normalize(NormalizationForm.FormD), ""));
                            }
                            var line = string.Format("{0},{1},{2},{3},{4}",
                                indiceRegistro,
                                item.nombre.Replace(",", " "),
                                (string.IsNullOrEmpty(item.telefono) ? "0" : item.telefono.Replace(",", "")),
                                item.correo,
                                (string.IsNullOrEmpty(item.municipio) ? " " : item.municipio.Replace(",", "")));
                            archivo.WriteLine(line);
                            archivo.Flush();

                        }
                        if (indiceRegistroTotal == 100)
                        {
                            indicellave++;
                            indiceRegistroTotal = 0;
                        }


                        this.backgroundWorker1.ReportProgress((indiceProceso + 1), item);
                        indiceProceso++;
                        indiceRegistroTotal++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error ->" + ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void EscribriCSV(List<Persona> Personas)
        {
            try
            {
                int indiceRegistro = 0;
                int indiceNombreArchivo = 0;
                int noRegistros = Convert.ToInt32(ConfigurationManager.AppSettings["noRegistrosArchivo"]);
                foreach (Persona item in Personas)
                {
                    if (indiceRegistro == 0 || indiceRegistro > noRegistros)
                    {
                        indiceRegistro = 1;
                        indiceNombreArchivo++;
                        if (archivo != null)
                        {
                            archivo.Close();
                            archivo = null;
                        }
                        archivo = CrearArchivoCSV(indiceNombreArchivo);
                        var line = string.Format("{0},{1},{2},{3},{4}", "id", "nombre", "telefono", "correo","municipio");
                        archivo.WriteLine(line);
                        archivo.Flush();
                    }


                    if (checkMail.IsValid(item.correo))
                    {
                        if (IsValidEmail(item.correo))
                        {
                           
                            if (!item.nombre.IsNormalized(NormalizationForm.FormD))
                            {
                                item.nombre = RemoveDiacritics(reg.Replace(item.nombre.Normalize(NormalizationForm.FormD), ""));
                            }
                            var line = string.Format("{0},{1},{2},{3},{4}",
                                indiceRegistro,
                                item.nombre.Replace(",", " "),
                                (string.IsNullOrEmpty(item.telefono) ? "0" : item.telefono.Replace(",", "")),
                                item.correo,
                                (string.IsNullOrEmpty(item.municipio) ? "N/A" : item.municipio.Replace(",", "")));
                            archivo.WriteLine(line);
                            archivo.Flush();
                            indiceRegistro++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("EscribriCSV: " + ex.Message);
            }
            finally
            {
                if (archivo != null)
                {
                    archivo.Close();
                    archivo = null;
                }
            }
        }

        private void btnArchivoErrores_Click(object sender, EventArgs e)
        {
            this.fileArchivoErrores.ShowDialog();
        }




        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                ResponseValidaCorreo r = ValidaMail.ValidaCorreo(txtValidaMail.Text);
                MessageBox.Show("Es valido :" + r.result.result + " resultado: " + r.result.result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    int indiceRegistro = 1;
                    List<Persona> lstUniverso = new List<Persona>();
                    String[] files = Directory.GetFiles(this.folderBrowserDialog1.SelectedPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        var csv = new ExcelQueryFactory(files[i]);
                        var Personas = from c in csv.Worksheet<Persona>()
                                       select c;
                        lstUniverso.AddRange(Personas);
                    }
                    archivo = Utilerias.ValidaMail.CrearArchivoCSV("Merge", "Merge");
                    var line = string.Format("{0},{1},{2},{3}", "id", "nombre", "telefono", "correo","municipio");
                    archivo.WriteLine(line);
                    archivo.Flush();
                    foreach (var item in lstUniverso)
                    {

                        var _line = string.Format("{0},{1},{2},{3},{4}",
                                    indiceRegistro,
                                    item.nombre.Replace(",", " "),
                                    (string.IsNullOrEmpty(item.telefono) ? "0" : item.telefono.Replace(",", "")),
                                    item.correo,
                                    (string.IsNullOrEmpty(item.municipio) ? "N/A" : item.municipio.Replace(",", "")));

                        archivo.WriteLine(_line);
                        archivo.Flush();
                        indiceRegistro++;

                    }
                }
                MessageBox.Show("Proceso terminado ...!!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
