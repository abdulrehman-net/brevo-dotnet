# Abdul.Brevo — .NET SDKs for Brevo

A collection of lightweight .NET SDKs for Brevo APIs, built for developers who want simple, dependency-injection friendly access without manually writing HttpClient code.

| Package | NuGet | Description |
|---------|-------|-------------|
| **Abdul.Brevo.Abstractions** | [![NuGet](https://img.shields.io/nuget/v/Abdul.Brevo.Abstractions)](https://www.nuget.org/packages/Abdul.Brevo.Abstractions) | Shared foundational layer for all Brevo SDKs |
| **Abdul.Brevo.Conversations** | [![NuGet](https://img.shields.io/nuget/v/Abdul.Brevo.Conversations)](https://www.nuget.org/packages/Abdul.Brevo.Conversations) | Brevo Conversations (live chat) REST API |
| **Abdul.Brevo.Email** | [![NuGet](https://img.shields.io/nuget/v/Abdul.Brevo.Email)](https://www.nuget.org/packages/Abdul.Brevo.Email) | Brevo Transactional Email API v3 |
| **Abdul.Brevo.Crm** | [![NuGet](https://img.shields.io/nuget/v/Abdul.Brevo.Crm)](https://www.nuget.org/packages/Abdul.Brevo.Crm) | Brevo Sales CRM API v3 |

## Features

### Abdul.Brevo.Conversations
- Send messages as an agent
- Get, update, and delete agent messages
- Send automated pushed messages to visitors
- Get, update, and delete automated messages
- Set agent online status
- Assign visitors to groups
- Strongly typed webhook payload models

### Abdul.Brevo.Email
- Send transactional emails (inline HTML or template)
- Batch send with message versions
- Schedule emails for future delivery
- List and retrieve sent email content
- Manage scheduled emails
- Delete hard bounces
- Strongly typed webhook payload models

### Abdul.Brevo.Crm
- Manage companies and their attributes
- Manage deals, pipelines, and stages
- Create, list, and complete CRM tasks
- Create and retrieve notes associated with entities
- Upload, download, and manage CRM files
- Create custom attributes for companies and deals
- Flexible attribute system via Dictionary access

### Abdul.Brevo.Abstractions
- Shared HTTP plumbing and authentication
- Rate-limit awareness and header parsing (`x-sib-ratelimit-*`)
- Automatic 429 (Too Many Requests) detection with `RetryAfter` timing
- Unified exception hierarchy with structured error parsing
- Standard pagination primitives (`limit`/`offset`)
- Shared dependency injection registration helpers

### Shared
- ASP.NET Core dependency injection support
- .NET 8 and .NET 10 support
- Unified versioning across all packages
- Central NuGet package management

## Installation

```bash
# Conversations SDK
dotnet add package Abdul.Brevo.Conversations

# Email SDK
dotnet add package Abdul.Brevo.Email

# CRM SDK
dotnet add package Abdul.Brevo.Crm
```

## Quick Start

### Conversations

```csharp
builder.Services.AddBrevoConversations(options =>
{
    options.ApiKey = builder.Configuration["Brevo:ApiKey"]!;
});
```

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
            AgentEmail = "support@example.com",
            AgentName = "Support",
            ReceivedFrom = "Helpdesk"
        });
    }
}
```

### Email

```csharp
builder.Services.AddBrevoEmail(options =>
{
    options.ApiKey = builder.Configuration["Brevo:ApiKey"]!;
});
```

```csharp
public class NotificationService
{
    private readonly IBrevoTransactionalEmailClient _email;

    public NotificationService(IBrevoTransactionalEmailClient email)
    {
        _email = email;
    }

    public async Task SendWelcomeEmailAsync(string recipientEmail, string name)
    {
        var result = await _email.SendAsync(new SendTransactionalEmailRequest
        {
            Sender = new EmailSender { Email = "hello@example.com", Name = "My App" },
            To = [new EmailRecipient { Email = recipientEmail, Name = name }],
            Subject = $"Welcome, {name}!",
            HtmlContent = $"<h1>Welcome aboard, {name}!</h1><p>We're glad to have you.</p>"
        });

        Console.WriteLine($"Sent! MessageId: {result.MessageId}");
    }
}
```

### CRM

```csharp
builder.Services.AddBrevoCrm(options =>
{
    options.ApiKey = builder.Configuration["Brevo:ApiKey"]!;
});
```

```csharp
public class DealService
{
    private readonly IBrevoCrmDealsClient _deals;

    public DealService(IBrevoCrmDealsClient deals)
    {
        _deals = deals;
    }

    public async Task CreateDealAsync(string name, double amount)
    {
        await _deals.CreateAsync(new CreateDealRequest
        {
            Name = name,
            Attributes = new Dictionary<string, object>
            {
                ["amount"] = amount,
                ["deal_status"] = "open"
            }
        });
    }
}
```

## Samples

Complete sample ASP.NET Core Minimal APIs are available:

- **Conversations**: `samples/Abdul.Brevo.Conversations.SampleApi/`
- **Email**: `samples/Abdul.Brevo.Email.SampleApi/`
- **CRM**: `samples/Abdul.Brevo.Crm.SampleApi/`

To run a sample:

1. Update the API key in the sample's `appsettings.json`.
2. Run:
   ```bash
   dotnet run --project samples/Abdul.Brevo.Email.SampleApi
   ```
3. Open the OpenAPI (Swagger) UI to test the endpoints.

## Project Structure

```
├── src/
│   ├── Abdul.Brevo.Abstractions/      # Shared foundation (HTTP, Exceptions, Pagination)
│   ├── Abdul.Brevo.Conversations/     # Conversations SDK
│   ├── Abdul.Brevo.Email/             # Transactional Email SDK
│   └── Abdul.Brevo.Crm/               # Sales CRM SDK
├── samples/
│   ├── Abdul.Brevo.Conversations.SampleApi/
│   ├── Abdul.Brevo.Email.SampleApi/
│   └── Abdul.Brevo.Crm.SampleApi/
├── tests/
│   ├── Abdul.Brevo.Abstractions.Tests/
│   ├── Abdul.Brevo.Conversations.Tests/
│   ├── Abdul.Brevo.Email.Tests/
│   └── Abdul.Brevo.Crm.Tests/
├── Directory.Build.props              # Shared version & metadata
├── Directory.Packages.props           # Central NuGet package management
└── Abdul.Brevo.slnx                   # Solution file
```

## Conversations REST API Access

Brevo may restrict Conversations REST API access depending on your account plan, enabled features, credits, or billing status.

If Brevo returns:

```json
{
  "code": "not_enough_credits",
  "message": "Upgrade your plan to use REST API"
}
```

`Abdul.Brevo.Conversations` throws:

```csharp
BrevoConversationsPaymentRequiredException
```

This means the SDK request was formed correctly, but the Brevo account does not currently have access to that REST API operation. Catch and handle it like this:

```csharp
try
{
    var result = await messages.SendAgentMessageAsync(request);
}
catch (BrevoConversationsPaymentRequiredException ex)
{
    // Domain-specific 402 exception
    // ex.BrevoCode  → "not_enough_credits"
    // ex.Message    → "Upgrade your plan to use REST API"
}
catch (BrevoRateLimitException ex)
{
    // Shared 429 exception
    // ex.RetryAfter → Suggests wait duration (e.g. 5 seconds)
}
catch (BrevoApiException ex)
{
    // Base exception for all other errors (400, 401, 403, 404, 5xx)
    // ex.StatusCode → 401
    // ex.BrevoCode  → "unauthorized"
}
```

> **Note:** This is not a package bug. Some Conversations REST API operations require the appropriate Brevo plan, enabled feature access, or available credits.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
