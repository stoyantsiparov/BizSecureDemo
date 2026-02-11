using System.Security.Claims;
using BizSecureDemo_22180022.Data;
using BizSecureDemo_22180022.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizSecureDemo.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) => _db = db;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderVm vm)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        _db.Orders.Add(new Order
        {
            UserId = uid,
            Title = vm.Title,
            Amount = vm.Amount
        });

        await _db.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Details(int id)
    {
        // Обърнете внимание: търсим само по Id, без проверка за собственост
        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        //var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == uid); // fixed
        if (order == null) return NotFound();
        return View(order);
    }
}