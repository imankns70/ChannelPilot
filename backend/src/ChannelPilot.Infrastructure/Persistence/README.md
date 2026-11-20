# Persistence

Contains the SQL Server EF Core `AppDbContext`, Fluent API entity configurations, and repository implementations. SQL Server repositories are the default DI registrations. Temporary in-memory repository files remain only as development references.

No migration has been created and no database has been provisioned. Development uses a credential-free local placeholder connection string; production must receive its connection string from deployment secrets.
