using System.Collections.ObjectModel;
using Workshops.WebApi.Contracts.Problems;

namespace Workshops.WebApi.ErrorHandling;

public class RequestValidationState
{
    public string RequestId { get; set; }
    public string TraceId { get; set; }
    public string CorrelationId { get; set; }
    private List<ApiValidationError> ErrorsIntrnl { get; set; } = new();
    public ReadOnlyCollection<ApiValidationError> Errors => ErrorsIntrnl.AsReadOnly();

    private bool IsValid => !Errors.Any();

    public void AddError(string message, string property = null)
    {
        ErrorsIntrnl.Add(new ApiValidationError(message, property));
    }

    public void AddError(ApiValidationError error)
    {
        ErrorsIntrnl.Add(error);
    }

    public void AddErrors(List<ApiValidationError> errors)
    {
        ErrorsIntrnl.AddRange(errors);
    }
}