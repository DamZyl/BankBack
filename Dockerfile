FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /work
COPY ./ .

FROM build AS publish
WORKDIR /work/Bank
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Bank.dll"]