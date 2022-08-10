﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamTec.Migrations
{
    public partial class InitialMigration : Migration
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

            migrationBuilder.InsertData(
                table: "Stream",
                columns: new[] { "StreamID", "Room", "Credits", "Day", "StartTime", "EndTime", "Capacity" },
                values: new object[,] {
					{"IT-4105-Com-B-02","T604","15","Friday","12:00pm","2:00pm","24"},
			 		{"IT-4104-Com-A-01","T603","15","Thursday","9:00am","11:00am","29"},
			 		{"IT-4104-Com-A-02","T604","15","Thursday","12:00pm","2:00pm","25"},
			 		{"IT-4104-Com-B-01","T605","15","Thursday","2:00pm","4:00pm","23"},
			 		{"IT-4104-Com-B-02","T607","15","Friday","9:00am","11:00am","26"},
			 		{"IT-4105-Com-A-01","T601","15","Wednesday","9:00am","11:00am","28"},
			 		{"IT-4105-Com-A-02","T607","15","Thursday","9:00am","11:00am","21"},
			 		{"IT-4105-Com-B-01","T602","15","Thursday","12:00pm","2:00pm","25"},
			 		{"IT-4106-Com-A-01","T604","15","Wednesday","1:00pm","3:00pm","26"},
			 		{"IT-4106-Com-A-02","T605","15","Tuesday","12:00pm","2:00pm","20"},
			 		{"IT-4106-Com-B-01","T601","15","Friday","9:00am","11:00am","23"},
			 		{"IT-4106-Com-B-02","T606","15","Monday","2:00pm","4:00pm","26"},
			 		{"IT-4107-Com-A-01","T601","15","Monday","11:00am","1:00pm","24"},
			 		{"IT-4107-Com-A-02","T604","15","Monday","11:00am","1:00pm","27"},
			 		{"IT-4107-Com-B-01","T604","15","Tuesday","12:00pm","2:00pm","29"},
			 		{"IT-4107-Com-B-02","T601","15","Wednesday","1:00pm","3:00pm","27"},
			 		{"IT-5504-Com-A-01","T603","15","Friday","9:00am","12:00pm","27"},
			 		{"IT-5504-Com-A-02","T605","15","Thursday","8:00am","11:00am","22"},
			 		{"IT-5506-Com-A-01","T607","15","Tuesday","1:00pm","3:00pm","20"},
			 		{"IT-5506-Com-A-02","T608","15","Wednesday","3:00pm","5:00pm","29"},
			 		{"IT-5506-Com-B-01","T608","15","Wednesday","12:00pm","2:00pm","28"},
			 		{"IT-5506-Com-B-02","T608","15","Thursday","3:00pm","5:00pm","28"},
			 		{"IT-5507-Com-A-01","T601","15","Thursday","9:00am","11:00am","22"},
			 		{"IT-5507-Com-A-02","T604","15","Tuesday","3:00pm","5:00pm","29"},
			 		{"IT-5507-Com-B-01","T608","15","Friday","2:00pm","4:00pm","22"},
			 		{"IT-5507-Com-B-02","T602","15","Friday","12:00pm","2:00pm","22"},
			 		{"CS-6501-Com-A-01","Zoom","15","Friday","12:00pm","2:00pm","22"},
			 		{"CS-6501-Com-A-02","Zoom","15","Thursday","9:00am","11:00am","28"},
			 		{"CS-6501-Lec-A-01","Zoom","15","Friday","9:00am","11:00am","24"},
			 		{"CS-6502-Com-A-01","Zoom","15","Wednesday","10:00am","12:00pm","22"},
			 		{"CS-6502-Com-A-02","Zoom","15","Tuesday","12:00pm","2:00pm","28"},
			 		{"CS-6502-Lec-A-01","Zoom","15","Monday","8:00am","10:00am","28"},
			 		{"CS-7501-Com-A-01","Zoom","15","Tuesday","10:00am","1:00pm","21"},
			 		{"CS-7501-Com-A-02","Zoom","15","Friday","2:00pm","5:00pm","26"},
			 		{"CS-7501-Lec-A-01","Zoom","15","Monday","3:00pm","4:00pm","20"},
			 		{"CS-7503-Com-A-01","Zoom","15","Wednesday","2:00pm","5:00pm","22"},
			 		{"CS-7503-Lec-A-01","Zoom","15","Monday","2:00pm","3:00pm","24"},
			 		{"CS-7504-Com-A-01","Zoom","15","Thursday","8:00am","11:00am","21"},
			 		{"CS-7504-Com-A-02","Zoom","15","Friday","9:00am","12:00pm","22"},
			 		{"CS-7504-Lec-A-01","Zoom","15","Tuesday","12:00pm","1:00pm","30"},
			 		{"CS-7505-Com-A-01","Zoom","15","Thursday","1:00pm","4:00pm","21"},
			 		{"CS-7505-Lec-A-01","Zoom","15","Wednesday","12:00pm","1:00pm","24"},
			 		{"DS-6504-Com-A-01","Zoom","15","Wednesday","12:00pm","2:00pm","22"},
			 		{"DS-6504-Com-B-01","Zoom","15","Thursday","2:00pm","4:00pm","20"},
			 		{"IT-4104-Lec-A-01","Zoom","15","Wednesday","12:00pm","1:00pm","25"},
			 		{"IT-4105-Lec-A-01","Zoom","15","Tuesday","3:00pm","4:00pm","21"},
			 		{"IT-4106-Lec-A-01","Zoom","15","Tuesday","10:00am","11:00am","24"},
			 		{"IT-4107-Lec-A-01","Zoom","15","Monday","9:00am","10:00am","28"},
			 		{"IT-5501-Lec-A-01","Zoom","15","Monday","10:00am","11:00am","22"},
			 		{"IT-5501-Lec-B-01","Zoom","15","Tuesday","8:00am","9:00am","26"},
			 		{"IT-5501-Tut-A-01","Zoom","15","Tuesday","9:00am","11:00am","26"},
			 		{"IT-5501-Tut-A-02","Zoom","15","Tuesday","12:00pm","2:00pm","23"},
			 		{"IT-5501-Tut-B-01","Zoom","15","Wednesday","3:00pm","5:00pm","23"},
			 		{"IT-5501-Tut-B-02","Zoom","15","Thursday","12:00pm","2:00pm","29"},
			 		{"IT-5504-Lec-A-01","Zoom","15","Monday","11:00am","1:00pm","29"},
			 		{"IT-5506-Lec-A-01","Zoom","15","Tuesday","11:00am","12:00pm","29"},
			 		{"IT-5507-Lec-A-01","Zoom","15","Monday","9:00am","10:00am","28"},
			 		{"IT-6501-Com-A-01","Zoom","15","Thursday","9:00am","11:00am","25"},
			 		{"IT-6501-Com-A-02","Zoom","15","Wednesday","10:00am","12:00pm","29"},
			 		{"IT-6501-Com-A-03","Zoom","15","Thursday","2:00pm","4:00pm","20"},
			 		{"IT-6501-Lec-A-01","Zoom","15","Monday","11:00am","1:00pm","29"},
			 		{"IT-6502-Com-A-01","Zoom","15","Friday","9:00am","11:00am","23"},
			 		{"IT-6502-Lec-A-01","Zoom","15","Wednesday","8:00am","10:00am","20"},
			 		{"IT-7502-Tut-A-01","Zoom","15","Tuesday","1:00pm","3:00pm","25"},
			 		{"IT-7502-Tut-A-02","Zoom","15","Monday","1:00pm","3:00pm","22"},
			 		{"IT-7510-Lec-A-01","Zoom","15","Tuesday","3:00pm","4:00pm","22"},
			 		{"IT-7510-Lec-B-01","Zoom","15","Tuesday","9:00am","10:00am","20"},
			 		{"IT-7510-Tut-A-01","Zoom","15","Thursday","12:00pm","1:00pm","22"},
			 		{"NI-6501-Com-A-01","Zoom","15","Tuesday","2:00pm","5:00pm","28"},
			 		{"NI-6501-Lec-A-01","Zoom","15","Wednesday","9:00am","10:00am","26"},
			 		{"NI-6503-Com-A-01","Zoom","15","Friday","12:00pm","3:00pm","22"},
			 		{"NI-6503-Lec-A-01","Zoom","15","Tuesday","10:00am","11:00am","22"},
			 		{"NI-7501-Com-A-01","Zoom","15","Tuesday","10:00am","12:00pm","30"},
			 		{"NI-7501-Com-B-01","Zoom","15","Thursday","11:00am","1:00pm","25"},
			 		{"SD-6501-Com-A-01","Zoom","15","Tuesday","9:00am","12:00pm","27"},
			 		{"SD-6501-Com-A-02","Zoom","15","Friday","1:00pm","4:00pm","24"},
			 		{"SD-6501-Lec-A-01","Zoom","15","Thursday","1:00pm","2:00pm","24"},
			 		{"SD-6502-Com-A-01","Zoom","15","Monday","2:00pm","5:00pm","21"},
			 		{"SD-6502-Com-B-01","Zoom","15","Tuesday","9:00am","12:00pm","22"},
			 		{"SD-6502-Lec-A-01","Zoom","15","Monday","1:00pm","2:00pm","27"},
			 		{"SD-7501-Com-A-01","Zoom","15","Wednesday","1:00pm","4:00pm","25"},
			 		{"SD-7501-Lec-A-01","Zoom","15","Wednesday","10:00am","11:00am","24"},
			 		{"IT-5115-Com-A-01","T602","15","Wednesday","1:00pm","3:00pm","20"},
			 		{"IT-5115-Com-A-02","T604","15","Wednesday","9:00am","11:00am","24"},
			 		{"IT-5115-Com-A-03","T605","15","Monday","8:00am","10:00am","20"},
			 		{"IT-5115-Com-B-01","T607","15","Tuesday","11:00am","1:00pm","25"},
			 		{"IT-5115-Com-B-02","T602","15","Monday","1:00pm","3:00pm","23"},
			 		{"IT-5115-Com-B-03","T608","15","Thursday","9:00am","11:00am","28"},
			 		{"IT-5116-Com-A-01","T606","15","Thursday","11:00am","1:00pm","21"},
			 		{"IT-5116-Com-A-02","T606","15","Wednesday","11:00am","1:00pm","25"},
			 		{"IT-5116-Com-A-03","T601","15","Monday","1:00pm","3:00pm","20"},
			 		{"IT-5116-Com-B-01","T602","15","Friday","10:00am","12:00pm","25"},
			 		{"IT-5116-Com-B-02","T602","15","Tuesday","10:00am","12:00pm","22"},
			 		{"IT-5116-Com-B-03","T606","15","Friday","2:00pm","4:00pm","20"},
			 		{"IT-5117-Com-A-01","T603","15","Wednesday","9:00am","12:00pm","28"},
			 		{"IT-5117-Com-A-02","T603","15","Thursday","1:00pm","4:00pm","21"},
			 		{"IT-5117-Com-A-03","T603","15","Wednesday","1:00pm","4:00pm","30"},
			 		{"IT-5118-Com-A-01","T604","15","Monday","1:00pm","3:00pm","27"},
			 		{"IT-5118-Com-A-02","T604","15","Friday","2:00pm","4:00pm","29"},
			 		{"IT-5118-Com-A-03","T306","15","Tuesday","10:00am","12:00pm","21"},
			 		{"IT-5118-Com-B-01","T306","15","Thursday","9:00am","11:00am","29"},
			 		{"IT-5118-Com-B-02","C103","15","Wednesday","2:00pm","4:00pm","29"},
			 		{"IT-5118-Com-B-03","T304","15","Thursday","1:00pm","3:00pm","27"},
			 		{"IT-5119-Com-A-01","T604","15","Thursday","2:00pm","4:00pm","28"},
			 		{"IT-5119-Com-A-02","T604","15","Tuesday","10:00am","12:00pm","25"},
			 		{"IT-5119-Com-B-01","T601","15","Tuesday","1:00pm","3:00pm","23"},
			 		{"IT-5119-Com-B-02","T604","15","Monday","9:00am","11:00am","21"},
			 		{"IT-5120-Com-A-01","T603","15","Tuesday","9:00am","12:00pm","27"},
			 		{"IT-5120-Com-A-02","T603","15","Tuesday","1:00pm","4:00pm","28"},
			 		{"IT-5121-Com-A-01","T601","15","Friday","12:00pm","3:00pm","26"},
			 		{"IT-5121-Com-A-02","T605","15","Wednesday","11:00am","2:00pm","28"},
			 		{"IT-5122-Com-A-01","T608","15","Thursday","11:00am","1:00pm","21"},
			 		{"IT-5122-Com-A-02","T608","15","Friday","12:00pm","2:00pm","27"},
			 		{"IT-5122-Com-B-01","T608","15","Wednesday","10:00am","12:00pm","25"},
			 		{"IT-5122-Com-B-02","T608","15","Thursday","1:00pm","3:00pm","20"},
			 		{"IT-5115-Lec-A-01","Zoom","15","Tuesday","9:00am","10:00am","26"},
			 		{"IT-5116-Lec-A-01","Zoom","15","Tuesday","1:00pm","2:00pm","20"},
			 		{"IT-5117-Lec-A-01","Zoom","15","Monday","10:00am","12:00pm","24"},
			 		{"IT-5118-Lec-A-01","Zoom","15","Tuesday","3:00pm","4:00pm","30"},
			 		{"IT-5119-Lec-A-01","Zoom","15","Monday","12:00pm","1:00pm","29"},
			 		{"IT-5120-Lec-A-01","Zoom","15","Monday","1:00pm","3:00pm","27"},
			 		{"IT-5121-Lec-A-01","Zoom","15","Friday","9:00am","11:00am","21"},
			 		{"IT-5122-Lec-A-01","Zoom","15","Wednesday","9:00am","10:00am","25"}
	});

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
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
					EnrollmentID = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
					StreamID = table.Column<string>(type: "nvarchar(50)", nullable: false),
					StudentId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EnrollmentID", x => x.EnrollmentID);
					table.ForeignKey("FK_StreamID", column: y => y.StreamID, principalTable: "Stream", principalColumn: "StreamID", onDelete: ReferentialAction.Cascade);
					table.ForeignKey("FK_StudentId", column: z => z.StudentId, principalTable: "Student", principalColumn: "StudentId", onDelete: ReferentialAction.Cascade);
				});
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stream");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}