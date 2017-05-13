using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;

namespace UFConsole
{
    public interface IPerson {

        string Occupation();
    }

    public interface IVehicle
    {
        string Type();
    }


    public class FireTruck : IVehicle
    {
        public string Type()
        {

            return "FireTruck";
        }

    }

    //Constructor Injection
    public class Fireman : IPerson {
 
        IVehicle _vechicle;

        public Fireman(IVehicle vechicle) {

            _vechicle = vechicle;
        }

        public string Occupation() {

            return "I am a Fireman " + _vechicle.Type(); ;
        }

    }

    //Property Injection
    public class Doctor : IPerson
    {
        [Dependency]
        public IVehicle _vechicle { get; set; }

        public string Occupation()
        {

            return "I am a Doctor " + _vechicle.Type(); ;
        }

    }

    //Method Injection
    public class Management
    {
        IVehicle _vehicle;
        public Management(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        IPerson _person;

        [InjectionMethod]
        public void DoingManagement(IPerson person)
        {
            _person = person;
            
        }

        public string Combine() {

            return _person.Occupation() + " " + _vehicle.Type();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.LoadConfiguration();

            container.RegisterType<IVehicle, FireTruck>();
            container.RegisterType<IPerson, Doctor>();
            container.RegisterType<Management>();

            var person = container.Resolve<IPerson>();
            var management = container.Resolve<Management>();

            Console.WriteLine(person.Occupation());
            Console.WriteLine(management.Combine());
            Console.ReadLine();
        }
    }
}
