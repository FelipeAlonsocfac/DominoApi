namespace Domino.Api.Infrastructure.Interfaces;

public interface IRedisCache
{
    /// <summary>
    /// Gets object by key.
    /// </summary>
    /// <typeparam name="T">The type of the object to retrieve</typeparam>
    /// <param name="key">The key of the object to retrieve</param>
    /// <returns>A <see cref="Task"/>&lt;<typeparamref name="T"/>?&gt; representing the asynchronous operation.</returns>
    Task<T?> Get<T>(string key) where T : class;


    /// <summary>
    /// Sets object with key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task Set<T>(string key, T value) where T : class;
}