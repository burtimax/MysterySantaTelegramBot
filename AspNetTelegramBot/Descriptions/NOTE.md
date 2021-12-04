# Notes

## Migrations

### BotContext Migrations
~~~
ADD MIGRATIONS
1) cd AspNetTelegramBot
2) dotnet ef migrations add Initial --context BotContext -p ../AspNetTelegramBot/SantaBot.csproj -s SantaBot.csproj -o Src/Bot/Db/MigrationsData
3) dotnet ef database update --context BotContext

REMOVE MIGRATIONS
1) cd AspNetTelegramBot
2) dotnet ef migrations remove --context BotContext -p ../AspNetTelegramBot/SantaBot.csproj -s SantaBot.csproj
~~~

### SantaContext Migrations
~~~
ADD MIGRATIONS
1) cd AspNetTelegramBot
2) dotnet ef migrations add Add_ChosenByOtherCount_Field --context SantaContext -p ../AspNetTelegramBot/SantaBot.csproj -s SantaBot.csproj -o Src/SantaBot/Db/MigrationsData
3) dotnet ef database update --context SantaContext

REMOVE MIGRATIONS
1) cd AspNetTelegramBot
2) dotnet ef migrations remove --context SantaContext -p ../AspNetTelegramBot/SantaBot.csproj -s SantaBot.csproj
~~~