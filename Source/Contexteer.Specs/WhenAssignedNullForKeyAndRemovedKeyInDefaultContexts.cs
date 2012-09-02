using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenAssignedNullForKeyAndRemovedKeyInDefaultContexts
    {
        static bool present;
        static string value;

        Establish ctx = () => In<Default>.Contexts.Set("test", null).Remove("test");

        Because of = () => present = In<Default>.Contexts.TryGet("test", out value);

        It should_not_have_value_present = () => present.ShouldBeFalse();
        It should_return_default_value = () => value.ShouldEqual(default(string));
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}