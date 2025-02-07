# Loan API

## Overview
The Loan API is a RESTful web service that enables users to apply for loans and manage their accounts while allowing accountants to review and control loan statuses. It follows best practices such as JWT authentication, role-based authorization, and proper exception handling.

## Features

### User Management
- **Register a user** (`POST /api/auth/register`)
- **User login** (`POST /api/auth/login`)
- **Get user details** (`GET /api/users/{id}`)
- **Role-based authorization**
  - `User`: Can apply for, view, update, and delete their own loans
  - `Accountant`: Can view, edit, and delete any loan, as well as block users from applying for loans

### Loan Management
- **Apply for a loan** (`POST /api/loans`)
- **View user’s loans** (`GET /api/loans/user/{userId}`)
- **Update a loan (only if status is 'Processing')** (`PUT /api/loans/{loanId}`)
- **Delete a loan (only if status is 'Processing')** (`DELETE /api/loans/{loanId}`)
- **Accountants can update or delete any loan**
- **Accountants can block a user from applying for loans** (`PUT /api/users/block/{userId}`)

## Entities

### User
| Field        | Type    | Description                          |
|-------------|--------|--------------------------------------|
| `id`        | `GUID` | Unique identifier                   |
| `firstName` | `string` | User's first name                   |
| `lastName`  | `string` | User's last name                    |
| `username`  | `string` | Unique username                     |
| `email`     | `string` | User's email                        |
| `age`       | `int`    | User's age                          |
| `monthlyIncome` | `decimal` | Monthly income of the user       |
| `isBlocked` | `bool`  | Whether the user is blocked (`false` by default) |
| `password`  | `string` | Hashed password                     |

### Loan
| Field        | Type    | Description                          |
|-------------|--------|--------------------------------------|
| `id`        | `GUID` | Unique identifier                   |
| `userId`    | `GUID` | ID of the user who applied for the loan |
| `loanType`  | `enum` | Type of loan (`Fast`, `Auto`, `Installment`) |
| `amount`    | `decimal` | Loan amount                        |
| `currency`  | `string` | Currency type (e.g., `GEL`, `USD`)  |
| `period`    | `int`    | Loan period (in months)             |
| `status`    | `enum` | Loan status (`Processing`, `Approved`, `Rejected`) |

## Security
- **JWT-based authentication** (`Bearer Token` required for protected routes)
- **Role-based authorization** (`Accountant` vs `User`)
- **Password hashing** (stored securely using a hashing algorithm)

## Error Handling
- Returns proper HTTP status codes (`400`, `401`, `403`, `404`, `500`)
- Uses structured error messages instead of exposing raw exceptions

## Database
- Normalized schema (Users and Loans are separate tables)
- Uses a relational database (e.g., PostgreSQL, MySQL, or MSSQL)
- Logging stored in database (e.g., loan requests, user actions)

## API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/api/auth/register` | Register a new user |
| `POST` | `/api/auth/login` | User login and token generation |

### User Management
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/api/users/{id}` | Get user details |
| `PUT`  | `/api/users/block/{userId}` | Block a user from applying for loans (Accountant only) |

### Loan Management
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/api/loans` | Apply for a new loan |
| `GET`  | `/api/loans/user/{userId}` | View user's loans |
| `PUT`  | `/api/loans/{loanId}` | Update loan (only if status is 'Processing') |
| `DELETE` | `/api/loans/{loanId}` | Delete loan (only if status is 'Processing') |

## Testing
- Unit and integration tests cover all functionalities
- Uses test frameworks (e.g., xUnit, NUnit) for C#

## Deployment
- Uses CI/CD pipeline for automated testing and deployment
- Can be hosted on cloud platforms such as AWS, Azure, or DigitalOcean

## Documentation
- **Swagger/OpenAPI** documentation included
- API contracts available via Swagger UI

