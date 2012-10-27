using System;
using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenUseNullAsKeyForAssigningValue
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => In<Default>.Contexts.Set(null, null));

        It should_occur_an_exception_of_specific_type = () => exception.ShouldBeOfType<ArgumentNullException>();
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}