using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherKata
{
	public enum DataType
    {
		Temperature,
		Humidity,
		Precipitations,
		Pressure,
	}

	public abstract partial class WeatherStat
	{
		public static WeatherStat<double> Temperature(double value)
        {
            return new UnitStat<double>(DataType.Temperature, value);
        }

		public static WeatherStat<double> Humidity(double value)
        {
            return new UnitStat<double>(DataType.Humidity, value);
        }

		public static WeatherStat<double> Precipitations(double value)
        {
            return new UnitStat<double>(DataType.Precipitations, value);
        }

		public static WeatherStat<float> Pressure(float value)
        {
            return new UnitStat<float>(DataType.Pressure, value);
        }

	}

	public partial class StatsCollection
    {
        public double? Temperature
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Temperature)
                    .Cast<WeatherStat<double>>()
                    .Select(s => (double?)s.Value)
                    .SingleOrDefault();
            }
        }

        public double? TemperatureStandardDeviation
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Temperature)
                    .Cast<WeatherStat<double>>()
                    .Select(s => (double?)s.StandardDeviation)
                    .SingleOrDefault();
            }
        }

		public string TemperatureConstructionTrace
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Temperature)
                    .Cast<WeatherStat<double>>()
                    .Select(s => s.ConstructionTrace)
                    .SingleOrDefault();
            }
        }

        public double? Humidity
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Humidity)
                    .Cast<WeatherStat<double>>()
                    .Select(s => (double?)s.Value)
                    .SingleOrDefault();
            }
        }

        public double? HumidityStandardDeviation
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Humidity)
                    .Cast<WeatherStat<double>>()
                    .Select(s => (double?)s.StandardDeviation)
                    .SingleOrDefault();
            }
        }

		public string HumidityConstructionTrace
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Humidity)
                    .Cast<WeatherStat<double>>()
                    .Select(s => s.ConstructionTrace)
                    .SingleOrDefault();
            }
        }

        public double? Precipitations
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Precipitations)
                    .Cast<WeatherStat<double>>()
                    .Select(s => (double?)s.Value)
                    .SingleOrDefault();
            }
        }

        public double? PrecipitationsStandardDeviation
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Precipitations)
                    .Cast<WeatherStat<double>>()
                    .Select(s => (double?)s.StandardDeviation)
                    .SingleOrDefault();
            }
        }

		public string PrecipitationsConstructionTrace
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Precipitations)
                    .Cast<WeatherStat<double>>()
                    .Select(s => s.ConstructionTrace)
                    .SingleOrDefault();
            }
        }

        public float? Pressure
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Pressure)
                    .Cast<WeatherStat<float>>()
                    .Select(s => (float?)s.Value)
                    .SingleOrDefault();
            }
        }

        public float? PressureStandardDeviation
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Pressure)
                    .Cast<WeatherStat<float>>()
                    .Select(s => (float?)s.StandardDeviation)
                    .SingleOrDefault();
            }
        }

		public string PressureConstructionTrace
        {
            get
            {
                return this.stats
                    .Where(s => s.DataType == DataType.Pressure)
                    .Cast<WeatherStat<float>>()
                    .Select(s => s.ConstructionTrace)
                    .SingleOrDefault();
            }
        }

    }
}