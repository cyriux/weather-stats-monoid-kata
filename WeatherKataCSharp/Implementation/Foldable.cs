using System;

namespace WeatherKata
{
    public interface IFoldable<T>
    {
        T Value { get; }
        IFoldable<T> Plus(IFoldable<T> other);
        IFoldable<T> Minus(IFoldable<T> other);
        IFoldable<T> DividedBy(int d);
        IFoldable<T> Squared();
        IFoldable<T> GetSquareRoot();
    }

    public partial class Fold
    {
        public static IFoldable<T> From<T>(T value)
        {
            dynamic foldFactory = new Fold();
            dynamic dv = (dynamic)value;
            return (IFoldable<T>)foldFactory.BuildFrom(dv);
        }
    }
}
