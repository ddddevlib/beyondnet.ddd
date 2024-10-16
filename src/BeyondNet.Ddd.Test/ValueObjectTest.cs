﻿using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Test.Stubs;
using BeyondNet.Ddd.ValueObjects;
using Shouldly;

namespace BeyondNet.Ddd.Test
{
    [TestClass]
    public class ValueObjectTest
    {   

        [TestMethod]
        public void Should_Implement_ValueObject()
        {
            var fieldName = Description.Create("foo");

            Assert.IsInstanceOfType(fieldName, typeof(ValueObject<string>));
        }

        [TestMethod]
        public void Should_Be_Equals()
        {
            var id = Guid.NewGuid().ToString(); 

            var fieldName1 = IdValueObject.Create(id);
            var fieldName2 = IdValueObject.Create(id);

            fieldName1.Equals(fieldName2).ShouldBeTrue();
        }

        [TestMethod]
        public void Should_Be_not_Equals()
        {
            var id = Guid.NewGuid().ToString();

            var fieldName1 = IdValueObject.Create(id);
            var fieldName2 = IdValueObject.Create(Guid.NewGuid().ToString());

            fieldName1.Equals(fieldName2).ShouldBeFalse();
        }

        [TestMethod]
        public void Should_Have_Same_HashCode()
        {
            var fieldName1 = Description.Create("foo");
            var fieldName2 = Description.Create("foo");

            Assert.AreEqual(fieldName1.GetHashCode(), fieldName2.GetHashCode());
        }

        [TestMethod]
        public void Should_Not_Have_Same_HashCode()
        {
            var fieldName1 = Description.Create("foo");
            var fieldName2 = Description.Create("bar");

            Assert.AreNotEqual(fieldName1.GetHashCode(), fieldName2.GetHashCode());
        }


        [TestMethod]
        public void Should_Not_Be_Equal_With_Operator()
        {
            var fieldName1 = Description.Create("foo");
            var fieldName2 = Description.Create("bar");

            Assert.IsTrue(fieldName1 != fieldName2);
        }

        [TestMethod]
        public void Should_Be_Equal_With_Null()
        {
            var fieldName1 = Description.Create("foo");
            Description? fieldName2 = null;

            Assert.IsFalse(fieldName1 == fieldName2);
        }

        [TestMethod]
        public void Should_Not_Be_Equal_With_Null()
        {
            var fieldName1 = Description.Create("foo");
            Description? fieldName2 = null;

            Assert.IsTrue(fieldName1 != fieldName2);
        }

        [TestMethod]
        public void Should_Be_Equal_With_Null_With_Operator()
        {
            var fieldName1 = Description.Create("foo");
            Description? fieldName2 = null;

            Assert.IsFalse(fieldName1.Equals(fieldName2));
        }

        [TestMethod]
        public void Should_Has_BrokenRules()
        {
            var vo = Name.Create("foo");

            vo.SetValue("");

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules()
        {
            var vo = Name.Create("foo");

            vo.SetValue("bar");

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_In_Create_Mode()
        {
            var vo = Name.Create("");

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_In_Create_Mode()
        {
            var vo = Name.Create("foo");

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_In_Create_Mode_With_Validation()
        {
            var vo = Name.Create("");

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_In_Create_Mode_With_Validation()
        {
            var vo = Name.Create("foo");

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_In_Create_Mode_With_Validation_With_Validator()
        {
            var vo = Name.Create("");

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_In_Create_Mode_With_Validation_With_Validator()
        {
            var vo = Name.Create("foo");

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Add_Validator_Ok() {
            var vo = Name.Create("foo");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Remove_Validator_Ok()
        {
            var vo = Name.Create("foo");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));
            vo.RemoveValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Add_Validators_Ok()
        {
            var vo = Name.Create("foo");

            vo.AddValidators(new List<AbstractRuleValidator<ValueObject<string>>> { new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)) });

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_With_Validator()
        {
            var vo = Name.Create("");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_With_Validator()
        {
            var vo = Name.Create("foo");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_With_Validators()
        {
            var vo = Name.Create("");

            vo.AddValidators(new List<AbstractRuleValidator<ValueObject<string>>> { new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)) });

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_With_Validators()
        {
            var vo = Name.Create("foo");

            vo.AddValidators(new List<AbstractRuleValidator<ValueObject<string>>> { new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)) });

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_With_Validator_With_Validation()
        {
            var vo = Name.Create("");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_With_Validator_With_Validation()
        {
            var vo = Name.Create("foo");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_With_Validators_With_Validation()
        {
            var vo = Name.Create("");

            vo.AddValidators(new List<AbstractRuleValidator<ValueObject<string>>> { new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)) });

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_With_Validators_With_Validation()
        {
            var vo = Name.Create("foo");

            vo.AddValidators(new List<AbstractRuleValidator<ValueObject<string>>> { new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)) });

            Assert.IsTrue(vo.IsValid);
        }

        [TestMethod]
        public void Should_Has_BrokenRules_With_Validator_With_Validation_With_Validator()
        {
            var vo = Name.Create("");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsFalse(vo.IsValid);
        }

        [TestMethod]
        public void Should_Not_Has_BrokenRules_With_Validator_With_Validation_With_Validator()
        {
            var vo = Name.Create("foo");

            vo.AddValidator(new StubValueObjectValidator(vo, nameof(StubValueObjectValidator)));

            Assert.IsTrue(vo.IsValid);
        }
    }
}
