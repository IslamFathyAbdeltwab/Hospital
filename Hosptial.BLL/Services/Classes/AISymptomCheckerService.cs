using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

public class AISymptomCheckerService
    : IAISymptomCheckerService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AISymptomCheckerService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<SymptomResponseDto>
        CheckSymptoms(string symptoms)
    {
        var apiKey =
            _configuration["OpenRouter:ApiKey"];

        var prompt = $@"
Patient symptoms:
{symptoms}

Return ONLY JSON in this format:

{{
  ""speciality"": ""string"",
  ""possibleDiseases"": [""disease1"", ""disease2""],
  ""advice"": ""string""
}}

Rules:
- Keep response short
- Suggest medical speciality
- Mention possible diseases only
- No markdown
";

        var body = new
        {
            model = "openai/gpt-3.5-turbo",

            messages = new[]
            {
                new
                {
                    role = "user",
                    content = prompt
                }
            }
        };

        var json =
            JsonConvert.SerializeObject(body);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                apiKey);

        _httpClient.DefaultRequestHeaders.Add(
            "HTTP-Referer",
            "http://localhost:4200");

        _httpClient.DefaultRequestHeaders.Add(
            "X-Title",
            "Hospital System");

        var response = await _httpClient.PostAsync(
            "https://openrouter.ai/api/v1/chat/completions",
            content
        );

        if (!response.IsSuccessStatusCode)
        {
            return new SymptomResponseDto
            {
                Speciality = "Unavailable",

                PossibleDiseases =
                    new List<string>(),

                Advice =
                    "AI service unavailable now."
            };
        }

        var responseString =
            await response.Content.ReadAsStringAsync();

        dynamic result =
            JsonConvert.DeserializeObject(responseString);

        string text =
            result.choices[0].message.content.ToString();

        text = text.Replace("```json", "")
                   .Replace("```", "")
                   .Trim();

        return JsonConvert
            .DeserializeObject<SymptomResponseDto>(text);
    }
}