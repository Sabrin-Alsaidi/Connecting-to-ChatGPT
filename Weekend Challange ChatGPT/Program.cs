using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
class Program
{
    //private static HttpClient httpClient = new HttpClient();
    public static string apiKey = "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";
    public static string apiURL = "https://api.openai.com/v1/completions";

    static async Task Main()
    {
        do
        {
            //Ask the user to input his question
            Console.WriteLine("Type your question here :  (Type exit if you want to exit the program)");
            String UserQuestion = Console.ReadLine();

            //check if the user entered exit or not / it yes get out the loop and the program
            if (UserQuestion.ToLower() == "exit")
            {
                break;
            }
          
            else
            {
                //outputing the answer from ChatGPT 
                string GPTAnswer = await ChatGPTAnswer(UserQuestion);
                Console.WriteLine($"The Answer of your question is : {GPTAnswer}");
                Console.WriteLine("---------------------------------");
                Console.WriteLine();
            }
        } while (true);
    }

    // getting the answer from ChatGPT 
    public static async Task<string> ChatGPTAnswer(string question)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");


        var answerRequest = new
        {
            model = "text-davinci-003",
            prompt = question,
            maxTokens = 250
        };

        //string jsonPayLoad = JsonConvert.SerializeObject(answerRequest);
       // StringContent content = new StringContent(jsonPayLoad, Encoding.UTF8, "application/json");

            //var request = new HttpRequestMessage(HttpMethod.Post, apiURL);
            //request.Headers.Add("Authorization", $"Bearer {apiKey}");
            //request.Content = new StringContent(jsonPayLoad,Encoding-UTF8,"application/Jason");
           // HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            var response = await httpClient.PostAsJsonAsync(apiURL, answerRequest);

        if (response.IsSuccessStatusCode)
        {
                //return await response.Content.ReadAsStringAsync();
                var respondData = await response.Content.ReadAsAsync<dynamic>();
                //ReadAsAsync<dynamic>();
                 return respondData.choices[0].text;
            }
        else
        {
           // return $"Error: {response.StatusCode}";
            return $"Error: {response.StatusCode} - {response.ReasonPhrase}\n{await response.Content.ReadAsStringAsync()}";


            }

        }

    }
}

