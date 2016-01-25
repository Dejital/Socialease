using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Socialease.Models;

namespace Socialease.Migrations
{
    [DbContext(typeof(SocialContext))]
    [Migration("20160125033329_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Socialease.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Socialease.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<int?>("PingId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Socialease.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PhoneNumber");

                    b.Property<int?>("PingId");

                    b.Property<int?>("Priority");

                    b.Property<string>("Relationship");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Socialease.Models.PersonGroup", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("GroupId");

                    b.HasKey("PersonId", "GroupId");
                });

            modelBuilder.Entity("Socialease.Models.Ping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Socialease.Models.PingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Socialease.Models.SpecialDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("PersonId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Socialease.Models.Note", b =>
                {
                    b.HasOne("Socialease.Models.Ping")
                        .WithMany()
                        .HasForeignKey("PingId");
                });

            modelBuilder.Entity("Socialease.Models.Person", b =>
                {
                    b.HasOne("Socialease.Models.Ping")
                        .WithMany()
                        .HasForeignKey("PingId");
                });

            modelBuilder.Entity("Socialease.Models.Ping", b =>
                {
                    b.HasOne("Socialease.Models.PingType")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("Socialease.Models.SpecialDay", b =>
                {
                    b.HasOne("Socialease.Models.Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });
        }
    }
}
