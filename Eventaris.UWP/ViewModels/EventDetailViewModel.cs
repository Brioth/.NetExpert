using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Eventaris.DAL;
using Eventaris.Domain;
using Eventaris.UWP.Services;
using Eventaris.UWP.Utility;
using Eventaris.UWP.Utility.Messaging;

namespace Eventaris.UWP.ViewModels
{
    public class EventDetailViewModel
    {
        private readonly IRepository _repository;
        private readonly INavigationService _navigationService;

        public CustomCommand GoBackCommand { get; set; }
        public CustomCommand SaveChangesCommand { get; set; }

        private Event _detailEvent;
        public Event DetailEvent
        {
            get => _detailEvent;
            set
            {
                if (value != null)
                {
                    _detailEvent = value;
                    RaisePropertyChanged(nameof(DetailEvent));
                }                
            }
        }

        public EventDetailViewModel(INavigationService navigationService, IRepository repository)
        {
            Messenger.Default.Register<DetailsMessage>(this, OnEventMessageReceived);

            _repository = repository;
            _navigationService = navigationService;

            GoBackCommand = new CustomCommand(GoBack, null);
            SaveChangesCommand = new CustomCommand(SaveChanges, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnEventMessageReceived(DetailsMessage message)
        {        
                DetailEvent = message.Event;
        }

        private void GoBack(object obj)
        {
            _navigationService.NavigateTo("Back");
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveChanges(object obj)
        {
                _repository.UpdateEventById(DetailEvent);
                GoBack(null);
        }

    }
}
