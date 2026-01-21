# CSE Mock Exam Backend API (In-Progress)

A backend REST API for a **Civil Service Exam (CSE) Mock Examination System**. This project provides endpoints for managing users, mock exams, questions, answers, scoring, and results. It is designed to be used by a web or mobile frontend.

---

## 🚀 Features

* User management (registration, authentication)
* Mock exam management
* Question bank (multiple choice)
* Exam attempts and submissions
* Automatic scoring
* Exam results and performance summary
* RESTful API design
* DTO-based response structure
* Database-backed persistence

---

## 🛠 Tech Stack

* **Backend Framework:** ASP.NET Core Web API
* **Language:** C#
* **Database:** PostgreSQL / SQL Server (configurable)
* **ORM:** Entity Framework Core (Code First)
* **Authentication:** JWT Bearer Authentication
* **API Documentation:** Swagger / OpenAPI
* **Environment:** .NET 7 / .NET 8

---

## 📁 Project Structure

```
CSEMockExam.API/
│── Controllers/        # API controllers
│── DTOs/               # Data Transfer Objects
│── Models/             # Entity models
│── Data/               # DbContext and configurations
│── Services/           # Business logic
│── Repositories/       # Data access layer (optional)
│── Migrations/         # EF Core migrations
│── Program.cs          # Application entry point
│── appsettings.json    # Configuration
```

---

## ⚙️ Setup & Installation

### Prerequisites

* .NET SDK 8 or 9
* PostgreSQL or SQL Server
* Git

### Clone the Repository

```bash
git clone https://github.com/your-username/csemockexam-backend-api.git
cd csemockexam-backend-api
```

### Configure Database

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=csemockexam;Username=postgres;Password=yourpassword"
}
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run the Application

```bash
dotnet run
```

API will be available at:

```
http://localhost:5000
```

Swagger UI:

```
http://localhost:5000/swagger
```

---

## 🔐 Authentication

This API uses **JWT Authentication**.

### Login Flow

1. User logs in using email and password
2. API returns a JWT token
3. Token is included in request headers:

```
Authorization: Bearer <token>
```

---

## 🧪 Testing

You can test endpoints using:

* Swagger UI
* Postman
* Curl

Example:

```bash
curl -X GET http://localhost:5000/api/exams
```

---

## 📦 Environment Variables (Optional)

```
ASPNETCORE_ENVIRONMENT=Development
JWT_SECRET=your_secret_key
```

---

## 📌 Future Improvements

* Role-based access control (Admin / Examinee)
* Question randomization
* Time-limited exams
* Detailed analytics per exam
* Docker support

---

## 🤝 Contributing

Contributions are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit changes
4. Open a pull request

---

## 📄 License

This project is for **educational and portfolio purposes**.

---

## 👤 Author

**Arvin**
**Francis**
Backend Developer (ASP.NET Core)

---

> This project is intended to demonstrate backend engineering skills such as API design, database modeling, authentication, and clean architecture.
