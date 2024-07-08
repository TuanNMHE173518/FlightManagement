using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BussinessObjects;

public partial class FlightManagementDbContext : DbContext
{
    public FlightManagementDbContext()
    {
    }

    public FlightManagementDbContext(DbContextOptions<FlightManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountMember> AccountMembers { get; set; }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Baggage> Baggages { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingPlatform> BookingPlatforms { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\TUANDEPTRY;uid=tuannm;pwd=123;database=FlightManagementDB;Encrypt=false;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountMember>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("AccountMember");

            entity.HasIndex(e => e.Email, "IX_AccountMember").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__airline__3213E83F71262BC0");

            entity.ToTable("airline");

            entity.HasIndex(e => e.Code, "UQ__airline__357D4CF9D8B2FEC9").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__airline__72E12F1B489FC1D1").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__airport__3213E83F5598A1E7");

            entity.ToTable("airport");

            entity.HasIndex(e => e.Code, "UQ__airport__357D4CF984153B6B").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
        });

        modelBuilder.Entity<Baggage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__baggage__3213E83FF8DC8B9C");

            entity.ToTable("baggage");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.WeightInKg)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("weight_in_kg");

            entity.HasOne(d => d.Booking).WithMany(p => p.Baggages)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__baggage__booking__4BAC3F29");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__booking__3213E83FDD96127F");

            entity.ToTable("booking");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BookingPlatformId).HasColumnName("booking_platform_id");
            entity.Property(e => e.BookingTime)
                .HasColumnType("datetime")
                .HasColumnName("booking_time");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.PassengerId).HasColumnName("passenger_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.BookingPlatform).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BookingPlatformId)
                .HasConstraintName("FK__booking__booking__48CFD27E");

            entity.HasOne(d => d.Flight).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__booking__flight___47DBAE45");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__booking__passeng__46E78A0C");
        });

        modelBuilder.Entity<BookingPlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bookingP__3213E83F43A89E9B");

            entity.ToTable("bookingPlatform");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__flight__3213E83F8975D25D");

            entity.ToTable("flight");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AirlineId).HasColumnName("airline_id");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("arrival_time");
            entity.Property(e => e.ArrivingAirport).HasColumnName("arriving_airport");
            entity.Property(e => e.ArrivingGate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("arriving_gate");
            entity.Property(e => e.DepartingAirport).HasColumnName("departing_airport");
            entity.Property(e => e.DepartingGate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("departing_gate");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("departure_time");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .HasConstraintName("FK__flight__airline___3E52440B");

            entity.HasOne(d => d.ArrivingAirportNavigation).WithMany(p => p.FlightArrivingAirportNavigations)
                .HasForeignKey(d => d.ArrivingAirport)
                .HasConstraintName("FK__flight__arriving__403A8C7D");

            entity.HasOne(d => d.DepartingAirportNavigation).WithMany(p => p.FlightDepartingAirportNavigations)
                .HasForeignKey(d => d.DepartingAirport)
                .HasConstraintName("FK__flight__departin__3F466844");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__passenge__3213E83F3A6ED8A0");

            entity.ToTable("passenger");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
