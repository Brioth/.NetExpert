using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Eventaris.DAL;
using Eventaris.Domain;
using Eventaris.UWP.Services;
using Eventaris.UWP.Utility;

namespace Eventaris.UWP.ViewModels
{
    public class ParticipantsViewModel :INotifyPropertyChanged
    {
        private readonly IRepository _repository;
        private readonly IList<User> _allUsers;
        private readonly INavigationService _navigationService;

        public CustomCommand GoBackCommand { get; set; }

        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                RaisePropertyChanged(nameof(SelectedEvent));

                Users = new ObservableCollection<User>(_repository.GetUsersByEventId(SelectedEvent.Id));
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public ParticipantsViewModel(INavigationService navigationService, IRepository repository)
        {
            Messenger.Default.Register<Event>(this, OnEventReceived );

            _repository = repository;
            _navigationService = navigationService;
            _allUsers = _repository.GetAllUsers();

            GoBackCommand = new CustomCommand(GoBack, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnEventReceived(Event happening)
        {
            SelectedEvent = happening;
        }

        private void GoBack(object obj)
        {
            _navigationService.NavigateTo("Back");
        }

        //[NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
