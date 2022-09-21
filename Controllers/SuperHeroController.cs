using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id = 0,
                Name = "Spider-man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "New York"
            },

            new SuperHero
            {
                Id = 1,
                Name = "Batman",
                FirstName = "Bruce",
                LastName = "Wayne",
                Place = "Gotham"
            }
        };

        private readonly AppDbContext _context;
        public SuperHeroController(AppDbContext context)
        {
            _context = context;
        } 

        [HttpGet]
        public async Task<IActionResult> GetAll()=> Ok(await _context.SuperHeroes.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hero = await _context.SuperHeroes.FirstOrDefaultAsync( x => x.Id == id);

            if(hero == null)
                return NotFound("Super herói não encontrado.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> AddHero(SuperHero hero)
        {
            await _context.SuperHeroes.AddAsync(hero);
            _context.SaveChanges();
            return Ok(hero);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(SuperHero hero, int id)
        {
            var superHero = await _context.SuperHeroes.FirstOrDefaultAsync(x => x.Id == id);
            
            if(hero == null)
                return NotFound("Super herói não encontrado.");

            superHero.Name = hero.Name;
            superHero.FirstName = hero.FirstName;
            superHero.LastName = hero.LastName;
            superHero.Place = hero.Place;

            _context.SaveChanges();

            return Ok(hero);           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var superHero = await _context.SuperHeroes.FirstOrDefaultAsync(x => x.Id == id);
            
            if(superHero == null)
                return NotFound();

            _context.SuperHeroes.Remove(superHero);
            _context.SaveChanges();

            return Ok(superHero);           
        }
    }
}