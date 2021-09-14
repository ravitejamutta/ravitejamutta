using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmark_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<MemoryBenchmarkerDemo>();
            //BenchmarkRunner.Run<StructVsClass>();
            BenchmarkRunner.Run<StructVsClassPlatoon>();
            #region allocations
            //UnderstandingAllocations understandingAllocations = new UnderstandingAllocations();
            //understandingAllocations.CheckClassReference();
            //understandingAllocations.CheckCheckStructReference();
            //Console.ReadKey(); 
            #endregion
        }
    }
    //public class UnderstandingAllocations
    //{

    //    public void CheckClassReference()
    //    {
    //        EmployeeClass employeeClass = new EmployeeClass(1, "Ravi");
    //        UpdateClassObject(employeeClass);
    //        Console.WriteLine("Employee Name with class:" + employeeClass.name);

    //    }
    //    public void UpdateClassObject(EmployeeClass employeeClass)
    //    {
    //        employeeClass.name = "Ravi new";
    //    }

    //    public void CheckCheckStructReference()
    //    {
    //        EmployeeStruct employeeStruct = new EmployeeStruct(1, "Ravi");
    //        UpdateStructObject(employeeStruct);
    //        Console.WriteLine("Employee Name with struct:" + employeeStruct.name);
    //    }

    //    public void UpdateStructObject(EmployeeStruct employeeStruct)
    //    {
    //        employeeStruct.name = "Ravi new";
    //    }
    //}

    [MemoryDiagnoser]
    [RankColumn]
    public class MemoryBenchmarkerDemo
    {
        int NumberOfItems = 100000;
        [Benchmark]
        public string ConcatStringsUsingStringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumberOfItems; i++)
            {
                sb.Append("Hello World!" + i);
            }
            return sb.ToString();
        }


        [Benchmark]
        public string ConcatStringsUsingGenericList()
        {
            var list = new List<string>(NumberOfItems);
            for (int i = 0; i < NumberOfItems; i++)
            {
                list.Add("Hello World!" + i);
            }
            return list.ToString();
        }
    }

    public struct EmployeeStruct
    {
        public int id;
        public string name;
        public EmployeeStruct(int id,string name)
        {
            this.id = id;
            this.name = name;

        }
    }
    public class EmployeeClass
    {
        public int id;
        public string name;
        public EmployeeClass(int id, string name)
        {
            this.id = id;
            this.name = name;

        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    public class StructVsClass
    {
        [Benchmark]
        public void CreateClassObj()
        {
            EmployeeClass employeeClass = new EmployeeClass(1, "Ravi");
        }

        [Benchmark]
        public void CreateStructObj()
        {
            EmployeeStruct employeeStruct = new EmployeeStruct(1, "Ravi");
        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    public class StructVsClassPlatoon
    {
        [Benchmark]
        public void CreateAPIResponseObj()
        {
            EmployeeClass employeeClass = new EmployeeClass(1, "Ravi");
            APIResponse<EmployeeClass> response = new APIResponse<EmployeeClass>()
            {
                Data = employeeClass,
                ErrorMessage = string.Empty,
                Token = string.Empty
            };
        }

        [Benchmark]
        public void CreateAPIResponseStrcutObj()
        {
            EmployeeStruct employeeStruct = new EmployeeStruct(1, "Ravi");
            APIResponseStruct<EmployeeStruct> response = new APIResponseStruct<EmployeeStruct>()
            {
                Data = employeeStruct,
                ErrorMessage = string.Empty,
                Token = string.Empty
            };

        }
    }

    public class APIResponse<T>
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }

    public struct APIResponseStruct<T>
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
