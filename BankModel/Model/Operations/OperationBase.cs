using System;
using System.Collections.Generic;
using System.Text;
using Test.Persons.Models;

namespace ConsoleApp1.Operations
{
    public interface IOperation
    {
        IOperationModel CurrentModel { get; set; }
        IOperationUser CurrentUser { get; set; }
        bool Open();
        bool Close(int id);
    }

    abstract public class OperationBase : IOperation
    {
        public int Id { get; set; }
        protected IPerson CurrentPerson { get; set; }
        public abstract IOperationModel CurrentModel { get; set; }
        public abstract IOperationUser CurrentUser { get; set; }

        public abstract bool Close(int id);
        public abstract bool Open();

        public OperationBase(IOperationModel currentModel, IOperationUser currentUser, IPerson currentPerson)
        {
            CurrentModel = currentModel;
            CurrentUser = currentUser;
            CurrentPerson = currentPerson;
        }
    }
}
