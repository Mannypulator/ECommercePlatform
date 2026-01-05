# E-Commerce Platform

A modern, cloud-native e-commerce platform built with .NET microservices architecture, following Domain-Driven Design (DDD) and CQRS patterns.

## 🏗️ Architecture

This project implements a microservices architecture with the following key characteristics:

- **Microservices**: Independent, loosely-coupled services for different business domains
- **CQRS Pattern**: Command Query Responsibility Segregation for scalable read/write operations
- **API Gateway**: YARP-based gateway for unified API access
- **Service Communication**: Message-based async communication using BuildingBlocks.Messaging
- **.NET Aspire**: Modern orchestration and service discovery

## 🚀 Services

### Core Services

- **Catalog.API**: Product catalog management, inventory, and product information
- **Basket.API**: Shopping cart functionality and basket management
- **Ordering.API**: Order processing and order history
- **Payment.API**: Payment processing and transaction management
- **Identity.API**: User authentication and authorization
- **Yarp.Gateway**: API Gateway for routing and request aggregation

### Infrastructure

- **ECommerce.AppHost**: .NET Aspire orchestration host
- **ECommerce.ServiceDefaults**: Shared service configurations and extensions
- **BuildingBlocks**: Reusable components and cross-cutting concerns
- **BuildingBlocks.Messaging**: Message bus abstractions and implementations

## 🛠️ Technologies

- **.NET 10.0**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **MediatR**: In-process messaging for CQRS
- **.NET Aspire**: Cloud-native orchestration
- **YARP**: Reverse proxy for API Gateway
- **FluentValidation**: Request validation (via ValidationBehavior)

## 📁 Project Structure

```
ECommercePlatform/
├── src/
│   ├── Services/
│   │   ├── Basket/          # Shopping basket service
│   │   ├── Catalog/         # Product catalog service
│   │   ├── Identity/        # Authentication service
│   │   ├── Ordering/        # Order management service
│   │   ├── Payment/         # Payment processing service
│   │   └── Gateway/         # API Gateway (YARP)
│   ├── BuildingBlocks/
│   │   ├── BuildingBlocks/         # Shared utilities and patterns
│   │   └── BuildingBlocks.Messaging/  # Message bus abstractions
│   ├── ECommerce.AppHost/          # Aspire orchestration
│   └── ECommerce.ServiceDefaults/  # Shared service configs
├── ECommercePlatform.sln
└── README.md
```

## 🔧 Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerization)
- [Visual Studio 2025](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET Aspire Workload](https://learn.microsoft.com/dotnet/aspire/fundamentals/setup-tooling)

## 🚦 Getting Started

### Installation

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd ECommercePlatform
   ```

2. **Install .NET Aspire workload** (if not already installed)

   ```bash
   dotnet workload install aspire
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

### Running the Application

#### Using .NET Aspire (Recommended)

```bash
cd src/ECommerce.AppHost
dotnet run
```

This will start the Aspire dashboard and all microservices. Access the dashboard at `https://localhost:17999` (or the URL shown in the console).

#### Running Individual Services

```bash
# Example: Running Catalog Service
cd src/Services/Catalog/Catalog.API
dotnet run
```

### Building the Solution

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## 📦 Building Blocks

The platform includes reusable building blocks that provide cross-cutting concerns:

### BuildingBlocks

- **CQRS**: Command and Query interfaces and handlers
- **Behaviors**:
  - `LoggingBehavior`: Request/response logging
  - `ValidationBehavior`: FluentValidation integration
- **Exceptions**: Custom exception types and global exception handler
- **Response**: Generic API response wrappers
- **Slices**: Vertical slice architecture support with endpoint registration

### BuildingBlocks.Messaging

- Message bus abstractions for inter-service communication
- Event-driven architecture support

## 🔐 Configuration

Service-specific configurations are managed through:

- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development environment overrides
- Environment variables: Production secrets and sensitive data

## 🏛️ Design Patterns

- **CQRS**: Separation of read and write operations
- **Mediator Pattern**: Using MediatR for in-process messaging
- **Repository Pattern**: Data access abstraction
- **Vertical Slice Architecture**: Feature-based organization
- **Domain-Driven Design**: Business logic encapsulation

## 📝 API Documentation

Once the services are running, API documentation is available via Swagger UI:

- Catalog API: `https://localhost:<port>/swagger`
- Basket API: `https://localhost:<port>/swagger`
- Ordering API: `https://localhost:<port>/swagger`
- Payment API: `https://localhost:<port>/swagger`
- Identity API: `https://localhost:<port>/swagger`
- API Gateway: `https://localhost:<gateway-port>/swagger`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📧 Contact

Project Maintainer - [@yourhandle](https://github.com/yourhandle)

Project Link: [https://github.com/yourusername/ECommercePlatform](https://github.com/yourusername/ECommercePlatform)

## 🙏 Acknowledgments

- Built with [.NET Aspire](https://learn.microsoft.com/dotnet/aspire/)
- API Gateway powered by [YARP](https://microsoft.github.io/reverse-proxy/)
- CQRS implementation using [MediatR](https://github.com/jbogard/MediatR)
