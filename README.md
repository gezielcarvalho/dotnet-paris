# DotNetParis API

This project is a .NET 8.0 web API application designed to demonstrate principles like OCP (Open/Closed Principle) and LSP (Liskov Substitution Principle). It includes endpoints for managing products and weather forecasts.

## Running the Application

Since the application runs inside a Docker container, the URLs need to listen on all interfaces. This is achieved by setting the `urls` parameter in the `dotnet run` command. The application uses `0.0.0.0` instead of `localhost` to bind to all interfaces.

### Commands to Run the Application

Use the following commands to start the application:

```bash
dotnet run --urls="http://0.0.0.0:5151"
dotnet watch --urls="http://0.0.0.0:5151"
```

### Accessing the API

The API is exposed on port `5151` inside the container, which is mapped to port `5153` on the host machine. You can access the API's Swagger documentation at:

[http://localhost:5153/swagger/index.html](http://localhost:5153/swagger/index.html)

## Features

- **Product Management**: Endpoints to create, retrieve, update, and delete products, including filtering by `PublicProduct` and `PrivateProduct`.
- **Weather Forecast**: A simple endpoint to retrieve weather forecasts with randomized data.
- **Swagger/OpenAPI**: Integrated Swagger UI for API exploration and testing.

## Development Environment

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Visual Studio Code](https://code.visualstudio.com/) (optional, for development)

### Running in Development Mode

The application is configured to use the `Development` environment by default. You can modify the environment settings in the `Properties/launchSettings.json` file.

### Swagger Configuration

Swagger is enabled in the development environment. To explore the API, navigate to the Swagger UI at the URL mentioned above.

## Project Structure

- **Controllers**: Contains API controllers like `ProductController` and `WeatherController`.
- **Models**: Defines data models such as `Product`, `PublicProduct`, `PrivateProduct`, and `Weather`.
- **Services**: Business logic layer, including `ProductService`.
- **Repositories**: Data access layer, including `ProductRepository`.
- **Data**: Contains the in-memory `ApplicationDbContext`.

## Key Principles Demonstrated

### Open/Closed Principle (OCP)

The application demonstrates OCP by allowing new functionality (e.g., filtering products by type) to be added without modifying existing code. For more details, see [docs/01_ocp_lsp_demos.md](docs/01_ocp_lsp_demos.md).

### Liskov Substitution Principle (LSP)

The application adheres to LSP by ensuring that subclasses (`PublicProduct` and `PrivateProduct`) can replace their base class (`Product`) without affecting the correctness of the program.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.