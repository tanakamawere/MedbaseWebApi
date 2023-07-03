﻿// <auto-generated />
using System;
using MedbaseApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedbaseApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230703125215_AnswerCorrectionsTable")]
    partial class AnswerCorrectionsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedbaseApi.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Writer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("MedbaseApi.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("MedbaseApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AnswerA")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerB")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerC")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerD")
                        .HasColumnType("bit");

                    b.Property<bool>("AnswerE")
                        .HasColumnType("bit");

                    b.Property<string>("ChildA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExplanationE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionMain")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopicRef")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MedbaseApi.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("MedbaseApi.Models.Topic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CourseRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TopicRef")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("MedbaseApi.Models.Topic", b =>
                {
                    b.HasOne("MedbaseApi.Models.Course", null)
                        .WithMany("Topics")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("MedbaseApi.Models.Course", b =>
                {
                    b.Navigation("Topics");
                });
#pragma warning restore 612, 618
        }
    }
}
