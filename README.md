# 🛡️ CyberQuiz

A web application for testing and improving your knowledge in cybersecurity. Built with Blazor Server and a REST API using clean architecture.

---

## 👥 Team

- Amin Nazari
- Linnea Schildt
- Henrik Sihvonen

---

The project is divided into 5 layers:

| Project | Responsibility |
|---|---|
| `CyberQuiz` | UI — Blazor Server, what the user sees |
| `CyberQuiz.API` | REST API — receives requests from UI and forwards them |
| `CyberQuiz.BLL` | Business logic — rules and calculations |
| `CyberQuiz.DAL` | Database — EF Core with SQL Server |
| `CyberQuiz.Shared` | Shared classes — DTOs used across multiple layers |

---

## ✨ Features

- Register and log in with JWT authentication
- Categories and subcategories within cybersecurity
- Subcategories unlock when the previous one is completed with at least 80%
- Quiz with multiple choice questions
- AI Coach — get personalized study recommendations and chat with an AI about your results
- User profile with statistics

---

## 🤖 AI Coach

The AI Coach uses a local Ollama model (phi3/llama3.2) to:
- Analyze the user's quiz results
- Provide personalized study recommendations based on weak areas
- Answer questions via chat about cybersecurity

---

## 🔐 Technologies

- **Blazor Server** — UI framework
- **ASP.NET Core Web API** — REST API
- **Entity Framework Core** — ORM for database access
- **ASP.NET Identity** — user management
- **JWT** — authentication between UI and API
- **Ollama** — local AI model for coaching
