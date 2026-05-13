using Abdul.Brevo.Conversations;
using Microsoft.AspNetCore.Mvc;

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
    try
    {
        var result = await client.SendAgentMessageAsync(request);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("SendAgentMessage");

messages.MapGet("/{id}", async (
    string id,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    try
    {
        var result = await client.GetMessageAsync(id);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("GetAgentMessage");

messages.MapPut("/{id}", async (
    string id,
    [FromBody] UpdateBrevoMessageRequest request,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    try
    {
        var result = await client.UpdateAgentMessageAsync(id, request);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("UpdateAgentMessage");

messages.MapDelete("/{id}", async (
    string id,
    [FromServices] IBrevoConversationMessagesClient client) =>
{
    try
    {
        await client.DeleteAgentMessageAsync(id);
        return Results.NoContent();
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("DeleteAgentMessage");


// --- Automated Messages API Samples ---

var automated = app.MapGroup("/automated-messages").WithTags("Automated Messages");

automated.MapPost("/", async (
    [FromBody] SendBrevoAutomatedMessageRequest request,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    try
    {
        var result = await client.SendAutomatedMessageAsync(request);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("SendAutomatedMessage");

automated.MapGet("/{id}", async (
    string id,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    try
    {
        var result = await client.GetAutomatedMessageAsync(id);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("GetAutomatedMessage");

automated.MapPut("/{id}", async (
    string id,
    [FromBody] UpdateBrevoMessageRequest request,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    try
    {
        var result = await client.UpdateAutomatedMessageAsync(id, request);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("UpdateAutomatedMessage");

automated.MapDelete("/{id}", async (
    string id,
    [FromServices] IBrevoAutomatedMessagesClient client) =>
{
    try
    {
        await client.DeleteAutomatedMessageAsync(id);
        return Results.NoContent();
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("DeleteAutomatedMessage");


// --- Agent Status API Samples ---

var status = app.MapGroup("/status").WithTags("Agent Status");

status.MapPost("/online", async (
    [FromBody] SetBrevoAgentOnlineRequest request,
    [FromServices] IBrevoConversationStatusClient client) =>
{
    try
    {
        await client.SetAgentOnlineAsync(request);
        return Results.Accepted();
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("SetAgentOnline");


// --- Visitor Groups API Samples ---

var visitors = app.MapGroup("/visitors").WithTags("Visitor Groups");

visitors.MapPut("/group", async (
    [FromBody] SetBrevoVisitorGroupRequest request,
    [FromServices] IBrevoConversationVisitorsClient client) =>
{
    try
    {
        var result = await client.SetVisitorGroupAsync(request);
        return Results.Ok(result);
    }
    catch (BrevoConversationsPaymentRequiredException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations REST API access is not available",
            detail: ex.Message,
            statusCode: StatusCodes.Status402PaymentRequired,
            extensions: new Dictionary<string, object?>
            {
                ["brevoCode"] = ex.BrevoCode,
                ["brevoResponse"] = ex.ResponseBody
            });
    }
    catch (BrevoConversationsApiException ex)
    {
        return Results.Problem(
            title: "Brevo Conversations API request failed",
            detail: ex.Message,
            statusCode: ex.StatusCode,
            extensions: new Dictionary<string, object?>
            {
                ["brevoResponse"] = ex.ResponseBody
            });
    }
})
.WithName("SetVisitorGroup");

app.Run();
