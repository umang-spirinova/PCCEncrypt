#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019

# Set the source argument and the working directory
ARG source
WORKDIR /inetpub/wwwroot

# Copy the application files to the working directory
COPY ${source:-obj/Docker/publish} .

# Expose the default port for IIS
EXPOSE 44312

# Ensure IIS starts automatically
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
