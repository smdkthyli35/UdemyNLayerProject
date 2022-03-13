using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.ApiService
{
    public class PersonApiService
    {
        private readonly HttpClient _httpClient;

        public PersonApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            IEnumerable<PersonDto> personDtos;

            var response = await _httpClient.GetAsync("persons");
            if (response.IsSuccessStatusCode)
            {
                personDtos = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                personDtos = null;
            }
            return personDtos;
        }

        public async Task<PersonDto> AddAsync(PersonDto personDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(personDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("persons", stringContent);
            if (response.IsSuccessStatusCode)
            {
                personDto = JsonConvert.DeserializeObject<PersonDto>(await response.Content.ReadAsStringAsync());
                return personDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"persons/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PersonDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Update(PersonDto personDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(personDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("persons", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _httpClient.DeleteAsync($"persons/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
