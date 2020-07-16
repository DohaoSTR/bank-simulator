using System;
using System.Collections.Generic;
using System.Text;
using Test.Persons.Models;

namespace ConsoleApp1.Operations
{
    public interface IOperationModel
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public abstract class OperationModel : IOperationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OperationModel(string name)
        {
            Name = name;
        }
    }
}
