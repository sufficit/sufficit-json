# Sistema de Auto-Registro de Conversores JSON

## Visão Geral

O sistema de auto-registro permite que conversores JSON sejam automaticamente registrados quando o método `AddSufficitConverters()` é chamado, eliminando a necessidade de registro manual.

## Como Funciona

1. **Atributo `AutoRegisterConverterAttribute`**: Marque qualquer classe de conversor que herda de `JsonConverter` com este atributo.

2. **Método `AddAutoRegisteredConverters()`**: Localizado em `Sufficit.Json.JsonSerializer`, este método usa reflexão para descobrir e instanciar conversores marcados.

3. **Integração com `AddSufficitConverters()`**: O método de extensão em `Sufficit.JsonExtensions` chama automaticamente o sistema de auto-registro.

## Exemplo de Uso

### 1. Marcar um Conversor

```csharp
using Sufficit.Json;

[AutoRegisterConverter]
public class MeuConversorCustomizado : JsonConverter<MeuTipo>
{
    // Implementação do conversor
}
```

### 2. Uso Automático

```csharp
var options = new JsonSerializerOptions();
options.AddSufficitConverters(); // O conversor marcado será automaticamente adicionado
```

## Conversores Registrados Automaticamente

- `JsonStringEnumConverter` (Sufficit.Utils) - Serializa enums usando nomes customizados

## Conversores Registrados Manualmente (Legacy)

- `TextFilterConverter`
- `TagFilterConverter` 
- `NormalizedStringConverter`

## Benefícios

- **Manutenibilidade**: Novos conversores são automaticamente descobertos
- **Separação de Responsabilidades**: Conversores ficam em seus projetos apropriados
- **Flexibilidade**: Sistema extensível para futuros conversores
- **Compatibilidade**: Mantém conversores legados registrados manualmente