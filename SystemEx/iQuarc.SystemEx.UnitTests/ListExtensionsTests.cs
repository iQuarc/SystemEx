using System.Linq;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class ListExtensionsTests {
        [Fact]
        public void BinarySearchBy_FromObjectEnumerable_GetsCorrectIndex()
        {
            var collection = new[]
            {
                new {Name = "Cristi", Age = 30},
                new {Name = "Cata", Age = 20},
                new {Name = "Ionut", Age = 19}
            };

            var index = collection.ToList().BinarySearchBy(new {Name = "", Age = 20}, x => x.Age);
            Assert.Equal(1, index);
        }
    }
}