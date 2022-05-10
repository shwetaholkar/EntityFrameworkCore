using EntityFrameworkCore.DataAccess;
using EntityFrameworkCore.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore
{
    internal class SampleStoreTest
    {
        public void Select()
        {
            using(var context=new SampleStoreContext())
            {
                var persons=context.Persons.ToList();
                Console.WriteLine("-----------------Person--------------");
                foreach(var person in persons)
                {
                    Console.WriteLine($"{person.person_id}, {person.first_name}, {person.last_name}, {person.dob}");
                }
                Console.WriteLine("-------------------------------------");
            }
        }
        public void Add()
        {
            var context=new SampleStoreContext();
            var person = new Person
            {
                first_name = "Sonali ",
                last_name = "Chaudhari",
                dob = new DateTime(1999, 12, 04)
            };
            context.Persons.Add(person);//add model object in Dbset of context
            context.SaveChanges();

            context.Dispose();
          

        }
        public void Delete()
        {
            Console.WriteLine("Enter id to be deleted ");
            var personIdText = Console.ReadLine();
            int personIdToBeupdated = int.Parse(personIdText);
            using var context = new SampleStoreContext();

            var person = context.Persons.FirstOrDefault(x => x.person_id == personIdToBeupdated);
            if (person == null)
            {
                Console.WriteLine($"Person with Id = {personIdToBeupdated} not Found");
                return;
            }

            context.Persons.Remove(person);
            context.SaveChanges();
        }
        public void Update()
        {
            Console.WriteLine("Enter id to be updated ");
            var personIdText= Console.ReadLine();
            int personIdToBeupdated=int.Parse(personIdText);
            using var context=new SampleStoreContext();

            var person=context.Persons.FirstOrDefault(x => x.person_id == personIdToBeupdated);
            if(person==null)
            {
                Console.WriteLine($"Person with Id = {personIdToBeupdated} not Found");
                return;
            }

            person.first_name = person.first_name + "00";
            person.last_name = person.last_name + "00";
            person.dob=DateTime.Now;

            context.Persons.Update(person);
            context.SaveChanges();
        }

    }
}
