using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter.LogIn;
using NovenaLibrary.Config;

namespace NovenaLibrary.View.LogIn
{
    public interface ILogInView : IView<ILogInPresenterCallbacks>
    {
        string Username { get; set; }
        string Password { get; set; }
        string UsernameMessage { get; set; }
        string PasswordMessage { get; set; }
        bool UsernameTextBoxEnabled { get; set; }
        bool PasswordTextBoxEnabled { get; set; }
        void CloseForm();
        AppConfig AppConfig { get; }
    }
}
