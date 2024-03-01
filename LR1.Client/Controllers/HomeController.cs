using System.ComponentModel.DataAnnotations;
using LR1.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LR1.Client.Services;

namespace LR1.Client.Controllers;

public class ProcessTextViewModel
{
    [Display(Name="Name")]
    public string TextToProcess { get; set; } = string.Empty;
    [Display(Name="Processed Text")]
    public string ProcessedText { get; set; } = string.Empty;
}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITextProcessingService _textProcessingService;

    public HomeController(ILogger<HomeController> logger, ITextProcessingService textProcessingService)
    {
        _logger = logger;
        _textProcessingService = textProcessingService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new ProcessTextViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ProcessTextViewModel viewModel)
    {
        var result = await _textProcessingService.ProcessTextAsync(viewModel.TextToProcess);
        viewModel.ProcessedText = string.Join(", ", result);
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}