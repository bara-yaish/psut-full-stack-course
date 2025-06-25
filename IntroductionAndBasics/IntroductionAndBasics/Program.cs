namespace IntroductionAndBasics;
public class Program
{
    public static void Main()
    {
        ArrayExample();
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
            Console.WriteLine($"{Name} is working!");
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

    #region Array
    public static void ArrayExample()
    {
        int[] numbersFixed = new int[5];
        int[] numbersFilled = { 1, 2, 3, 4, 5 };
        int[] numbersUnsorted = { 5, 2, 1, 3, 4 };
        int[] numbersFixedFilled = new int[5] { 1, 2, 3, 4, 5 };

        Array.Sort(numbersUnsorted);
        foreach (var item in numbersUnsorted) Console.WriteLine(item);
    }
    #endregion

    #region List
    public static void ListExample<T>(T genericType)
    {
        var listEmpty = new List<int>();
        var listFilled = new List<int>() { 1, 2, 3 };

        listFilled.Remove(0);
        listFilled.RemoveAt(0);
        listFilled.Clear();
    }
    #endregion

    #region Exercises
    // Execution Methods
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
    public static void Exercise3()
    {
        var suv = new SUV("Bara' Yaish", "Ford Expedition", 72);
        var motorcycle = new Motorcycle("Omar Yaish", "Suzuki 1000cc", 12);

        Console.WriteLine($"Vehicle Counter: {Vehicle.VehicleCount}");
        Console.WriteLine();

        suv.ShowInfo();
        Console.WriteLine();
        motorcycle.ShowInfo();
    }

    // Helper Methods
    public static void EvenCounter(ref int counter, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0) counter++;
        }
    }

    // Classes
    public interface IInforDisplay
    {
        public void ShowInfo(); // display vehicle info
    }
    public class Vehicle
    {
        protected decimal BasePrice { get; set; } = 20.0m;
        public static int VehicleCount { get; set; } = 0;
        public string OwnerName { get; set; }
        public string ModelName { get; set; }
        public int Hours { get; set; }

        public Vehicle(string OwnerName, string ModelName, int Hours)
        {
            this.OwnerName = OwnerName;
            this.ModelName = ModelName;
            this.Hours = Hours;

            VehicleCount++;
        }

        public virtual decimal CalculateTotalPrice() => BasePrice * Hours;
    }
    public class SUV : Vehicle, IInforDisplay
    {
        public SUV(string OwnerName, string ModelName, int Hours)
            : base(OwnerName, ModelName, Hours) { }

        public override decimal CalculateTotalPrice() => (BasePrice + 10) * Hours;
        public void ShowInfo()
        {
            Console.WriteLine($"Owner: {OwnerName}");
            Console.WriteLine($"Type: {GetType().Name}");
            Console.WriteLine($"Model: {ModelName}");
            Console.WriteLine($"Hours: {Hours}");
            Console.WriteLine($"Total Fee: {CalculateTotalPrice()} JOD");
        }
    }
    public class Motorcycle : Vehicle, IInforDisplay
    {
        public Motorcycle(string OwnerName, string ModelName, int Hours)
            : base(OwnerName, ModelName, Hours) { }

        public override decimal CalculateTotalPrice() => (BasePrice - 5) * Hours;
        public void ShowInfo()
        {
            Console.WriteLine($"Owner: {OwnerName}");
            Console.WriteLine($"Type: {GetType().Name}");
            Console.WriteLine($"Model: {ModelName}");
            Console.WriteLine($"Hours: {Hours}");
            Console.WriteLine($"Total Fee: {CalculateTotalPrice()} JOD");
        }
    }
    #endregion
}