# 🏥 Hospital Appointment Management System

## 📌 Project Overview

The **Hospital Appointment Management System** is a web application developed using ASP.NET Core MVC and C#. It displays doctor information, calculates consultation charges, manages patient appointment requests, retrieves posts from an external API, and demonstrates state management and caching techniques.

This project is developed as part of an academic assignment to understand important ASP.NET Core MVC concepts.

## 🎯 Objectives

- Display doctor details and medical specializations.
- Calculate total consultation charges, including a ₹100 service charge.
- Retrieve posts asynchronously from an external REST API.
- Store patient information using cookies.
- Maintain the selected specialization using session state.
- Pass DoctorId using query strings and hidden form fields.
- Implement response caching and in-memory caching.

## 🛠️ Technologies Used

- **Frontend:** HTML, CSS, Razor Views, Bootstrap
- **Backend:** C#, ASP.NET Core MVC
- **API:** JSONPlaceholder REST API
- **State Management:** Cookies, Session, Query Strings, Hidden Fields
- **Caching:** IMemoryCache and Response Caching
- **Development Tools:** Visual Studio, Git, GitHub

## ✨ Features

### 1. Doctor Management
Displays doctor information, including:

- Doctor ID
- Doctor Name
- Specialization
- Years of Experience
- Consultation Fee

### 2. Total Consultation Charge

An extension method named `CalculateTotalCharge()` calculates the total amount by adding a fixed ₹100 service charge to the doctor's consultation fee.

**Formula:**

Total Charge = Consultation Fee + ₹100

**Example:**

| Description | Amount |
|---|---:|
| Consultation Fee | ₹500 |
| Service Charge | ₹100 |
| Total Charge | ₹600 |

### 3. Asynchronous Web Request

The application retrieves posts from the following API:

https://jsonplaceholder.typicode.com/posts

It uses asynchronous HTTP requests to retrieve and display post IDs and titles in a table.

### 4. State Management

- **Cookie:** Stores the patient's name.
- **Session:** Stores the selected medical specialization.
- **Query String:** Passes DoctorId to the doctor details page.
- **Hidden Field:** Submits DoctorId with the appointment form.

### 5. Caching

- **Response Caching:** Configured for the doctor details page with a 60-second duration.
- **In-Memory Caching:** Stores the doctor list for three minutes to reduce repeated data creation.

## 📂 Project Structure

```text
HospitalAppointmentSystem/
│
├── Controllers/
│   └── HomeController.cs
│
├── Models/
│   └── Doctor.cs
│
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Details.cshtml
│   │   ├── Appointment.cshtml
│   │   ├── Confirmation.cshtml
│   │   └── Posts.cshtml
│   │
│   └── _ViewImports.cshtml
│
├── wwwroot/
├── Program.cs
├── appsettings.json
└── README.md
```

## ⚙️ Installation and Setup

### Prerequisites

- Visual Studio 2022 or a compatible .NET development environment
- A compatible .NET SDK for the project's target framework
- Internet connection for the external API

### Steps to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/YOUR-USERNAME/HospitalAppointmentSystem.git
   ```

2. Open the project solution in Visual Studio.

3. Restore the NuGet packages.

4. Verify the namespace used in the project files.

5. Build the project using **Build → Build Solution**.

6. Run the application using **F5** or **Ctrl + F5**.

## 🌐 Application URLs

| Page | URL |
|---|---|
| Doctor List | `/Home/Index` |
| Doctor Details | `/Home/Details?doctorId=1` |
| Appointment Form | `/Home/Appointment?doctorId=1` |
| External API Posts | `/Home/Posts` |
| Filter by Specialization | `/Home/Index?specialization=Cardiology` |

## 🧪 Sample Test Cases

| Test Case | Expected Result |
|---|---|
| Open the doctor list | Displays available doctors |
| View doctor details | Displays doctor information and total charge |
| Select a specialization | Stores the selection in session |
| Submit patient name | Updates the patient name cookie |
| Submit appointment form | Displays the confirmation page |
| Open the posts page | Displays post IDs and titles |
| Request doctor list again | Reuses cached data while valid |

## 📚 Concepts Learned

- ASP.NET Core MVC architecture
- C# classes, properties, and extension methods
- Razor Views and model binding
- Asynchronous programming with `async` and `await`
- HTTP GET and POST requests
- Cookies and session management
- Query strings and hidden form fields
- In-memory and response caching
- REST API integration
- Git and GitHub project management

## ⚠️ Limitations

- Doctor records are sample data stored in memory.
- Appointment requests are not persisted in a database.
- The project demonstrates appointment submission but does not implement a complete booking and scheduling system.
- Authentication, authorization, and appointment conflict detection are not implemented.

## 🔮 Future Enhancements

- Integrate SQL Server using Entity Framework Core.
- Add patient registration and login.
- Add appointment date and time selection.
- Prevent duplicate or conflicting bookings.
- Create an admin dashboard to manage doctors and appointments.
- Add appointment status tracking and cancellation.
- Improve the UI with responsive styling.

## 👨‍💻 Author

**Student Project**

Developed as part of an academic assignment on ASP.NET Core MVC.

## 📄 License

This project is intended for educational purposes. Add a suitable open-source license if you plan to distribute or reuse the project publicly.
