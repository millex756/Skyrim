using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skyrim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {};

        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get()
        {
            return Ok(await _context.Characters.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> HttpGet(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
                return BadRequest("Character not found!");
            else
                return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> AddCharacter(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return Ok(await _context.Characters.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Character>>> UpdateCharacter(Character request)
        {
            var dbCharacter = await _context.Characters.FindAsync(request.Id);
            if (dbCharacter == null)
                return BadRequest("Character not found!");

            dbCharacter.Level = request.Level;
            dbCharacter.Race = request.Race;
            dbCharacter.FirstName = request.FirstName;
            dbCharacter.LastName = request.LastName;

            await _context.SaveChangesAsync();
            return Ok(await _context.Characters.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Character>>> Delete(int id)
        {
            var dbCharacter = await _context.Characters.FindAsync(id);
            if (dbCharacter == null)
                return BadRequest("Character not found!");
            else
                _context.Characters.Remove(dbCharacter);
            await _context.SaveChangesAsync();
            return (await _context.Characters.ToListAsync());
        }

    }
}
