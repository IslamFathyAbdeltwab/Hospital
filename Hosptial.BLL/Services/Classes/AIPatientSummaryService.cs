using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Hosptital.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
//for doctor to get a summary about the patient's history by using AI to analyze his prescriptions and give a summary about his history in short format
public class AIPatientSummaryService
    : IAIPatientSummaryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AIPatientSummaryService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GenerateSummary(
        List<PrescriptionViewModel> prescriptions)
    {
        var apiKey = _configuration["OpenRouter:ApiKey"];

        var allData = new StringBuilder();

        foreach (var prescription in prescriptions)
        {
            allData.AppendLine(
                $"Diagnosis: {prescription.Diagnosis}");

            foreach (var treatment in prescription.Treatments)
            {
                allData.AppendLine(
                    $"Medicine: {treatment.MedicationName}");

                allData.AppendLine(
                    $"Notes: {treatment.Notes}");
            }

            allData.AppendLine("----------------");
        }

        var prompt = $@"
Summarize this patient's medical history
for doctor in SHORT professional format.

Patient Data:
{allData}

Rules:
- Keep summary short
- Mention repeated diseases
- Mention chronic conditions
- Mention common medicines
- Mention important patterns
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

        var json = JsonConvert.SerializeObject(body);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

     
        _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", apiKey);
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
        var responseString = await response.Content.ReadAsStringAsync();

      

        // LIMIT HANDLING
        if (!response.IsSuccessStatusCode)
        {
            return "AI summary unavailable now. please Doctor Try again in another time";
        }

        

        dynamic result =
            JsonConvert.DeserializeObject(responseString);

        return result.choices[0].message.content;
    }
}