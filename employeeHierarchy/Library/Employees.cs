using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Library
{
    public class Employees
    {
        public Employees(string[] lines)
        {
            List<EmployeeProperties> empList = new List<EmployeeProperties>();
            int ceoCount = 0;
            string[] circularArrayOne = new string[2];
            string[] circularArrayTwo = new string[2];
            string employeeManager = "";
            var lns = lines.Select(a => a.Split('\t'));
            var csv = from line in lns
                      select (from piece in line
                              select piece);

            foreach (var n in csv)
            {
                var p = n.GetEnumerator();
                while (p.MoveNext())
                {
                    try
                    {
                        var data = p.Current.Split(',');
                        if (string.IsNullOrEmpty(data[0]))
                        {
                            Console.WriteLine("Oops! Employee cannot have empty Id");//all emloyees must be listed
                            continue;
                        }

                        if (string.IsNullOrEmpty(data[1]) && ceoCount < 1)
                        {
                            ceoCount++;
                        }
                        else if (string.IsNullOrEmpty(data[1]) && ceoCount == 1)
                        {
                            Console.WriteLine("Oops! There can only be one CEO");
                            continue;
                        }

                        int sal = 0;

                        if (Int32.TryParse(data[2], out sal))
                        {
                            empList.Add(new EmployeeProperties { Id = data[0], ManagerId = data[1], Salary = int.Parse(data[2]) });
                        }
                        else
                        {
                            Console.WriteLine("Oops! Salary must be a valid integer");
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }//end while
                p.Dispose();
            }//end for each
            //start outside loop
            var empListD = empList.GroupBy(s => s.Id)
                        .SelectMany(grp => grp.Skip(1)).ToList();

            if (!employeeHasOneLineManager(empList, empListD))
            {
                Console.WriteLine("Oops! Employee should report to one manager");
            }
            //check for circular occurances 
            foreach (var emp in empList)
            {
                employeeManager = emp.ManagerId;
                circularArrayOne[0] = emp.Id;
                circularArrayOne[1] = emp.ManagerId;

                foreach (var empIn in empList)
                {
                    circularArrayTwo[0] = emp.Id;
                    circularArrayTwo[1] = emp.ManagerId;

                    if (!checkForCircularOccurence(circularArrayOne, circularArrayTwo))
                    {
                        Console.WriteLine("Oops! Circular reference not allowed=> " + "Employee " + circularArrayOne[0] + "  managerId: " + circularArrayOne[1]);
                    }
                }
            }
            employeeManager = "";
            //end outside loop
        }

        public Boolean checkForCircularOccurence(string[] arr1, string[] arr2)
        {
            var equal = Enumerable.SequenceEqual(arr1, arr2);
            return equal;
        }

        public Boolean employeeHasOneLineManager(List<EmployeeProperties> empList, List<EmployeeProperties> empListD)
        {
            Boolean value = true; string empM = "";
            foreach (var empD in empListD)
            {
                empM = empD.ManagerId;
                foreach (var emp in empList)
                {
                    if (String.Equals(empD.Id, emp.Id))
                    {
                        if (!String.Equals(empM, emp.ManagerId))
                        {
                            value = false;
                        }
                    }
                }
                empM = "";
            }

            return value;
        }

        public int SalaryBudget(string[] lines, string manager)
        {
            List<EmployeeProperties> empListk = generateCustomEmployeeList(lines);
            int total = 0; int manager_salary = 0;

            foreach (var emp in empListk)
            {
                if (String.Equals(emp.ManagerId, manager))
                {
                    total += emp.Salary;
                }
                if (String.Equals(emp.Id, manager))
                {
                    manager_salary = emp.Salary;
                }

            }
            return total + manager_salary;
        }

        public List<EmployeeProperties> generateCustomEmployeeList(string[] lines)//repeated functionality...to be deprecated
        {
            List<EmployeeProperties> customEmpList = new List<EmployeeProperties>();
            var lns2 = lines.Select(a => a.Split('\t'));
            var csv2 = from line in lns2
                       select (from piece in line
                               select piece);
            foreach (var n in csv2)
            {
                var p = n.GetEnumerator();
                while (p.MoveNext())
                {
                    try
                    {
                        var data = p.Current.Split(',');
                        int sal = 0;
                        if (Int32.TryParse(data[2], out sal))
                        {
                            customEmpList.Add(new EmployeeProperties { Id = data[0], ManagerId = data[1], Salary = int.Parse(data[2]) });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                p.Dispose();
            }
            return customEmpList;
        }
    }

    public class EmployeeProperties
    {
        public string Id { get; set; }
        public int Salary { get; set; }
        public string ManagerId { get; set; }

    }

}
