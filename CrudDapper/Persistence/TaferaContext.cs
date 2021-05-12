using CrudDapper.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapper.Persistence
{
    public class TaferaContext : DbContext
    {
        public TaferaContext(DbContextOptions<TaferaContext> options) : base(options)
        {

        }

        public DbSet<Tafera> Taferas { get; set; }
     
    }
}
