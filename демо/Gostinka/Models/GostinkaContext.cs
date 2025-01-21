using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gostinka.Models;

public partial class GostinkaContext : DbContext
{
    public GostinkaContext()
    {
    }

    public GostinkaContext(DbContextOptions<GostinkaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingsService> BookingsServices { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CleaningSchedule> CleaningSchedules { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=gostinka", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.IdBooking).HasName("PRIMARY");

            entity.ToTable("bookings");

            entity.HasIndex(e => e.ClientId, "ClientId");

            entity.HasIndex(e => e.RoomId, "RoomId");

            entity.Property(e => e.Amount).HasPrecision(10, 2);

            entity.HasOne(d => d.Client).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("bookings_ibfk_2");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("bookings_ibfk_1");
        });

        modelBuilder.Entity<BookingsService>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("bookings_services");

            entity.HasIndex(e => e.BookingId, "BookingId");

            entity.HasIndex(e => e.ServiceId, "ServiceId");

            entity.HasOne(d => d.Booking).WithMany()
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("bookings_services_ibfk_2");

            entity.HasOne(d => d.Service).WithMany()
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("bookings_services_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("tinytext");
            entity.Property(e => e.PricePerDay).HasPrecision(10, 2);
        });

        modelBuilder.Entity<CleaningSchedule>(entity =>
        {
            entity.HasKey(e => e.IdCleaning).HasName("PRIMARY");

            entity.ToTable("cleaning_schedule");

            entity.HasIndex(e => e.CleanerId, "CleanerId");

            entity.HasOne(d => d.Cleaner).WithMany(p => p.CleaningSchedules)
                .HasForeignKey(d => d.CleanerId)
                .HasConstraintName("cleaning_schedule_ibfk_1");
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.IdPassport).HasName("PRIMARY");

            entity.ToTable("passports");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Number).HasMaxLength(6);
            entity.Property(e => e.Serial).HasMaxLength(4);
            entity.Property(e => e.WhoIssue).HasMaxLength(100);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.IdRoom).HasName("PRIMARY");

            entity.ToTable("rooms");

            entity.HasIndex(e => e.CategoryId, "CategoryId");

            entity.HasIndex(e => e.StatusId, "StatusId");

            entity.HasOne(d => d.Category).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("rooms_ibfk_1");

            entity.HasOne(d => d.Status).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("rooms_ibfk_2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PRIMARY");

            entity.ToTable("services");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.ServiceName).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("statuses");

            entity.Property(e => e.StatusName).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.PassportId, "PassportId");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Role).HasColumnType("enum('Руководитель','Администратор','Клиент','Сотрудник','Гость')");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Passport).WithMany(p => p.Users)
                .HasForeignKey(d => d.PassportId)
                .HasConstraintName("users_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
