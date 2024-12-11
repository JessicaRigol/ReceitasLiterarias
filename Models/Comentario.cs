namespace ReceitasLiterarias.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string NomeReceita { get; set; } // Identificador da receita
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
    }

}