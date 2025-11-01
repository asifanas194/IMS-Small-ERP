# IMS Dashboard

**Inventory Management System (ASP.NET Core MVC)**

---

## Features

- User authentication & role management
- Product management (Add/Edit/Delete)
- Category management
- Vendor management
- Reports:
  - All Products
  - Low Stock
  - Vendor List
- User activity logs
- Responsive admin dashboard
- SweetAlert2 notifications for actions
- Role-based sidebar navigation

---

## Screenshots

**Dashboard**  
![Dashboard](screenshots/dashboard.png)

**Products Page**  
![Products](screenshots/products.png)

**Reports Page**  
![Reports](screenshots/reports.png)

---

## Technologies Used

- ASP.NET Core MVC  
- Entity Framework Core  
- SQL Server  
- Bootstrap 5  
- SweetAlert2  

---

## Setup Instructions

1. Clone the repository:

git clone https://github.com/asifanas194/IMS-Small-ERP.git
Open the project in Visual Studio 2022 or later.

Important: Update your appsettings.json with your SQL Server connection string:


"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=IMS;Trusted_Connection=True;"
}
⚠️ Without your connection string, the project will compile but data will not show.

Run the migrations (if using EF Core migrations):


dotnet ef database update
Run the project (F5 in Visual Studio or dotnet run from terminal).

Notes
Make sure UserActivityLogs table exists in your database for logging to work.

Only Admin role can access Category and Vendor management pages.

License
This project is open source for learning and personal use.
