namespace Requiem.Facts.Web.Data.Base;

public abstract class DbContextBase : IdentityDbContext
{
    public SaveChangesResult SaveChangesResult { get; set; }

    protected DbContextBase(DbContextOptions options)
        : base(options)
    {
        SaveChangesResult = new SaveChangesResult(); 
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Startup).Assembly);
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        DbSaveChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        DbSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        DbSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DbSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void DbSaveChanges()
    {
        const string defaultUser = "System";
        DateTime defaultDate = DateTime.UtcNow;

        var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

        foreach (var entry in addedEntries)
        {
            if (entry.Entity is not IAuditable) return;

            var createdBy = entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue;
            var updatedBy = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;
            var createdAt = entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
            var updatedAt = entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue;

            if (string.IsNullOrEmpty(createdBy?.ToString()))
            {
                entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue = defaultUser;
            }

            if (string.IsNullOrEmpty(updatedBy?.ToString()))
            {
                entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = defaultUser;
            }

            if (DateTime.Parse(createdAt?.ToString()!).Year < 1970)
            {
                entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = defaultDate;
            }

            if (updatedAt != null && DateTime.Parse(updatedAt.ToString()!).Year < 1970)
            {
                entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
            }
            else
            {
                entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
            }

            SaveChangesResult.AddMessage("Some entities were created");    
        }

        var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

        foreach(var entry in modifiedEntries) 
        {
            if (entry.Entity is IAuditable)
            {
                var userName = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue == null
                    ? defaultUser
                    : entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;
                entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = userName;
            }

            SaveChangesResult.AddMessage("Some entities were modified");
        }
    }
}