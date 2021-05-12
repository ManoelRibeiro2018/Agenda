using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapper.Entites
{
    public class Tafera
    {
        public int Id { get; set; }
        public string titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DatePrazo { get; set; }
    }
}
