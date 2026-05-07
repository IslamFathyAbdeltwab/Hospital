using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AiPatientSummaryController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    private readonly IAIPatientSummaryService _aiService;

    public AiPatientSummaryController(
        IPrescriptionService prescriptionService,
        IAIPatientSummaryService aiService)
    {
        _prescriptionService = prescriptionService;
        _aiService = aiService;
    }

    [HttpGet("{patientId}")]
    public async Task<ActionResult> GenerateSummary(
        int patientId)
    {
        var prescriptions =
            await _prescriptionService.GetAll(patientId);

        var summary =
            await _aiService.GenerateSummary(
                prescriptions);

        return Ok(new PatientSummaryDto
        {
            Summary = summary
        });
    }
}