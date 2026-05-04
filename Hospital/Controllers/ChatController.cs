using Hosptial.BLL.Services.Interfaces;
using Hosptial.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        // Create a new conversation for an appointment
        [HttpPost("conversations")]
        public async Task<IActionResult> CreateConversation(CreateConversationDto dto)
        {
            try
            {
                var conv = await _chatService.CreateConversation(dto);
                return Ok(conv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get all conversations for a patient
        [HttpGet("conversations/patient/{patientId}")]
        public async Task<IActionResult> GetPatientConversations(int patientId)
        {
            var convs = await _chatService.GetPatientConversations(patientId);
            return Ok(convs);
        }

        // Get messages for a conversation
        [HttpGet("conversations/{conversationId}/messages")]
        public async Task<IActionResult> GetMessages(int conversationId)
        {
            var messages = await _chatService.GetMessages(conversationId);
            return Ok(messages);
        }
    }
}
