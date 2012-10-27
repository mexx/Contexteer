using System;
using System.Globalization;
using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenUseConcreteValueForKeyInDefaultContexts
    {
        static readonly Random _random = new Random();
        
        static string expectedValue;
        static bool present;
        static string value;

        Establish ctx = () =>
                            {
                                expectedValue = _random.Next().ToString(CultureInfo.InvariantCulture);
                                In<Default>.Contexts.Set("test", expectedValue);
                            };

        Because of = () => present = In<Default>.Contexts.TryGet("test", out value);

        It should_have_value_present = () => present.ShouldBeTrue();
        It should_return_correct_value = () => value.ShouldEqual(expectedValue);
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}