FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Meetme.ProfileService
COPY ["Meetme.ProfileService/Meetme.ProfileService.API/Meetme.ProfileService.API.csproj", "Meetme.ProfileService.API/"]
COPY ["Meetme.ProfileService/Meetme.ProfileService.BLL/Meetme.ProfileService.BLL.csproj", "Meetme.ProfileService.BLL/"]
COPY ["Meetme.ProfileService/Meetme.ProfileService.DAL/Meetme.ProfileService.DAL.csproj", "Meetme.ProfileService.DAL/"]
RUN dotnet restore "Meetme.ProfileService.API/Meetme.ProfileService.API.csproj"
COPY . /
WORKDIR /Meetme.ProfileService/Meetme.ProfileService.API
RUN dotnet build "Meetme.ProfileService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5133
EXPOSE 5133
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meetme.ProfileService.API.dll"]
