using Abdul.Brevo.Crm;
using Abdul.Brevo.Crm.Models.Common;
using Abdul.Brevo.Crm.Models.Companies;
using Abdul.Brevo.Crm.Models.Deals;
using Abdul.Brevo.Crm.Models.Tasks;
using Abdul.Brevo.Crm.Models.Notes;
using Abdul.Brevo.Crm.Models.Files;
using Abdul.Brevo.Crm.Models.Attributes;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register Brevo CRM SDK
builder.Services.AddBrevoCrm(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// --- Companies API Samples ---
var companies = app.MapGroup("/companies").WithTags("CRM Companies");

companies.MapPost("/", async (CreateCompanyRequest req, IBrevoCrmCompaniesClient client) => 
    Results.Created($"/companies/{ (await client.CreateAsync(req)).Id }", await client.CreateAsync(req)));

companies.MapGet("/", async ([AsParameters] ListCompaniesRequest req, IBrevoCrmCompaniesClient client) => 
    Results.Ok(await client.ListAsync(req)));

companies.MapGet("/{id}", async (string id, IBrevoCrmCompaniesClient client) => 
    Results.Ok(await client.GetAsync(id)));

companies.MapPatch("/{id}", async (string id, UpdateCompanyRequest req, IBrevoCrmCompaniesClient client) => {
    await client.UpdateAsync(id, req);
    return Results.NoContent();
});

companies.MapDelete("/{id}", async (string id, IBrevoCrmCompaniesClient client) => {
    await client.DeleteAsync(id);
    return Results.NoContent();
});

companies.MapPatch("/{id}/link-unlink", async (string id, LinkUnlinkRequest req, IBrevoCrmCompaniesClient client) => {
    await client.LinkUnlinkAsync(id, req);
    return Results.NoContent();
});

// --- Deals API Samples ---
var deals = app.MapGroup("/deals").WithTags("CRM Deals");

deals.MapPost("/", async (CreateDealRequest req, IBrevoCrmDealsClient client) => 
    Results.Created($"/deals/{ (await client.CreateAsync(req)).Id }", await client.CreateAsync(req)));

deals.MapGet("/", async ([AsParameters] ListDealsRequest req, IBrevoCrmDealsClient client) => 
    Results.Ok(await client.ListAsync(req)));

deals.MapGet("/{id}", async (string id, IBrevoCrmDealsClient client) => 
    Results.Ok(await client.GetAsync(id)));

deals.MapPatch("/{id}", async (string id, UpdateDealRequest req, IBrevoCrmDealsClient client) => {
    await client.UpdateAsync(id, req);
    return Results.NoContent();
});

deals.MapDelete("/{id}", async (string id, IBrevoCrmDealsClient client) => {
    await client.DeleteAsync(id);
    return Results.NoContent();
});

deals.MapPatch("/{id}/link-unlink", async (string id, DealLinkUnlinkRequest req, IBrevoCrmDealsClient client) => {
    await client.LinkUnlinkAsync(id, req);
    return Results.NoContent();
});

// --- Pipelines API Samples ---
var pipelines = app.MapGroup("/pipelines").WithTags("CRM Pipelines");

pipelines.MapGet("/", async (IBrevoCrmPipelineClient client) => 
    Results.Ok(await client.ListAsync()));

pipelines.MapGet("/{id}", async (string id, IBrevoCrmPipelineClient client) => 
    Results.Ok(await client.GetAsync(id)));

// --- Tasks API Samples ---
var tasks = app.MapGroup("/tasks").WithTags("CRM Tasks");

tasks.MapPost("/", async (CreateTaskRequest req, IBrevoCrmTasksClient client) => 
    Results.Created($"/tasks/{ (await client.CreateAsync(req)).Id }", await client.CreateAsync(req)));

tasks.MapGet("/", async ([AsParameters] ListTasksRequest req, IBrevoCrmTasksClient client) => 
    Results.Ok(await client.ListAsync(req)));

tasks.MapGet("/types", async (IBrevoCrmTasksClient client) => 
    Results.Ok(await client.ListTaskTypesAsync()));

// --- Notes API Samples ---
var notes = app.MapGroup("/notes").WithTags("CRM Notes");

notes.MapPost("/", async (CreateNoteRequest req, IBrevoCrmNotesClient client) => 
    Results.Created($"/notes/{ (await client.CreateAsync(req)).Id }", await client.CreateAsync(req)));

notes.MapGet("/", async ([AsParameters] ListNotesRequest req, IBrevoCrmNotesClient client) => 
    Results.Ok(await client.ListAsync(req)));

// --- Files API Samples ---
var files = app.MapGroup("/files").WithTags("CRM Files");

files.MapPost("/upload", async (IFormFile file, [FromForm] string? dealId, [FromForm] string? companyId, [FromForm] long? contactId, IBrevoCrmFilesClient client) => {
    using var stream = file.OpenReadStream();
    var result = await client.UploadAsync(new UploadFileRequest {
        FileStream = stream,
        FileName = file.FileName,
        DealId = dealId,
        CompanyId = companyId,
        ContactId = contactId
    });
    return Results.Ok(result);
}).DisableAntiforgery();

files.MapGet("/", async ([AsParameters] ListFilesRequest req, IBrevoCrmFilesClient client) => 
    Results.Ok(await client.ListAsync(req)));

files.MapGet("/{id}/download", async (string id, IBrevoCrmFilesClient client) => 
    Results.Ok(await client.GetDownloadUrlAsync(id)));

// --- Attributes API Samples ---
var attributes = app.MapGroup("/attributes").WithTags("CRM Attributes");

attributes.MapPost("/", async (CreateCrmAttributeRequest req, IBrevoCrmAttributesClient client) => {
    await client.CreateAsync(req);
    return Results.NoContent();
});

attributes.MapGet("/companies", async (IBrevoCrmAttributesClient client) => 
    Results.Ok(await client.ListCompanyAttributesAsync()));

attributes.MapGet("/deals", async (IBrevoCrmAttributesClient client) => 
    Results.Ok(await client.ListDealAttributesAsync()));

app.Run();
