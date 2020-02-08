using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharp_IO_Exercises
{
    class Program
    {

        [System.Serializable]
        public class SuperHero
        {
            public string heroName;
            public string realName;
            public double age;
            public double height;
            public double weight;
            public string powers;
            
            //Constructor to create from values
            public SuperHero(string _heroName, string _realName, double _age, double _height, double _weight, string _powers)
            {
                heroName = _heroName;
                realName = _realName;
                age = _age;
                height = _height;
                weight = _weight;
                powers = _powers;
            }

            //Constructor for blank
            public SuperHero()
            {
            }

            public string ToCSV()
            {
                return $"{heroName},{realName},{age},{height},{weight},{powers}";
            }

            public static SuperHero FromCSV(string csv)
            {
                SuperHero temphero = new SuperHero();

                string[] values = csv.Split(',');

                temphero.heroName = values[0];
                temphero.realName = values[1];
                temphero.age = Convert.ToDouble(values[2]);
                temphero.height = Convert.ToDouble(values[3]);
                temphero.weight = Convert.ToDouble(values[4]);
                temphero.powers = values[5];

                return temphero;
            }
        }

        static void Main(string[] args)
        {
            SuperHero hero = new SuperHero();   //make empty hero info cache

            while (true)
            {

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. New Hero File");
                Console.WriteLine("2. Load Hero File");
                Console.WriteLine("3. Save File");
                Console.WriteLine("4. Display Loaded File");

                string input = Console.ReadLine();
                int command = Convert.ToInt32(input);
                if (command == 1 || command == 2 || command == 3 || command == 4)
                {
                    switch (command)
                    {
                        case 1:
                            Console.Write("\nEnter the superhero's \'super\' name: ");
                            hero.heroName = Console.ReadLine();
                            Console.Clear();
                            Console.Write("\nEnter the superhero's \'real\' name: ");
                            hero.realName = Console.ReadLine();
                            Console.Clear();
                            Console.Write("\nEnter the superhero's age: ");
                            hero.age = Convert.ToDouble(Console.ReadLine());
                            Console.Clear();
                            Console.Write("\nEnter the superhero's height in cm: ");
                            hero.height = Convert.ToDouble(Console.ReadLine());
                            Console.Clear();
                            Console.Write("\nEnter the superhero's weight in kg: ");
                            hero.weight = Convert.ToDouble(Console.ReadLine());
                            Console.Clear();
                            Console.Write("\nEnter the superhero's \'powers\': ");
                            hero.powers = Console.ReadLine();
                            Console.Clear();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("Enter hero FileName (The hero's Real Name)");
                            string filename = Console.ReadLine();
                            hero = Load(filename);
                            Console.WriteLine("File Loaded Successfully");
                            PressAnyKeyToContinue(false, true);

                            break;

                        case 3:
                            Save(hero);
                            Console.WriteLine("Saved Successfully");
                            PressAnyKeyToContinue(true, true);
                            Console.Clear();
                            break;

                        case 4:
                            DisplayFile(hero);

                            break;
                    }
                }
            }

        }

        static void Save(SuperHero _heroData)
        {
            //BinaryFormatter formatter = new BinaryFormatter();  //Formatter for serialisation
            //string fileName = _heroData.realName + ".hero";
            //string saveFileFullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            //FileStream stream = new FileStream(saveFileFullPath, FileMode.Create);

            //formatter.Serialize(stream, hero);

            string fileName = _heroData.realName + ".csv";
            string saveFileFullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName);

            File.WriteAllText(saveFileFullPath, _heroData.ToCSV());
        }

        static SuperHero Load(string _fileName)
        {
            //BinaryFormatter formatter = new BinaryFormatter();  //Formatter for serialisation
            //_fileName += ".hero";
            //string saveFileFullPath = Path.Combine(Directory.GetCurrentDirectory(), _fileName);

            //FileStream stream = new FileStream(saveFileFullPath, FileMode.Open);

            //return (SuperHero)formatter.Deserialize(stream);

            _fileName += ".csv";
            string saveFileFullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _fileName);
            string csvdata = File.ReadAllText(saveFileFullPath); 
            return SuperHero.FromCSV(csvdata);
        }

        public static void PressAnyKeyToContinue(bool clearfirst, bool clearafter)
        {
            if (clearfirst)
            {
                Console.Clear(); 
            }
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
            if (clearafter)
            {
                Console.Clear();
            }
        }

        public static void DisplayFile(SuperHero hero)
        {
            if (hero.heroName != null)
            {
                Console.Clear();
                Console.WriteLine($"######################\nSUPERHERO: \"{hero.heroName}\"\n######################\n##Real Name: {hero.realName}\n##Age: {hero.age} years old\n##Height: {hero.height} cm\n##Weight: {hero.weight} kg\n##Powers: {hero.powers}\n######################");
                PressAnyKeyToContinue(false, true);
            }
        }
    }
}
