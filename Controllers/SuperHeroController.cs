using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        // private readonly AppDbContext _context;
        // public SuperHeroController(AppDbContext context)
        // {
        //     _context = context;
        // } 

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hero = heroes.FirstOrDefault( x => x.Id == id);

            if(hero == null)
                return NotFound("Super herói não encontrado.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(hero);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHero(SuperHero hero)
        {
            var superHero = heroes.FirstOrDefault(x => x.Id == hero.Id);
            
            if(hero == null)
                return NotFound("Super herói não encontrado.");

            superHero.Name = hero.Name;
            superHero.FirstName = hero.FirstName;
            superHero.LastName = hero.LastName;
            superHero.Place = hero.Place;

            return Ok(hero);           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(SuperHero hero)
        {
            var superHero = heroes.FirstOrDefault(x => x.Id == hero.Id);
            
            if(hero == null)
                return NotFound("Super herói não encontrado.");

            heroes.Remove(superHero);

            return Ok(superHero);           
        }
    }
}