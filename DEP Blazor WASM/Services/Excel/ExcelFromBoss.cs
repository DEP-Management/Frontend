using ClosedXML.Excel;
using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Services.Interfaces;
using static DEP_Blazor_WASM.Services.Excel.ExcelFromBoss;

namespace DEP_Blazor_WASM.Services.Excel
{
    public class ExcelFromBoss : IExcelFromBoss

    {
        int row = 1;
        public async Task<byte[]> GenerateBossExcelAsync(List<EducationalBossViewModel> bosses)
        {
            return await Task.Run(() =>
            {

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Leader Data");

                row = 1;

                var lastboss = bosses.LastOrDefault();
                AddBossHeaders(worksheet);

                foreach (var boss in bosses)
                {
                    AddBossRow(worksheet, boss);
                    if (boss.EducationalLeaders.Any())
                    {
                        var lastLeader = boss.EducationalLeaders.LastOrDefault();

                        AddLeaderHeaders(worksheet);
                        foreach (var leader in boss.EducationalLeaders)
                        {
                            AddLeaderRow(worksheet, leader);
                            if (leader.Teachers.Any())
                            {
                                var lastTeacher = leader.Teachers.LastOrDefault();
                                AddTeacherHeaders(worksheet);
                                foreach (var teacher in leader.Teachers)
                                {
                                    var lastPersonCourses = teacher.PersonCourses.LastOrDefault();

                                    AddTeacherRow(worksheet, teacher);
                                    if (teacher.PersonCourses.Any())
                                    {
                                        AddCourseHeaders(worksheet);
                                        foreach (var personCourse in teacher.PersonCourses)
                                        {
                                            AddCourseRow(worksheet, personCourse);
                                            if (personCourse == lastPersonCourses && teacher != lastTeacher)
                                            {
                                                AddTeacherHeaders(worksheet);
                                            }
                                        }
                                    }
                                    if (teacher == lastTeacher && leader != lastLeader)
                                    {
                                        AddLeaderHeaders(worksheet);
                                    }
                                }
                            }
                            if (leader == lastLeader && boss != lastboss)
                            {
                                AddBossHeaders(worksheet);
                            }
                        }
                    }
                }


                worksheet.Columns().AdjustToContents();
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            });
        }

        public async Task<byte[]> GenerateLeaderExcelAsync(List<EducationalLeaderViewModel> leaders)
        {
            return await Task.Run(() =>
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Leader Data");

                row = 1;
                var lastLeader = leaders.LastOrDefault();

                AddLeaderHeaders(worksheet);

                foreach (var leader in leaders)
                {
                    AddLeaderRow(worksheet, leader);
                    if (leader.Teachers.Any())
                    {
                        var lastTeacher = leader.Teachers.LastOrDefault();
                        AddTeacherHeaders(worksheet);
                        foreach (var teacher in leader.Teachers)
                        {
                            var lastPersonCourses = teacher.PersonCourses.LastOrDefault();

                            AddTeacherRow(worksheet, teacher);
                            if (teacher.PersonCourses.Any())
                            {
                                AddCourseHeaders(worksheet);
                                foreach (var personCourse in teacher.PersonCourses)
                                {
                                    AddCourseRow(worksheet, personCourse);
                                    if (personCourse == lastPersonCourses && teacher != lastTeacher)
                                    {
                                        AddTeacherHeaders(worksheet);
                                    }
                                }
                            }
                            if (teacher == lastTeacher && leader != lastLeader)
                            {
                                AddLeaderHeaders(worksheet);
                            }
                        }
                    }
                }

                worksheet.Columns().AdjustToContents();
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            });
        }

        public async Task<byte[]> GenerateTeacherExcelAsync(List<Person> teachers)
        {
            return await Task.Run(() =>
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Teacher Data");

                row = 1;
                var lastTeacher = teachers.LastOrDefault();

                AddTeacherHeaders(worksheet);
                foreach (var teacher in teachers)
                {
                    var lastPersonCourses = teacher.PersonCourses.LastOrDefault();

                    AddTeacherRow(worksheet, teacher);
                    if (teacher.PersonCourses.Any())
                    {
                        AddCourseHeaders(worksheet);
                        foreach (var personCourse in teacher.PersonCourses)
                        {
                            AddCourseRow(worksheet, personCourse);
                            if (personCourse == lastPersonCourses && teacher != lastTeacher)
                            {
                                AddTeacherHeaders(worksheet);
                            }
                        }
                    }
                }


                worksheet.Columns().AdjustToContents();
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            });
        }

        private void AddBossRow(IXLWorksheet worksheet, EducationalBossViewModel boss)
        {
            worksheet.Cell(row, 2).Value = boss.Name;
            row++;
        }

        private void AddLeaderRow(IXLWorksheet worksheet, EducationalLeaderViewModel leader)
        {
            worksheet.Cell(row, 2).Value = leader.Name;
            worksheet.Cell(row, 4).Value = leader.Department?.Name;
            worksheet.Cell(row, 5).Value = leader.Location?.Name;
            row++;
        }

        private void AddTeacherRow(IXLWorksheet worksheet, Person teacher)
        {
            worksheet.Cell(row, 2).Value = teacher!.Name;
            worksheet.Cell(row, 3).Value = teacher!.Initials;
            worksheet.Cell(row, 4).Value = teacher!.Department?.Name;
            worksheet.Cell(row, 5).Value = teacher!.Location?.Name;
            worksheet.Cell(row, 6).Value = teacher.EndDate.ToShortDateString();
            row++;
        }

        private void AddCourseRow(IXLWorksheet worksheet, PersonCourse personCourse)
        {
            worksheet.Cell(row, 2).Value = row;
            worksheet.Cell(row, 2).Value = personCourse.Course?.Module?.Name;
            worksheet.Cell(row, 3).Value = personCourse.Course?.StartDate.ToShortDateString();
            worksheet.Cell(row, 4).Value = personCourse.Course?.EndDate.ToShortDateString();
            worksheet.Cell(row, 5).Value = personCourse.Course?.CourseType.ToString().Replace('_', ' ');
            worksheet.Cell(row, 7).Value = personCourse.Status.ToString().Replace('_', ' ');
            row++;
        }

        private void AddBossHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(row, 1).Value = "Uddannelseschef";
            worksheet.Cell(row, 2).Value = "Navn";

            ApplyHeaderStyling(worksheet);
            row++;
        }

        private void AddLeaderHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(row, 1).Value = "Uddannelsesleder";
            worksheet.Cell(row, 2).Value = "Navn";
            worksheet.Cell(row, 3).Value = "Afdeling";
            worksheet.Cell(row, 4).Value = "Lokation";

            ApplyHeaderStyling(worksheet);
            row++;
        }

        private void AddTeacherHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(row, 1).Value = "Underviser";
            worksheet.Cell(row, 2).Value = "Navn";
            worksheet.Cell(row, 3).Value = "Initialer";
            worksheet.Cell(row, 4).Value = "Afdeling";
            worksheet.Cell(row, 5).Value = "Lokation";
            worksheet.Cell(row, 6).Value = "Slut dato";

            ApplyHeaderStyling(worksheet);
            row++;
        }

        private void AddCourseHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(row, 1).Value = "Kursus";
            worksheet.Cell(row, 2).Value = "Navn";
            worksheet.Cell(row, 3).Value = "Kursus Nr.";
            worksheet.Cell(row, 4).Value = "Start dato";
            worksheet.Cell(row, 5).Value = "Slut dato";
            worksheet.Cell(row, 6).Value = "Kursus type";
            worksheet.Cell(row, 6).Value = "Status";

            ApplyHeaderStyling(worksheet);
            row++;
        }

        private void ApplyHeaderStyling(IXLWorksheet worksheet)
        {
            worksheet.Row(row).Style.Fill.BackgroundColor = XLColor.GrannySmithApple;
            worksheet.Cell(row, 1).Style.Font.Bold = true;
        }
    }
}
