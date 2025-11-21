namespace Entities.DTO
{
    public class AvisoDto
    {
        public int Id { get; set; }            // Para update
        public string Titulo { get; set; }     // Para create
        public string Mensagem { get; set; }   // Para create/update
    }
}
