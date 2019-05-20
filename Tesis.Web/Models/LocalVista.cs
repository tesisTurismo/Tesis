using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesis.Comun.Modelo;

namespace Tesis.Web.Models
{
    public class LocalVista:Local
    {
        public HttpPostedFileBase fotoFile { get; set; }
    }
}