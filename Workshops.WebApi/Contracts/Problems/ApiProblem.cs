using Microsoft.AspNetCore.Mvc;

namespace Workshops.WebApi.Contracts.Problems;

public class ApiProblem : ProblemDetails
{
    public const string TypeUrl = "https://workshopy.apidog.io/v1/errors-961422m0#problemdetails";
    public ApiProblem()
    {
        Type = TypeUrl;
    }
}