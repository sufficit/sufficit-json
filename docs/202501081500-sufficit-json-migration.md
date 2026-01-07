# 202501081500 - Sufficit.Json Migration

## Overview
This document describes the migration of JSON utilities from `sufficit-base` to a dedicated `Sufficit.Json` library.

## Migration Details

### Source
- **Original Location**: `sufficit-base/json/`
- **Files Migrated**:
  - `Sufficit.Json.csproj`
  - `JsonExtensions.cs`
  - `JsonSerializer.cs`
  - `JsonSerializer+System.cs`
  - `ExceptionConverter.cs`
  - `GuidConverter.cs`
  - `JsonStringEnumConverter.cs`
  - `JsonStringTypeConverter.cs`
  - `NullableGuidConverter.cs`
  - `RFC2822DateTimeConverter.cs`

### Target Structure
```
sufficit-json/
├── src/
│   ├── Sufficit.Json.csproj
│   ├── JsonExtensions.cs
│   ├── JsonSerializer.cs
│   └── ... (all converter files)
├── docs/
│   └── 202501081500-sufficit-json-migration.md
├── .github/
│   └── copilot-instructions.md
├── README.md
├── license
└── .gitignore
```

### Framework Support
- **Target Frameworks**: `netstandard2.0;net6.0;net7.0;net9.0`
- **Compatibility**: Maintains compatibility with all existing Sufficit projects

### Next Steps
1. **Package Creation**: Create NuGet package for `Sufficit.Json`
2. **Dependency Update**: Update `sufficit-base` to reference the NuGet package instead of ProjectReference
3. **Testing**: Validate all dependent projects still compile and function correctly
4. **Documentation**: Update all relevant documentation to reference the new package

## Benefits
- **Separation of Concerns**: JSON utilities are now in a dedicated library
- **Reusability**: Other projects can use JSON utilities without depending on `Sufficit.Base`
- **Maintainability**: Easier to maintain and update JSON-related code
- **Versioning**: Independent versioning for JSON utilities