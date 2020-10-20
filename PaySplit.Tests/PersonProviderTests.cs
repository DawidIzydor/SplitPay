using PaySplit.Abstractions.PaySources;
using Xunit;

namespace PaySplit.Tests
{
    public class PersonProviderTests
    {
        [Fact]
        public void ShouldReturnPersons()
        {
            var person1 = new Person();
            var person2 = new Person();

            IPaySourcesProvider paySourcesProvider = new PaySourcesProvider(person1, person2);
            var result = paySourcesProvider.GetPaySources();

            Assert.Contains(person1, result);
            Assert.Contains(person2, result);
        }
    }
}