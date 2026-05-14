using Abdul.Brevo.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Brevo Core SDK
builder.Services.AddBrevoCore(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/account", async (IBrevoAccountClient accountClient) =>
{
    var account = await accountClient.GetAsync();
    return Results.Ok(account);
})
.WithName("GetAccount");

app.MapGet("/api/contacts", async (IBrevoContactsClient contactsClient) =>
{
    var contacts = await contactsClient.ListAsync();
    return Results.Ok(contacts);
})
.WithName("GetContacts");

app.Run();
