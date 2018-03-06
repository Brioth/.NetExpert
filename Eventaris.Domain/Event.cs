using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Eventaris.Domain
{
    public class Event : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public String Location { get; set; }
        public String Description { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }  

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}