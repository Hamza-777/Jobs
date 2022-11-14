# Jobs

A jobs portal....

## AppSettings:

Add json object given below to your appsettings if haven't already...

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "<Your_Connection_String>"
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "<Your_Secret_Key>",
    "Issuer": "https://localhost:7067"
  },
  "EmailConfiguration": {
    "From": "<From_Email>",
    "SmtpServer": "smtp.gmail.com",
    "Port": "587",
    "Username": "<Username>",
    "Password": "<Password>"
  }
}
```
