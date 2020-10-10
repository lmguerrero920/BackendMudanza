using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMudanza.Constantes
{
    public class Cadenas
    {

        public string cargarArchivo = "CargarArchivo";

        public string attachment = "attachment";
        public string nombreArchivo = "ArchivoFinal.txt";
        public string mediaType = "application/octet-stream";
        public string vacio = "";
      

        public string CargarArchivo
        {
            get => cargarArchivo;set => cargarArchivo = value;
        }
         public string Attachment
        {
            get => attachment; set => attachment = value;
        }
            public string NombreArchivo
        {
            get => nombreArchivo; set => nombreArchivo = value;
        }   
        public string MediaType
        {

            get => mediaType; set => mediaType = value;
        }

        public string Vacio
        {

            get => vacio; set => vacio = value;
        }



        //PostCargarArchivo/{id}
    }
}