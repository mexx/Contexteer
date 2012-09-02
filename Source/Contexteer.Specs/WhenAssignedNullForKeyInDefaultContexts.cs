using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenAssignedNullForKeyInDefaultContexts
    {
        static bool present;
        static string value;

        Establish ctx = () => In<Default>.Contexts.Set("test", null);

        Because of = () => present = In<Default>.Contexts.TryGet("test", out value);

        It should_have_value_present = () => present.ShouldBeTrue();
        It should_return_correct_value = () => value.ShouldBeNull();
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}