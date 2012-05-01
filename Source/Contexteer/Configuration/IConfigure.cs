namespace Contexteer.Configuration
{
    // ReSharper disable UnusedTypeParameter
    /// <summary>
    /// <para>Extensions point for configuration of <typeparamref name="TContext"/>.</para>
    /// <para>Contexteer never provides an instance, used only for type safe syntax.</para>
    /// </summary>
    /// <typeparam name="TContext">The type of contexts to configure</typeparam>
    public interface IConfigure<TContext>
        where TContext : IContext
    {
    }
    // ReSharper restore UnusedTypeParameter
}