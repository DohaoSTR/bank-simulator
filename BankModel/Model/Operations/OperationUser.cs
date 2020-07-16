using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Operations
{
    public interface IOperationUser
    {
        public IOperationModel CurrentModel { get; set; }
    }

    abstract public class OperationUser : IOperationUser
    {
        public abstract IOperationModel CurrentModel { get; set; }

        public OperationUser(IOperationModel currentModel)
        {
            CurrentModel = currentModel;
        }
    }
}
