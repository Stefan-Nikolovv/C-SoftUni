
using Entity_Practice.Data;
namespace SoftUni
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployeesFullInformation(context));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                                    .Select(e => new
                                    {
                                        e.EmployeeId,
                                        e.FirstName,
                                        e.LastName,
                                        e.MiddleName,
                                        e.JobTitle,
                                        e.Salary
                                    })
                                      .OrderBy(e => e.EmployeeId)
                                      .ToList();

            string result = string.Join(Environment.NewLine, employees
                                                              .Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}"));
            return result;
        }

        //public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        //{
        //    var employees = context.Employees.Select(e =>
        //   new {
        //        e.FirstName,
        //        e.Salary
        //        })
        //        .Where(s => s.Salary > 50000)
        //        .OrderBy(n => n.FirstName)
        //        .ToList();

        //    string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} - {e.Salary:f2}"));


        //    return result;
        //}

        //public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //                                    .Where(d => d.Department.Name == "Research and Development")
        //                                     .Select(e => new
        //                                     {
        //                                         e.FirstName ,
        //                                         e.LastName ,
        //                                        DepartmentName =  e.Department.Name,
        //                                         e.Salary,
        //                                     })
        //                                     .OrderBy(s => s.Salary)
        //                                     .ThenByDescending(f => f.FirstName)
        //                                     .ToList();
        //    return string.Join(Environment.NewLine, employees.Select(e =>
        //    $"{e.FirstName} {e.LastName} from {e.DepartmentName} - {e.Salary:f2}"));                               
        //}
    }
}
