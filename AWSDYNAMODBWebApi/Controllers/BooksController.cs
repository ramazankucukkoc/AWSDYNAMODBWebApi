using Amazon.DynamoDBv2.DataModel;
using AWSDYNAMODBWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AWSDYNAMODBWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IDynamoDBContext _dbContext;

        public BooksController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            List<Book> books = await _dbContext.ScanAsync<Book>(default).GetRemainingAsync();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            Book book = await _dbContext.LoadAsync<Book>(id);
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            //Book getById = await _dbContext.LoadAsync<Book>(book.ID);
            //if (getById != null)
            //    return BadRequest($"Book with Id {book.ID} already exists.");
            await _dbContext.SaveAsync(book);
            return CreatedAtAction(nameof(BooksController.Add), new { book.Id }, book);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Book book)
        {
            Book getById = await _dbContext.LoadAsync<Book>(book.Id);
            if (getById == null)
                return NotFound();
            await _dbContext.SaveAsync(book);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            Book getById = await _dbContext.LoadAsync<Book>(id);
            if (getById == null)
                return NotFound();
            await _dbContext.DeleteAsync(getById);
            return Ok();
        }
    }
}
