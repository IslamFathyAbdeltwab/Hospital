using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Hosptital.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

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
        var apiKey = _configuration["Gemini:ApiKey"];

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
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

        var response =
            await _httpClient.PostAsync(url, content);

        // LIMIT HANDLING
        if (!response.IsSuccessStatusCode)
        {
            return "AI summary unavailable now. please Doctor Try again in another time";
        }

        var responseString =
            await response.Content.ReadAsStringAsync();

        dynamic result =
            JsonConvert.DeserializeObject(responseString);

        return result.candidates[0]
            .content.parts[0].text.ToString();
    }
}