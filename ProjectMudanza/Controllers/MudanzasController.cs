using ProjectMudanza.Constantes;
using ProjectMudanza.Database;
using ProjectMudanza.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProjectMudanza.Controllers
{


    [RoutePrefix("api/Mudanza")]

   
    public class MudanzaController : ApiController
    {
        Cadenas cd = new Cadenas();

        //
        [HttpPost]
        [Route("PostCargarArchivo/{id}")]
       

        #region Metodo Post
        public IHttpActionResult PostCargarArchivo(Int64 id)
        {
            try
            {

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                  
                    //obtiene el archivo publicado 
                    var archivo = HttpContext.Current.Request.Files[cd.CargarArchivo.ToString()];
                 

                    Stream fs = archivo.InputStream;
                    var streamReader = new StreamReader(fs);

               
                    var archivoReader = streamReader.ReadToEnd();
                    //lee el archivo
                    archivoReader = archivoReader.Replace("\n", cd.Vacio.ToString());
                //Salta n espacios
                    var listado = archivoReader.Split('\r').ToList();
              //Elimina vacios
                    listado.Remove(cd.Vacio.ToString());

                   
                    var estadoFinal = CargarDiasLaborados(listado, id);

      
                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Accepted)
                    {
                        Content = new StringContent(estadoFinal)
                    };
                    httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(cd.Attachment.ToString())
                    {
                        FileName = cd.NombreArchivo.ToString()
                    };
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(cd.MediaType.ToString());

                    ResponseMessageResult responseMessageResult = ResponseMessage(httpResponseMessage);
                    return responseMessageResult;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        #endregion



        #region  CalculoViajes
        public static int CalculoViajes(List<Int64> listaElementos)
        {
            var pivot = listaElementos.Max();
            listaElementos.Remove(pivot);

            var peso = 0;
            var i = 1;  
            var trayectos = 0;

            while (peso < 50 && pivot < 50)
            {
                if (listaElementos.Count == 0)  
                    return 0;

                var valorMinimo = listaElementos.Min();
                listaElementos.Remove(valorMinimo);
                i++;
                peso = Convert.ToInt32(pivot) * i;
            }

            trayectos++;

            if (listaElementos.Count > 0)
                trayectos += CalculoViajes(listaElementos); 

            return trayectos;
        }
        #endregion

        #region CargarDiasLaborados
        private string CargarDiasLaborados(List<string> txt, Int64 id)
        {
            List<Int64> listaInicial = new List<Int64>();

            listaInicial = txt.Select(x => Convert.ToInt64(x)).ToList();

            Int64 dia = 0;
            Int64 i;

            string resultado = "";


          
            for (int z = 1; z < listaInicial.Count; z++)
            {
                dia++;
                var numeroObjetos = listaInicial[z];
                List<Int64> listaPesoObjetosPorDia = new List<Int64>();

                for (i = z + 1; i <= (z + numeroObjetos); i++)
                {
                    listaPesoObjetosPorDia.Add(listaInicial[Convert.ToInt32(i)]);
                }

                var resultadoxDia = "Case #" + dia + ": " + CalculoViajes( listaPesoObjetosPorDia);

                resultado = string.Concat(resultado, resultadoxDia, Environment.NewLine);

                GuardarArchivo(id, resultadoxDia);

                z = Convert.ToInt32(i) - 1;
            }

            return resultado;
        }
        #endregion


        #region Guardar

         
        private void GuardarArchivo(Int64 id, string resultadoFinal)
        {

            using (var context = new MudanzasDbContext())
            {

                var file = new Mudanza()
                {
                    Id = 0,
                    Document = Convert.ToInt32(id),
                    DateProcess = DateTime.Now,
                    NumberTrips = resultadoFinal
                };

                context.LogMudanza.Add(file);
                context.SaveChanges();
            }
        }
        #endregion

    }


}
