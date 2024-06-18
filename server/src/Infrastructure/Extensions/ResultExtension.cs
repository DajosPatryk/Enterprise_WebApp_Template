namespace server.Infrastructure.Extensions;

public static class ResultExtension
{
    /// <summary>
    /// Converts a list of error objects into a standardized format suitable for API error responses. This method 
    /// constructs an anonymous object that encapsulates the error messages if there are any errors present.
    /// </summary>
    /// <param name="errors">The list of IError instances, each representing a specific error. Each error should 
    /// include details such as an error message.</param>
    /// <returns>
    /// An anonymous object that contains an array of error messages structured for easy integration and 
    /// consistent handling by client applications. If the errors list is null or empty, returns null.
    /// </returns>
    public static object ToErrorResponse(this List<IError> errors)
    {
        if (errors == null) return null;
        return new
        {
            errors = new
            {
                Result = errors.Select(err => err.Message).ToArray()
            }
        };
    }
}