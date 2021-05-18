using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinal.Contexto
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Manutencao> Manutencaos { get; set; }
        public object Veiculo { get; internal set; }
    }
}
