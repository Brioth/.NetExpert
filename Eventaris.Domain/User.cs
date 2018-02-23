using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Eventaris.Domain
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }    
        public virtual ICollection<Participation> Participations { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}