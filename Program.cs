using System;

namespace ResistanceCalculator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите первое сопротивление (Ом):");
            double R1 = GetValidResistance();

            Console.WriteLine("Введите второе сопротивление (Ом):");
            double R2 = GetValidResistance();

            IResistanceStrategy strategy;
            Console.WriteLine("Выберите тип соединения (1 - последовательное, 2 - параллельное):");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                strategy = new SeriesResistanceStrategy();
            }
            else
            {
                strategy = new ParallelResistanceStrategy();
            }

            double resistance = strategy.CalculateResistance(R1, R2);
            Console.WriteLine($"Сопротивление в {strategy.GetType().Name} соединении: {resistance} Ом");

            Console.ReadLine();
        }

        public static double GetValidResistance(string prompt = "")
        {
            double resistance;
            while (!double.TryParse(Console.ReadLine(), out resistance) || resistance <= 0)
            {
                Console.WriteLine($"Некорректное значение сопротивления. {prompt}");
            }
            return resistance;
        }
    }

    // Интерфейс стратегии для расчета сопротивления
    interface IResistanceStrategy
    {
        double CalculateResistance(double R1, double R2);
    }

    // Стратегия для последовательного соединения
    class SeriesResistanceStrategy : IResistanceStrategy
    {
        public double CalculateResistance(double R1, double R2)
        {
            return R1 + R2;
        }
    }

    // Стратегия для параллельного соединения
    class ParallelResistanceStrategy : IResistanceStrategy
    {
        public double CalculateResistance(double R1, double R2)
        {
            double oneOverR = (1 / R1) + (1 / R2);
            return 1 / oneOverR;
        }
    }
}