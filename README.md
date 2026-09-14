# 🚗 DVLD - Driving & Vehicle License Department Management System

A complete **Driving & Vehicle License Department (DVLD) Management System** built with **C# Windows Forms, SQL Server, and ADO.NET**.

This project simulates a real-world driving license management system, covering the complete workflow from managing people and applications to issuing, renewing, replacing, detaining, and releasing driving licenses.

---

## 📌 About The Project

DVLD is a full desktop application designed to simulate the operations of a driving license department.

The main goal of this project was not simply to build a CRUD application, but to apply software development concepts in a **large, real-world-style system** with multiple modules, business rules, database relationships, and interconnected workflows.

This project was developed as part of my programming journey with **ProgrammingAdvices**.

---

## ✨ Main Features

### 👤 People Management
- Add new people
- Update personal information
- Delete people
- Search and filter people
- View complete person information

### 🔐 Users & Authentication
- User login
- User management
- Account settings
- User activation/deactivation
- Password management
- User permissions

### 📝 Applications
- Manage application types
- Create new applications
- Track application status
- Manage application fees
- Track applications by person

### 🧪 Tests & Appointments
- Vision Test
- Written Test
- Street Test
- Schedule test appointments
- Retake tests
- Record test results
- Track test history

### 🪪 Local Driving Licenses
- Issue new driving licenses
- View license information
- Search for licenses
- Renew licenses
- Replace lost licenses
- Replace damaged licenses
- Track license history

### 🌍 International Driving Licenses
- Issue international driving licenses
- Validate eligibility
- Manage international license information
- Track issued international licenses

### 🚨 Detain & Release
- Detain driving licenses
- Record detention information
- Add and manage fines
- Release detained licenses
- Track detention and release history

### 📊 Search & Tracking
- Search people
- Search applications
- Search drivers
- Search licenses
- View license history
- Track different operations throughout the system

---

# 🏗️ Architecture

The application follows a **3-Tier Architecture** to separate responsibilities and keep the code organized and maintainable.

```text
┌──────────────────────────────┐
│     Presentation Layer       │
│       C# Windows Forms       │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│       Business Layer         │
│     Business Logic / Rules   │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│      Data Access Layer       │
│          ADO.NET             │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│        SQL Server            │
│          Database            │
└──────────────────────────────┘

🗄️ Database

The system uses SQL Server as the database management system and ADO.NET for database communication.

The database contains multiple related tables responsible for storing and managing:

- People
- Users
- Applications
- Application Types
- Licenses
- Drivers
- Tests
- Test Appointments
- International Licenses
- Detained Licenses
- Fines
- License History

The database was designed with relationships between entities to maintain data consistency and support the different workflows inside the system.

---

🔄 System Workflow

The project implements complete workflows for different driving license operations.

🪪 New Driving License

Person
   ↓
Create Application
   ↓
Schedule Tests
   ↓
Vision Test
   ↓
Written Test
   ↓
Street Test
   ↓
Application Approval
   ↓
Issue Driving License

🔁 License Renewal

Search License
      ↓
Check License Information
      ↓
Create Renewal Application
      ↓
Pay Fees
      ↓
Renew License
      ↓
Update License Information

🚨 License Detention & Release

Search License
      ↓
Detain License
      ↓
Record Fine
      ↓
License Becomes Detained
      ↓
Release License
      ↓
Update Release Information

---

🧩 Project Modules

The application is divided into several modules to make the system easier to manage and maintain.

DVLD
│
├── People Management
├── Users Management
├── Applications
├── Tests & Appointments
├── Local Licenses
├── International Licenses
├── Drivers
├── Detained Licenses
├── License History
└── Reports & Search

---

💻 Technologies Used

- C#
- .NET / Windows Forms
- SQL Server
- ADO.NET
- 3-Tier Architecture
- Object-Oriented Programming (OOP)
- SQL Queries
- Database Relationships
- Exception Handling
- Data Validation

---

🧠 Programming Concepts Applied

This project was an opportunity to apply several programming and software engineering concepts in a practical system.

Object-Oriented Programming

- Classes & Objects
- Encapsulation
- Inheritance
- Abstraction
- Polymorphism
- Constructors
- Properties
- Static Members

Database Programming

- CRUD Operations
- SQL Queries
- Stored Procedures
- Database Relationships
- Foreign Keys
- Data Retrieval
- Data Validation

Software Architecture

- 3-Tier Architecture
- Separation of Concerns
- Business Logic Layer
- Data Access Layer
- Presentation Layer
- Reusable Components

---

🔒 Security & Validation

The system includes different validation and permission mechanisms to control user access and maintain data integrity.

- User authentication
- User permissions
- Active / inactive accounts
- Input validation
- Application status validation
- License eligibility checks
- Prevention of invalid operations

---

🎯 Project Goals

The main goals of this project were:

- Build a large real-world desktop application.
- Practice working with relational databases.
- Apply 3-Tier Architecture in a practical project.
- Improve C# and Windows Forms skills.
- Work with ADO.NET and SQL Server.
- Understand how different modules communicate with each other.
- Implement business rules instead of relying only on basic CRUD operations.
- Improve problem-solving and software design skills.

---

📸 Screenshots

Login Screen

Add screenshot here

Main Dashboard

Add screenshot here

People Management

Add screenshot here

Applications

Add screenshot here

Driving Licenses

Add screenshot here

Tests & Appointments

Add screenshot here

License Detention

Add screenshot here

---

🚀 How To Run The Project

1. Clone the repository

git clone YOUR_REPOSITORY_URL

2. Open the project

Open the solution using Visual Studio.

3. Configure SQL Server

- Install SQL Server.
- Restore the provided database backup.
- Make sure the database is running correctly.

4. Configure the Connection String

Update the database connection string in the project according to your SQL Server configuration.

5. Build & Run

Build the solution and run the application from Visual Studio.

---

📚 What I Learned

Building this project helped me move from developing small applications to working with a much larger system containing multiple interconnected modules.

Throughout the project, I gained practical experience in:

- Designing and working with relational databases.
- Building multi-layer applications.
- Writing reusable and maintainable code.
- Implementing complex business logic.
- Working with SQL Server and ADO.NET.
- Managing relationships between different entities.
- Designing complete application workflows.
- Debugging and solving real development problems.

---

🙏 Acknowledgment

This project was developed as part of my learning journey with ProgrammingAdvices.

Special thanks to Eng. Abu-Hadhoud for the valuable educational content, guidance, and practical projects that helped me improve my programming and software development skills.

---

📌 Project Status

Completed — First Major .NET Project

This project represents an important milestone in my journey toward becoming a professional .NET Developer.

More projects and improvements are coming soon.

---
<img width="1920" height="1030" alt="Screenshot 2026-09-14 082007" src="https://github.com/user-attachments/assets/d7e289f6-b2f8-41b8-90d3-18fa81359af5" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081945" src="https://github.com/user-attachments/assets/bf4360f1-8c52-4183-95eb-dfba2c37987a" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081852" src="https://github.com/user-attachments/assets/9d9139e5-10d5-4c40-bea5-71ba45bc84dd" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081820" src="https://github.com/user-attachments/assets/ce3d307c-bf90-4e1c-a8a4-719f87705c5c" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081732" src="https://github.com/user-attachments/assets/0a10f461-b29f-4873-a044-fe287bc1bc56" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081652" src="https://github.com/user-attachments/assets/2254c2c5-8523-4638-973e-f0dab9786c41" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081617" src="https://github.com/user-attachments/assets/20befce9-43bc-4786-a934-e9c9c266837f" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081558" src="https://github.com/user-attachments/assets/9be3fa87-565e-4754-8648-95833b909132" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081534" src="https://github.com/user-attachments/assets/68f619ee-5362-4846-b522-51bfb62aa6b6" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081514" src="https://github.com/user-attachments/assets/6be88659-9848-42f9-baf0-cf35bda6e155" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081450" src="https://github.com/user-attachments/assets/067747cb-99b3-4d66-9c5d-a1ee62fce39f" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081418" src="https://github.com/user-attachments/assets/77aab8e7-961e-4217-b648-0ba41de19272" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081356" src="https://github.com/user-attachments/assets/9b164234-ed66-4b71-8d9f-26fda9672c47" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081313" src="https://github.com/user-attachments/assets/382e0b2a-45c0-45f3-b396-7460ab743875" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081252" src="https://github.com/user-attachments/assets/109a4ef8-7ae5-4d7f-b79b-7cf132fe703a" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081223" src="https://github.com/user-attachments/assets/e3d6ba65-04e8-4e1f-a754-88c6ce827df9" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081200" src="https://github.com/user-attachments/assets/41990225-32bf-4ad5-a8b5-62ad719fb885" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 081056" src="https://github.com/user-attachments/assets/15c0c7a1-6b0f-498a-a36d-01ae09ce51e7" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 080927" src="https://github.com/user-attachments/assets/361c2bb9-923d-49b3-a87d-c78d61a4cecd" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 080834" src="https://github.com/user-attachments/assets/7766bbec-fa7f-4d58-9532-7f6d7db311e6" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 080811" src="https://github.com/user-attachments/assets/7b1519d1-2abb-4889-8e36-d3759cb5cbb5" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 080624" src="https://github.com/user-attachments/assets/b82290a9-783d-45f7-8284-d4855a8f1105" />
<img width="1920" height="1030" alt="Screenshot 2026-09-14 082030" src="https://github.com/user-attachments/assets/ca222997-6e39-4df1-9e7f-1b7b171d0348" />


👨‍💻 Author

eslammasry1

C# / .NET Developer in Progress

Focused on building real-world applications and continuously improving my software development skills.
