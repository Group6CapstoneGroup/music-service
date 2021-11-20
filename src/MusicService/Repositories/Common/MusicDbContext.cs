using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicService.Models;

namespace MusicService.Repositories.Common
{
    public partial class MusicDbContext : DbContext
    {
        public MusicDbContext()
        {
        }

        public MusicDbContext(DbContextOptions<MusicDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Music> Music { get; set; }
        public virtual DbSet<SelectedSong> SelectedSong { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Music>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("Id");

                entity.ToTable("music");

                entity.Property(e => e.RecordId)
                    .HasColumnName("record_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Artist).HasColumnName("artist");
                entity.Property(e => e.RecordNumber).HasColumnName("record_number");

                entity.Property(e => e.Track).HasColumnName("track");

                entity.Property(e => e.Playlist).HasColumnName("playlist");
                entity.Property(e => e.Album).HasColumnName("album");

            });

            modelBuilder.Entity<SelectedSong>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("Id");

                entity.ToTable("selected_songs");

                entity.Property(e => e.RecordId)
                    .HasColumnName("record_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Artist).HasColumnName("artist");
                entity.Property(e => e.RecordNumber).HasColumnName("record_number");

                entity.Property(e => e.Track).HasColumnName("track");

                entity.Property(e => e.Playlist).HasColumnName("playlist");
                entity.Property(e => e.Album).HasColumnName("album");

            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
