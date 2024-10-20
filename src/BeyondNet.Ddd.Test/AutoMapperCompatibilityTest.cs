﻿using AutoMapper;
using BeyondNet.Ddd.Test.AutoMapper;
using BeyondNet.Ddd.Test.Dtos;
using BeyondNet.Ddd.Test.Entities;
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
        public void MapperFromDtoToBOEntityShouldBeOk()
        {
            var dto = new SampleEntityDto
            {
                Id = "1",
                Name = "foo",
                Status = 2                
            };


            var entityProps = mapper.Map<SampleEntityProps>(dto);

            var entity = SampleEntity.Create(entityProps.Name);

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
