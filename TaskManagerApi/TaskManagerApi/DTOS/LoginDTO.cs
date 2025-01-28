namespace TaskManagerApi.DTOS
{
    public class LoginDTO
    {
        // As propriedades são obrigatórias, portanto não podem ser nulas.
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
