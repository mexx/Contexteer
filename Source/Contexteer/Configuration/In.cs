namespace Contexteer.Configuration
{
    /// <summary>
    /// Initiates the configuration of <typeparamref name="TContext"/>
    /// </summary>
    /// <typeparam name="TContext">The type of contexts to configure</typeparam>
    public static class In<TContext>
        where TContext : IContext
    {
        /// <summary>
        /// <para>Gets the configuration for <typeparamref name="TContext"/></para>
        /// <para>Always returns <c>null</c>, used only for type safe syntax.</para>
        /// </summary>
        public static IConfigure<TContext> Contexts
        {
            get { return null; }
        }
    }
}