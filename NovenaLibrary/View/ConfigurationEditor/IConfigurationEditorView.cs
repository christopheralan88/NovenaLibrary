using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovenaLibrary.Presenter.ConfigurationEditor;
using System.Drawing;
using System.ComponentModel;
using NovenaLibrary.Config;

namespace NovenaLibrary.View.ConfigurationEditor
{
    public interface IConfigurationEditorView : IView<IConfigurationEditorPresenterCallbacks>
    {
        IList<string> DatabaseConnections { get; set; }
        string AddNickname { get; set; }
        string AddDatabaseType { get; set; }
        string AddConnectionString { get; set; }
        string EditDatabaseType { get; set; }
        string EditConnectionString { get; set; }
        Image AddPictureBox { get; set; }
        Image EditPictureBox { get; set; }
        string HighlightedConnectionNickname { get; }
        BindingList<string> AvailableConnectionsNicknames { get; set; }
        string DefaultConnectionNickname { get; set; }
        DatabaseType DefaultConnectionDatabaseType { get; set; }
        string DefaultConnectionString { get; set; }
        void CloseForm();
        void CloseFormWithOKDialogResult();
    }
}
