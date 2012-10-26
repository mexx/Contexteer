using System.Collections.Generic;

namespace Contexteer.Configuration
{
    /// <summary>
    /// <para>Holds the configuration of <typeparamref name="TContext"/>.</para>
    /// <para>Can be used as extensions point for configuration of <typeparamref name="TContext"/>.</para>
    /// </summary>
    /// <typeparam name="TContext">The type of contexts to configure.</typeparam>
    public class ConfigurationOf<TContext>
        where TContext : IContext
    {
        private readonly IDictionary<object, object> _values = new Dictionary<object, object>();

        /// <summary>
        /// Associates the value with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to set.</param>
        /// <param name="value">The value to associate with the specified key.</param>
        /// <returns><c>this</c> for fluent continuations.</returns>
        public ConfigurationOf<TContext> Set(object key, object value)
        {
            _values[key] = value;
            return this;
        }

        /// <summary>
        /// Removes the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to remove.</param>
        /// <returns><c>this</c> for fluent continuations.</returns>
        public ConfigurationOf<TContext> Remove(object key)
        {
            _values.Remove(key);
            return this;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">The value associated with the specified key if it was found; otherwise, the default value for <typeparamref name="T"/>.</param>
        /// <returns>Whether the specified key was found.</returns>
        public bool TryGet<T>(object key, out T value)
        {
            object v;
            var keyFound = _values.TryGetValue(key, out v);
            value = keyFound ? (T) v : default(T);
            return keyFound;
        }
    }
}