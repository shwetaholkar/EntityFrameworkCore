using EntityFrameworkCore.DataAccess;
using EntityFrameworkCore.DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            using (var context = new SampleStoreContext())
            {
                var persons = context.Persons.ToList();
                Console.WriteLine("-----------------Person--------------");
                foreach (var person in persons)
                {
                    Console.WriteLine($"{person.person_id}, {person.first_name}, {person.last_name}, {person.dob}");
                }
                Console.WriteLine("-------------------------------------");
            }
        }
        public void Add()
        {
            var context = new SampleStoreContext();
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
            var personIdText = Console.ReadLine();
            int personIdToBeupdated = int.Parse(personIdText);
            using var context = new SampleStoreContext();

            var person = context.Persons.FirstOrDefault(x => x.person_id == personIdToBeupdated);
            if (person == null)
            {
                Console.WriteLine($"Person with Id = {personIdToBeupdated} not Found");
                return;
            }

            person.first_name = person.first_name + "00";
            person.last_name = person.last_name + "00";
            person.dob = DateTime.Now;

            context.Persons.Update(person);
            context.SaveChanges();
        }
        public void SelectWithSP()
        {
            //var sqlParameterPersonId = new SqlParameter
            //{
            //    ParameterName = "@person_id",
            //    SqlDbType=System.Data.SqlDbType.Int,
            //    Direction=System.Data.ParameterDirection.Input,
            //    Value=0

            //};

            var sqlParameterPersonId = new SqlParameter("@person_id", System.Data.SqlDbType.Int);
            sqlParameterPersonId.Value = 0;

            using var context = new SampleStoreContext();
            var person = context.Persons.FromSqlRaw("[dbo].[GetAllPersons] @person_id ", sqlParameterPersonId);

            Console.WriteLine("------------------Person----------------");
            foreach (var p in person)
            {
                Console.WriteLine($"{p.person_id} , {p.first_name} , {p.last_name} , {p.dob}");

            }
            Console.WriteLine("------------------------------------------");


        }

        //[GetBrandProductInfo]
        public void SelectWithCustomEntity()
        {
            //@minPrice
            var sqlParameterMinPrice = new SqlParameter("@minPrice", System.Data.SqlDbType.Decimal);
            sqlParameterMinPrice.Value = 400;

            using var context = new SampleStoreContext();
            var products = context.Set<BrandProductInfoResult>().FromSqlRaw("[dbo].[GetBrandProductInfo] @minPrice", sqlParameterMinPrice).ToList();

            Console.WriteLine("------------------Person----------------");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.product_name} , {p.brand_name} , {p.category_name} , {p.list_price}");
            }
            Console.WriteLine("-------------------------------------------");
        }

        //Loading related data
        public void SelectRelatedData()
        {
            using var context = new SampleStoreContext();

            //select * from products
            var product1=context.Products.ToList();

            //select product_id from products
            var product2 = context.Products.Select(p => p.product_id).ToList();

            //select * from product where brand_id=5
            var product3=context.Products.Where(p => p.brand_id==5).ToList();

            //select * from product order by category_id
            var product4 = context.Products.OrderBy(p => p.category_id).ToList();

           //select product_id , product_name from production.products 
           //where category_id =1 and brand_id =3
           //order by product_name



            var product5 = context.Products
                .Where(p => p.brand_id == 3 && p.category_id == 1)
                .OrderBy(p => p.product_name)
                .Select(p => new ProductResult {ProductId = p.product_id, ProductName = p.product_name}).ToList();


            var products = context.Products.Include("brand").Include("category").ToList();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.product_name} , {p.brand_id} , {p.brand.brand_name} , {p.category.category_name}");

            }
            Console.WriteLine("----------------------------------------------");
        }

    }

    public class ProductResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
