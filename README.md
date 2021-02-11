# DeliveryOrder.Api

Go-Logs ordering API for DO online request.

## Setting up

### Cloning

1. Clone this repository:  
   `git clone <url>`.
2. Update submodules:    
    1. `cd <repo_dir>`.
    2. `git submodule update --init --recursive`.
3. Install latest dotnet-format:  
   `dotnet tool install --global dotnet-format --version 5.0.0-alpha.335`
4. Restore packages. **Must** be executed manually for **every project that uses GoLogs.Framework.Core**:  
    `dotnet restore DeliveryOrder.Api`.

### Database

1. Create an instance of PostgreSQL locally.
2. Install Evolve.Tool:  
   `dotnet tool install --global Evolve.Tool`.
3. Run migrations:  
   `evolve migrate postgresql -c "Server=localhost;Uid=postgres;Database=postgres;" -l "DeliveryOrder.Api/database/migrations"`.
