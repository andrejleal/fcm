using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FCM.Repositories.EF.SQLServer;

namespace FCM.Repositories.EF.SQLServer.Migrations
{
    [DbContext(typeof(SQLServerFCMContext))]
    [Migration("20170922155154_CreateDB")]
    partial class CreateDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FCM.DomainModel.Entities.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("CreationTimestamp");

                    b.Property<DateTimeOffset>("LastUpdateTimestamp");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Owner");

                    b.HasKey("Id");

                    b.HasAlternateKey("AppId");


                    b.HasAlternateKey("Name");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("FCM.DomainModel.Entities.ComponentProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("CreationTimestamp");

                    b.Property<DateTimeOffset>("LastUpdateTimestamp");

                    b.Property<string>("Name");

                    b.Property<string>("Owner");

                    b.Property<int>("ParentId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasAlternateKey("AppId");

                    b.HasIndex("ParentId");

                    b.HasIndex("Name", "Owner", "ParentId")
                        .IsUnique();

                    b.ToTable("ComponentProperties");
                });

            modelBuilder.Entity("FCM.DomainModel.Entities.ExternalSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternateToken")
                        .IsRequired();

                    b.Property<Guid>("AppId");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("CreationTimestamp");

                    b.Property<bool>("IsSysAdmin");

                    b.Property<DateTimeOffset>("LastUpdateTimestamp");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NotificationToken");

                    b.Property<string>("NotificationURL");

                    b.Property<string>("Token")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("AlternateToken");


                    b.HasAlternateKey("AppId");


                    b.HasAlternateKey("Name");


                    b.HasAlternateKey("Token");

                    b.ToTable("ExternalSystem");
                });

            modelBuilder.Entity("FCM.DomainModel.Entities.ComponentProperty", b =>
                {
                    b.HasOne("FCM.DomainModel.Entities.Component", "Parent")
                        .WithMany("Properties")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
