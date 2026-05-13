using Abdul.Brevo.Email;
using Abdul.Brevo.Email.Models;
using Abdul.Brevo.Email.Webhooks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register Brevo Email SDK (reads from shared "Brevo" config section)
var brevoSection = builder.Configuration.GetSection("Brevo");
builder.Services.AddBrevoEmail(options =>
{
    options.ApiKey = brevoSection["ApiKey"] ?? string.Empty;
    options.BaseUrl = brevoSection["BaseUrl"] ?? options.BaseUrl;
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// --- Transactional Email API Samples ---

var emails = app.MapGroup("/emails").WithTags("Transactional Emails");

emails.MapPost("/send", async (
    [FromBody] SendTransactionalEmailRequest request,
    [FromServices] IBrevoTransactionalEmailClient client) =>
{
    var result = await client.SendAsync(request);
    return Results.Ok(result);
})
.WithName("SendTransactionalEmail");

emails.MapGet("/", async (
    [FromServices] IBrevoTransactionalEmailClient client,
    string? email,
    long? templateId,
    string? startDate,
    string? endDate,
    int? limit,
    int? offset) =>
{
    var request = new ListTransactionalEmailsRequest
    {
        Email = email,
        TemplateId = templateId,
        StartDate = startDate,
        EndDate = endDate,
        Limit = limit,
        Offset = offset
    };

    var result = await client.ListAsync(request);
    return Results.Ok(result);
})
.WithName("ListTransactionalEmails");

emails.MapGet("/{messageId}", async (
    string messageId,
    [FromServices] IBrevoTransactionalEmailClient client) =>
{
    var result = await client.GetContentAsync(messageId);
    return Results.Ok(result);
})
.WithName("GetTransactionalEmailContent");


// --- Scheduled Email API Samples ---

var scheduled = app.MapGroup("/scheduled").WithTags("Scheduled Emails");

scheduled.MapGet("/{identifier}", async (
    string identifier,
    [FromServices] IBrevoScheduledEmailClient client) =>
{
    var result = await client.GetScheduledAsync(identifier);
    return Results.Ok(result);
})
.WithName("GetScheduledEmail");

scheduled.MapDelete("/{identifier}", async (
    string identifier,
    [FromServices] IBrevoScheduledEmailClient client) =>
{
    await client.DeleteScheduledAsync(identifier);
    return Results.NoContent();
})
.WithName("DeleteScheduledEmail");


// --- Hard Bounce API Samples ---

var bounces = app.MapGroup("/bounces").WithTags("Hard Bounces");

bounces.MapPost("/delete", async (
    [FromBody] DeleteHardBouncesRequest request,
    [FromServices] IBrevoHardBounceClient client) =>
{
    await client.DeleteAsync(request);
    return Results.NoContent();
})
.WithName("DeleteHardBounces");


// --- Webhook Receiver Sample ---

app.MapPost("/webhooks/email", (
    [FromBody] BrevoEmailWebhookPayload payload) =>
{
    return Results.Ok(new
    {
        Received = true,
        payload.Event,
        payload.Email,
        payload.MessageId
    });
})
.WithTags("Webhooks")
.WithName("ReceiveEmailWebhook");

app.Run();
