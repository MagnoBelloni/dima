# Entity Framework CLI

- To run the necessary migrations you'll need to install the latest EF Core CLI, to do that run the following command:
- ``dotnet tool install --global dotnet-ef``

- TO add migrations you'll need to run:
- ``dotnet ef migrations add migrationNameGoesHere``

- To run your migrations you'll need to run:
- ``dotnet ef database update``