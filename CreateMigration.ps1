# Prompt the user for the migration name
$migrationName = Read-Host "Enter the migration name:"


# Generate the command to create EF Core migrations
$command = "dotnet ef migrations add $migrationName --startup-project src\BerryJuice -o Persistence\Migrations --project src\Backend\Accounts.Infrastructure\ --context AccountsContext"

# Execute the command
Invoke-Expression $command

# Generate the command to create EF Core migrations
$command = "dotnet ef migrations add $migrationName --startup-project src\BerryJuice -o Persistence\Migrations --project src\Backend\BerryJuice.Infrastructure\ --context BerryJuiceDbContext"

# Execute the command
Invoke-Expression $command