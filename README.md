# Experiment 8 – Inventory Management Module

**Aim:** Develop Inventory Management Module using ADO.NET and CRUD operations.

This project follows the practical's Product model, ProductController, and Index/Create/Edit views. It uses **ASP.NET Core MVC (.NET 8)** and **ADO.NET with SQLite** so it can be run on macOS as well as Windows. The supplied PDF uses classic ASP.NET MVC and SQL Server LocalDB; LocalDB is Windows-specific, so SQLite is used here as the cross-platform database while retaining ADO.NET CRUD operations.

## Requirements
- .NET 8 SDK
- VS Code or Visual Studio
- Internet connection for the first NuGet package restore

## Run on macOS
1. Extract the ZIP.
2. Open Terminal and enter the project folder:
   ```bash
   cd InventoryManagement
   ```
3. Restore packages and run:
   ```bash
   dotnet restore
   dotnet run
   ```
4. Open the local URL printed in Terminal (usually `http://localhost:5xxx` or `https://localhost:7xxx`).

The SQLite database file `inventory.db` is created automatically in the project folder the first time the app starts.

## CRUD operations
- **Create:** Add New Product form inserts a product.
- **Read:** Inventory page lists all products.
- **Update:** Edit link updates the selected product.
- **Delete:** Delete button removes a product after confirmation.

## Database table
The application creates this table automatically:
```sql
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName TEXT NOT NULL,
    Quantity INTEGER NOT NULL,
    Price NUMERIC NOT NULL
);
```

## Main files
- `Models/Product.cs` – Product model
- `Controllers/ProductController.cs` – ADO.NET SELECT, INSERT, UPDATE and DELETE
- `Data/Database.cs` – connection and table initialization
- `Views/Product/Index.cshtml` – inventory list
- `Views/Product/Create.cshtml` – add product
- `Views/Product/Edit.cshtml` – edit product
- `wwwroot/css/site.css` – basic styling

**Note:** This is a runnable cross-platform adaptation of the PDF practical, not a byte-for-byte extraction of a ZIP embedded in the PDF. The PDF contains source code and screenshots, not an attached ZIP archive.
