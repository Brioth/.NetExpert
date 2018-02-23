using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.Protection.PlayReady;
using Eventaris.DAL;
using Eventaris.Domain;
using Eventaris.UWP.Services;
using Eventaris.UWP.Utility;


namespace Eventaris.UWP.ViewModels
{
    public class EventsViewModel: INotifyPropertyChanged
    {
        private readonly IRepository _repository;
        private readonly INavigationService _navigationService;

        public CustomCommand LoadCommand { get; set; }
        public CustomCommand ShowParticipantsCommand { get; set; }

        private ObservableCollection<Event> _events;

        public ObservableCollection<Event> Events
        {
            get => _events;
            set
            {
                _events = value;
                RaisePropertyChanged(nameof(Events));
            }
        }

        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                RaisePropertyChanged(nameof(SelectedEvent));
            }
        }

        public EventsViewModel(INavigationService navigationService, IRepository repository)
        {
            _repository = repository;
            _navigationService = navigationService;
            LoadCommands();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadData()
        {
            Events = new ObservableCollection<Event>(_repository.GetAllEvents());
            SelectedEvent = Events.First();
        }

        private void LoadCommands()
        {
            LoadCommand = new CustomCommand((obj) => { LoadData(); }, null);
            ShowParticipantsCommand = new CustomCommand(ShowParticipantsForSelectedEvent, null);
        }

        private void ShowParticipantsForSelectedEvent(object obj)
        {
            _navigationService.NavigateTo("Participants");
            Messenger.Default.Send(SelectedEvent);
        }

    }
}
