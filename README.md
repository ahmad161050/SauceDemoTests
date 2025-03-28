### ✅ `README.md`

```markdown
# 🧪 SauceDemo End-to-End UI Tests with SpecFlow, Playwright, and MySQL

This project automates key user journeys on [saucedemo.com](https://www.saucedemo.com) using:
- ✅ SpecFlow (BDD)
- ✅ Playwright (.NET)
- ✅ NUnit
- ✅ MySQL (to store & verify orders)

---

## 🚀 Features Covered

- 🔐 Login functionality (positive & negative)
- 🛒 Checkout flow with DB insert
- ✅ Database validation of orders
- 🔁 Logout & session clearing
- 📋 Full assertions of order confirmation UI

---

## 🧰 Tech Stack

| Layer        | Tool                |
|-------------|---------------------|
| BDD          | SpecFlow            |
| Browser      | Microsoft Playwright|
| Test Runner  | NUnit               |
| Data         | MySQL               |
| Language     | C# (.NET 8)         |

---

## 🏗️ Folder Structure

```bash
SauceDemoTests/
├── Features/           # .feature files for BDD
├── Pages/              # Page Object Models
├── StepDefinitions/    # SpecFlow step bindings
├── Utils/              # DB helper classes
├── specflow.json       # SpecFlow configuration
└── SauceDemoTests.csproj
```

---

## 💻 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- MySQL user with access to the `Orders` table
- Node.js (auto-installed by Playwright)

---

## 🗄️ MySQL Setup

Ensure you have a MySQL DB running and create this schema/table:

```sql
CREATE DATABASE IF NOT EXISTS saucedemo;

USE saucedemo;

CREATE TABLE Orders (
  OrderId INT AUTO_INCREMENT PRIMARY KEY,
  Username VARCHAR(255),
  ProductName VARCHAR(255),
  TotalPrice DECIMAL(10,2),
  OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

✅ Make sure to update your **MySQL connection string** in `OrderDatabaseHelper.cs`:

```csharp
private const string ConnectionString = "Server=localhost;Database=saucedemo;User=root;Password=yourpassword;";
```

---

## 🧪 Running the Tests

1. **Restore dependencies**
   ```bash
   dotnet restore
   ```

2. **Install Playwright (first time only)**
   ```bash
   playwright install
   ```

3. **Run the tests**
   ```bash
   dotnet test
   ```

> 💡 Make sure MySQL is running before you test database validations!

---

## 🧼 Clean Build (Optional)

To ensure a fresh environment:
```bash
dotnet clean
dotnet build
```

---

## ✅ Parallel Test Support

The project is ready for parallel execution via:
- `AssemblyInfo.cs`
- `[Parallelizable]` tags in SpecFlow/NUnit
- Isolated browser instances in `Hooks.cs`

---

## 📸 Test Results

Screenshots from failed tests are saved in:
```
/TestResults/Screenshots/
```

---


