#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Presentation/EvaluationSystem.API/EvaluationSystem.API.csproj", "src/Presentation/EvaluationSystem.API/"]
COPY ["src/Core/EvaluationSystem.Application/EvaluationSystem.Application.csproj", "src/Core/EvaluationSystem.Application/"]
COPY ["src/Core/EvaluationSystem.Domain/EvaluationSystem.Domain.csproj", "src/Core/EvaluationSystem.Domain/"]
COPY ["src/Infrastructure/EvaluationSystem.Persistence/EvaluationSystem.Persistence.csproj", "src/Infrastructure/EvaluationSystem.Persistence/"]
RUN dotnet restore "src/Presentation/EvaluationSystem.API/EvaluationSystem.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/EvaluationSystem.API"
RUN dotnet build "EvaluationSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EvaluationSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvaluationSystem.API.dll"]