using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Comun.Modelo;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Web.Models
{
    public class CategoriaVista: Categoria
    {
        [Display(Name= "imagen de categoria")]
        public HttpPostedFileBase fotoFileCat { get; set; }


    }
}