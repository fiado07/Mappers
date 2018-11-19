using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapperTests.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace MapperTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ShouldMapSingleProperty()
        {
            var src = new Person { Age = 10 };
            var target = new Pessoa();
            Mappers.Mapper.Map(src, target);
            target.Age.Should().Be(10);
        }

        [TestMethod]
        public void ShouldMapCollectionInnerProperty()
        {
            var numbers = new List<int>() { 1, 2, 3 };
            var src = new Person {PhoneNumbers=numbers};
            var target = new Pessoa();

            Mappers.Mapper.Map(src, target);
            //target.PhoneNumbers.Should().NotBeNull();
            target.PhoneNumbers.Count().Should().Be(3);
        }

        [TestMethod]
        public void ShouldMapWhenSrcHasMoreProperties()
        {
            var src = new Person { Age = 10, Name = "Person" };
            var target = new Pessoa();
            Mappers.Mapper.Map(src, target);
            target.Age.Should().Be(10);
        }
        [TestMethod]
        public void ShouldMapWhenTargetHasMoreProperties()
        {
            var src = new Pessoa { Age = 10};
            var target = new Person();
            Mappers.Mapper.Map(src, target);
            target.Name.Should().BeNull();
        }
    }
}
