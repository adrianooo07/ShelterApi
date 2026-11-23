using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public QueriesController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpGet("allSheltersWithAnimals")]
        public async Task<IActionResult> AllSheltersWithAnimals()
        {
            try
            {
                var result = await _context.Shelters
                    .Include(s => s.Animals)
                    .ToListAsync();

                return StatusCode(200, new
                {
                    message = "Menhelyek és a hozzájuk tartozó állatok lekérdezve.",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    message = ex.Message,
                    result = (object?)null
                });
            }
        }

        [HttpGet("allAnimalsWithShelter")]
        public async Task<IActionResult> AllAnimalsWithShelter()
        {
            try
            {
                var result = await (
                    from animal in _context.Animals
                    join shelter in _context.Shelters
                    on animal.ShelterId equals shelter.Id
                    select new
                    {
                        animal.Id,
                        animal.Name,
                        animal.Species,
                        animal.Age,
                        animal.Weight,
                        animal.RegTime,
                        ShelterName = shelter.Name
                    }
                ).ToListAsync();

                return StatusCode(200, new
                {
                    message = "Állatok és a menhelyeik lekérdezve.",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    message = ex.Message,
                    result = (object?)null
                });
            }
        }

        [HttpGet("animalsOfShelter")]
        public async Task<IActionResult> AnimalsOfShelter(Guid shelterId)
        {
            try
            {
                var result = await _context.Shelters
                    .Include(s => s.Animals)
                    .Where(s => s.Id == shelterId)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return StatusCode(404, new
                    {
                        message = "A megadott menhely nem található.",
                        result = (object?)null
                    });
                }

                return StatusCode(200, new
                {
                    message = "A menhely és a hozzá tartozó állatok lekérdezve.",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    message = ex.Message,
                    result = (object?)null
                });
            }
        }


        [HttpGet("animalCountByShelter")]
        public async Task<IActionResult> AnimalCountByShelter()
        {
            try
            {
                var result = await _context.Shelters
                    .Select(s => new
                    {
                        ShelterName = s.Name,
                        AnimalCount = s.Animals.Count()
                    })
                    .OrderByDescending(s => s.AnimalCount)
                    .ToListAsync();

                return StatusCode(200, new
                {
                    message = "Menhelyenkénti állatdarabszámok lekérdezve.",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    message = ex.Message,
                    result = (object?)null
                });
            }
        }
    }
}
