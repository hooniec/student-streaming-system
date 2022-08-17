using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamTec.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stream",
                columns: table => new
                {
                    StreamID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stream", x => x.StreamID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StreamID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => new { x.StreamID, x.StudentId });
                    table.ForeignKey(
                        name: "FK_Enrollment_Stream_StreamID",
                        column: x => x.StreamID,
                        principalTable: "Stream",
                        principalColumn: "StreamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId",
                table: "Enrollment",
                column: "StudentId");

            migrationBuilder.InsertData(
                table: "Stream",
                columns: new[] { "StreamID", "Room", "Credits", "Day", "StartTime", "EndTime", "Capacity" },
                values: new object[,] {
                    {"IT-4105-Com-B-02","T604","15","Friday","12:00pm","2:00pm","24"},
                    {"IT-5507-Lec-A-01","Zoom","15","Monday","9:00am","10:00am","26"},
                 {"IT-5119-Com-B-02","T604","15","Monday","9:00am","11:00am","25"},
                 {"IT-5501-Lec-A-01","Zoom","15","Monday","10:00am","11:00am","30"},
                 {"IT-5117-Lec-A-01","Zoom","15","Monday","10:00am","12:00pm","28"},
                 {"IT-5504-Lec-A-01","Zoom","15","Monday","11:00am","1:00pm","30"},
                 {"IT-6501-Lec-A-01","Zoom","15","Monday","11:00am","1:00pm","28"},
                 {"IT-5119-Lec-A-01","Zoom","15","Monday","12:00pm","1:00pm","25"},
                 {"SD-6502-Lec-A-01","Zoom","15","Monday","1:00pm","2:00pm","27"},
                 {"IT-5115-Com-B-02","T602","15","Monday","1:00pm","3:00pm","24"},
                 {"IT-5116-Com-A-03","T601","15","Monday","1:00pm","3:00pm","23"},
                 {"IT-5118-Com-A-01","T604","15","Monday","1:00pm","3:00pm","29"},
                 {"IT-5120-Lec-A-01","Zoom","15","Monday","1:00pm","3:00pm","21"},
                 {"IT-7502-Tut-A-02","Zoom","15","Monday","1:00pm","3:00pm","22"},
                 {"SD-6502-Com-A-01","Zoom","15","Monday","2:00pm","5:00pm","27"},
                 {"IT-5501-Lec-B-01","Zoom","15","Tuesday","8:00am","9:00am","26"},
                 {"IT-5115-Lec-A-01","Zoom","15","Tuesday","9:00am","10:00am","20"},
                 {"IT-7510-Lec-B-01","Zoom","15","Tuesday","9:00am","10:00am","28"},
                 {"IT-5501-Tut-A-01","Zoom","15","Tuesday","9:00am","11:00am","21"},
                 {"IT-5120-Com-A-01","T603","15","Tuesday","9:00am","12:00pm","21"},
                 {"SD-6501-Com-A-01","Zoom","15","Tuesday","9:00am","12:00pm","22"},
                 {"SD-6502-Com-B-01","Zoom","15","Tuesday","9:00am","12:00pm","30"},
                 {"NI-6503-Lec-A-01","Zoom","15","Tuesday","10:00am","11:00am","29"},
                 {"IT-5116-Com-B-02","T602","15","Tuesday","10:00am","12:00pm","24"},
                 {"IT-5118-Com-A-03","T306","15","Tuesday","10:00am","12:00pm","28"},

    });


            migrationBuilder.InsertData(
               table: "Student",
               columns: new[] { "StudentId", "Email" },
               values: new object[,] {
                    {"2208266","ethan.riwaka01@student.weltec.ac.nz"},
                  {"2208827","justin.martin01@student.weltec.ac.nz"},
                 {"2208282","sunghoon.cho@student.weltec.ac.nz"},
                 {"2107212","jeremy2001@gmail.com"},
                 {"2307542","darolmansfield@outlook.com"},
                 {"2007872","rebeccasmith01@hotmail.com"},
                 {"2209341","lily2008@gmail.com"},
                 {"2106785","abel.abraham01@student.weltec.ac.nz"},
                 {"2309174","susan.solomons01@student.weltec.ac.nz"},
               });



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Stream");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
