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

![IMS-login](https://github.com/user-attachments/assets/b3f09641-cfe0-4c93-a8c6-0d5385ace89c)
![dashboard](https://github.com/user-attachments/assets/95017810-54d2-45a4-9edc-fc9b9f20b1e6)
![category](https://github.com/user-attachments/assets/c42779c6-f8c0-44df-b1d7-0ae76f4214e8)
![products](https://github.com/user-attachments/assets/c5060e01-6f78-4c27-8587-c1f2c62cffe6)
![add-product](https://github.com/user-attachments/assets/97a039aa-da50-4c6c-bcd2-f025ab53e3a4)
![all-product-report](https://github.com/user-attachments/assets/0b6e651f-6c40-4d92-8084-f270232a76eb)
![low-stock-report](https://github.com/user-attachments/assets/cca83885-0b8f-493d-995e-1fef3071e542)






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
