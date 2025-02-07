using BeanOfTheDay.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace BeanOfTheDay.Library.Services
{
    public class BeanService(HttpClient http, IConfiguration configuration)
    {
        public async Task<List<Bean>> GetBeansAsync()
        {
            var data = await http.GetFromJsonAsync<List<Bean>>($"{configuration.GetConnectionString("BeanOfTheDayAPI")}/Bean/GetBeans") ?? [];
            foreach (var item in data)
            {
                item.Description = item.Description?.Replace("\\n", "<br>").Replace("\\r", "").Replace("\n", "<br>").Replace("\r", "");
            }

            return data.OrderByDescending(x => x.IsBOTD).ThenBy(x => x.Index).ToList();
        }
        
        public async Task<HttpResponseMessage> DeleteBeanAsync(string id)
        {
            return await http.DeleteAsync($"{configuration.GetConnectionString("BeanOfTheDayAPI")}/Bean/DeleteBean?id={id}");
        }

        public async Task<HttpResponseMessage> MergeBeanAsync(Bean bean)
        {
            return await http.PostAsJsonAsync($"{configuration.GetConnectionString("BeanOfTheDayAPI")}/Bean/MergeBean", bean);
        }

        public async Task<HttpResponseMessage> ImportBeansAsync(MemoryStream ms)
        {
            using var memoryStream = ms;
            var content = new MultipartFormDataContent();
            var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
            content.Add(byteArrayContent, "file", "uploadedFile.json");

            return await http.PostAsync($"{configuration.GetConnectionString("BeanOfTheDayAPI")}/Bean/ImportBeans", content);
        }
    }
}
