namespace Entities
{
    public class Aviso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? EditadoEm { get; set; }

        public bool Ativo { get; set; } = true;
    }

}
