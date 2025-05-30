using Workshops.WebApi.Contracts.Problems;

namespace Workshops.WebApi.ErrorHandling
{
    /// <summary>
    /// Chyby vytvořené .NET frameworkem jsou převedeny na ApiProblem
    /// </summary>
    public class CustomizedProblemDetails
    {
        public static async Task Apply(ProblemDetailsContext pd)
        {
            pd.ProblemDetails.Type = ApiProblem.TypeUrl;
            pd.ProblemDetails.Status ??= 500;
            pd.ProblemDetails.Extensions = new Dictionary<string, object>();

            if (pd.HttpContext.GetEndpoint() == null && pd.HttpContext.Response.StatusCode == 404)
            {
                pd.ProblemDetails.Title = "Resource (Path) Not Found";
            }
        }
    }
}