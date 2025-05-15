namespace pr2.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdDepartment { get; set; }

    public int IdPosition { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;

    public virtual Position IdPositionNavigation { get; set; } = null!;
}
