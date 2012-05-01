namespace Contexteer
{
    /// <summary>
    /// <para>Represents the default context.</para>
    /// <para>Can be used by libraries to mark the default case where no context is provided by the client.</para>
    /// </summary>
    public sealed class Default : IContext
    {
        /// <summary>
        /// The instance of the default context.
        /// </summary>
        public static Default Context { get; private set; }

        static Default()
        {
            Context = new Default();
        }

        private Default()
        {
        }
    }
}