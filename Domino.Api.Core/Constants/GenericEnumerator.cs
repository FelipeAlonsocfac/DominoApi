namespace Domino.Api.Core.Constants;

public class GenericEnumerator
{
    public enum Status
    {
        Successful,
        Failed,
        Notfound
    }

    public enum ResponseCode
    {
        Ok = 200,
        NoContent = 204,
        BadRequest = 400,
        InternalError = 500
    }
}
