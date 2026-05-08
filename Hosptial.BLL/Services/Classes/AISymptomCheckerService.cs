using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

public class AISymptomCheckerService : IAISymptomCheckerService
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

    public async Task<SymptomResponseDto> CheckSymptoms(string symptoms)
    {
        var apiKey = _configuration["Gemini:ApiKey"];

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
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
        };

        var json = JsonConvert.SerializeObject(body);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        var url =
    "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key="
    + apiKey;

        var response = await _httpClient.PostAsync(url, content);

        // HANDLE LIMIT ERROR
        if (!response.IsSuccessStatusCode)
        {
            return new SymptomResponseDto
            {
                Speciality = "Unavailable",
                PossibleDiseases = new List<string>(),
                Advice = "AI service busy now. Please try again later"
            };
        }

        var responseString = await response.Content.ReadAsStringAsync();

        dynamic result = JsonConvert.DeserializeObject(responseString);

        string text =
            result.candidates[0].content.parts[0].text.ToString();

        // REMOVE ```json
        text = text.Replace("```json", "")
                   .Replace("```", "")
                   .Trim();

        return JsonConvert.DeserializeObject<SymptomResponseDto>(text);
    }
}