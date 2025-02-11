# AIBlogGenerator

A simple app works with Ollama (llama3, deepseek-r1, etc.) using SemanticKernel library, and generates blog posts.  

You should have [`Ollama`](https://ollama.com/) installed and started.  
Starting `Ollama` is easy. After downloading and installing it, just run:

```shell
ollama run llama3:latest

# or

ollama run deepseek-r1:1.5b
```

You should have [`dotnet 9.0`](https://dotnet.microsoft.com/en-us/download) too, and you're good to go.  

So first run:

```shell
dotnet restore
```

and then run the app with:

```shell
dotnet run
```  

I've learned it from this [YouTube Video](https://www.youtube.com/watch?v=s4xWkZL69pg).  
