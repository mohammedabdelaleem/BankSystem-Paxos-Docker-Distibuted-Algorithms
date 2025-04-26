# Paxos Bank System

## 📘 Introduction

**Paxos Bank System** is a distributed banking application built with **ASP.NET Core Web API** and **Docker**. It uses the **Paxos consensus algorithm** to maintain reliable and consistent state across multiple nodes, ensuring transaction integrity in a distributed environment.

---

## 🏛️ System Architecture

The system is composed of the following components:

- **SQL Server** container for centralized data storage.
- **Three ASP.NET Core Web API nodes** (`Node1`, `Node2`, `Node3`) acting as distributed replicas.
- **Docker Compose** to orchestrate the multi-container deployment.

---

## 🔧 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server 2022 (Docker image)
- Docker & Docker Compose
- Paxos Algorithm (for distributed consensus)

---

## 🚀 Features

- 🧠 **Paxos-based distributed consensus** mechanism
- 🔐 **User authentication** with role-based access control
- 💸 **Bank account CRUD operations**
- 📜 **Transaction history tracking**
- 🐳 **Fully Dockerized deployment**

---

## 🛠️ How to Run the Project

1. **Install Docker** and **Docker Compose** if not already installed.
2. Navigate to the project root directory in your terminal.
3. Run the following command to build and start the containers:
   ```bash
   docker-compose up -d --build
