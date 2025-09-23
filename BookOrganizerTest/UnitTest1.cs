using BookOrganizer;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;


namespace BookOrganizerTest
{
    public class UnitTest1: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public UnitTest1() {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
        }
        //[Fact]
        //public async Task TestTodoBookLoads()
        //{
        //    var client = _factory.CreateClient();
        //    var response = await client.GetAsync("/TodoBook/Index");
        //    int code = (int)response.StatusCode;
        //    Assert.Equal(200, code);
        //}

        [Theory]
        [InlineData("/")]
        [InlineData("/TodoBook/Index")]
        [InlineData("/TodoBook/Create")]
        [InlineData("/Account/Login")]
        [InlineData("/Account/Register")]

        public async void TestPagesWithoutId(string URL)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }

    
    }
}
