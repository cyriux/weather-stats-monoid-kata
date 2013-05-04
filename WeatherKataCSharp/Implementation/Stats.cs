using System;
using System.Linq;
using System.Collections.Generic;

namespace WeatherKata
{
    public abstract partial class WeatherStat
    {
        public readonly DataType DataType;

        protected WeatherStat(DataType dataType)
        {
            this.DataType = dataType;
        }
    }

    public abstract class WeatherStat<T> : WeatherStat
    {
        public abstract int Count { get; }
        public abstract T Value { get; }
        public abstract string ConstructionTrace { get; }
        internal abstract IFoldable<T> SumOfValues { get; }
        internal abstract IFoldable<T> SumOfSquaredValues { get; }

        public T StandardDeviation
        {
            get
            {
                var left = this.SumOfSquaredValues.DividedBy(this.Count);
                var right = this.SumOfValues.DividedBy(this.Count).Squared();

                return left.Minus(right).GetSquareRoot().Value;
            }
        }

        protected WeatherStat(DataType dataType) : base(dataType) { }

        public override bool Equals(object obj)
        {
            var other = obj as WeatherStat<T>;
            if (other == null) return false;

            return this.DataType == other.DataType && this.Count == other.Count && this.SumOfValues.Equals(other.SumOfValues);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(this.Count, this.SumOfValues).GetHashCode();
        }
    }

    public class UnitStat<T> : WeatherStat<T>
    {
        private readonly IFoldable<T> value;

        internal UnitStat(DataType dataType, T value)
            : base(dataType)
        {
            this.value = Fold.From(value);
        }

        public override int Count
        {
            get { return 1; }
        }

        public override T Value
        {
            get { return this.value.Value; }
        }

        internal override IFoldable<T> SumOfValues
        {
            get { return this.value; }
        }

        internal override IFoldable<T> SumOfSquaredValues
        {
            get { return this.value.Squared(); }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.DataType, this.value);
        }

        public override string ConstructionTrace
        {
            get { return this.ToString(); }
        }
    }

    public class AgregateStat<T> : WeatherStat<T>
    {
        private readonly int count;
        private readonly IFoldable<T> sumOfValues;
        private readonly IFoldable<T> sumOfSquaredValues;
        private readonly string constructionTraceA;
        private readonly string constructionTraceB;

        internal AgregateStat(DataType dataType, WeatherStat<T> a, WeatherStat<T> b)
            : base(dataType)
        {
            this.count = a.Count + b.Count;
            this.sumOfValues = a.SumOfValues.Plus(b.SumOfValues);
            this.sumOfSquaredValues = a.SumOfSquaredValues.Plus(b.SumOfSquaredValues);

            this.constructionTraceA = a.ConstructionTrace;
            this.constructionTraceB = b.ConstructionTrace;
        }

        public override int Count
        {
            get { return count; }
        }

        public override T Value
        {
            get { return this.sumOfValues.DividedBy(this.count).Value; }
        }

        internal override IFoldable<T> SumOfValues
        {
            get { return this.sumOfValues; }
        }

        internal override IFoldable<T> SumOfSquaredValues
        {
            get { return this.sumOfSquaredValues; }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} (*{2})", this.DataType, this.Value, this.Count);
        }

        public override string ConstructionTrace
        {
            get { return string.Format("({0} + {1})", this.constructionTraceA, this.constructionTraceB); }
        }
    }

    public partial class StatsCollection
    {
        internal readonly IEnumerable<WeatherStat> stats;

        public StatsCollection(params WeatherStat[] stats)
            : this(stats.AsEnumerable())
        {
        }

        public StatsCollection(IEnumerable<WeatherStat> stats)
        {
            this.stats = StatsCollection.AgregateStats(stats).OrderBy(m => m.DataType);
        }

        public override bool Equals(object obj)
        {
            var other = obj as StatsCollection;
            if (other == null) return false;

            return this.stats.SequenceEqual(other.stats);
        }

        public override int GetHashCode()
        {
            return this.stats.Count();
        }

        public override string ToString()
        {
            return string.Format("{{{0}}}", string.Join(", ", this.stats.Select(s => s.ToString())));
        }

        public static StatsCollection Agregate(params StatsCollection[] statsCollections)
        {
            return new StatsCollection(statsCollections.SelectMany(c => c.stats));
        }

        private static IEnumerable<WeatherStat> AgregateStats(IEnumerable<WeatherStat> stats)
        {
            Reducer reducer = new Reducer();

            return stats
                .GroupBy(s => s.DataType)
                .Select(g => g.Aggregate(reducer.Reduce));
        }
    }

    internal class Reducer
    {
        internal WeatherStat Reduce(WeatherStat a, WeatherStat b)
        {
            return ((dynamic)this).GenericReduce((dynamic)a, (dynamic)b);
        }

        internal WeatherStat<T> GenericReduce<T>(WeatherStat<T> a, WeatherStat<T> b)
        {
            if (a.DataType != b.DataType)
                throw new ArgumentException("Incompatible data types", "b");

            return new AgregateStat<T>(a.DataType, a, b);
        }
    }
}
