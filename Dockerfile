# Usar a imagem do SDK .NET 6.0 para o build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar os arquivos do projeto e restaurar dependências
COPY *.sln .
COPY ReceitasLiterarias.csproj ./
RUN dotnet restore

# Copiar o restante do código e fazer o build
COPY . ./
RUN dotnet publish -c Release -o /out

# Usar a imagem do runtime .NET 6.0 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Configurar a porta para o container
EXPOSE 5000

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "ReceitasLiterarias.dll"]
