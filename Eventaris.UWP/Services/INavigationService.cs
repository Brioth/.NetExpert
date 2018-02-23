using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventaris.UWP.Services
{
    public interface INavigationService
    {
        void NavigateTo(string key);
    }
}
