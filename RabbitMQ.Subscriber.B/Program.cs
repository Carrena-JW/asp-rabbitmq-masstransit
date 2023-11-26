using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(config =>
{

    config.SetKebabCaseEndpointNameFormatter();
    config.SetInMemorySagaRepositoryProvider();

    var ass = typeof(Program).Assembly;

    config.AddConsumers(ass);
    config.AddSagaStateMachines(ass);
    config.AddSagas(ass);
    config.AddActivities(ass);

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("carrena");
            h.Password("carrena");
        });

        cfg.ConfigureEndpoints(ctx);

        cfg.PrefetchCount = 1; // bus control specific
        cfg.UseConcurrencyLimit(1);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
