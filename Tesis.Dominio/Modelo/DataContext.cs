using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Dominio.Modelo
{
    public class DataContext:DbContext
    {
        public DataContext():base("DefaultConnection")
        {
            
        }

        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Local> Locals { get; set; }

        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Sucursal> Sucursals { get; set; }

        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.LugarHistorico> LugarHistoricoes { get; set; }

        public System.Data.Entity.DbSet<Tesis.Comun.Modelo.Usuario> Usuarios { get; set; }
    }
}
