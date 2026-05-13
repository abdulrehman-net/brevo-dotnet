# Abdul.Brevo.Conversations

A lightweight .NET SDK for Brevo Conversations REST API and webhook payloads.

Built for developers who want simple, dependency-injection friendly access to Brevo Conversations without manually writing HttpClient code for every endpoint.

## Features

- Send messages as an agent
- Get, update, and delete agent messages
- Send automated pushed messages to visitors
- Get, update, and delete automated messages
- Set agent online status
- Assign visitors to groups
- Strongly typed webhook payload models
- ASP.NET Core dependency injection support
- .NET 8 and .NET 10 support

## Installation

```bash
dotnet add package Abdul.Brevo.Conversations
```

## Quick Start

Configure the SDK in your `Program.cs` or `Startup.cs`:

```csharp
builder.Services.AddBrevoConversations(options =>
{
    options.ApiKey = builder.Configuration["Brevo:ApiKey"]!;
});
```

Inject and use the clients:

```csharp
public class SupportMessageService
{
    private readonly IBrevoConversationMessagesClient _messages;

    public SupportMessageService(IBrevoConversationMessagesClient messages)
    {
        _messages = messages;
    }

    public async Task SendAsync(string visitorId, string message)
    {
        await _messages.SendAgentMessageAsync(new SendBrevoAgentMessageRequest
        {
            VisitorId = visitorId,
            Text = message,
            AgentEmail = "support@Abdulit.com",
            AgentName = "Abdul Support",
            ReceivedFrom = "AbdulHelpdesk"
        });
    }
}
```

## Samples

You can find a complete sample ASP.NET Core Minimal API demonstrating all SDK features in the `samples/Abdul.Brevo.Conversations.SampleApi` directory.

To run the sample:

1. Update the `BrevoConversations:ApiKey` in `samples/Abdul.Brevo.Conversations.SampleApi/appsettings.json`.
2. Navigate to the sample directory:
   ```bash
   cd samples/Abdul.Brevo.Conversations.SampleApi
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open the OpenAPI (Swagger) UI to test the endpoints.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
