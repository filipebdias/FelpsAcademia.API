namespace FelpsAcademia.API.Entities
{
    public class Pagamento
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }  
        public Usuario Usuario { get; set; }  
        public Guid PlanoId { get; set; }   
        public Plano Plano { get; set; }    
    }
}