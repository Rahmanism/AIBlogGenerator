using Codeblaze.SemanticKernel.Connectors.Ollama;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

static string GetUserInput(string prompt)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(prompt);
    Console.ResetColor();
    string input = Console.ReadLine() ?? string.Empty;
    return input;
}

var builder = Kernel
    .CreateBuilder()
    // .AddOllamaChatCompletion("deepseek-r1:1.5b", "http://localhost:11434");
    .AddOllamaChatCompletion("llama3:latest", "http://localhost:11434");

builder.Services.AddScoped<HttpClient>();

Kernel kernel = builder.Build();

string topic = GetUserInput("What's the topic of the blog?");
string tone = GetUserInput("What's the tone of the blog?");
int length = Convert.ToInt32(GetUserInput("How long is the blog post?"));
string keywords = GetUserInput("Please share a few keywords:");

string prompt = $"""
You are an expert blog writer. Write a detailed and SEO-optimized blog post about "{topic}". Follow these guidelines:
- Include a compelling and SEO-friendly title that captures the topic's essence.
- Repeat the SEO title naturally in the introduction and conclusion.
- Write an engaging introduction that hooks the reader.
- Structure the blog with exactly five detailed sections, each with a clear and descriptive subheading.
- Use at least one of the provided SEO keywords in each section.
- Write a conclusion that summarizes the blog and includes a strong call-to-action for the reader.
- Ensure the tone is {tone}.
- The total length of the blog should be approximately {length} words.
- Incorporate the following SEO keywords: {keywords}.
- Write in simple, easy-to-understand language for a broad audience.
- Use short paragraphs, bullet points, or numbered lists where appropriate to improve readability.
- Ensure the blog flows naturally, maintaining logical transitions between sections.
""";

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\nGenerating blog content for {topic}...");

var blogResponse = await kernel.InvokePromptAsync(prompt);

string folderPath = Directory.GetCurrentDirectory();
string filePath = Path.Combine(folderPath, "BlogPosts", $"{topic}.md");
Console.WriteLine($"Saving blog content to {filePath}...");
await File.WriteAllTextAsync(filePath, blogResponse.ToString());

var functionResult = await kernel.InvokePromptAsync(prompt);
var response = functionResult.GetValue<string>();
Console.WriteLine($"\u001b[1mResult:\u001b[0m\n\n{response}\n");
Console.ResetColor();
