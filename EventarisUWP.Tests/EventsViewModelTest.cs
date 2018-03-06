using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventaris.DAL;
using Eventaris.Domain;
using Eventaris.UWP.Services;
using Eventaris.UWP.ViewModels;
using EventarisUWP.Tests.Mocks;
using Moq;
using NUnit.Framework;


namespace EventarisUWP.Tests
{
    [TestFixture]
    public class EventsViewModelTest
    {
        private IRepository repository;
        private INavigationService navigationService;

        [SetUp]
        public void Setup()
        {
            repository = new MockIRepository();
            navigationService = new MockNavigationService();
        }

        [Test]
        public void AllEventsShouldLoadOnStartup()
        {
            //Arrange
            ObservableCollection<Event> events = null;
            var expectedEvents = repository.GetAllEvents();
            var expectedSelectedEvent = expectedEvents.FirstOrDefault();

            //Act
            var viewModel = new EventsViewModel(navigationService, repository);
            events = viewModel.Events;
            var selectedEvent = viewModel.SelectedEvent;

            //Assert
            CollectionAssert.AreEqual(expectedEvents, events);
            Assert.That(expectedSelectedEvent.Equals(selectedEvent));
        }

    }
}
