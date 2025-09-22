FROM mcr.microsoft.com/vscode/devcontainers/dotnet:8.0

# Install Node.js 18 (LTS)
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y nodejs

# Install Postgres client (to test remote DB)
RUN apt-get update && apt-get install -y postgresql-client

# Install Azure CLI
RUN curl -sL https://aka.ms/InstallAzureCLIDeb | bash

USER vscode
