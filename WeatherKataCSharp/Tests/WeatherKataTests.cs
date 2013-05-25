using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherKata
{
    [TestClass]
    public class WeatherKataTests
    {
        [TestMethod]
        public void UnitStatIsEqualToSelf()
        {
            Assert.AreEqual(
                WeatherStat.Temperature(19.0),
                WeatherStat.Temperature(19.0));
        }

        [TestMethod]
        public void UnitStatsOfDifferentValuesAreNotEqual()
        {
            Assert.AreNotEqual(
                WeatherStat.Temperature(19.0),
                WeatherStat.Temperature(20.0));
        }

        [TestMethod]
        public void UnitStatsOfDifferentTypesAreNotEqual()
        {
            Assert.AreNotEqual(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(19.0));
        }

        [TestMethod]
        public void ObservationIsEqualToSelf()
        {
            Assert.AreEqual(
                new StatsCollection(
                    WeatherStat.Temperature(19.0),
                    WeatherStat.Humidity(59.0)),
                new StatsCollection(
                    WeatherStat.Temperature(19.0),
                    WeatherStat.Humidity(59.0)));
        }

        [TestMethod]
        public void SingleObservationIsEqualToItsAverage()
        {
            var observation = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var average = StatsCollection.Agregate(observation);

            Assert.AreEqual(observation, average);
        }

        [TestMethod]
        public void AverageOfTwoEqualObservations()
        {
            var observation = new StatsCollection(
                WeatherStat.Temperature(20.0),
                WeatherStat.Humidity(64.0));

            var average = StatsCollection.Agregate(observation, observation);

            Assert.AreEqual(average.Temperature.Value, 20.0);
            Assert.AreEqual(average.Humidity.Value, 64.0);
        }

        [TestMethod]
        public void AverageOfTwoDifferentObservations()
        {
            var lowObservation = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var highObservation = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0));

            var average = StatsCollection.Agregate(lowObservation, highObservation);

            Assert.AreEqual(average.Temperature.Value, 20.0);
            Assert.AreEqual(average.Humidity.Value, 64.0);
        }

        [TestMethod]
        public void EquivalentAverages()
        {
            var lowObservation = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var highObservation = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0));

            var averageObservation = new StatsCollection(
                WeatherStat.Temperature(20.0),
                WeatherStat.Humidity(64.0));

            var average1 = StatsCollection.Agregate(lowObservation, highObservation);
            var average2 = StatsCollection.Agregate(averageObservation, averageObservation);

            Assert.AreEqual(average1, average2);
        }

        [TestMethod]
        public void AverageOfSeveralObservations()
        {
            var observation1 = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0),
                WeatherStat.Precipitations(0.0));

            var observation2 = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0),
                WeatherStat.Precipitations(15.0));

            var observation3 = new StatsCollection(
                WeatherStat.Temperature(20.0),
                WeatherStat.Humidity(67.0),
                WeatherStat.Precipitations(45.0));

            var average = StatsCollection.Agregate(observation1, observation2, observation3);

            Assert.AreEqual(average.Temperature.Value, 20.0);
            Assert.AreEqual(average.Humidity.Value, 65.0);
            Assert.AreEqual(average.Precipitations.Value, 20.0);
        }

        [TestMethod]
        public void AverageOfAverages()
        {
            var observation1 = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0),
                WeatherStat.Precipitations(0.0));

            var observation2 = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0),
                WeatherStat.Precipitations(15.0));

            var observation3 = new StatsCollection(
                WeatherStat.Temperature(20.0),
                WeatherStat.Humidity(67.0),
                WeatherStat.Precipitations(45.0));

            var average1 = StatsCollection.Agregate(StatsCollection.Agregate(observation1, observation2), observation3);
            var average2 = StatsCollection.Agregate(observation1, StatsCollection.Agregate(observation2, observation3));

            Assert.AreEqual(average1, average2);
        }

        [TestMethod]
        public void StandardDeviationOfSingleObservation()
        {
            var observation = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            Assert.AreEqual(observation.TemperatureStandardDeviation, 0.0);
        }

        [TestMethod]
        public void StandardDeviationOfSingleObservationAgregate()
        {
            var observation = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var agregate = StatsCollection.Agregate(observation);

            Assert.AreEqual(agregate.TemperatureStandardDeviation, 0.0);
        }

        [TestMethod]
        public void StandardDeviationOfTwoObservations()
        {
            var observation1 = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var observation2 = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0));

            var agregate = StatsCollection.Agregate(observation1, observation2);

            Assert.AreEqual(agregate.TemperatureStandardDeviation, 1.0);
            Assert.AreEqual(agregate.HumidityStandardDeviation, 5.0);
        }

        [TestMethod]
        public void AverageAndDeviationOfAbsentMeasure()
        {
            var observation1 = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var observation2 = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0));

            var agregate = StatsCollection.Agregate(observation1, observation2);

            Assert.AreEqual(agregate.Precipitations, null);
            Assert.AreEqual(agregate.PrecipitationsStandardDeviation, null);
        }

        [TestMethod]
        public void AverageAndStandardDeviationWithMissingMeasures()
        {
            var observation1 = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0));

            var observation2 = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Precipitations(18.0));

            var observation3 = new StatsCollection(
                WeatherStat.Pressure(1020.0f),
                WeatherStat.Humidity(55.0));

            var agregate = StatsCollection.Agregate(observation1, observation2, observation3);

            Assert.AreEqual(agregate.Temperature, 20.0);
            Assert.AreEqual(agregate.TemperatureStandardDeviation, 1.0);
            Assert.AreEqual(agregate.Humidity, 57);
            Assert.AreEqual(agregate.HumidityStandardDeviation, 2.0);
            Assert.AreEqual(agregate.Precipitations, 18.0);
            Assert.AreEqual(agregate.PrecipitationsStandardDeviation, 0.0);
            Assert.AreEqual(agregate.Pressure, 1020.0f);
            Assert.AreEqual(agregate.PressureStandardDeviation, 0.0f);
        }

        [TestMethod]
        public void StandardDeviationOfAgregates()
        {
            var observation1 = new StatsCollection(
                WeatherStat.Temperature(19.0),
                WeatherStat.Humidity(59.0),
                WeatherStat.Precipitations(0.0));

            var observation2 = new StatsCollection(
                WeatherStat.Temperature(21.0),
                WeatherStat.Humidity(69.0),
                WeatherStat.Precipitations(15.0));

            var observation3 = new StatsCollection(
                WeatherStat.Temperature(20.0),
                WeatherStat.Humidity(67.0),
                WeatherStat.Precipitations(45.0));

            var average1 = StatsCollection.Agregate(StatsCollection.Agregate(observation1, observation2), observation3);
            var average2 = StatsCollection.Agregate(observation1, StatsCollection.Agregate(observation2, observation3));

            Assert.AreEqual(average1.TemperatureStandardDeviation, average2.TemperatureStandardDeviation);
            Assert.AreEqual(average1.HumidityStandardDeviation, average2.HumidityStandardDeviation);
            Assert.AreEqual(average1.PrecipitationsStandardDeviation, average2.PrecipitationsStandardDeviation);
        }

        [TestMethod]
        public void ConstructionTraceOfSingleObservation()
        {
            var observation = new StatsCollection(
                WeatherStat.Temperature(19.0));

            Assert.AreEqual("Temperature: 19", observation.TemperatureConstructionTrace);
        }

        [TestMethod]
        public void ConstructionTraceOfTwoObservations()
        {
            var observation1 = new StatsCollection(WeatherStat.Temperature(19.0));
            var observation2 = new StatsCollection(WeatherStat.Temperature(21.0));

            var agregate = StatsCollection.Agregate(observation1, observation2);

            Assert.AreEqual("(Temperature: 19 + Temperature: 21)", agregate.TemperatureConstructionTrace);
        }

        [TestMethod]
        public void ConstructionTraceOfThreeObservations()
        {
            var observation1 = new StatsCollection(WeatherStat.Temperature(19.0));
            var observation2 = new StatsCollection(WeatherStat.Temperature(21.0));
            var observation3 = new StatsCollection(WeatherStat.Temperature(23.0));

            var agregate = StatsCollection.Agregate(observation1, observation2, observation3);

            Assert.AreEqual("((Temperature: 19 + Temperature: 21) + Temperature: 23)", agregate.TemperatureConstructionTrace);
        }

        [TestMethod]
        public void ConstructionTraceOfAgregatesOfAgregates()
        {
            var observation1 = new StatsCollection(WeatherStat.Temperature(19.0));
            var observation2 = new StatsCollection(WeatherStat.Temperature(21.0));
            var observation3 = new StatsCollection(WeatherStat.Temperature(23.0));
            var observation4 = new StatsCollection(WeatherStat.Temperature(25.0));

            var agregateLeft = StatsCollection.Agregate(observation1, observation2);
            var agregateRight = StatsCollection.Agregate(observation3, observation4);

            var agregate = StatsCollection.Agregate(agregateLeft, agregateRight);

            Assert.AreEqual(
                "((Temperature: 19 + Temperature: 21) + (Temperature: 23 + Temperature: 25))",
                agregate.TemperatureConstructionTrace);
        }
    }
}
