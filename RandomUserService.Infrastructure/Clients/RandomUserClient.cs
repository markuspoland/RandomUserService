using RandomUserService.Domain.Entities;
using RandomUserService.Infrastructure.Clients.DTO;
using RandomUserService.Infrastructure.Clients.Interfaces;
using RandomUserService.Infrastructure.Clients.Mappers;
using System.Net.Http.Json;

namespace RandomUserService.Infrastructure.Clients
{
    public class RandomUserClient(HttpClient _httpClient) : IRandomUserClient
    {
        public string Url => "https://randomuser.me/api/";
        public async Task<User> GetRandomUserAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<RandomUserResponse>(Url);

            var userDto = response?.Results?.FirstOrDefault();

            if (userDto == default)
                throw new Exception("No users found.");

            return userDto.ToDomainUser();
        }
    }
}
