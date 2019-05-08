using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Tesis.Web.Models

{
    using Tesis.Dominio.Modelo;
    public class LocalDataContext:DataContext
    {
        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Local> Locals { get; set; }
        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Categoria> Categorias { get; set; }
        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.LugarHistorico> LugarHistoricoes { get; set; }
        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Sucursal> Sucursals { get; set; }
        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Usuario> Usuarios { get; set; }


    }
}