# Movie Ticket Booking System

A backend REST API application built using **C#, ASP.NET Core Web API, Entity Framework Core, and SQL Server** to manage movies, theatres, screens, seats, show schedules, and movie ticket bookings.

## Project Overview

The Movie Ticket Booking System allows users to view movies and show schedules, check seat availability, book movie tickets, retrieve booking details, and cancel bookings.

The application follows a layered architecture to maintain separation of concerns and make the code easier to maintain.

## Technologies Used

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server
- LINQ
- Scalar / Postman
- Visual Studio 2026

## Architecture

The project follows this architecture:

**Controller → Service → Repository → DbContext → SQL Server**

- **Controller:** Handles HTTP requests and responses.
- **Service:** Contains business logic and validation.
- **Repository:** Handles database operations.
- **DbContext:** Manages entity configurations and database interactions.
- **SQL Server:** Stores application data.

## Project Structure

```text
MovieTicketBooking/
│
├── Controllers/
│   ├── MoviesController.cs
│   ├── TheatresController.cs
│   ├── ScreensController.cs
│   ├── SeatsController.cs
│   ├── ShowsController.cs
│   └── BookingsController.cs
│
├── Models/
│   ├── Movie.cs
│   ├── Theatre.cs
│   ├── Screen.cs
│   ├── Seat.cs
│   ├── Show.cs
│   ├── ShowSeat.cs
│   ├── Booking.cs
│   └── BookingSeat.cs
│
├── DTOs/
├── Data/
│   └── ApplicationDbContext.cs
│
├── Repositories/
├── Services/
├── appsettings.json
├── Program.cs
└── README.md
```

## Features

### Movie Management
- Add, view, update, and delete movies.
- Retrieve movies by ID and search movies.

### Theatre Management
- Add, view, update, and delete theatres.

### Screen Management
- Manage screens associated with theatres.
- Retrieve screens by theatre.

### Seat Management
- Add individual seats or multiple seats.
- View seats by screen.
- Update and delete seat details.

### Show Management
- Schedule shows for movies and screens.
- Prevent overlapping show schedules on the same screen.
- Retrieve shows by movie and screen.
- Update and delete shows.
- Automatically create show-specific seat records.
- Track seat availability for each show.

### Booking Management
- Book available seats for a show.
- Generate a unique booking reference.
- Calculate the total booking amount.
- Retrieve bookings by ID and booking reference.
- Cancel bookings.
- Release seats when a booking is cancelled.
- Preserve booking history.

## Database Entities

| Entity | Description |
|---|---|
| Movie | Stores movie information |
| Theatre | Stores theatre details |
| Screen | Represents screens inside theatres |
| Seat | Stores physical seats for each screen |
| Show | Represents a movie scheduled on a screen |
| ShowSeat | Tracks seat availability for a particular show |
| Booking | Stores customer booking information |
| BookingSeat | Stores seats associated with each booking |

## Database Relationships

- One Theatre has many Screens.
- One Screen has many Seats.
- One Screen can have many Shows.
- One Movie can have many Shows.
- One Show has many ShowSeats.
- One Booking has many BookingSeats.
- One Seat can be associated with multiple bookings across different shows.

## Getting Started

### Prerequisites

- Visual Studio 2026
- .NET 10 SDK
- SQL Server Express
- SQL Server Management Studio (optional)
- Postman

### 1. Clone the repository

```bash
git clone <your-github-repository-url>
```

### 2. Open the project

Open `MovieTicketBooking.sln` in Visual Studio.

### 3. Configure the database connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MovieTicketBookingDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Apply database migrations

Open Package Manager Console in Visual Studio and execute:

```powershell
Update-Database
```

Alternatively, using the .NET CLI:

```bash
dotnet ef database update
```

### 5. Run the application

Press `F5` or `Ctrl + F5` in Visual Studio to start the Web API.

## API Testing

Use Postman to test the REST API endpoints.

| Module | Endpoint | HTTP Method |
|---|---|---|
| Movies | `/api/movies` | GET, POST |
| Movies | `/api/movies/{id}` | GET, PUT, DELETE |
| Theatres | `/api/theatres` | GET, POST |
| Screens | `/api/screens` | GET, POST |
| Seats | `/api/seats` | GET, POST |
| Shows | `/api/shows` | GET, POST |
| Shows | `/api/shows/{id}` | GET, PUT, DELETE |
| Show Seats | `/api/shows/{showId}/seats` | GET |
| Available Seats | `/api/shows/{showId}/seats/available` | GET |
| Bookings | `/api/bookings` | GET, POST |
| Booking by ID | `/api/bookings/{id}` | GET |
| Booking by Reference | `/api/bookings/reference/{reference}` | GET |
| Cancel Booking | `/api/bookings/{id}/cancel` | PUT |

*Refer to the API documentation for additional endpoints and request details.*

## Transaction Management

Booking creation and cancellation use database transactions to maintain consistency between booking records, booking seats, and show seat availability.

- Booking creation saves the booking and associated seats while marking show seats as booked.
- Booking cancellation updates the booking status and releases the associated show seats.
- Cancelled bookings are preserved to maintain booking history.

## Future Enhancements

- Authentication and authorization
- Payment integration
- Frontend application
- Email booking confirmation
- Advanced movie and show search

## Author

**Poomani Nayinan**

Cybersecurity Student | Aspiring Software Developer

GitHub: [poomani06](https://github.com/poomani06)
