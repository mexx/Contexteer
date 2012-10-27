using System;
using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenUseNullAsKeyForGettingValue
    {
        static int value;
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => In<Default>.Contexts.TryGet(null, out value));

        It should_occur_an_exception_of_specific_type = () => exception.ShouldBeOfType<ArgumentNullException>();
        It should_return_default_value = () => value.ShouldEqual(default(int));
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}