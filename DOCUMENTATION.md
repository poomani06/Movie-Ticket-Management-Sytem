# Movie Ticket Booking System – Technical Documentation

## 1. Introduction

The Movie Ticket Booking System is a RESTful backend application developed using ASP.NET Core Web API and Entity Framework Core.

The application manages movies, theatres, screens, seats, show schedules, and customer bookings through HTTP endpoints.

## 2. System Architecture

The application follows a layered architecture.

```text
Client (Postman)
      |
      v
   Controller
      |
      v
    Service
      |
      v
  Repository
      |
      v
  DbContext
      |
      v
  SQL Server
```

### Controller Layer

Receives HTTP requests, validates incoming DTOs, invokes service methods, and returns appropriate HTTP responses.

### Service Layer

Contains business logic, validation, and coordination between repositories.

### Repository Layer

Performs database operations using Entity Framework Core.

### Data Layer

Contains `ApplicationDbContext`, entity configurations, and database migrations.

## 3. Entity Description

### Movie

Stores movie details such as title, description, duration, release date, language, and genre.

### Theatre

Stores theatre name and location.

### Screen

Represents a screen belonging to a theatre.

### Seat

Represents a physical seat belonging to a screen. Each seat has a row name, seat number, and seat type.

### Show

Represents a movie scheduled on a particular screen with a start time, end time, and ticket price.

### ShowSeat

Represents the availability of a physical seat for a specific show.

Possible statuses:

- Available
- Booked

### Booking

Stores customer information, booking reference, booking date, total amount, and booking status.

Possible statuses:

- Confirmed
- Cancelled

### BookingSeat

Stores the seats associated with a booking and their individual prices.

## 4. Database Relationships

```text
Theatre 1 -------- * Screen

Screen  1 -------- * Seat

Movie   1 -------- * Show

Screen  1 -------- * Show

Show    1 -------- * ShowSeat

Seat    1 -------- * ShowSeat

Booking  1 -------- * BookingSeat

Seat    1 -------- * BookingSeat
```

## 5. Booking Workflow

### Create Booking

1. Client submits customer details, show ID, and selected seat IDs.
2. Service validates customer information.
3. Service checks whether the requested show exists.
4. Service verifies that the selected seats belong to the show and are available.
5. Service calculates the total booking amount.
6. A unique booking reference is generated.
7. Booking and BookingSeat records are saved.
8. Selected ShowSeat records are updated to `Booked`.
9. Transaction commits.
10. API returns the booking details.

### Cancel Booking

1. Client sends a cancellation request using the booking ID.
2. Service checks whether the booking exists.
3. Service verifies that the booking has not already been cancelled.
4. Associated BookingSeat records are retrieved.
5. Booking status is changed to `Cancelled`.
6. Associated ShowSeat records are changed to `Available`.
7. Changes are saved within a database transaction.
8. API returns a cancellation confirmation.

## 6. HTTP Status Codes

| Status Code | Description |
|---|---|
| 200 OK | Request completed successfully |
| 201 Created | Resource created successfully |
| 204 No Content | Request completed without response body |
| 400 Bad Request | Invalid request or business validation failure |
| 404 Not Found | Requested resource does not exist |
| 405 Method Not Allowed | HTTP method is not supported for the endpoint |
| 500 Internal Server Error | Unexpected server-side error |

## 7. Validation Rules

- Required fields must be provided.
- Customer email must have a valid email format.
- Customer phone must satisfy the configured phone validation.
- Show end time must be later than start time.
- Ticket price cannot be negative.
- A screen cannot have overlapping show schedules.
- Duplicate seat selections in a booking are not allowed.
- Only available seats can be booked.
- A cancelled booking cannot be cancelled again.

## 8. Database Transactions

Transactions are used for booking-related operations to ensure that related database changes succeed or fail together.

### Booking Creation Transaction

- Insert Booking.
- Insert BookingSeat records.
- Update ShowSeat status.
- Commit transaction.

### Booking Cancellation Transaction

- Update Booking status.
- Update ShowSeat status to Available.
- Commit transaction.

If an operation fails, the transaction is rolled back.

## 9. API Endpoint Reference

### Movies

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/movies` | Retrieve all movies |
| GET | `/api/movies/{id}` | Retrieve movie by ID |
| POST | `/api/movies` | Create movie |
| PUT | `/api/movies/{id}` | Update movie |
| DELETE | `/api/movies/{id}` | Delete movie |

### Theatres

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/theatres` | Retrieve all theatres |
| GET | `/api/theatres/{id}` | Retrieve theatre by ID |
| POST | `/api/theatres` | Create theatre |
| PUT | `/api/theatres/{id}` | Update theatre |
| DELETE | `/api/theatres/{id}` | Delete theatre |

### Screens

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/screens` | Retrieve all screens |
| GET | `/api/screens/{id}` | Retrieve screen by ID |
| POST | `/api/screens` | Create screen |
| PUT | `/api/screens/{id}` | Update screen |
| DELETE | `/api/screens/{id}` | Delete screen |

### Seats

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/seats` | Retrieve all seats |
| GET | `/api/seats/{id}` | Retrieve seat by ID |
| GET | `/api/seats/screen/{screenId}` | Retrieve seats by screen |
| POST | `/api/seats` | Create seat |
| POST | `/api/seats/bulk` | Create multiple seats |
| PUT | `/api/seats/{id}` | Update seat |
| DELETE | `/api/seats/{id}` | Delete seat |

### Shows

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/shows` | Retrieve all shows |
| GET | `/api/shows/{id}` | Retrieve show by ID |
| GET | `/api/shows/movie/{movieId}` | Retrieve shows by movie |
| GET | `/api/shows/screen/{screenId}` | Retrieve shows by screen |
| POST | `/api/shows` | Create show |
| PUT | `/api/shows/{id}` | Update show |
| DELETE | `/api/shows/{id}` | Delete show |
| GET | `/api/shows/{showId}/seats` | Retrieve show seats |
| GET | `/api/shows/{showId}/seats/available` | Retrieve available seats |
| GET | `/api/shows/{showId}/seats/booked` | Retrieve booked seats |

### Bookings

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/bookings` | Retrieve all bookings |
| GET | `/api/bookings/{id}` | Retrieve booking by ID |
| GET | `/api/bookings/reference/{reference}` | Retrieve booking by reference |
| POST | `/api/bookings` | Create booking |
| PUT | `/api/bookings/{id}/cancel` | Cancel booking |

*Note: Verify endpoint paths against the final controller implementation before publishing.*

## 10. Testing

The API endpoints were tested using Postman.

Testing includes:

- CRUD operations.
- Resource retrieval by ID.
- Movie and screen-based show retrieval.
- Show schedule overlap validation.
- Automatic ShowSeat creation.
- Seat availability checking.
- Successful booking creation.
- Booking retrieval by ID and reference.
- Seat status update after booking.
- Booking cancellation.
- Seat release after cancellation.
- Duplicate cancellation validation.

## 11. Conclusion

This project demonstrates the development of a structured REST API using ASP.NET Core Web API, Entity Framework Core, and SQL Server. It applies layered architecture, entity relationships, data validation, LINQ, dependency injection, and transaction management to implement a movie ticket booking workflow.
