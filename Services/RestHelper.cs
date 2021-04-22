using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using WpfAppApiService.Models;

namespace WpfAppApiService.Services
{
    public static class RestHelper
    {
        private static readonly string baseURL = "http://161.97.144.100:9990/ecare/api/";
        public static async Task<string> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseURL + "person/all"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null )
                        {
                            return data;
                        } 
                    }
                }
            }
            return string.Empty;
        }
        public static string Post(string age, string name, string surname)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var inputdata = new Person();

            inputdata.age = int.Parse(age);
            inputdata.personName = name;
            inputdata.personSurname = surname;
            
            var response = client.PostAsJsonAsync("person/save", inputdata).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Employee Added");
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + 
                    " : Message - " + response.ReasonPhrase);
            }
            return string.Empty;
        }
        public static string Update(int Id ,string age, string name, string surname)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var inputdata = new Person();

            inputdata.age = int.Parse(age);
            inputdata.personName = name;
            inputdata.personSurname = surname;

            var response = client.PostAsJsonAsync("/update/"+Id, inputdata).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Employee Added");
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode +
                    " : Message - " + response.ReasonPhrase);
            }
            return string.Empty;
        }

        internal static void Put(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "person/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var person = response.Content.ReadAsAsync<Person>().Result;
                MessageBox.Show("Employee Found : " + person.personName + 
                    " " + person.personSurname );
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + 
                    " : Message - " + response.ReasonPhrase);
            }
        }

        internal static void Del(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            var url = "person/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User Deleted");
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode +
                    " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
