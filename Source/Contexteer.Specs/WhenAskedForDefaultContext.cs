using Contexteer.Configuration;
using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenAskedForDefaultContext
    {
        It should_not_be_null = () => Default.Context.ShouldNotBeNull();
    }

    public class WhenForDefaultContextConfiguredByFunc
    {
        Because of = () => In<Default>.Contexts.TestsAre().
            ConfiguredBy(ctx => "Test");

        It should_not_fail = () => true.ShouldBeTrue();
    }

    public class WhenConfiguredByFunc
    {
        Because of = () => In<Test>.Contexts.TestsAre().
            ConfiguredBy(ctx => "Test");

        It should_not_fail = () => true.ShouldBeTrue();
    }

    public class WhenConfiguredByComplexBehavior
    {
        Because of = () => In<Test>.Contexts.TestsAre().
            ConfiguredBy(Complex.Behavior);

        It should_not_fail = () => true.ShouldBeTrue();
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}
