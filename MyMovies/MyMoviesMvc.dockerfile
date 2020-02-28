FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["./src/MyMoviesMvc/MyMoviesMvc.csproj", "./"]
RUN dotnet restore "./MyMoviesMvc.csproj"
COPY ./src/MyMoviesMvc .
RUN dotnet build "MyMoviesMvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyMoviesMvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "MyMoviesMvc.dll"]