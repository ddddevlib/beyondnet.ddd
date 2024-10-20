using BeyondNet.Ddd.Test.Entities;
using Shouldly;

namespace BeyondNet.Ddd.Test
{
    [TestClass]
    public class EntityTest
    {
        SampleEntity sampleEntity = null;

        [TestInitialize]
        public void Setup()
        {
            sampleEntity = SampleEntity.Create(SampleName.Create("foo"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            sampleEntity = null;
        }

        [TestMethod]
        public void Entity_Should_not_be_equal()
        {
            var id = Guid.NewGuid().ToString();

            var other = SampleEntity.Create(SampleName.Create("foo"));

            sampleEntity.Equals(other).ShouldBeFalse();
        }


        [TestMethod]
        public void Should_Implement_Entity()
        {
            Assert.IsInstanceOfType(sampleEntity, typeof(SampleEntity));
        }

        [TestMethod]
        public void Should_Have_Empty_DomainEvents_Collection()
        {
            Assert.AreEqual(0, sampleEntity.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Validate_Entity_NotValid()
        {
            sampleEntity.ChangeName(SampleName.Create(""));
            sampleEntity.IsValid().ShouldBeFalse();
        }

        [TestMethod]
        public void Should_Add_DomainEvent()
        {
            sampleEntity.ChangeName(SampleName.Create(""));

            sampleEntity.AddDomainEvent(new SampleCreatedDomainEvent(nameof(SampleCreatedDomainEvent)));

            Assert.AreEqual(1, sampleEntity.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Remove_DomainEvent()
        {
            sampleEntity.ChangeName(SampleName.Create(""));

            var domainEvent = new SampleCreatedDomainEvent(nameof(SampleCreatedDomainEvent));

            sampleEntity.AddDomainEvent(domainEvent);

            sampleEntity.RemoveDomainEvent(domainEvent);

            Assert.AreEqual(0, sampleEntity.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Clear_DomainEvents()
        {
            sampleEntity.ChangeName(SampleName.Create(""));

            sampleEntity.AddDomainEvent(new SampleCreatedDomainEvent(nameof(SampleCreatedDomainEvent)));

            sampleEntity.ClearDomainEvents();

            Assert.AreEqual(0, sampleEntity.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Set_Version()
        {
            sampleEntity.ChangeName(SampleName.Create(""));

            sampleEntity.SetVersion(1);

            Assert.AreEqual(1, sampleEntity.Version);
        }

        [TestMethod]
        public void Should_Validate_Entity_Track_IsNew()
        {
            sampleEntity.IsNew.ShouldBeTrue();
        }
    }
}
