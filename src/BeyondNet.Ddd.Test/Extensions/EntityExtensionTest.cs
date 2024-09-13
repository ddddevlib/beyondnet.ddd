using BeyondNet.Ddd.Test.Stubs;
using BeyondNet.Ddd.ValueObjects;
using Shouldly;

namespace BeyondNet.Ddd.Test.Extensions
{
    [TestClass]
    public class EntityExtensionTest
    {
        [TestMethod]
        public void GetPropertiesBrokenRules_ShouldReturnEmptyList_WhenNoBrokenRulesExist()
        {
            var entity = ParentRootEntity.Create(Name.Create("foo"), Description.Create("foo"));

            entity.GetBrokenRules().Any().ShouldBeFalse();
        }

        [TestMethod]
        public void GetPropertiesBrokenRules_ShouldReturnBrokenRules_WhenBrokenRulesExist()
        {
            var entity = ParentRootEntity.Create(Name.Create("foo"), Description.Create(""));

            entity.GetBrokenRules().Any().ShouldBeTrue();
        }
    }

}
