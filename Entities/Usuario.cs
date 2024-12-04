using System.Numerics;

namespace FelpsAcademia.API.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public List<Plano> Planos { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
    }
}