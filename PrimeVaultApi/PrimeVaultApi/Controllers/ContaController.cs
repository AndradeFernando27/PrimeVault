using Microsoft.AspNetCore.Mvc;


namespace PrimeVaultApi.Controllers;

[ApiController]
[Route("Controller")]
public class ContaController : Controller
{
    private readonly AppDbContext _context;

    public ContaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public Task<IActionResult> GetUserById(int id)
    {
        
    }
}
