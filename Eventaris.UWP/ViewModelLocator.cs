using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventaris.DAL;
using Eventaris.UWP.Services;
using Eventaris.UWP.ViewModels;

namespace Eventaris.UWP
{
    public class ViewModelLocator
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;

        public EventsViewModel EventsViewModel { get; }
        public ParticipantsViewModel ParticipantsViewModel { get; }

        public ViewModelLocator()
        {
            _navigationService = new NavigationService();
            _repository = new ApiRepository();

            EventsViewModel = new EventsViewModel(_navigationService, _repository);
            ParticipantsViewModel = new ParticipantsViewModel(_navigationService, _repository);
        }
    }
}
