public class Program
{
    public static void Main()
    {        
        Console.WriteLine(Operation.Counter);
        Console.WriteLine(Operation.Add(5, 10));
    }

    #region OOP
    public interface IEmployee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public void Work();
        public void Description();
    }
    public class Employee
    {
        protected string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public Employee(string Name)
        {
            this.Name = Name;
        }

        public void Work()
        {
            Console.WriteLine($"{this.Name} is working!");
        }
        public virtual void Description()
        {
            Console.WriteLine("This class is for all employees!");
        }
    }
    public class Developer : Employee
    {
        public string Project { get; set; }

        public Developer(string Name) : base(Name)
        {
        }

        public override void Description()
        {

        }
    }
    public class Manager : Employee
    {
        public int EmployeesCount { get; set; }

        public Manager(string Name) : base(Name)
        {
        }
    }
    public abstract class Shapes
    {
        double Radius { get; set; } = 5.5;
        public abstract double GetArea();
    }
    public class Circle : Shapes
    {
        double Radius { get; set; } = 5.5;
        public override double GetArea()
        {
            return Radius * Radius * Math.PI;
        }
    }
    #endregion

    #region Static
     /*
     * Static can be used by the following:
     * 1. Methods
     * 2. Properties
     * 3. Classes
     * 4. Constructors
     * 
     * When using static on a class, every method and property needs to be
     * static and cannot be accessed from an instance object since you
     * cannot create an object instance. Plus, cannot be inherited by a
     * child class.
     */

    public static class Operation
    {
        public static int Counter { get; set; }
        static Operation()
        {
            Counter++;
        }
        public static int Add(int x, int y) => x + y;
    }
    #endregion

    #region Exercise Methods
    public static void Exercise1()
    {
        try
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine() ?? string.Empty);

            Console.Write("Enter your salary: ");
            decimal salary = decimal.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine($"\n\nHello {name}!");
            Console.WriteLine($"Your age: {age}!");
            Console.WriteLine($"Your monthly salary: {salary}!");
            Console.WriteLine($"Your yearly salary: {salary * 12}!");
            Console.WriteLine(salary >= 600.0m ? "Your salary is above average" : "Your salary is below average");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Wrong age or salary format!");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Salary input is too long!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void Exercise2()
    {
        try
        {
            Console.Write("Enter your start number: ");
            int startNum = int.Parse(Console.ReadLine() ?? string.Empty);

            Console.Write("Enter your end number: ");
            int endNum = int.Parse(Console.ReadLine() ?? string.Empty);

            int counter = 0;
            EvenCounter(ref counter, startNum, endNum);

            Console.WriteLine($"There are {counter} even numbers between {startNum} and {endNum}.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format for start or end value!");
        }
        catch (OverflowException)
        {
            Console.WriteLine("The value for start or end value is too long!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void EvenCounter(ref int counter, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0) counter++;
        }
    }
    #endregion
}