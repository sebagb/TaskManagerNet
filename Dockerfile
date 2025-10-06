FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY TaskManager.Api/*.csproj ./TaskManager.Api/
COPY TaskManager.Application/*.csproj ./TaskManager.Application/
COPY TaskManager.Contract/*.csproj ./TaskManager.Contract/

RUN dotnet restore "TaskManager.Api/TaskManager.Api.csproj"
RUN dotnet restore "TaskManager.Application/TaskManager.Application.csproj"
RUN dotnet restore "TaskManager.Contract/TaskManager.Contract.csproj"

COPY . .

WORKDIR /source/TaskManager.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV IS_DOCKER_CONTAINER=true
ENTRYPOINT ["dotnet", "TaskManager.Api.dll"]