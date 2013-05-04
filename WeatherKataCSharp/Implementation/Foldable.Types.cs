using System;

namespace WeatherKata
{
    public struct FoldableDouble : IFoldable<Double>
    {
        private readonly Double value;

        public FoldableDouble(Double value)
        {
            this.value = value;
        }

        public Double Value { get { return value; } }

        public IFoldable<Double> Plus(IFoldable<Double> other)
        {
            return new FoldableDouble(this.value + other.Value);
        }

		public IFoldable<Double> Minus(IFoldable<Double> other)
        {
            return new FoldableDouble(this.value - other.Value);
        }

        public IFoldable<Double> DividedBy(int ratio)
        {
            return new FoldableDouble((Double)(this.value / (Double)ratio));
        }

        public IFoldable<Double> Squared()
        {
            return new FoldableDouble(this.value * this.value);
        }

        public IFoldable<Double> GetSquareRoot()
        {
            return new FoldableDouble((Double)Math.Sqrt((double)value));
        }

		public override string ToString()
        {
            return this.value.ToString();
        }
    }

    public struct FoldableSingle : IFoldable<Single>
    {
        private readonly Single value;

        public FoldableSingle(Single value)
        {
            this.value = value;
        }

        public Single Value { get { return value; } }

        public IFoldable<Single> Plus(IFoldable<Single> other)
        {
            return new FoldableSingle(this.value + other.Value);
        }

		public IFoldable<Single> Minus(IFoldable<Single> other)
        {
            return new FoldableSingle(this.value - other.Value);
        }

        public IFoldable<Single> DividedBy(int ratio)
        {
            return new FoldableSingle((Single)(this.value / (Single)ratio));
        }

        public IFoldable<Single> Squared()
        {
            return new FoldableSingle(this.value * this.value);
        }

        public IFoldable<Single> GetSquareRoot()
        {
            return new FoldableSingle((Single)Math.Sqrt((double)value));
        }

		public override string ToString()
        {
            return this.value.ToString();
        }
    }

    public partial class Fold
    {
        private IFoldable<Double> BuildFrom(Double value)
        {
            return new FoldableDouble(value);
        }

        private IFoldable<Single> BuildFrom(Single value)
        {
            return new FoldableSingle(value);
        }

    }
}