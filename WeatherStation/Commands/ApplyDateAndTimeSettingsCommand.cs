﻿using System;
using System.Windows.Input;
using Prism.Events;
using WeatherStation.Services;
using WeatherStation.ViewModels;

namespace WeatherStation.Commands
{
    public class ApplyDateAndTimeSettingsCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISettingsService _settingsService;
        private readonly DateAndTimeSettingsWindowViewModel _viewModel;

        public ApplyDateAndTimeSettingsCommand(IEventAggregator eventAggregator,
            DateAndTimeSettingsWindowViewModel viewModel, ISettingsService settingsService)
        {
            _eventAggregator = eventAggregator;
            _viewModel = viewModel;
            _settingsService = settingsService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var newDateTime = DateTime.ParseExact($"{_viewModel.CurrentDate} {_viewModel.CurrentTime}", "dd.MM.yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);
            var timeSpan = DateTime.Now - newDateTime;
            _settingsService.SaveDateTimeOffset(timeSpan);
        }

        public event EventHandler CanExecuteChanged;
    }
}