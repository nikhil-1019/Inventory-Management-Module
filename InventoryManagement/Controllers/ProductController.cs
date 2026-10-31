using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace InventoryManagement.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        var products = new List<Product>();
        using var connection = Database.GetConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT ProductId, ProductName, Quantity, Price FROM Products ORDER BY ProductId";
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                ProductId = reader.GetInt32(0),
                ProductName = reader.GetString(1),
                Quantity = reader.GetInt32(2),
                Price = reader.GetDecimal(3)
            });
        }
        return View(products);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid) return View(product);

        using var connection = Database.GetConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO Products (ProductName, Quantity, Price)
                                VALUES ($name, $quantity, $price)";
        command.Parameters.AddWithValue("$name", product.ProductName);
        command.Parameters.AddWithValue("$quantity", product.Quantity);
        command.Parameters.AddWithValue("$price", product.Price);
        command.ExecuteNonQuery();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT ProductId, ProductName, Quantity, Price FROM Products WHERE ProductId = $id";
        command.Parameters.AddWithValue("$id", id);
        using var reader = command.ExecuteReader();

        if (!reader.Read()) return NotFound();
        var product = new Product
        {
            ProductId = reader.GetInt32(0),
            ProductName = reader.GetString(1),
            Quantity = reader.GetInt32(2),
            Price = reader.GetDecimal(3)
        };
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid) return View(product);

        using var connection = Database.GetConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE Products
                                SET ProductName = $name, Quantity = $quantity, Price = $price
                                WHERE ProductId = $id";
        command.Parameters.AddWithValue("$id", product.ProductId);
        command.Parameters.AddWithValue("$name", product.ProductName);
        command.Parameters.AddWithValue("$quantity", product.Quantity);
        command.Parameters.AddWithValue("$price", product.Price);
        var rows = command.ExecuteNonQuery();

        if (rows == 0) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Products WHERE ProductId = $id";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();

        return RedirectToAction(nameof(Index));
    }
}
