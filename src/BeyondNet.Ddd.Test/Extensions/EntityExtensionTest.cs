using BeyondNet.Ddd.Test.Stubs;
using BeyondNet.Ddd.ValueObjects;
using Shouldly;

namespace BeyondNet.Ddd.Test.Extensions
{
    [TestClass]
    public class EntityExtensionTest
    {
        ParentRootEntity? parentEntityWithoutRules = null;

        ParentRootEntity? parentEntityWithRules = null;


        [TestInitialize]
        public void Setup()
        {
            parentEntityWithoutRules = ParentRootEntity.Create(Name.Create("foo"), Description.Create("foo"));

            parentEntityWithRules = ParentRootEntity.Create(Name.Create("foo"), Description.Create(""));
        }


        [TestMethod]
        public void GetPropertiesBrokenRules_ShouldReturnEmptyList_WhenNoBrokenRulesExist()
        {
            parentEntityWithoutRules!.GetBrokenRules().ShouldBeEmpty();
        }

        [TestMethod]
        public void GetPropertiesBrokenRules_ShouldReturnBrokenRules_WhenBrokenRulesExist()
        {
            parentEntityWithRules!.GetBrokenRules().ShouldNotBeEmpty();
        }

        [TestMethod]
        public void GetPropertiesBrokenRules_ShouldReturnEmptyList_WhenNoBrokenRulesExistInValueObject()
        {

            parentEntityWithoutRules!.GetBrokenRules().ShouldBeEmpty();
        }

        [TestMethod]
        public void GetPropertiesBrokenRules_ShouldReturnBrokenRules_WhenBrokenRulesExistInValueObjectAndEntity()
        {
            parentEntityWithRules!.GetBrokenRules().ShouldNotBeEmpty();
        }

        [TestCleanup]
        public void Clean()
        {
            parentEntityWithRules = null;
            parentEntityWithoutRules = null;
        }
    }

}
