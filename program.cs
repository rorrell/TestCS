using System;
using System.Linq;
using System.Collections.Generic;

namespace Test
{

    public abstract class Animal
    {
        public string Name { get; set; }

        public Animal(string animalName)
        {
            Name = animalName;
        }

        public abstract string speak();
    }

    public class Cat : Animal
    {

        public Cat(string catName) : base(catName) { }

        public override string speak()
        {
            return "meow";
        }
    }

    public class Dog : Animal
    {
        public Dog(string dogName) : base(dogName) { }

        public override string speak()
        {
            return "woof";
        }
    }

    public class Cow : Animal
    {
        public Cow(string cowName) : base(cowName) { }

        public override string speak()
        {
            return "moo";
        }
    }

    public class Horse : Animal
    {
        public Horse(string horseName) : base(horseName) { }

        public override string speak()
        {
            return "neigh";
        }
    }

    public class Duck : Animal
    {
        public Duck(string duckName) : base(duckName) { }

        public override string speak()
        {
            return "quack";
        }
    }

    enum AnimalType
    {
        Cat, Dog, Cow, Horse, Duck
    }

    public class AnimalFactory
    {
        public static Animal createAnimal(string type, string name)
        {
            Animal animal = null;
            AnimalType aType;
            if (Enum.TryParse(type, true, out aType) &&
                Enum.IsDefined(typeof(AnimalType), aType))
            {
                switch (aType)
                {
                    case AnimalType.Cat:
                        animal = new Cat(name);
                        break;
                    case AnimalType.Dog:
                        animal = new Dog(name);
                        break;
                    case AnimalType.Cow:
                        animal = new Cow(name);
                        break;
                    case AnimalType.Horse:
                        animal = new Horse(name);
                        break;
                    case AnimalType.Duck:
                        animal = new Duck(name);
                        break;
                }
            }
            return animal;
        }
    }

    public class Program
    {

        public static void Main(string[] args)
        {

            List<Type> animals = typeof(Animal).Assembly.GetTypes()
                .Where(t => t.BaseType == typeof(Animal)).ToList();
            string response;

            do
            {
                printMenu(animals);
                response = Console.ReadLine();
                    Animal animal;
                    animal = AnimalFactory.createAnimal(response, "");
                    if (animal == null)
                    {
                        Console.WriteLine("An error was encountered.  Please try again.");
                        continue;
                    }
                    Console.WriteLine("Please enter a name for your animal:");
                    animal.Name = Console.ReadLine();
                    animalGoes(animal);
                Console.WriteLine("Play again? y/n");
                response = Console.ReadLine();
            } while (response.Equals("y", StringComparison.OrdinalIgnoreCase));
            return;
        }

        public static void animalGoes(Animal animal)
        {
            Console.WriteLine("{0} goes {1}", animal.Name, animal.speak());
        }

        public static void printMenu(List<Type> animals)
        {
            Console.WriteLine("Enter one of the following animals:");
            foreach (Type item in animals)
            {
                Console.WriteLine("{0}", item.Name);
            }
        }
    }

}