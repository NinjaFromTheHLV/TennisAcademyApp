# TennisAcademyApp 
Welcome to my first ever project - TennisAcademy.
Tennis Academy is an web app which provides separate interfaces for User and Admin. 

## Features

### Reservations

- Make court reservations within academy hours.
- View reservation details.
- View reservation history.
- Admins do not have reservation privileges.

### Tennis academy shop

- Browse and add rackets, balls, and bags to your cart.
- Checkout cart items (deleting items in cart from database).
- Add to cart multiple times within the item's stock limit.
- Remove items from cart.

### Shop management
- Admin feature: add, edit and delete items if nessecarry.

### Tennis academy coaches

- Search and view coach details with pagination.
- Add coaches to favorites.
- Remove coaches from favourites.
- Admin-sided only: Manage coaches by adding, updating, or deleting.

### User management (Admin-sided)

- View all users registered.
- Manage their roles(assign, delete)
- Delete users

## Technologies
#### Backend
* Entity Framework Core for database interactions.
* Model-View-Controller architecture pattern.
#### Frontend
* Razor Views with HTML, CSS, JavaScript and Bootstrap for responsive design.
#### Database
* SQL Server Management Studio for secure and efficient data storage.

### Testing
- NUnit: Comprehensive testing framework.
- Moq: Mocking framework for unit testing.

## How to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/NinjaFromTheHLV/TennisAcademyApp.git
   ```

2. Navigate to the project directory:
   ```bash
   cd TennisAcademyApp
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   dotnet build
   ```

4. Create the database:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```
