namespace Contexteer.Configuration
{
    /// <summary>
    /// Initiates the configuration of <typeparamref name="TContext"/>.
    /// </summary>
    /// <typeparam name="TContext">The type of contexts to configure</typeparam>
    public static class In<TContext>
        where TContext : IContext
    {
        private static readonly ConfigurationOf<TContext> Configuration = new ConfigurationOf<TContext>();

        /// <summary>
        /// Gets the configuration of <typeparamref name="TContext"/>.
        /// </summary>
        public static ConfigurationOf<TContext> Contexts
        {
            get { return Configuration; }
        }
    }
}