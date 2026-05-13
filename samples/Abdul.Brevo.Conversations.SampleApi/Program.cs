using Abdul.Brevo.Conversations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Register Brevo Conversations SDK (reads from shared "Brevo" config section)
var brevoSection = builder.Configuration.GetSection("Brevo");
builder.Services.AddBrevoConversations(options =>
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

// --- Agent Messages API Samples ---

var messages = app.MapGroup("/messages").WithTags("Agent Messages");

messages.MapPost("/", async (
    [FromBody] SendBrevoAgentMessageRequest request,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    var result = await client.SendAgentMessageAsync(request);
    return Results.Ok(result);
})
.WithName("SendAgentMessage");

messages.MapGet("/{id}", async (
    string id,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    var result = await client.GetMessageAsync(id);
    return Results.Ok(result);
})
.WithName("GetAgentMessage");

messages.MapPut("/{id}", async (
    string id,
    [FromBody] UpdateBrevoMessageRequest request,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    var result = await client.UpdateAgentMessageAsync(id, request);
    return Results.Ok(result);
})
.WithName("UpdateAgentMessage");

messages.MapDelete("/{id}", async (
    string id,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    await client.DeleteAgentMessageAsync(id);
    return Results.NoContent();
})
.WithName("DeleteAgentMessage");


// --- Automated Messages API Samples ---

var automated = app.MapGroup("/automated-messages").WithTags("Automated Messages");

automated.MapPost("/", async (
    [FromBody] SendBrevoAutomatedMessageRequest request,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    var result = await client.SendAutomatedMessageAsync(request);
    return Results.Ok(result);
})
.WithName("SendAutomatedMessage");

automated.MapGet("/{id}", async (
    string id,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    var result = await client.GetAutomatedMessageAsync(id);
    return Results.Ok(result);
})
.WithName("GetAutomatedMessage");

automated.MapPut("/{id}", async (
    string id,
    [FromBody] UpdateBrevoMessageRequest request,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    var result = await client.UpdateAutomatedMessageAsync(id, request);
    return Results.Ok(result);
})
.WithName("UpdateAutomatedMessage");

automated.MapDelete("/{id}", async (
    string id,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    await client.DeleteAutomatedMessageAsync(id);
    return Results.NoContent();
})
.WithName("DeleteAutomatedMessage");


// --- Agent Status API Samples ---

var status = app.MapGroup("/status").WithTags("Agent Status");

status.MapPost("/online", async (
    [FromBody] SetBrevoAgentOnlineRequest request,
    [FromServices] IBrevoConversationStatusClient client) =>
{
    await client.SetAgentOnlineAsync(request);
    return Results.Accepted();
})
.WithName("SetAgentOnline");


// --- Visitor Groups API Samples ---

var visitors = app.MapGroup("/visitors").WithTags("Visitor Groups");

visitors.MapPut("/group", async (
    [FromBody] SetBrevoVisitorGroupRequest request,
    [FromServices] IBrevoConversationVisitorsClient client) =>
{
    var result = await client.SetVisitorGroupAsync(request);
    return Results.Ok(result);
})
.WithName("SetVisitorGroup");

app.Run();
