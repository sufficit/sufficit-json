# Sufficit.Json
<a href="https://github.com/sufficit"><img src="https://avatars.githubusercontent.com/u/66928451?s=200&v=4" alt="Sufficit Logo" width="80" align="right">
</a>

[![NuGet](https://img.shields.io/nuget/v/Sufficit.Json.svg)](https://www.nuget.org/packages/Sufficit.Json/)

## 📖 About the Project

`Sufficit.Json` is a specialized library for JSON serialization and deserialization utilities within the Sufficit software ecosystem. It provides custom JSON converters, extension methods, and serialization helpers that ensure consistent JSON handling across all Sufficit applications and services.

The goal of this library is to centralize JSON-related functionality, promote code reuse, and maintain consistent serialization standards throughout the ecosystem.

### ✨ Key Features

* Custom JSON converters for complex types (Guid, DateTime, Enums, etc.)
* Extension methods for JSON serialization/deserialization
* Consistent JSON handling across all Sufficit projects
* Support for multiple .NET target frameworks (netstandard2.0, net6.0, net7.0, net9.0)

## 🚀 Getting Started

This project is a class library. The recommended way to use it is by installing the NuGet package into your project.

### 📦 NuGet Package

You can install the package via the .NET CLI or the NuGet Package Manager Console.

**.NET CLI:**

    dotnet add package Sufficit.Json

**Package Manager Console:**

    Install-Package Sufficit.Json

## 🛠️ Usage

`Sufficit.Json` provides utilities for JSON operations throughout the Sufficit ecosystem.

**Example of custom serialization:**

    using Sufficit.Json;

    var options = JsonSerializer.CreateDefaultOptions();
    // Options now include all custom converters

**Example of extension methods:**

    using Sufficit.Json.Extensions;

    var json = myObject.ToJson();
    var obj = json.FromJson<MyType>();

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project.
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`).
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the Branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.

## 📧 Contact

Sufficit - [contato@sufficit.com.br](mailto:contato@sufficit.com.br)

Project Link: [https://github.com/sufficit/sufficit-json](https://github.com/sufficit/sufficit-json)