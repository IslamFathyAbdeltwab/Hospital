using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AiSymptomCheckerController : ControllerBase
{
    private readonly IAISymptomCheckerService _aiService;

    public AiSymptomCheckerController(
        IAISymptomCheckerService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("check")]
    public async Task<ActionResult> CheckSymptoms(
        SymptomRequestDto request)
    {
        var result =
            await _aiService.CheckSymptoms(request.Symptoms);

        return Ok(result);
    }
}