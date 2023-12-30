# Blazor Implement ICurrentUserService CurrentUserService
reference https://learn.microsoft.com/en-us/aspnet/core/blazor/security/server/additional-scenarios?view=aspnetcore-8.0

![image](https://github.com/ganiputras/BlazorCurrentUser/assets/8809768/049fe481-236b-4727-af1d-0b7cc59964c1)

## 1. Update connectionString
    "ConnectionStrings": {
        "DefaultConnection": "Server=.;Database=BlazorWebAppSample;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
    },
    
## 2. Update database
PM> Update-Database

## 3. Add Class
![image](https://github.com/ganiputras/BlazorCurrentUser/assets/8809768/7a5292a9-9b69-4070-bf17-735ace3aa550)

## 4. Add service
![image](https://github.com/ganiputras/BlazorCurrentUser/assets/8809768/2e8cacaa-6351-4cd8-8ef9-6acabf641b32)

## 5. Login
login: admin@cm.com
password: P@ssw0rd

## NOTE You can also apply at SaveChangesInterceptor
![image](https://github.com/ganiputras/BlazorCurrentUser/assets/8809768/e9d6b393-6acb-4645-9c94-d59f8c6a47e5)



