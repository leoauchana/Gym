using Microsoft.EntityFrameworkCore;
using Web_Domain.Entities;
using Web_Domain.Entities.Rule;
using Web_Domain.Logs;

namespace Web_Infraestructure.Data;

public class GymContext : DbContext
{
    public GymContext(DbContextOptions<GymContext> options) : base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Fee> Fees { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }
    public DbSet<Pay> Pays { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rule> Rules { get; set; }
    public DbSet<LogAccess> LogAccesses { get; set; }
    public DbSet<LogEmployeeRegister> LogEmployees { get; set; }
    public DbSet<LogClientsRegister> LogClientsRegisters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EmployeeConfig(modelBuilder);
        ClientConfig(modelBuilder);
        FeeConfig(modelBuilder);
        InscriptionConfig(modelBuilder);
        PayConfig(modelBuilder);
        UserConfig(modelBuilder);
        RuleConfig(modelBuilder);
        LogAccessConfig(modelBuilder);
        LogClientsConfig(modelBuilder);
        LogEmployeesConfig(modelBuilder);
    }
    private void EmployeeConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Employee>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(25);
        modelBuilder.Entity<Employee>()
            .Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(25);
        modelBuilder.Entity<Employee>()
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(45);
        modelBuilder.Entity<Employee>()
            .Property(e => e.Age)
            .IsRequired();
        modelBuilder.Entity<Employee>()
            .Property(e => e.Domicile)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Employee>()
            .Property(e => e.File)
            .IsRequired();
    }
    private void ClientConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .ToTable("Clients")
            .HasKey(c => c.Id);
        modelBuilder.Entity<Client>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(25);
        modelBuilder.Entity<Client>()
            .Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(25);
        modelBuilder.Entity<Client>()
            .Property(c => c.Dni)
            .IsRequired();
        modelBuilder.Entity<Client>()
            .Property(c => c.Age)
            .IsRequired();
        modelBuilder.Entity<Client>()
            .Property(c => c.Domicile)
            .IsRequired()
            .HasMaxLength(50);
    }
    private void FeeConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fee>()
            .ToTable("Fees")
            .HasKey(f => f.Id);
        modelBuilder.Entity<Fee>()
            .Property(f => f.Value)
            .IsRequired();
        modelBuilder.Entity<Fee>()
            .Property(f => f.FeeNumber)
            .IsRequired();
        modelBuilder.Entity<Fee>()
            .Property(f => f.InitialDate)
            .IsRequired();
        modelBuilder.Entity<Fee>()
            .Property(f => f.EndDate)
            .IsRequired();
        modelBuilder.Entity<Fee>()
            .Property(f => f.IsCancelled)
            .IsRequired();
    }
    private void InscriptionConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inscription>()
            .ToTable("Inscriptions")
            .HasKey(i => i.Id);
        modelBuilder.Entity<Inscription>()
            .Property(i => i.InscriptionNumber)
            .IsRequired();
        modelBuilder.Entity<Inscription>()
            .Property(i => i.InscriptionDate)
            .IsRequired();
    }
    private void PayConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pay>()
            .ToTable("Pays")
            .HasKey(p => p.Id);
        modelBuilder.Entity<Pay>()
            .Property(p => p.PayDate)
            .IsRequired();
    }
    private void UserConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(U => U.Id);
        modelBuilder.Entity<User>()
            .Property(u => u.UserName)
            .HasMaxLength(20)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(20)
            .IsRequired();
    }
    private void RuleConfig(ModelBuilder modelBuilder)
    {
             modelBuilder.Entity<Rule>()
            .ToTable("Rules")
            .HasKey(r => r.Id);
        modelBuilder.Entity<Rule>()
            .Property(r => r.Value)
            .IsRequired();
        modelBuilder.Entity<Rule>()
            .Property(r => r.UpdatedDate)
            .IsRequired()
            .HasMaxLength(200);
    }
    private void LogAccessConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogAccess>()
            .ToTable("LogAccesses")
            .HasKey(l => l.Id);
        modelBuilder.Entity<LogAccess>()
            .Property(l => l.AccessDate)
            .IsRequired();
        modelBuilder.Entity<LogAccess>()
            .Property(l => l.isSuccess)
            .IsRequired();
    }
    private void LogClientsConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogClientsRegister>()
            .ToTable("LogClientsRegister")
            .HasKey(l => l.Id);
        modelBuilder.Entity<LogClientsRegister>()
            .Property(l => l.RegisterDate)
            .IsRequired();
    }
    private void LogEmployeesConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEmployeeRegister>()
            .ToTable("LogEmployeesRegister")
            .HasKey(l => l.Id);
        modelBuilder.Entity<LogEmployeeRegister>()
            .Property(l => l.RegisterDate)
            .IsRequired();
        modelBuilder.Entity<LogEmployeeRegister>()
            .Property(l => l.Description)
            .HasMaxLength(100)
            .IsRequired();
    }

}