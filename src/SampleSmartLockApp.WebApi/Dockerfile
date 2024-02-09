FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/SampleSmartLockApp.WebApi/SampleSmartLockApp.WebApi.csproj", "SampleSmartLockApp.WebApi/"]
COPY ["src/SampleSmartLockApp.WebApi/SampleSmartLockApp.Domain.csproj", "SampleSmartLockApp.Domain/"]
COPY ["src/SampleSmartLockApp.WebApi/SampleSmartLockApp.Infrastructure.csproj", "SampleSmartLockApp.Infrastructure/"]
COPY ["src/SampleSmartLockApp.WebApi/SampleSmartLockApp.Application.csproj", "SampleSmartLockApp.Application/"]
RUN dotnet restore "SampleSmartLockApp.WebApi/SampleSmartLockApp.WebApi.csproj"
COPY . .
WORKDIR "/src/SampleSmartLockApp.WebApi"
RUN dotnet build "SampleSmartLockApp.WebApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "SampleSmartLockApp.WebApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleSmartLockApp.WebApi.dll"]
