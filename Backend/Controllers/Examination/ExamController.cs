using Microsoft.AspNetCore.Http;
using Backend.DTOs.Exams;
using Backend.Services.ExamService;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Examination;

[Route("api/[controller]")]
[ApiController]
public class ExamController : ControllerBase
{
    private readonly IExamService _service;

    public ExamController(IExamService service)
    {
        _service = service;
    }

    [HttpPost("submit-exam")]
    public async Task<IActionResult> Submit(List<UserExamAnswerDTO> dto)
    {
        await _service.SubmitExamService(dto);
        return Ok(new { message = "Examination submitted!" });
    }

    [HttpGet("get-exam-data")]
    public async Task<IActionResult> StartExam()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
}

