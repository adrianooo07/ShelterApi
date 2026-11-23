using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;
using ShelterApi.Models.DTOs;

namespace ShelterApi.Controllers
{
    [Route("animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public AnimalsController(AnimalshelterContext context)
        {
            _context = context;
        }

        //uj allat hozzaadasa
        [HttpPost("newAnimal")]
        public async Task<IActionResult> NewAnimal(NewAnimalDto dto)
        {
            try
            {
                var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Id == dto.ShelterId);

                if (shelter == null)
                {
                    return StatusCode(404, new
                    {
                        Message = "A menhely nem talalhato.",
                        result = (object?)null
                    });
                }

                Animal animal = new Animal();

                animal.Id = Guid.NewGuid();
                animal.Name = dto.Name;
                animal.Species = dto.Species;
                animal.Age = dto.Age;
                animal.Weight = dto.Weight;
                animal.RegTime = DateTime.Now;
                animal.ShelterId = dto.ShelterId;

                _context.Animals.Add(animal);
                await _context.SaveChangesAsync();

                return StatusCode(201, new
                {
                    Message = "Az allat sikeresen hozzaadva.",
                    result = animal
                });
            }
            catch (Exception e)
            {
                return StatusCode(400, new
                {
                    message = e.Message,
                    result = (object?)null
                });
            }
        }

        //osszes allat lekerdezese
        [HttpGet("allAnimal")]
        public async Task<IActionResult> AllAnimal()
        {
            try
            {
                var animals = await _context.Animals.ToListAsync();

                if (animals.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        Message = "Nincsennek allatok a rendszerben.",
                        result = (object?)null
                    });
                }

                return StatusCode(200, new
                {
                    Message = "Az allatok sikeresen lekerdezve.",
                    result = animals
                });
            }
            catch (Exception e)
            {
                return StatusCode(400, new
                {
                    message = e.Message,
                    result = (object?)null
                });
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetAnimalById(Guid id)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

                if (animal == null)
                {
                    return StatusCode(404, new
                    {
                        Message = "Az allat nem talalhato.",
                        result = (object?)null
                    });
                }

                return StatusCode(200, new
                {
                    Message = "Az allat sikeresen lekerdezve.",
                    result = animal
                });
            }
            catch (Exception e)
            {
                return StatusCode(400, new
                {
                    message = e.Message,
                    result = (object?)null
                });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, UpdateAnimalDto dto)
        {
            try
            {
                var animal = await _context.Animals
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (animal == null)
                {
                    return StatusCode(404, new
                    {
                        message = "Az állat nem található.",
                        result = (object?)null
                    });
                }

                var shelter = await _context.Shelters
                    .FirstOrDefaultAsync(x => x.Id == dto.ShelterId);

                if (shelter == null)
                {
                    return StatusCode(404, new
                    {
                        message = "A megadott menhely nem található.",
                        result = (object?)null
                    });
                }

                animal.Name = dto.Name;
                animal.Species = dto.Species;
                animal.Age = dto.Age;
                animal.Weight = dto.Weight;
                animal.ShelterId = dto.ShelterId;

                await _context.SaveChangesAsync();

                return StatusCode(200, new
                {
                    message = "Az allat adatai sikeresen modositva.",
                    result = animal
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

        [HttpDelete("deleteAnimal")]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

                if (animal == null)
                {
                    return StatusCode(404, new
                    {
                        Message = "Az allat nem talalhato.",
                        result = (object?)null
                    });
                }

                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();

                return StatusCode(200, new
                {
                    Message = "Az allat sikeresen torolve.",
                    result = animal
                });
            }
            catch (Exception e)
            {
                return StatusCode(400, new
                {
                    message = e.Message,
                    result = (object?)null
                });
            }
        }
    }
}
