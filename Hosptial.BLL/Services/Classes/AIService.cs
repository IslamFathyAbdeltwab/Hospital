using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
//for help the patient to understand his prescription by using AI to explain it in simple language
public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AIService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> ExplainPrescription(
        AddPrescriptionViewModel prescription)
    {
        var apiKey =
            _configuration["OpenRouter:ApiKey"];

        var treatmentsText = string.Join(
            "\n",
            prescription.Treatments.Select(t =>
                $"Medicine: {t.MedicationName}, Notes: {t.Notes}")
        );

        var prompt = $@"
You are a medical assistant.

Explain this prescription in SIMPLE language for patient.

Diagnosis:
{prescription.Diagnosis}

Treatments:
{treatmentsText}

Rules:
- Use simple English
- Keep explanation short
- Explain medicine usage clearly
- Add simple healthy advice
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

        var responseString =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return "AI explanation unavailable now.";
        }

        dynamic result =
            JsonConvert.DeserializeObject(responseString);

        return result.choices[0].message.content;
    }
}