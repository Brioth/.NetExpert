using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Eventaris.UWP.Views;

namespace Eventaris.UWP.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo(string key)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            switch (key)
            {
                case "Participants":
                    rootFrame.Navigate(typeof(ParticipantsView));
                    break;
                case "Back":
                    rootFrame.GoBack();
                    break;
            }

        }
    }
}
