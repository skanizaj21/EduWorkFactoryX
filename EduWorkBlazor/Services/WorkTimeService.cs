namespace EduWorkBlazor.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using DataAccess.Entities;

    public class WorkTimeService
    {
        private readonly HttpClient _httpClient;

        public WorkTimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WorkTime>> GetWorkTimes()
        {
            return await _httpClient.GetFromJsonAsync<List<WorkTime>>("api/WorkTime");
        }

        public async Task<WorkTime> GetWorkTime(int id)
        {
            return await _httpClient.GetFromJsonAsync<WorkTime>($"api/WorkTime/{id}");
        }

        public async Task AddWorkTime(WorkTime workTime)
        {
            await _httpClient.PostAsJsonAsync("api/WorkTime", workTime);
        }

        public async Task UpdateWorkTime(int id, WorkTime workTime)
        {
            await _httpClient.PutAsJsonAsync($"api/WorkTime/{id}", workTime);
        }

        public async Task DeleteWorkTime(int id)
        {
            await _httpClient.DeleteAsync($"api/WorkTime/{id}");
        }
    }

}
