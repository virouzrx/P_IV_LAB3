﻿using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            using var baza = new Kontekst();
            baza.Database.EnsureCreated(); 
            var nowyStudent = new Student() { Imie = "Jan", Nazwisko = "Kowalski" };
            baza.Students.Add(nowyStudent);
            baza.SaveChanges();
            var studenci = baza.Students.Where(x => x.Imie == "Jan");
            foreach (var item in studenci)
            {
                Console.WriteLine($"{item.Id}. {item.Nazwisko}");
            }
            var student = baza.Students.Where(x => x.Id == 2).First();
            student.Imie = "Piotr";
            baza.Students.Update(student);
            baza.SaveChanges();
            var stud1 = baza.Students.Where(x => x.Imie == "Piotr");
            baza.Remove(nowyStudent);
            baza.SaveChanges();
            Console.WriteLine();
            Console.WriteLine();
            using var baza2 = new NorthwindContext();
            var klient = baza2.Customers.Where(x => x.CustomerId == "BOLID");
            foreach (var item in klient)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Podaj firmę:");
            string firma;
            firma = Console.ReadLine();
            var customers = baza2.Customers.Where(x => x.CustomerId == firma).ToList();
            var order = baza2.Orders.Where(x => x.CustomerId == firma).ToList();
            var ordersDetails = baza2.OrderDetails.Select(x => new OrderDetails() ).ToList();
            var products = baza2.Products.Select(x => new { x.ProductId, x.ProductName }).OrderBy(x => x.ProductId)
                .ToList();
            var queryJoining = customers.Join(order, x => x.CustomerId, d => d.CustomerId,
                (customers, order) => new { CustId = customers.CustomerId, OrdId = order.OrderId }).ToList();
            foreach (var item in queryJoining)
            {
                Console.WriteLine(item.CustId+ " "+item.OrdId);
            }
            Console.WriteLine();
            List<Customers> cust = baza2.Customers.Where(x=>x.CustomerId==firma).ToList();
            List<Orders> ord = baza2.Orders.ToList();
            List<OrderDetails> ordDet = baza2.OrderDetails.ToList();
            List<Products> prod = baza2.Products.ToList();
            
            var querryToCreateSuperProductResultStat =
                from c in cust
                join o in ord on c.CustomerId equals o.CustomerId into tab1
                from t1 in tab1.ToList()
                join oD in ordDet on t1.OrderId equals oD.OrderId into tab2
                from t2 in tab2.ToList()
                join p in prod on t2.ProductId equals p.ProductId into finTab
                from x in finTab.ToList()
                select new ViewModel
                {
                    IdKlient = c.CustomerId,
                    IdProd = x.ProductId,
                    NazwaProd= x.ProductName
                };
            foreach (var item in querryToCreateSuperProductResultStat)
            {
                Console.WriteLine($"{item.IdKlient} - {item.IdProd} - {item.NazwaProd}");
            }
        }
        public class ViewModel
        {
            public string IdKlient { get; set; }
            public int IdProd { get; set; }
            public string NazwaProd { get; set; }
        }
    }
}
