using Microsoft.EntityFrameworkCore;
using pr2;
using pr2.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            //создаём экземпляр контекста бд для работы с данными
            using (DbCompanyContext context = new DbCompanyContext())
            {
                Console.WriteLine("Загрузка данных с помощью Include():");
                LoadWithInclude(context);

                Console.WriteLine("\n------------------------------\n");

                Console.WriteLine("Загрузка данных с помощью Load():");
                LoadWithLoad(context);
            }
        }

        //связанные данные (отдел и должность) загружаются сразу вместе с сотрудниками одним запросом.
        static void LoadWithInclude(DbCompanyContext context)
        {
            // Загружаем сотрудников в список employees
            List<Employee> employees = context.Employees
                .Include(employee => employee.Department) //загружаем для каждого сотрудника отедл
                .Include(employee => employee.Position) //загружаем для каждого сотрудника должность
                .ToList(); //получаем список сотрудников и их связй

            //проходимся по каждому сотруднику
            foreach (Employee employee in employees)
            {
                //получаем имя, отдел, должность
                string employeeName = employee.Name;
                string departmentName = employee.Department.Name;
                string positionName = employee.Position.Name;

                Console.WriteLine($"Работник: {employeeName}");
                Console.WriteLine($"  Отдел: {departmentName}");
                Console.WriteLine($"  Должность: {positionName}");
                Console.WriteLine();
            }
        }

        //сначала загружаются все сотрудники, а затем отдельно загружаются все отделы и должности
        static void LoadWithLoad(DbCompanyContext context)
        {
            // Загружаем всех сотрудников в список employees из бд
            List<Employee> employees = context.Employees.ToList();

            // Загружаем все отделы и должности
            context.Departments.Load();
            context.Positions.Load();

            foreach (Employee employee in employees)
            {
                // Свойства навигации доступны благодаря загрузке
                string employeeName = employee.Name;
                string departmentName = employee.Department.Name;
                string positionName = employee.Position.Name;

                Console.WriteLine($"Работник: {employeeName}");
                Console.WriteLine($"  Отдел: {departmentName}");
                Console.WriteLine($"  Должность: {positionName}");
                Console.WriteLine();
            }
        }
    }
}