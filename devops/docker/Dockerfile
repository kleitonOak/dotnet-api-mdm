FROM mcr.microsoft.com/dotnet/sdk:8.0.100 as build-env

WORKDIR /app
# Copy csproj e restore dependences 
COPY ./mdm-person/ ./

# Build app
RUN dotnet publish -c Release -o out

# Build Image 
FROM mcr.microsoft.com/dotnet/sdk:8.0.100
WORKDIR /app
COPY --from=build-env /app/out . 

RUN rm appsettings.Development.json 

ENTRYPOINT [ "dotnet", "mdm-person.dll"]




