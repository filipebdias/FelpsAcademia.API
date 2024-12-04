using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FelpsAcademia.API.Entities;

namespace FelpsAcademia.API.Data
{
    public class FelpsAcademiaAPIContext : DbContext
    {
        public FelpsAcademiaAPIContext (DbContextOptions<FelpsAcademiaAPIContext> options)
            : base(options)
        {
        }

        public DbSet<FelpsAcademia.API.Entities.Usuario> Usuario { get; set; } = default!;
        public DbSet<FelpsAcademia.API.Entities.Treino> Treino { get; set; } = default!;
        public DbSet<FelpsAcademia.API.Entities.Plano> Plano { get; set; } = default!;
        public DbSet<FelpsAcademia.API.Entities.Pagamento> Pagamento { get; set; } = default!;
        public DbSet<FelpsAcademia.API.Entities.Instrutor> Instrutor { get; set; } = default!;
        public DbSet<FelpsAcademia.API.Entities.Aula> Aula { get; set; } = default!;
    }
}
