using Shouldly;
using BeyondNet.Ddd.Test.Stubs;
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.Test
{
    [TestClass]
    public class TrackingTests
    {
        [TestMethod]
        public void MarkNew_ShouldSetIsNewToTrue_AndIsDirtyToFalse()
        {
            var name = Name.Create("foo");
            var fieldName = Description.Create("foo");

            var subject = ParentRootEntity.Create(name, fieldName);

            subject.IsNew.ShouldBeTrue();
            subject.IsDirty.ShouldBeFalse();
        }

        [TestMethod]
        public void MarkNew_ShouldSetIsNewTofalse_AndIsDirtyToTrue()
        {
            var name = Name.Create("foo");
            var fieldName = Description.Create("foo");

            var subject = ParentRootEntity.Create(name, fieldName);

            var props = subject.Props;
            
            props.Name!.SetValue("bar");

            subject.SetProps(props);

            subject.IsNew.ShouldBeFalse();
            subject.IsDirty.ShouldBeTrue();
        }
    }
}
