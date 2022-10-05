# The .NET SDK used for building the microservice
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# Restore Nuget packages
COPY ["ShoppingCart/ShoppingCart.csproj", "ShoppingCart/"]
RUN dotnet restore "ShoppingCart/ShoppingCart.csproj"
COPY . .
# Build microservice in release mode
WORKDIR "/src/ShoppingCart"
RUN dotnet build "ShoppingCart.csproj" -c Release -o /app/build

# Publish microservice to publish folder
FROM build AS publish
RUN dotnet publish "ShoppingCart.csproj" -c Release -o /app/publish

# Image final container is based on
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
# Container should accept requests on port 80
EXPOSE 80
# Copy files from app/publish to final container
COPY --from=publish /app/publish .
#Specify that when the final final container runs it will start up dotnet ShoppingCart.dll
ENTRYPOINT ["dotnet", "ShoppingCart.dll"]