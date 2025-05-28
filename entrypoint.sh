#!/bin/bash

# Inicia o SQL Server em background
/opt/mssql/bin/sqlservr &

# Aguarda o SQL Server ficar disponível
echo "Aguardando SQL Server iniciar..."
sleep 20

# Executa o script SQL
echo "Executando script caixas.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Embalagens123! -i /scripts/caixas.sql

# Aguarda indefinidamente para manter o container vivo
wait