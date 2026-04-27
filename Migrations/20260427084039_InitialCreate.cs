using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevRequestPortal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annotations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrackingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    RequesterName = table.Column<string>(type: "TEXT", nullable: false),
                    RequesterEmail = table.Column<string>(type: "TEXT", nullable: false),
                    SystemName = table.Column<string>(type: "TEXT", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ManagerName = table.Column<string>(type: "TEXT", nullable: false),
                    ManagerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ChangeType = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    AsIs = table.Column<string>(type: "TEXT", nullable: false),
                    ToBe = table.Column<string>(type: "TEXT", nullable: false),
                    ScreenShotDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeLogRef = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeLogDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UrgencyLevel = table.Column<string>(type: "TEXT", nullable: false),
                    DesiredDeadline = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AffectedUsers = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annotations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrackingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ScreeningCategory = table.Column<string>(type: "TEXT", nullable: true),
                    ScreeningScore = table.Column<int>(type: "INTEGER", nullable: true),
                    ScreeningRecommendation = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    RequesterName = table.Column<string>(type: "TEXT", nullable: false),
                    RequesterEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    ManagerName = table.Column<string>(type: "TEXT", nullable: false),
                    ManagerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Problem = table.Column<string>(type: "TEXT", nullable: false),
                    Objective = table.Column<string>(type: "TEXT", nullable: false),
                    UserGroupsJson = table.Column<string>(type: "TEXT", nullable: false),
                    Benefits = table.Column<string>(type: "TEXT", nullable: true),
                    FeaturesJson = table.Column<string>(type: "TEXT", nullable: false),
                    WorkflowDescription = table.Column<string>(type: "TEXT", nullable: false),
                    BusinessRules = table.Column<string>(type: "TEXT", nullable: true),
                    RequiredScreensJson = table.Column<string>(type: "TEXT", nullable: false),
                    DataFieldsJson = table.Column<string>(type: "TEXT", nullable: false),
                    UIDraftDescription = table.Column<string>(type: "TEXT", nullable: true),
                    SampleDataDescription = table.Column<string>(type: "TEXT", nullable: true),
                    UrgencyLevel = table.Column<string>(type: "TEXT", nullable: false),
                    DesiredDeadline = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HardDeadline = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeadlineReason = table.Column<string>(type: "TEXT", nullable: true),
                    ChecklistJson = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnotationAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnnotationId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnotationAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnotationAttachments_Annotations_AnnotationId",
                        column: x => x.AnnotationId,
                        principalTable: "Annotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProposalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", nullable: false),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalAttachments_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnotationAttachments_AnnotationId",
                table: "AnnotationAttachments",
                column: "AnnotationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalAttachments_ProposalId",
                table: "ProposalAttachments",
                column: "ProposalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnotationAttachments");

            migrationBuilder.DropTable(
                name: "ProposalAttachments");

            migrationBuilder.DropTable(
                name: "Annotations");

            migrationBuilder.DropTable(
                name: "Proposals");
        }
    }
}
