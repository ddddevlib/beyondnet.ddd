using AutoMapper;
using BeyondNet.Ddd.Test.AutoMapper;
using BeyondNet.Ddd.Test.Stubs;
using BeyondNet.Ddd.ValueObjects;
using Shouldly;

namespace BeyondNet.Ddd.Test
{
    [TestClass]
    public class AutoMapperCompatibilityTest
    {
        Mapper mapper = null;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ParentRootProfile>();
            });

            mapper = new Mapper(config);
        }

        [TestMethod]
        public void TestAutoMapperConfiguration()
        {
            // Assert
            Assert.IsNotNull(mapper);
        }

        [TestMethod]
        public void MapperFromBOEntityToDtoShouldBeOk()
        {
            var parentRoot = ParentRootEntity.Create(
                    Name.Create("foo"),
                    Description.Create("foo"));

            var dto = mapper.Map<ParentRootDto>(parentRoot.GetPropsCopy());

            dto.ShouldNotBeNull();
        }

        [TestMethod]
        public void MapperFromDtoToBOEntityShouldBeOk()
        {
            var dto = new ParentRootDto
            {
                Id = "1",
                Name = "foo",
                Description = "foo",
                Status = 2                
            };

            dto.Audit = new AuditDto
            {
                CreatedBy = "foo",
                CreatedAt = DateTime.Now,
                UpdatedBy = "foo",
                UpdatedAt = DateTime.Now,
                TimeSpan = TimeSpan.FromDays(1).ToString()
            };

            var entityProps = mapper.Map<ParentRootProps>(dto);

            var entity = ParentRootEntity.Create(entityProps.Name, entityProps.Description);

            entity.ShouldNotBeNull();
        }

        [TestCleanup]
        public void CleanUp()
        {
            // Act
            mapper = null;
        }
    }
}
