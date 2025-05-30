using Microsoft.AspNetCore.Mvc;

namespace Workshops.WebApi.Contracts.Problems;

public class ApiValidationProblem : ProblemDetails
{
    public ApiValidationProblem(List<ApiValidationError> errors)
    {
        Type = "https://workshopy.apidog.io/v1/errors-961422m0#validationproblemdetails";
        Extensions.Add("errors", errors);
    }
}