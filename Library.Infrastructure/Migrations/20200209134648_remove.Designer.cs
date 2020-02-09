﻿// <auto-generated />
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200209134648_remove")]
    partial class remove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library.Domain.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(55)")
                        .HasMaxLength(55);

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "William Shakespeare"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Villiam Skakspjut"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Robert C. Martin"
                        });
                });

            modelBuilder.Entity("Library.Domain.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DetailsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DetailsId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DetailsId = 1
                        },
                        new
                        {
                            Id = 2,
                            DetailsId = 1
                        },
                        new
                        {
                            Id = 3,
                            DetailsId = 1
                        },
                        new
                        {
                            Id = 4,
                            DetailsId = 2
                        },
                        new
                        {
                            Id = 5,
                            DetailsId = 3
                        });
                });

            modelBuilder.Entity("Library.Domain.BookDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Description = "Arguably Shakespeare's greatest tragedy",
                            ISBN = "1472518381",
                            Title = "Hamlet"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Description = "King Lear is a tragedy written by William Shakespeare. It depicts the gradual descent into madness of the title character, after he disposes of his kingdom by giving bequests to two of his three daughters egged on by their continual flattery, bringing tragic consequences for all.",
                            ISBN = "9780141012292",
                            Title = "King Lear"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Description = "An intense drama of love, deception, jealousy and destruction.",
                            ISBN = "1853260185",
                            Title = "Othello"
                        });
                });

            modelBuilder.Entity("Library.Domain.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SSN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Library.Domain.Book", b =>
                {
                    b.HasOne("Library.Domain.BookDetails", "Details")
                        .WithMany("Copeis")
                        .HasForeignKey("DetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Library.Domain.BookDetails", b =>
                {
                    b.HasOne("Library.Domain.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}