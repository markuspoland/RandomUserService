namespace RandomUserService.Infrastructure.Clients.DTO
{
    public class RandomUserResponse
    {
        public List<RandomUserDto> Results { get; set; }
        public Info Info { get; set; }
    }
}
