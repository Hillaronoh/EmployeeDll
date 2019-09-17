using Library;
using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void Test()
        {
            var lines = File.ReadAllLines("../../../test.txt");
            Employees employees = new Employees(lines);
            Assert.Equal(3300, employees.SalaryBudget(lines, "Employee1"));
            Assert.Equal(1000, employees.SalaryBudget(lines, "Employee2"));
        }

        [Fact]
        public void Test1()
        {
            var lines = File.ReadAllLines("../../../test1.txt");
            Employees employees = new Employees(lines);
            Assert.Equal(2800, employees.SalaryBudget(lines, "Employee1"));
            Assert.Equal(1000, employees.SalaryBudget(lines, "Employee2"));

        }

        [Fact]
        public void Test2()
        {
            var lines = File.ReadAllLines("../../../test2.txt");
            Employees employees = new Employees(lines);
            Assert.Equal(2800, employees.SalaryBudget(lines, "Employee1"));
            Assert.Equal(1800, employees.SalaryBudget(lines, "Employee2"));
            Assert.Equal(500, employees.SalaryBudget(lines, "Employee3"));

        }

        [Fact]
        public void Test3()
        {
            var lines = File.ReadAllLines("../../../test3.txt");
            Employees employees = new Employees(lines);
            Assert.Equal(2800, employees.SalaryBudget(lines, "Employee1"));
            Assert.Equal(1300, employees.SalaryBudget(lines, "Employee2"));
            Assert.Equal(500, employees.SalaryBudget(lines, "Employee3"));
            Assert.Equal(0, employees.SalaryBudget(lines, "Employee6"));
        }

    }
}
