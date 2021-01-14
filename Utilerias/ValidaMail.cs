using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilerias
{
  public static  class ValidaMail
    {
        public static ResponseValidaCorreo ValidaCorreo(string validar , string api = "")
        {
            ResponseValidaCorreo responseValidaCorreo = new ResponseValidaCorreo();
            try
            {
                //https://app.emailmarker.com/api/verify?apikey=plOxDJ0w6Gb9p2lgmyb1H&email=sapitopicador%40gmail.com
                //https://app.emailmarker.com/api/verify?apiKey=wkzxEIa0EXSMZgcvw62Ow&email=test@example.com
                api = string.IsNullOrEmpty(api) ? ConfigurationManager.AppSettings["apikey"].ToString() : "api="+api;
                string apiURL = ConfigurationManager.AppSettings["apiurl"].ToString() + api  + "&email=" +validar;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
                request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader ostream = new StreamReader(response.GetResponseStream()))
                    {
                        responseValidaCorreo.result = JsonConvert.DeserializeObject<ResultValidaCorreo>(ostream.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseValidaCorreo;
        }

        public static  StreamWriter CrearArchivoCSV(string NombreFolder, string NombreArchivo )
        {
            StreamWriter sw = null;
            try
            {
                if (!Directory.Exists(NombreFolder))
                    Directory.CreateDirectory(NombreFolder);
                string file = Path.Combine(NombreFolder, NombreArchivo + ".csv");
                if (File.Exists(file))
                    File.Delete(file);
                sw = new StreamWriter(file);
                sw.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sw;
        }
    }
}
