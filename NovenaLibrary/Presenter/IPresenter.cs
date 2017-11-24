using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Presenter
{
    public interface IPresenter
    {
        void Initialize();
        object UI { get; }
    }
}
