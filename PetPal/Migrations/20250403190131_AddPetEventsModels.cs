using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetPal.Migrations
{
    /// <inheritdoc />
    public partial class AddPetEventsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Pet_PetId",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "Schedules");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameColumn(
                name: "TrainingDuration",
                table: "Training",
                newName: "TrainingDurationMinutes");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_PetId",
                table: "Schedules",
                newName: "IX_Schedules_PetId");

            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "Appointments",
                newName: "AppointmentDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_PetId",
                table: "Appointments",
                newName: "IX_Appointments_PetId");

            migrationBuilder.AlterColumn<string>(
                name: "Portion",
                table: "Schedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Medication",
                table: "Schedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Dosage",
                table: "Schedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ReminderNote",
                table: "Schedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ScheduleTime",
                table: "Schedules",
                type: "time",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "ScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDateTime", "AppointmentType", "Location", "Notes", "PetId", "PetName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), "Vet", "Happy Paws Clinic", "Regualar check-up and vaccination update", 1, "Joey" },
                    { 2, new DateTime(2025, 4, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), "Grooming", "Pet Smart", "Full grooming with nail trim", 2, "Mavis" },
                    { 3, new DateTime(2025, 4, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), "Vet", "Dog & Cat Hospital - East Hill", "Follow-up for paw stitches", 3, "Ollie" }
                });

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "PetId",
                keyValue: 2,
                columns: new[] { "PetAge", "PetBirthday", "PetGender" },
                values: new object[] { 4, new DateTime(2022, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female" });

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "PetId",
                keyValue: 3,
                column: "PetBirthday",
                value: new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$wp5ru8F9xrKXCbHAz1eDf.CJYczC6Z8WFaKtaSNG99Kla9u6Peqim");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$W11KvxC86p7FMW452vF/7ePDQ7GsJKp1uFnn.ywxghsl6CItcSwzG");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pet_PetId",
                table: "Appointments",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Pet_PetId",
                table: "Schedules",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pet_PetId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Pet_PetId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "AppointmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "TrainingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "TrainingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Training",
                keyColumn: "TrainingId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ReminderNote",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ScheduleTime",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Schedule");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameColumn(
                name: "TrainingDurationMinutes",
                table: "Training",
                newName: "TrainingDuration");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_PetId",
                table: "Schedule",
                newName: "IX_Schedule_PetId");

            migrationBuilder.RenameColumn(
                name: "AppointmentDateTime",
                table: "Appointment",
                newName: "AppointmentTime");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PetId",
                table: "Appointment",
                newName: "IX_Appointment_PetId");

            migrationBuilder.AlterColumn<string>(
                name: "Portion",
                table: "Schedule",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medication",
                table: "Schedule",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dosage",
                table: "Schedule",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Schedule",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "ScheduleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "AppointmentId");

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "PetId",
                keyValue: 2,
                columns: new[] { "PetAge", "PetBirthday", "PetGender" },
                values: new object[] { 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male" });

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "PetId",
                keyValue: 3,
                column: "PetBirthday",
                value: new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ejEtGjvHiVrM.NDZqKAp7ejl6Y.NeOmM4sSRMKCSSxq2qDIHxXnZW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$kLQOcxVBMrelBvtY4Bh5IugXKJ5cKAI.lGKV50d5apjVoTFn5hVwm");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Pet_PetId",
                table: "Schedule",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
