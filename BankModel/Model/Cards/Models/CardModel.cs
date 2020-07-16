using System;

namespace Test.Cards.Models
{
    public interface ICardModel
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal AnnualFee { get; set; }
        decimal InitialFee { get; set; }
        int ValidityYears { get; set; }
    }
    abstract public class CardModel : ICardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AnnualFee { get; set; }
        public decimal InitialFee { get; set; }
        public int ValidityYears { get; set; }

        protected CardModel(string name)
        {
            Name = name;         
        }
    }
}
