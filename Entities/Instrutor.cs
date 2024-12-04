namespace FelpsAcademia.API.Entities
{
    public class Instrutor
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public List<Aula> Aulas { get; set; }  
    }
}