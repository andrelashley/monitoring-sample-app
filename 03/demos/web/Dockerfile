FROM mcr.microsoft.com/dotnet/core/sdk:3.1.401-alpine3.12 AS builder

WORKDIR /src
COPY src/WiredBrain.Web/WiredBrain.Web.csproj .
RUN dotnet restore

COPY src/WiredBrain.Web/ .
RUN dotnet publish -c Release -o /out WiredBrain.Web.csproj

# app image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.7-alpine3.12

ENV ProductsApi:Url="http://products-api/products" \
    StockApi:Url="http://stock-api:8080/stock" 

ENTRYPOINT ["dotnet", "/app/WiredBrain.Web.dll"]

WORKDIR /app
COPY --from=builder /out/ .