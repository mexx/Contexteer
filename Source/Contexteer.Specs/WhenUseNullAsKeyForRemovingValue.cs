using System;
using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenUseNullAsKeyForRemovingValue
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => In<Default>.Contexts.Remove(null));

        It should_occur_an_exception_of_specific_type = () => exception.ShouldBeOfType<ArgumentNullException>();
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}