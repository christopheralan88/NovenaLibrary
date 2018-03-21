using QueryBuilder.DatabaseConnections;
using QueryBuilder.Config;
using System;
using NovenaLibrary.View.LogIn;
using NovenaLibrary.Config;
using System.Windows.Forms;

namespace NovenaLibrary.Presenter.LogIn
{
    public class LogInPresenter : ILogInPresenter, ILogInPresenterCallbacks
    {
        private ILogInView _view;
        private IDatabaseConnection dbConnection;

        public LogInPresenter(ILogInView view)
        {
            _view = view;
        }

        public object UI
        {
            get { return _view; }
        }

        public void Initialize()
        {
            _view.Attach(this);
        }

        public void OnLoad()
        {
            if (_view.AppConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.None)
            {
                MessageBox.Show("This connection string already has a username and password.  You do not need to sign in.", "Log In", MessageBoxButtons.OK);
                _view.CloseForm();
            }
            else if (_view.AppConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.PasswordOnly)
            {
                // If AppConfig's User has Username, then set username textbox's text to username, disable username textbox, and display username message
                _view.Username = _view.AppConfig.User.Username;
                _view.UsernameMessage = "This connection string already has a username set";
                _view.UsernameTextBoxEnabled = false;
            }
            else if (_view.AppConfig.GetCredentialsRequired == AppConfig.CredentialsRequired.UsernameOnly)
            {
                // If AppConfig's User has Password, then set password's textbox's text to password, disable password textbox, and display password message
                _view.Password = _view.AppConfig.User.Password;
                _view.PasswordMessage = "This connection string already has a password set";
                _view.PasswordTextBoxEnabled = false;
            }
        }

        public void OnOk()
        {
            // If Username and Password textboxes are enabled, then they should contain text
            if (string.IsNullOrEmpty(_view.Username))
            {
                MessageBox.Show("The connection string requires a username");
                return;
            }
            
            if (string.IsNullOrEmpty(_view.Password))
            {
                MessageBox.Show("The connection string requires a password");
            }

            // Get username and password from view and instantiate new User object
            var user = new User(_view.Username, _view.Password);
            _view.AppConfig.User = user;

            // Instantiate new DatabaseConnection object and intialize the dbConnection field with the object.  
            // The DatabaseConnection object will use the AppConfig's User field that was set above to update
            // the AppConfig's connection string.
            SetDbConnection();

            // try to login
            try
            {
                // TODO:  later try adding ability to see if user is superuser by querying on user table in database
                dbConnection.UserSignIn();
                _view.CloseForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Log In Exception", MessageBoxButtons.OK);
                _view.AppConfig.User = null; // The appliation assumes that a null User property means that the user is NOT signed in.
                dbConnection = null;  // set the dbConnection field to null so that User object and connection string are not used again.
            }
        }

        private void SetDbConnection()
        {
            dbConnection = new DatabaseConnectionFactory().CreateDbConnection(_view.AppConfig.DatabaseType, _view.AppConfig.ConnectionString);
        } 
    }
}
