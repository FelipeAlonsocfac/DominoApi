namespace Domino.Api.Application.Interfaces;

public interface IDominoService
{
    /// <summary>
    /// Sorts dominoes to match.
    /// </summary>
    /// <param name="dominoes">the dominoes</param>
    /// <returns>A <see cref="Task"/>&lt;<see cref="List{string}"/>&gt; representing the asynchronous operation.</returns>
    Task<List<string>> Sort(List<string> dominoes);
}