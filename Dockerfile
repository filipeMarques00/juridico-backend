# Estágio 1: Build - Usa a imagem completa do .NET SDK para compilar o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj e restaura as dependências primeiro (para otimizar o cache)
COPY ["GerenciarProcessos.API/GerenciarProcessos.API.csproj", "GerenciarProcessos.API/"]
COPY ["GerenciarProcessos.Application/GerenciarProcessos.Application.csproj", "GerenciarProcessos.Application/"]
COPY ["GerenciarProcessos.Domain/GerenciarProcessos.Domain.csproj", "GerenciarProcessos.Domain/"]
COPY ["GerenciarProcessos.InfraIoc/GerenciarProcessos.InfraIoc.csproj", "GerenciarProcessos.InfraIoc/"]
COPY ["GerenciarProcessos.Infrastructure/GerenciarProcessos.Infrastructure.csproj", "GerenciarProcessos.Infrastructure/"]
COPY ["GerenciarProcessos.Ioc/GerenciarProcessos.Ioc.csproj", "GerenciarProcessos.Ioc/"]

RUN dotnet restore "GerenciarProcessos.API/GerenciarProcessos.API.csproj"

# Copia todo o resto do código e publica a aplicação
COPY . .
WORKDIR "/src/GerenciarProcessos.API"
RUN dotnet publish "GerenciarProcessos.API.csproj" -c Release -o /app/publish --no-restore

# Estágio 2: Runtime - Usa a imagem leve do ASP.NET Runtime para executar a aplicação.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# ✅ AJUSTE CRÍTICO PARA O RAILWAY E OUTRAS PLATAFORMAS DE NUVEM
# A plataforma define a variável $PORT. Esta linha instrui o Kestrel a escutar nela.
# A porta é exposta automaticamente pela plataforma; 'EXPOSE' não é necessário.
ENV ASPNETCORE_URLS="http://*:${PORT}"

# Comando para iniciar a API quando o contêiner for executado
ENTRYPOINT ["dotnet", "GerenciarProcessos.API.dll"]