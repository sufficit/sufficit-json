# Intervalo de DI para consumidores

Ampliado apenas o cap net7 da dependência DI Abstractions para <9, mantendo compilação/piso 7.x. Elimina NU1608 com DI 8 no consumidor que utiliza MySqlConnector 2.6.1. Serialização e contratos não foram modificados.

Build isolado dos três frameworks aprovado. [Plano](../PLAN-CONNECTOR-DI-COMPATIBILITY.md). Publicação segue o workflow existente; pacote local com proveniência atende o consumidor enquanto a fila publica/indexa.
