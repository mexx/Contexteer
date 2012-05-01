using System;
using Contexteer.Configuration;

namespace Contexteer.Specs
{
    internal class Test : IContext
    {
    }

    internal static class TestsExtensions
    {
        public static ConfigureTestIn<TContext> TestsAre<TContext>(this IConfigure<TContext> This)
            where TContext : IContext
        {
            return new ConfigureTestIn<TContext>();
        }
    }

    internal class ConfigureTestIn<TContext>
        where TContext : IContext
    {
        public void ConfiguredBy(Func<TContext, string> behavior)
        {
        }
    }

    internal class Complex
    {
        public static string Behavior<T>(T context)
        {
            return context.GetType().ToString();
        }
    }
}
