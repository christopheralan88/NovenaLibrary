using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NovenaLibrary.Presenter.LogIn;
using NovenaLibrary.Config;

namespace NovenaLibrary.View.LogIn
{
    public partial class LogInView : Form, ILogInView
    {
        private AppConfig _appConfig;

        public LogInView(AppConfig appConfig)
        {
            InitializeComponent();
            _appConfig = appConfig;
            ILogInPresenter presenter = new LogInPresenter(this);
            presenter.Initialize();
        }

        public AppConfig AppConfig
        {
            get { return _appConfig; }
        }

        public void Attach(ILogInPresenterCallbacks presenter)
        {
            button_SignIn.Click += (sender, e) => presenter.OnOk();
            this.Load += (sender, e) => presenter.OnLoad();
        }

        public string Password
        {
            get { return txtBox_password.Text; }
            set { txtBox_password.Text = value; }
        }

        public string PasswordMessage
        {
            get { return lbl_passwordMessage.Text; }
            set { lbl_passwordMessage.Text = value; }
        }

        public string Username
        {
            get { return txtBox_username.Text; }
            set { txtBox_username.Text = value; }
        }

        public string UsernameMessage
        {
            get { return lbl_usernameMessage.Text; }
            set { lbl_usernameMessage.Text = value; }
        }

        public bool UsernameTextBoxEnabled
        {
            get { return txtBox_username.Enabled; }
            set { txtBox_username.Enabled = value; }
        }
   
        public bool PasswordTextBoxEnabled
        {
            get { return txtBox_username.Enabled; }
            set { txtBox_password.Enabled = value; }
        }

        public void CloseForm()
        {
            Close();
        }
    }
}
