﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Events;
using WeatherStation.Handler;
using WeatherStation.Messages;
using WeatherStation.Model;
using WeatherStation.Storage.Repository;

namespace WeatherStation.Storage
{
    public class TemperatureRepository : IRepository, IHandler
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataBaseConnector _dataBaseConnector;

        public TemperatureRepository(IEventAggregator eventAggregator, IDataBaseConnector dataBaseConnector)
        {
            _eventAggregator = eventAggregator;
            _dataBaseConnector = dataBaseConnector;
            Measurements = new ObservableCollection<IMeasurement>();
            dataBaseConnector.GetTemperatureMeasurements().ForEach(m => Measurements.Add(m));
            _eventAggregator.GetEvent<NewTemperature>().Subscribe(UpdateMeasurements);
        }

        private void UpdateMeasurements(IMeasurement newMeasurement)
        {
            Application.Current.Dispatcher.Invoke(delegate { Measurements.Add(newMeasurement); });
            _dataBaseConnector.WriteMeasurementIntoDataBase(newMeasurement);
        }

        public ObservableCollection<IMeasurement> Measurements { get; set; }
    }
}
