#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Books.Books/Books.Books.csproj", "Books.Books/"]
RUN dotnet restore "Books.Books/Books.Books.csproj"
COPY . .
WORKDIR "/src/Books.Books"
RUN dotnet build "Books.Books.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Books.Books.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books.Books.dll"]