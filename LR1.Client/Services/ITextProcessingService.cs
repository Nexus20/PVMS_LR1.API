namespace LR1.Client.Services;

public class ProcessTextRequest
{
    public string? TextToProcess { get; set; }
}

public interface ITextProcessingService
{
    Task<IEnumerable<string>> ProcessTextAsync(string inputText, CancellationToken cancellationToken = default);
}

public class TextProcessingService : ITextProcessingService
{
    private readonly HttpClient _httpClient;

    public TextProcessingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<string>> ProcessTextAsync(string inputText, CancellationToken cancellationToken = default)
    {
        var request = new ProcessTextRequest()
        {
            TextToProcess = inputText
        };
        
        var result = await _httpClient.PostAsJsonAsync("TextProcessing/process", request, cancellationToken);
        
        result.EnsureSuccessStatusCode();
        
        var response = await result.Content.ReadFromJsonAsync<IEnumerable<string>>(cancellationToken: cancellationToken);
        
        return response ?? Enumerable.Empty<string>();
    }
}