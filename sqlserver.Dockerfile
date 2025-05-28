FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

# Instalar dependências
RUN apt-get update && apt-get install -y curl apt-transport-https gnupg2 software-properties-common

# Adicionar chave GPG corretamente
RUN curl -sSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.gpg

# Adicionar o repositório do mssql-tools
RUN echo "deb [arch=amd64] https://packages.microsoft.com/debian/12/prod bookworm main" > /etc/apt/sources.list.d/mssql-release.list

# Instalar os pacotes
RUN apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    ln -s /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && \
    ln -s /opt/mssql-tools/bin/bcp /usr/bin/bcp && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

USER mssql
