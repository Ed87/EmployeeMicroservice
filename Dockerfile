FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["EmployeeAPI/EmployeeAPI.csproj", "EmployeeAPI/"]
RUN dotnet restore "EmployeeAPI/EmployeeAPI.csproj"
COPY . .
WORKDIR "/src/EmployeeAPI"
RUN dotnet build "EmployeeAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EmployeeAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmployeeAPI.dll"]