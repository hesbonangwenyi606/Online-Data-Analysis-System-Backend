### Doctor Appointment System (ASP.NET MVC 5)
A fully functional Doctor Appointment Management System built with ASP.NET MVC 5. This system allows patients to book appointments, view schedules, and manage appointments online. Doctors and admins can manage schedules, appointments, and reports.

## Full Name of Project
**ODAS: Online Doctor Appointment System**
Project Namespace (Backend): ODAS.Backend

## Features
Patient Features
Register and login as a patient
Book, view, and cancel appointments
View doctors’ profiles and schedules
Receive notifications for upcoming appointments

**Doctor Features**
View scheduled appointments
Update availability and schedules
Manage patient appointments

**Admin Features**
Manage doctors, patients, and appointments
Generate reports and statistics
View system activity and logs

**Technology Stack**
Frontend: ASP.NET MVC 5, Razor views, Bootstrap 4
Backend: C#, ASP.NET MVC 5, Entity Framework 6
Database: SQL Server (local or remote)
Tools: Visual Studio 2019/2022, Git, Bitbucket

**Project Structure**
Doctor-Appointment-System/
│
├── Controllers/          # MVC Controllers
├── Models/               # Entity and View Models
├── Views/                # Razor Views (UI)
├── Scripts/              # JavaScript files
├── Content/              # CSS, images, fonts
├── App_Data/             # Database files
├── Web.config            # Project configuration
└── README.md             # Project documentation

## Dependencies Installation
Before running the project, make sure you have all dependencies installed:

1 Visual Studio
Download Visual Studio 2019 or 2022 with ASP.NET and web development workload.

2 .NET Framework
Ensure .NET Framework 4.7.2 or higher is installed.

3 SQL Server
Install SQL Server Express and SQL Server Management Studio (SSMS).

4 NuGet Packages
All project dependencies are managed via NuGet. To restore them:

Open the solution in Visual Studio.
Right-click the solution → Restore NuGet Packages.

## Update-Package -reinstall
This will install all required packages such as Entity Framework, Bootstrap, and other libraries used in the project.

## How to Start the Project
1.Clone the Repository
git clone git@github.com:hesbonangwenyi606/Online-Data-Analysis-System-Backend.git
cd doctor-appointment-system

2.Open in Visual Studio
Open Doctor-Appointment-System.sln.

Wait for Visual Studio to restore NuGet packages automatically.
3.Configure the Database
Open Web.config and update the connectionString:

<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Server=localhost\SQLEXPRESS;Database=DoctorAppointmentDB;Trusted_Connection=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>


If using Code First Migrations, run these in Package Manager Console:

Enable-Migrations
Add-Migration InitialCreate
Update-Database

4.Run the Project in Visual Studio
Press F5 to start the project.

The app will run at:
http://localhost:<port>/
Use the app in your browser to test patient, doctor, and admin functionality.

5.Run the Project via Command Line (Optional)
If you want to run the project without opening Visual Studio:

Open a terminal in the project folder.
Navigate to the backend project folder:
cd ODAS.Backend


Build the project:
dotnet build


Run the project:
dotnet run


The terminal will show something like:
Now listening on: http://localhost:5000


Open the URL in your browser to access the application.
Sample API Endpoints & Example JSON Responses
This project exposes RESTful API endpoints for appointments, doctors, and patients.

1.Get All Appointments
Endpoint:

**GET /api/appointments**
Response Example:

[
  {
    "AppointmentId": 1,
    "PatientName": "John Doe",
    "DoctorName": "Dr. Smith",
    "Date": "2026-01-02T10:00:00",
    "Status": "Scheduled"
  }
]

2.Get Appointment by ID
Endpoint:

**GET /api/appointments/{id}**
Response Example:

{
  "AppointmentId": 1,
  "PatientName": "John Doe",
  "DoctorName": "Dr. Smith",
  "Date": "2026-01-02T10:00:00",
  "Status": "Scheduled",
  "Notes": "Patient has a mild fever"
}

3.Create a New Appointment
Endpoint:

**POST /api/appointments**
Request Body Example:

{
  "PatientId": 5,
  "DoctorId": 2,
  "Date": "2026-01-10T11:00:00",
  "Notes": "First-time consultation"
}


Response Example:

{
  "AppointmentId": 10,
  "PatientId": 5,
  "DoctorId": 2,
  "Date": "2026-01-10T11:00:00",
  "Status": "Scheduled",
  "Notes": "First-time consultation"
}

4.Update an Appointment
Endpoint:

**PUT /api/appointments/{id}**
Request Body Example:

{
  "Date": "2026-01-11T12:00:00",
  "Status": "Rescheduled",
  "Notes": "Patient requested new time"
}


Response Example:

{
  "AppointmentId": 10,
  "PatientId": 5,
  "DoctorId": 2,
  "Date": "2026-01-11T12:00:00",
  "Status": "Rescheduled",
  "Notes": "Patient requested new time"
}

5.Delete an Appointment
Endpoint:

**DELETE /api/appointments/{id}**
Response Example:

{
  "message": "Appointment deleted successfully."
}

6.Get All Doctors
Endpoint:

**GET /api/doctors**
Response Example:

[
  {
    "DoctorId": 1,
    "Name": "Dr. Smith",
    "Specialization": "Cardiology",
    "AvailableSlots": ["2026-01-02T10:00:00","2026-01-03T14:00:00"]
  }
]

Tip: Use Postman or cURL to test endpoints. Example:
curl -X GET http://localhost:5000/api/appointments

**Contribution Guidelines**
Fork the repository and create a new branch:
git checkout -b feature/your-feature-name
Commit your changes:
git commit -m "Add feature XYZ"
Push to your branch:
git push origin feature/your-feature-name
Create a Pull Request to merge into main.

## License
This project is licensed under the MIT License. See LICENSE