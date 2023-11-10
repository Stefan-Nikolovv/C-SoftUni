
using Entity_Practice.Data;
using Entity_Practice.Models;

namespace SoftUni
{
    public class Program
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            //Console.WriteLine(GetEmployeesFullInformation(context));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
            //Console.WriteLine(AddNewAddressToEmployee(context));
            Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
        }

        //public static string GetEmployeesFullInformation(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //                            .Select(e => new
        //                            {
        //                                e.EmployeeId,
        //                                e.FirstName,
        //                                e.LastName,
        //                                e.MiddleName,
        //                                e.JobTitle,
        //                                e.Salary
        //                            })
        //                              .OrderBy(e => e.EmployeeId)
        //                              .ToList();

        //    string result = string.Join(Environment.NewLine, employees
        //                                                      .Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}"));
        //    return result;
        //}

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

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address addres = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4,
            };
            var employee = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            if (employee != null)
            {
                employee.Address = addres;
                context.SaveChanges();
            }

            var employees = context.Employees
                                             .Select(e => new
                                             {
                                                 e.AddressId,
                                                 e.Address.AddressText
                                             })
                                             .OrderByDescending(e => e.AddressId)
                                             .Take(10)
                                             .ToList();

            return string.Join(Environment.NewLine, employees
                                                             .Select(e => $"{e.AddressText}"));

        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var emoployees = context.Employees
                                              .Where(e => e.FirstName.StartsWith("sa"))
                                              .Select(e => new
                                              {
                                                  e.FirstName,
                                                  e.LastName,
                                                  e.JobTitle,
                                                  e.Salary
                                              })
                                              .OrderBy(e => e.FirstName)
                                              .ThenBy(e => e.LastName)
                                              .ToList();
            return string.Join(Environment.NewLine, emoployees.Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2}) "));
        }
    }
}
