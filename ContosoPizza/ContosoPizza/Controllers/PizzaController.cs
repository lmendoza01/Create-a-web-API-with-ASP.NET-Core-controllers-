using ContosoPizza.Data;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaContext context;
    public PizzaController(PizzaContext context)
    {
        this.context = context;
    }


    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
    PizzaService.GetAll();

    [HttpGet("useEF")]
    public ActionResult<List<Pizza>> GetEF() => this.context.Pizzas.AsNoTracking().ToList();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action
   [HttpPost] 
   public IActionResult Create(Pizza pizza)
    {
        //PizzaService.Add(pizza);
        this.context.Pizzas.Add(pizza);
        this.context.SaveChanges();

        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}