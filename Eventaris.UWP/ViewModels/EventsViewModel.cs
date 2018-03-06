using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Media.Protection.PlayReady;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Eventaris.DAL;
using Eventaris.Domain;
using Eventaris.UWP.Services;
using Eventaris.UWP.Utility;
using Eventaris.UWP.Utility.Messaging;
using QRCoder;


namespace Eventaris.UWP.ViewModels
{
    public class EventsViewModel: INotifyPropertyChanged
    {
        private readonly IRepository _repository;
        private readonly INavigationService _navigationService;

        public CustomCommand LoadCommand { get; set; }
        public CustomCommand ShowParticipantsCommand { get; set; }
        public CustomCommand NewEventCommand { get; set; }
        public CustomCommand EditEventCommand { get; set; }
        public CustomCommand RemoveEventCommand { get; set; }

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
                SetQrCode(value.Id.ToString());
            }
        }

        //TODO move to converter, property SelectedEvent.Id, problem async?
        private BitmapImage _qrCode;

        public BitmapImage QrCode
        {
            get => _qrCode;
            set
            {
                _qrCode = value;
                RaisePropertyChanged(nameof(QrCode));
            }
        }

        private async void SetQrCode(string value)
        {
            if (value != null)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);

                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    using (DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes(qrCodeAsBitmapByteArr);
                        await writer.StoreAsync();
                    }

                    var image = new BitmapImage();
                    await image.SetSourceAsync(stream);
                    QrCode = image;
                }
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
            NewEventCommand = new CustomCommand(NewEvent, null);
            EditEventCommand = new CustomCommand(EditEvent, null);
            RemoveEventCommand = new CustomCommand(RemoveEvent, null);
        }

        private void ShowParticipantsForSelectedEvent(object obj)
        {
            _navigationService.NavigateTo("Participants");

            ParticipantsMessage message = new ParticipantsMessage()
            {
                Event = SelectedEvent
            };
            
            Messenger.Default.Send(message);
        }

        private void NewEvent(object obj)
        {
            _navigationService.NavigateTo("NewEvent");

            NewEventMessage message = new NewEventMessage()
            {
                Event = new Event()
                {
                    StartingDateTime = DateTime.Now,
                    EndingDateTime = DateTime.Now

                }
            };
            Messenger.Default.Send(message);
        }

        private void EditEvent(object obj)
        {

            DetailsMessage message = new DetailsMessage()
            {
                Event = SelectedEvent
            };
            Messenger.Default.Send(message);

            _navigationService.NavigateTo("EventDetails");

        }

        //TODO put dialog in service, possible problem await result
        private async void RemoveEvent(object obj)
        {
            ContentDialog deleteEventDialog = new ContentDialog
            {
                Title = String.Format("Delete Event {0} permanently?", SelectedEvent.Name),
                Content = "If you delete this event, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteEventDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                _repository.DeleteEventById(SelectedEvent);
                Events = new ObservableCollection<Event>(_repository.GetAllEvents());
                SelectedEvent = _events.FirstOrDefault();
            }
                       
        }

    }
}
