using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tesis.Comun.Modelo;

namespace Tesis.Web.Models
{
    public class LugarHistoricoVista: LugarHistorico
    {

        [Display(Name = "imagen de lugar historico")]
        public HttpPostedFileBase fotoFilelugar { get; set; }


    }
}