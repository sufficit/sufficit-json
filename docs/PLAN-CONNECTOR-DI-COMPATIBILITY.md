# Intervalo DI para o conector corrigido

Objetivo: permitir que EFData/Standard net7 utilizem DI Abstractions 8 exigido pelo conector corrigido.

1. [concluído] Ampliar apenas o intervalo net7 para [7.*,9), mantendo build/piso 7.x.
2. [concluído] Validar build/pack dos três frameworks e metadados do pacote.
3. [concluído] Registrar atividade e commit/push na main.

Sem mudança em serialização, contratos CLR ou demais targets. O cap <8 do pacote antigo é o último NU1608 da cadeia isolada.

## Fechamento da entrega

Fonte e5b398d entregue na main; CI 43082471984 success. Build/pack local dos três targets e integração Standard 31ce9f6 aprovados. Nenhum deploy.
