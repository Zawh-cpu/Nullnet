using Database.Application.Abstractions.Persistence;
using Database.Infrastructure.Data.Entities;

namespace Database.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ResourceEntity> Resources => Set<ResourceEntity>();
    public DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
    public DbSet<RoleAssignmentEntity> RoleAssignments => Set<RoleAssignmentEntity>();
    
    public DbSet<KeyEntity> Keys => Set<KeyEntity>();
    public DbSet<LocationEntity> Locations => Set<LocationEntity>();
    public DbSet<ProtocolConfigEntity> ProtocolConfigs => Set<ProtocolConfigEntity>();
    public DbSet<RateEntity> Rates => Set<RateEntity>();
    public DbSet<ServerEntity> Servers => Set<ServerEntity>();
    public DbSet<SubscriptionEntity> Subscriptions => Set<SubscriptionEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}