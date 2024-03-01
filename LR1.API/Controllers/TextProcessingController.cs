using Microsoft.AspNetCore.Mvc;

namespace LR1.API.Controllers;

public class ProcessTextRequest
{
    public string? TextToProcess { get; set; }
}

[ApiController]
[Route("[controller]")]
public class TextProcessingController : ControllerBase
{
    private static readonly char[] Separators = [' ', ',', '.', '!', '?', ':', ';', '-', '(', ')', '\n', '\r'];

    private readonly ILogger<TextProcessingController> _logger;

    public TextProcessingController(ILogger<TextProcessingController> logger)
    {
        _logger = logger;
    }

    [HttpPost("process")]
    public IActionResult ProcessText([FromBody]ProcessTextRequest request)
    {
        var inputText = request.TextToProcess ?? string.Empty;
        
        var words = inputText.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
         
        var result = words.Where(word => word.EndsWith("во")).ToList();

        _logger.LogInformation("Text processed successfully");
        return Ok(result);
    }
}