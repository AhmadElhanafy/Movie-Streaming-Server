using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAtUtc",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LibraryPaths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    IncludePattern = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    ExcludePattern = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    LastScanStartedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastScanCompletedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryPaths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TmdbId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ImdbId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    ShowTitle = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    SeasonNumber = table.Column<int>(type: "integer", nullable: true),
                    EpisodeNumber = table.Column<int>(type: "integer", nullable: true),
                    SourcePath = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    SourceSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    SourceVideoCodec = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    SourceAudioCodec = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    SourceWidth = table.Column<int>(type: "integer", nullable: true),
                    SourceHeight = table.Column<int>(type: "integer", nullable: true),
                    SourceFrameRate = table.Column<double>(type: "double precision", nullable: true),
                    Duration = table.Column<long>(type: "bigint", nullable: true),
                    TmdbId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ImdbId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LibraryPathId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaItems_LibraryPaths_LibraryPathId",
                        column: x => x.LibraryPathId,
                        principalTable: "LibraryPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaPeople",
                columns: table => new
                {
                    MediaItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CharacterName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    BillingOrder = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPeople", x => new { x.MediaItemId, x.PersonId, x.Role });
                    table.ForeignKey(
                        name: "FK_MediaPeople_MediaItems_MediaItemId",
                        column: x => x.MediaItemId,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaPeople_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamRenditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    BitrateKbps = table.Column<int>(type: "integer", nullable: false),
                    FrameRate = table.Column<double>(type: "double precision", nullable: true),
                    VideoCodec = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    AudioCodec = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Container = table.Column<int>(type: "integer", nullable: false),
                    ObjectKeyPrefix = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    ManifestObjectKey = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    SegmentDurationSeconds = table.Column<int>(type: "integer", nullable: true),
                    SegmentCount = table.Column<int>(type: "integer", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamRenditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamRenditions_MediaItems_MediaItemId",
                        column: x => x.MediaItemId,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaybackSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    StreamRenditionId = table.Column<Guid>(type: "uuid", nullable: true),
                    PlaybackType = table.Column<int>(type: "integer", nullable: false),
                    PositionSeconds = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    Device = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ClientVersion = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    StartedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    LastUpdatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    EndedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaybackSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaybackSessions_MediaItems_MediaItemId",
                        column: x => x.MediaItemId,
                        principalTable: "MediaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaybackSessions_StreamRenditions_StreamRenditionId",
                        column: x => x.StreamRenditionId,
                        principalTable: "StreamRenditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PlaybackSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryPaths_Path",
                table: "LibraryPaths",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_ImdbId",
                table: "MediaItems",
                column: "ImdbId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_Kind_ShowTitle_SeasonNumber_EpisodeNumber",
                table: "MediaItems",
                columns: new[] { "Kind", "ShowTitle", "SeasonNumber", "EpisodeNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_LibraryPathId",
                table: "MediaItems",
                column: "LibraryPathId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_Title",
                table: "MediaItems",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_TmdbId",
                table: "MediaItems",
                column: "TmdbId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPeople_MediaItemId_Role_BillingOrder",
                table: "MediaPeople",
                columns: new[] { "MediaItemId", "Role", "BillingOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaPeople_PersonId",
                table: "MediaPeople",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ImdbId",
                table: "People",
                column: "ImdbId");

            migrationBuilder.CreateIndex(
                name: "IX_People_Name",
                table: "People",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_People_TmdbId",
                table: "People",
                column: "TmdbId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaybackSessions_MediaItemId",
                table: "PlaybackSessions",
                column: "MediaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaybackSessions_StreamRenditionId",
                table: "PlaybackSessions",
                column: "StreamRenditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaybackSessions_UserId_MediaItemId",
                table: "PlaybackSessions",
                columns: new[] { "UserId", "MediaItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_StreamRenditions_MediaItemId_Width_Height_BitrateKbps",
                table: "StreamRenditions",
                columns: new[] { "MediaItemId", "Width", "Height", "BitrateKbps" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaPeople");

            migrationBuilder.DropTable(
                name: "PlaybackSessions");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "StreamRenditions");

            migrationBuilder.DropTable(
                name: "MediaItems");

            migrationBuilder.DropTable(
                name: "LibraryPaths");

            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }
    }
}
