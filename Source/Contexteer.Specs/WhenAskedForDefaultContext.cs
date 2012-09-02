using Machine.Specifications;

namespace Contexteer.Specs
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Local
    public class WhenAskedForDefaultContext
    {
        static Default context;
        
        Because of = () => context = Default.Context;

        It should_not_be_null = () => context.ShouldNotBeNull();
        It should_be_a_context = () => context.ShouldBeOfType<IContext>();
        It should_be_of_correct_type = () => context.ShouldBeOfType<Default>();
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore InconsistentNaming
}
