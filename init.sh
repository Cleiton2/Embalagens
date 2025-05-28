/opt/mssql/bin/sqlservr &
echo "Esperando o SQL Server iniciar..."
sleep 20
echo "Executando script de inicialização..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Embalagens123! -i /scripts/caixas.sql
wait