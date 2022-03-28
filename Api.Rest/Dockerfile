#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Api.Rest/Api.Rest.csproj", "Api.Rest/"]
COPY ["Api.Business/Api.Business.csproj", "Api.Business/"]
COPY ["Api.Models/Api.Models.csproj", "Api.Models/"]
COPY ["Api.DataAccess/Api.DataAccess.csproj", "Api.DataAccess/"]
RUN dotnet restore "Api.Rest/Api.Rest.csproj"
COPY . .
WORKDIR "/src/Api.Rest"
RUN dotnet build "Api.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Rest.dll"]