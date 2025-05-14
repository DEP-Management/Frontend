using ClosedXML.Excel;
using DEP_Blazor_WASM.Entities.Models;
using DEP_Blazor_WASM.Entities.ViewModels;
using DEP_Blazor_WASM.Services.Interfaces;

namespace DEP_Blazor_WASM.Services.Excel
{
    public class ExcelFromModule : IExcelFromModule
    {
        int row = 1;

        public async Task<byte[]> GenerateModuleExcelAsync(List<ModuleWithCourseViewModel> modules)
        {
            // Task.Run() is used to make it run Async becorse ClosedXML Dont have it
            return await Task.Run(() =>
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Modules Data");

                row = 1;
                var lastModule = modules.LastOrDefault();

                AddModuleHeaders(worksheet);

                foreach (var module in modules)
                {
                    AddModuleRow(worksheet, module);
                    if (module.Courses.Any())
                    {
                        var lastCourse = module.Courses.LastOrDefault();

                        AddCourseHeaders(worksheet);
                        foreach (var course in module.Courses)
                        {
                            AddCourseRow(worksheet, course);
                            if (course.PersonCourses.Any())
                            {
                                var lastPersonCourse = course.PersonCourses.LastOrDefault();

                                AddTeacherHeaders(worksheet);
                                foreach (var personCourse in course.PersonCourses)
                                {
                                    AddTeacherRow(worksheet, personCourse);
                                    if (personCourse == lastPersonCourse && course != lastCourse)
                                    {
                                        AddCourseHeaders(worksheet);
                                    }
                                }
                            }
                            if (course == lastCourse && module != lastModule)
                            {
                                AddModuleHeaders(worksheet);
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

        public async Task<byte[]> GenerateCourseExcelAsync(List<Course> courses)
        {
            return await Task.Run(() =>
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Courses Data");

                row = 1;
                var lastCourse = courses.LastOrDefault();

                AddCourseHeaders(worksheet);
                foreach (var course in courses)
                {
                    AddCourseRow(worksheet, course);
                    if (course.PersonCourses.Any())
                    {
                        var lastPersonCourse = course.PersonCourses.LastOrDefault();

                        AddTeacherHeaders(worksheet);
                        foreach (var personCourse in course.PersonCourses)
                        {
                            AddTeacherRow(worksheet, personCourse);
                            if (personCourse == lastPersonCourse && course != lastCourse)
                            {
                                AddCourseHeaders(worksheet);
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


        private void AddModuleRow(IXLWorksheet worksheet, ModuleWithCourseViewModel module)
        {
            worksheet.Cell(row, 2).Value = module.Name;
            row++;
        }

        private void AddCourseRow(IXLWorksheet worksheet, Course course)
        {
            worksheet.Cell(row, 2).Value = course.CourseNumber;
            worksheet.Cell(row, 3).Value = course.StartDate.ToShortDateString();
            worksheet.Cell(row, 4).Value = course.EndDate.ToShortDateString();
            worksheet.Cell(row, 5).Value = course.CourseType.ToString().Replace('_', ' ');
            row++;
        }

        private void AddTeacherRow(IXLWorksheet worksheet, PersonCourse personCourse)
        {
            worksheet.Cell(row, 2).Value = personCourse.Person!.Name;
            worksheet.Cell(row, 3).Value = personCourse.Person!.Initials;
            worksheet.Cell(row, 4).Value = personCourse.Person!.Department?.Name;
            worksheet.Cell(row, 5).Value = personCourse.Person!.Location?.Name;
            worksheet.Cell(row, 6).Value = personCourse.Person!.EndDate.ToShortDateString();
            worksheet.Cell(row, 7).Value = personCourse.Status.ToString().Replace('_', ' ');
            row++;
        }

        private void AddModuleHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(row, 1).Value = "Modul";
            worksheet.Cell(row, 2).Value = "Navn";

            ApplyHeaderStyling(worksheet);
            row++;
        }

        private void AddCourseHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(row, 1).Value = "Kursus";
            worksheet.Cell(row, 2).Value = "Kursus Nr.";
            worksheet.Cell(row, 3).Value = "Start dato";
            worksheet.Cell(row, 4).Value = "Slut dato";
            worksheet.Cell(row, 5).Value = "Kursus type";

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
            worksheet.Cell(row, 7).Value = "Kursus status";

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
