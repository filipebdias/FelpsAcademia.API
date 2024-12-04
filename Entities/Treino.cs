namespace FelpsAcademia.API.Entities
{
    public class Treino
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataTreino { get; set; }
        public Usuario Usuario { get; set; }
        public List<Aula> Aulas { get; set; }
    }

}
