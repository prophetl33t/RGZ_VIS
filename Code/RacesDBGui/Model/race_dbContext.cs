using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RacesDBGui.Model
{
    public partial class race_dbContext : DbContext
    {
        public race_dbContext()
        {
        }

        public race_dbContext(DbContextOptions<race_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Race> Races { get; set; } = null!;
        public virtual DbSet<Rider> Riders { get; set; } = null!;
        public virtual DbSet<RiderRaceStat> RiderRaceStats { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;
        public virtual DbSet<Track> Tracks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=./race_db.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Race>(entity =>
            {
                entity.ToTable("Race");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvgSpeed)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("avgSpeed");

                entity.Property(e => e.Duration)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("duration");

                entity.Property(e => e.Laps).HasColumnName("laps");

                entity.Property(e => e.Tournament).HasColumnName("tournament");

                entity.Property(e => e.Track).HasColumnName("track");

                entity.Property(e => e.VictoryMargin)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("victoryMargin");

                entity.HasOne(d => d.TrackNavigation)
                    .WithMany(p => p.Races)
                    .HasForeignKey(d => d.Track);
            });

            modelBuilder.Entity<Rider>(entity =>
            {
                entity.ToTable("Rider");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Country)
                    .HasColumnType("VARCHAR (40)")
                    .HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasColumnType("VARCHAR (40)")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<RiderRaceStat>(entity =>
            {
                entity.ToTable("RiderRaceStat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chassis)
                    .HasColumnType("VARCHAR (30)")
                    .HasColumnName("chassis");

                entity.Property(e => e.FinishPlace).HasColumnName("finishPlace");

                entity.Property(e => e.Laps).HasColumnName("laps");

                entity.Property(e => e.LapsLed).HasColumnName("lapsLed");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.Race).HasColumnName("race");

                entity.Property(e => e.Rider).HasColumnName("rider");

                entity.Property(e => e.Sponsor)
                    .HasColumnType("VARCHAR (30)")
                    .HasColumnName("sponsor");

                entity.Property(e => e.StartPlace).HasColumnName("startPlace");

                entity.Property(e => e.TeamName)
                    .HasColumnType("VARCHAR (30)")
                    .HasColumnName("teamName");

                entity.HasOne(d => d.RaceNavigation)
                    .WithMany(p => p.RiderRaceStats)
                    .HasForeignKey(d => d.Race);

                entity.HasOne(d => d.RiderNavigation)
                    .WithMany(p => p.RiderRaceStats)
                    .HasForeignKey(d => d.Rider);
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("Tournament");

                entity.Property(e => e.TournamentId).HasColumnName("tournamentId");

                entity.Property(e => e.Name)
                    .HasColumnType("VARCHAR (30)")
                    .HasColumnName("name");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Track");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Length)
                    .HasColumnType("DOUBLE")
                    .HasColumnName("length");

                entity.Property(e => e.Name)
                    .HasColumnType("VARCHAR (30)")
                    .HasColumnName("name");

                entity.Property(e => e.RoadDescription)
                    .HasColumnType("VARCHAR (70)")
                    .HasColumnName("roadDescription");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
