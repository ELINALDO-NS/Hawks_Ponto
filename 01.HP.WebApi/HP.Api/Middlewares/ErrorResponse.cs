namespace HP.Api.Middlewares
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public string Mensagem { get; set; }

        public ErrorResponse(string idErro, string mensagem = "Erro inesperado !")
        {
            Id = idErro;
            Data = DateTime.Now;
            Mensagem = mensagem;
        }
    }
}
