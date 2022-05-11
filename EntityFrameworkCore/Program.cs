// See https://aka.ms/new-console-template for more information
using EntityFrameworkCore;
using EntityFrameworkCore.DataAccess;

Console.WriteLine("Hello, World!");
SampleStoreTest s1 = new SampleStoreTest();

//s1.Select();
//s1.Add();
//s1.Update();
//s1.Delete();
//s1.SelectWithSP();
//s1.SelectWithCustomEntity();
s1.SelectRelatedData();


