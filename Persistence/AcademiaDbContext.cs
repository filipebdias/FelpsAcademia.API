using FelpsAcademia.API.Entities;
namespace FelpsAcademia.API.Persistence
{
    public class AcademiaDbContext
    {
        public List<Usuario> Usuarios { get; set; }
        public List<Plano> Planos { get; set; }
        public List<Treino> Treinos { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
        public List<Instrutor> Instrutores { get; set; }
        public List<Aula> Aulas { get; set; }

        public AcademiaDbContext()
        {
            Usuarios = new List<Usuario>();
            Planos = new List<Plano>();
            Treinos = new List<Treino>();
            Pagamentos = new List<Pagamento>();
            Instrutores = new List<Instrutor>();
            Aulas = new List<Aula>();
        }
    }
}
