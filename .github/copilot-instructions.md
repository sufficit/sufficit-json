# Copilot Instructions - Sufficit.Json
<!-- Version: 202501081500 -->

## Project Overview
* **Project**: Sufficit.Json - JSON utilities library for Sufficit platform
* **Technology**: .NET Class Library (.NET Standard 2.0, .NET 6.0, 7.0, 9.0)
* **Purpose**: Centralized JSON serialization utilities with custom converters

## Common Guidelines
* code comments should always be in English
* response to user queries should be in IDE current language
* avoid changing code that was not related to the query
* when agent has to change a method and it changes the async status, the agent should update the method callers too
* for extension methods use always "source" as default parameter name
* use one file for each class
* for #region tags: no blank lines between consecutive regions, but always add one blank line after region opening and one blank line before region closing
* do not try to build if you just changed the code comments or documentation files
* **when making relevant code changes, always create or update internal documentation following the Internal Documentation Guidelines**

## API Development Guidelines
* **Converters**: All custom converters should inherit from JsonConverter<T>
* **Extensions**: Create extension methods in JsonExtensions.cs
* **Serialization**: Use consistent serialization patterns across all utilities
* **Multi-targeting**: Support netstandard2.0, net6.0, net7.0, net9.0 frameworks

## Internal Documentation Guidelines
* **Directory**: /docs, use this directory to store internal documentation files
* **File Name**: use the following format: YYYYMMDDHHMM-description.md, like 202501081500-new-converter.md
* **Content**: include detailed information about JSON utilities, converter implementations, and serialization patterns
* **Trigger Events**: create documentation for:
  - New JSON converters or utilities
  - Significant serialization changes
  - Breaking changes in JSON handling
  - New features or converter integrations
  - Complex bug fixes in serialization
  - Performance optimizations in JSON processing

## Build & Publish
* **Project File**: src/Sufficit.Json.csproj
* **Target Frameworks**: netstandard2.0;net6.0;net7.0;net9.0
* **Publish Folder**: publish/
* **Configuration**: Release
* **Command**: dotnet publish src/Sufficit.Json.csproj -c Release -o publish/

## Testing
* **Test Requests**: Use unit tests for converter validation
* **Test Documentation**: See /docs/ for converter testing examples
