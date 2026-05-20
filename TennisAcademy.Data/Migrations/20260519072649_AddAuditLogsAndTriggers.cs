using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    public partial class AddAuditLogsAndTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "log_22180021",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_22180021", x => x.Id);
                });

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180021].[tr_Coaches_Audit]
                ON [22180021].[Coaches]
                AFTER INSERT, UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Type VARCHAR(20) = 'INSERT';
                    IF EXISTS(SELECT * FROM deleted) SET @Type = 'UPDATE';

                    INSERT INTO [22180021].[log_22180021] (TableName, OperationType, OperationTimestamp)
                    VALUES ('Coaches', @Type, GETDATE());
                END");

            migrationBuilder.Sql(@"
                CREATE TRIGGER [22180021].[tr_Reservations_Audit]
                ON [22180021].[Reservations]
                AFTER INSERT, UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Type VARCHAR(20) = 'INSERT';
                    IF EXISTS(SELECT * FROM deleted) SET @Type = 'UPDATE';

                    INSERT INTO [22180021].[log_22180021] (TableName, OperationType, OperationTimestamp)
                    VALUES ('Reservations', @Type, GETDATE());
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS [22180021].[tr_Coaches_Audit]");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS [22180021].[tr_Reservations_Audit]");


            migrationBuilder.DropTable(
                name: "log_22180021",
                schema: "22180021");
        }
    }
}