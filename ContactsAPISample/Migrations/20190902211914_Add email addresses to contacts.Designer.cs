﻿// <auto-generated />
using System;
using ContactsAPISample.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactsAPISample.Migrations
{
    [DbContext(typeof(ContactsContext))]
    [Migration("20190902211914_Add email addresses to contacts")]
    partial class Addemailaddressestocontacts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactsAPISample.Models.DB.Contact", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Birthdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NickName")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ContactsAPISample.Models.DB.EmailAddress", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40);

                    b.Property<long>("ContactID");

                    b.Property<string>("Type")
                        .HasMaxLength(30);

                    b.HasKey("Email");

                    b.HasIndex("ContactID");

                    b.ToTable("EmailAddresses");
                });

            modelBuilder.Entity("ContactsAPISample.Models.DB.EmailAddress", b =>
                {
                    b.HasOne("ContactsAPISample.Models.DB.Contact", "Contact")
                        .WithMany("EmailAddresses")
                        .HasForeignKey("ContactID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
