Here is the foundational documentation for the project. You can save this as a `README.md` file in the root of your solution. It covers the architecture, the core workflows, and how a developer can get the system running locally.

# Hannah's Pampered Pets App

**Version:** 1.0.0-draft

**Platform:** ASP.NET Core 10 (Minimal APIs)

**Database:** Google Cloud Firestore

**Architecture:** Clean Architecture (Onion)

---

## 1. System Overview

Hannah's Pampered Pets is a modular, cloud-ready web application designed to manage dog-sitting services. It handles customer profiles, pet profiles, and drop-in booking scheduling.

The application is built using a **Clean Architecture** approach. This ensures that the core business logic (Domain and Application layers) is completely decoupled from the UI framework, database technology, and external services (like notifications or Calendly integrations).

---

## 2. Project Structure

The solution `HannahsPamperedPetsApp.sln` consists of five main projects:

| Project Name | Layer | Responsibility |
| --- | --- | --- |
| `*.Domain` | Core | The heart of the app. Contains enterprise-wide logic and entity models (`Customer`, `Pet`, `Booking`). **No external dependencies.** |
| `*.Application` | Use Cases | Business rules and application logic. Contains Interfaces (e.g., `IBookingRepository`, `INotificationService`) and Services (e.g., `BookingService`). Depends only on Domain. |
| `*.Infrastructure` | External | Implementations of interfaces defined in Application. This handles Google Cloud Firestore data access and external API integrations (email/texts). |
| `*.WebUI` | Presentation | The ASP.NET Core web server. Houses the Minimal API endpoints, security middleware, and the frontend CSS/theming engine. |
| `*.Tests` | Testing | xUnit test suite for validating Domain logic and Application services without hitting the database. |

---

## 3. Core Entities (Domain)

The application revolves around three primary data structures:

* **Customer:** The human client. Contains contact information and authentication links.
* **Pet:** The dog(s) belonging to the customer. Contains breed info, care instructions, and links to the `Customer.Id`.
* **Booking:** A scheduled service (e.g., a Drop-In). Contains the date, status (`Pending`, `Confirmed`, `Completed`), associated `CustomerId`, and specific notes.

---

## 4. Security & Configuration

### Secrets Management

The application strictly adheres to a zero-trust model for configuration. No sensitive data is checked into source control.

* **Local Development:** Uses ASP.NET Core User Secrets (`dotnet user-secrets`).
* **Production:** Uses Google Cloud Secret Manager and Environment Variables.

### API Security

The `WebUI` project is configured with standard cybersecurity middleware:

* Enforced HTTPS redirection and HSTS in production.
* Anti-Forgery (CSRF) protection on all state-changing endpoints.
* Strict Security Headers (X-Frame-Options, X-Content-Type-Options, CSP) to prevent XSS and Clickjacking.

### Database Security

The Firestore database is secured via IAM rules. Client-side access is completely disabled. All read/write operations must pass through the authenticated .NET backend using the Google Admin SDK.

---

## 5. Developer Setup Instructions

### Prerequisites

1. [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
2. Google Cloud CLI (optional, but recommended for local Firestore auth)

### Quick Start

1. **Initialize the Solution:**
Run the setup script from the root directory to generate the architecture and install dependencies.
```bash
./setup-app.sh

```


2. **Configure Local Secrets:**
Set your Google Cloud Project ID locally.
```bash
cd HannahsPamperedPetsApp.WebUI
dotnet user-secrets init
dotnet user-secrets set "Firestore:ProjectId" "your-gcp-project-id"

```


3. **Run the Server:**
```bash
dotnet run --project HannahsPamperedPetsApp.WebUI

```


The API will be available at `http://localhost:5000`.
4. **Run the Test Suite:**
From the root directory:
```bash
dotnet test

```



---

## 6. Future Roadmap

1. **Notification Engine:** Implement `INotificationService` in the Infrastructure layer to send SMS/Email confirmations using Twilio or SendGrid.
2. **Calendly Integration:** Create a webhook endpoint in WebUI to listen for Calendly scheduling events and sync them to Firestore.
3. **Dynamic Theming:** Implement a CSS variable injection system in WebUI to allow the client to update color schemes via an admin dashboard.
4. **Containerization:** Finalize the `Dockerfile` for deployment to Google Cloud Run.