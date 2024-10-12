using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Test.Stubs;
using Moq;
using BeyondNet.Ddd.ValueObjects;
using Shouldly;

namespace BeyondNet.Ddd.Test
{
    [TestClass]
    public class EntityTest
    {
        ParentRootEntity owner;

        [TestInitialize]
        public void Setup()
        {
            owner = ParentRootEntity.Create(Name.Create("foo"), Description.Create("bar"));
        }

        [TestCleanup]
        public void Cleanup()
        {
            owner = null;
        }

        [TestMethod]
        public void Should_Implement_Entity()
        {
            Assert.IsInstanceOfType(owner, typeof(ParentRootEntity));
        }

        [TestMethod]
        public void Should_Have_Empty_DomainEvents_Collection()
        {
            Assert.AreEqual(0, owner.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Entity_Valid_Be_True()
        {
            owner.IsValid().ShouldBeTrue();
        }

        [TestMethod]
        public void Should_Validate_Entity_NotValid()
        {
            owner.ChangeDescription(Description.Create(""));
            owner.IsValid().ShouldBeFalse();
        }

        [TestMethod]
        public void Should_Validate_Entity_Valid()
        {
            owner.Validate();

            owner.IsValid().ShouldBeTrue();
        }

        [TestMethod]
        public void Should_Add_BrokenRule()
        {
            owner.ChangeDescription(Description.Create(""));

            owner.AddBrokenRule("FieldName", "Field Name is required");

            Assert.AreEqual(1, owner.GetBrokenRules().Count);
        }

        [TestMethod]
        public void Should_Add_DomainEvent()
        {
            owner.ChangeDescription(Description.Create(""));

            owner.AddDomainEvent(new StubMockDomainEvent(nameof(StubMockDomainEvent)));

            Assert.AreEqual(1, owner.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Remove_DomainEvent()
        {
            owner.ChangeDescription(Description.Create(""));

            var domainEvent = new StubMockDomainEvent(nameof(StubMockDomainEvent));

            owner.AddDomainEvent(domainEvent);

            owner.RemoveDomainEvent(domainEvent);

            Assert.AreEqual(0, owner.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Clear_DomainEvents()
        {
            owner.ChangeDescription(Description.Create(""));

            owner.AddDomainEvent(new StubMockDomainEvent(nameof(StubMockDomainEvent)));

            owner.ClearDomainEvents();

            Assert.AreEqual(0, owner.GetDomainEvents().Count);
        }

        [TestMethod]
        public void Should_Set_Version()
        {
            owner.ChangeDescription(Description.Create(""));

            owner.SetVersion(1);

            Assert.AreEqual(1, owner.Version);
        }

        [TestMethod]
        public void Should_Add_Validator()
        {
            owner.ChangeDescription(Description.Create(""));

            owner.AddValidator(new StubEntityRuleValidator<ParentRootEntity>(owner, nameof(StubValueObjectValidator)));

            owner.GetValidators().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_Validator()
        {
            owner.ChangeDescription(Description.Create(""));

            owner.AddValidator(new StubEntityRuleValidator<ParentRootEntity>(owner, nameof(StubValueObjectValidator)));

            owner.Validate();

            owner.IsValid().ShouldBeFalse();
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            mockValidator.Verify(x => x.AddRules(It.IsAny<RuleContext>()), Times.Once);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_NotValid()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.IsValid().ShouldBeFalse();
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_Valid()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.IsValid().ShouldBeTrue();
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules()[0].Message.ShouldBe("Field Name is required");
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_PropertyName()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules()[0].Property.ShouldBe("FieldName");
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_PropertyName_NotValid()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules()[0].Property.ShouldBe("FieldName");
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_PropertyName_Valid()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_Valid()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_NotValid()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_Valid_With_MockValidator()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_NotValid_With_MockValidator()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_Valid_With_MockValidator_With_BrokenRule()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_NotValid_With_MockValidator_With_BrokenRule()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_Valid_With_MockValidator_With_BrokenRule_Message()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_NotValid_With_MockValidator_With_BrokenRule_Message()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_Valid_With_MockValidator_With_BrokenRule_Message_NotValid()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_NotValid_With_MockValidator_With_BrokenRule_Message_NotValid()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_Valid_With_MockValidator_With_BrokenRule_Message_Valid()
        {
            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>()));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBe(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_With_MockValidator_With_BrokenRule_Message_NotValid_With_MockValidator_With_BrokenRule_Message_Valid()
        {
            owner.ChangeDescription(Description.Create(""));

            var mockValidator = new Mock<StubEntityRuleValidator<ParentRootEntity>>(owner, nameof(StubValueObjectValidator));

            mockValidator.Setup(x => x.AddRules(It.IsAny<RuleContext>())).Callback(() => owner.AddBrokenRule("FieldName", "Field Name is required"));

            owner.AddValidator(mockValidator.Object);

            owner.Validate();

            owner.GetBrokenRules().Count.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Should_Validate_Entity_Track_IsNew()
        {
            owner.IsNew.ShouldBeTrue();
        }
    }
}
