class Employee
{
    private string surname;
    private string department;
    private double[] salaries;

    public Employee(string surname, string department, double[] salaries)
    {
        this.surname = surname;
        this.department = department;
        this.salaries = salaries;
    }

    public string GetSurname()
    {
        return surname;
    }

    public string GetDepartment()
    {
        return department;
    }

    public double GetBigSalary()
    {
        double sum = 0;
        for(int i = 0; i < salaries.Length; i++)
        {
            sum += salaries[i];
        }

        return sum / salaries.Length;
    }

    public double GetSmallSalary()
    {
        double min = salaries[0];
        for(int i = 1; i < salaries.Length; i++)
        {
            if(salaries[i] < min)
            {
                min = salaries[i];
            }
        }

        return min;
    }

    public double this[int index]
    {
      get { return salaries[index]; }
     set { salaries[index] = value; }
    }

    public void Print()
    {
        Console.WriteLine("Surname:" + surname + "Department" + department +  "Average salary" + GetBigSalary());
    }
}

