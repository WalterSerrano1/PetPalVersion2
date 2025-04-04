using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetPal.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PetType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetBreed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PetBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetAge = table.Column<int>(type: "int", nullable: false),
                    PetGender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pet_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recurrence = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScheduleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Portion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Medication = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReminderNote = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrainingDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    TrainingNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.TrainingId);
                    table.ForeignKey(
                        name: "FK_Training_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "$2a$11$XvhBmNBynzFRtkLpc3fGOOlTLAajDyI5speB1xs8L3bBYXBTRGiVq", "Josie" },
                    { 2, "$2a$11$WIKNUkJktAuWq8Hk2E/7QObhCzXmf07Ig/PVHbw3DSFxVXhFhU2Ha", "Walter" }
                });

            migrationBuilder.InsertData(
                table: "Pet",
                columns: new[] { "PetId", "ImageUrl", "PetAge", "PetBirthday", "PetBreed", "PetGender", "PetName", "PetType", "UserId" },
                values: new object[,]
                {
                    { 1, "/images/Joey.jpg", 15, new DateTime(2010, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Japanese Bobtail", "Male", "Joey", "Cat", 1 },
                    { 2, "/images/Mavis.jpg", 4, new DateTime(2022, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Australian Shepard", "Female", "Mavis", "Dog", 2 },
                    { 3, "/images/Ollie.jpg", 4, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Golden Doodle", "Male", "Ollie", "Dog", 2 },
                    { 4, "", 4, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Labrador", "Male", "Max", "Dog", 2 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDateTime", "AppointmentType", "Location", "Notes", "PetId", "PetName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), "Vet", "Happy Paws Clinic", "Regualar check-up and vaccination update", 1, "Joey" },
                    { 2, new DateTime(2025, 4, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), "Grooming", "Pet Smart", "Full grooming with nail trim", 2, "Mavis" },
                    { 3, new DateTime(2025, 4, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), "Vet", "Dog & Cat Hospital - East Hill", "Follow-up for paw stitches", 3, "Ollie" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "Dosage", "EndDate", "Medication", "PetId", "PetName", "Portion", "Recurrence", "ReminderNote", "ScheduleDate", "ScheduleTime", "ScheduleType" },
                values: new object[,]
                {
                    { 1, null, null, null, 1, "Joey", "1/4 cup dry food", "Daily", "Morning feeding", new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0), "Feeding" },
                    { 2, null, null, null, 2, "Mavis", "1/2 cup of OpenFarm food", "Daily", "Morning feeding", new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 11, 0, 0, 0), "Feeding" },
                    { 3, "5 ml", null, "Antibiotic", 3, "Ollie", null, "Daily", "After evening meal", new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0), "Medication" }
                });

            migrationBuilder.InsertData(
                table: "Training",
                columns: new[] { "TrainingId", "PetId", "PetName", "TrainingDate", "TrainingDurationMinutes", "TrainingNotes", "TrainingType" },
                values: new object[,]
                {
                    { 1, 1, "Joey", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Interactive toy session indoors.", "Play Time" },
                    { 2, 2, "Mavis", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Walked around the park. Very energetic.", "Walking" },
                    { 3, 3, "Ollie", new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, "Practiced recall and fetch. Good progress.", "New Trick" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetId",
                table: "Appointments",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_UserId",
                table: "Pet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PetId",
                table: "Schedules",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_PetId",
                table: "Training",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
