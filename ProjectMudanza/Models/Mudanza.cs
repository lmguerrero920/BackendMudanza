using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMudanza.Models
{
    public class Mudanza
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "You must enter {0}")]
      //  [MaxLength(10)]
        public int Document { get; set; }


        [Display(Name = "Fecha de Proceso ")]
        [Required(ErrorMessage = "You must enter {0}")]
        public DateTime DateProcess { get; set; }

        [Display(Name = "Numero de Viajes")]
        [Required(ErrorMessage = "You must enter {0}")]
        public string NumberTrips { get; set; }


    }
}