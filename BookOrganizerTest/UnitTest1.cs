using BookOrganizer;
using BookOrganizer.Controllers;
using BookOrganizer.Data;
using BookOrganizer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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

        private BookOrganizerContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<BookOrganizerContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new BookOrganizerContext(options);

            context.Database.OpenConnection(); // abre a conexão in-memory
            context.Database.EnsureCreated();  // cria as tabelas no SQLite

            return context;
        }
        [Fact]
        public async Task TestTodoBookIndex()
        {
            using var context = GetInMemoryContext();

            // se precisar, insere dados fake no banco
            context.Book.Add(new Book { Title = "Livro de teste" });
            context.SaveChanges();

            // usa o contexto nos testes
            var controller = new TodoBookController(context);
            var result = await controller.Index(null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BookGenreViewModel>(viewResult.Model);

            Assert.Single(model.Books!);
            Assert.Equal("Livro de teste", model.Books!.First().Title);
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/TodoBook/Create")]
        [InlineData("/Account/Login")]
        [InlineData("/Account/Register")]

        public async Task TestPagesWithoutId(string URL)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;
            Assert.Equal(200, code);
        }

    
    }
}
