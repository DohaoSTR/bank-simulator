using Test.Persons.Models;

namespace ConsoleApp1.Operations.Credits.Models
{
    public interface ICredit : IOperation
    {

    }

    abstract public class CreditBase : OperationBase, ICredit
    {
        public CreditBase(ICreditModel currentModel, ICreditUser currentUserData, IPerson currentPerson) : base(currentModel, currentUserData, currentPerson)
        {

        }
    }
}
