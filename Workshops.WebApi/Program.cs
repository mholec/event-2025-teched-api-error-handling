using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Workshops.Data;
using Workshops.WebApi.Endpoints;
using Workshops.WebApi.ErrorHandling;
using Workshops.WebApi.ErrorHandling.Filters;
using Workshops.WebApi.ErrorHandling.Handlers;
using Workshops.WebApi.HostedServices;
using Workshops.WebApi.Routing;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// system
builder.Services.AddRouting(x => { x.ConstraintMap.Add("apid", typeof(ApidRouteConstraint)); });

// database
builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")); });
builder.Services.AddHostedService<InitDbService>();

builder.Services.AddScoped<RequestValidationState>();
builder.Services.AddScoped<ContractValidator>();

builder.Services.AddProblemDetails(x=> x.CustomizeProblemDetails = (context) => _ = CustomizedProblemDetails.Apply(context));
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<EmptyBodyExceptionHandler>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

WebApplication app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseMiddleware<TraceMiddleware>();
app.UseHttpsRedirection();

var api = app
    .MapGroup(string.Empty)
    .AddEndpointFilter<MediaTypeFilter>();

api.GetWorkshop();
api.GetWorkshops();
api.CreateWorkshop();
api.UpdateWorkshop();
api.DeleteWorkshop();

api.GetRegistrations();
api.DeleteRegistration();

app.Run();