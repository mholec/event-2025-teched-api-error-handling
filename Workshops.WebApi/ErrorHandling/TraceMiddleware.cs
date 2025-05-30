namespace Workshops.WebApi.ErrorHandling;

public class TraceMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, RequestValidationState requestValidationState)
    {
        context.Response.Headers.Append("CorrelationId",
            context.Request.Headers.ContainsKey("CorrelationId")
                ? context.Request.Headers["CorrelationId"].ToString()
                : Guid.NewGuid().ToString());

        context.Response.Headers.Append("TraceId",
            context.Request.Headers.ContainsKey("TraceId")
                ? context.Request.Headers["TraceId"].ToString()
                : Guid.NewGuid().ToString());

        context.TraceIdentifier = context.Response.Headers["TraceId"];
        context.Response.Headers.Append("RequestId", Guid.NewGuid().ToString());

        requestValidationState.RequestId = context.Response.Headers["RequestId"];
        requestValidationState.TraceId = context.Response.Headers["TraceId"];
        requestValidationState.CorrelationId = context.Response.Headers["CorrelationId"];

        await next(context);
    }
}