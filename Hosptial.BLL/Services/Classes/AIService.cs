using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AIService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> ExplainPrescription(AddPrescriptionViewModel prescription)
    {
        var apiKey = _configuration["Gemini:ApiKey"];

        var treatmentsText = string.Join("\n",
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
- Short explanation
- Explain how to take medicines
- Add healthy advice if needed
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

        var response = await _httpClient.PostAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}",
            content
        );

        if (!response.IsSuccessStatusCode)
            return "AI explanation unavailable";

        var responseString = await response.Content.ReadAsStringAsync();

        dynamic result = JsonConvert.DeserializeObject(responseString);

        return result.candidates[0].content.parts[0].text.ToString();
    }
}