using BeyondNet.Ddd;

namespace BeyondNet.Ddd.valueObjects
{
    public struct PriceProps
    {
        public Symbol Symbol { get; set; }
        public double Amount { get; set; }
    }
    public class Price : ValueObject<PriceProps>
    {
        public Price(PriceProps value) : base(value)
        {
        }

        public static Price Create(Symbol symbol, double amount)
        {
            return new Price(new PriceProps { Symbol = symbol, Amount = amount });
        }

        public Symbol Symbol => GetValue().Symbol;
        public double Amount => GetValue().Amount;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue().Symbol;
            yield return GetValue().Amount;
        }

    }

    public class Symbol : Enumeration
    {
        public static Symbol USD = new Symbol(1, "DOLAR");
        public static Symbol EUR = new Symbol(2, "EURO");
        public static Symbol SOL = new Symbol(3, "SOL PERUANO");

        public Symbol(int id, string name) : base(id, name)
        {
        }
    }
}
