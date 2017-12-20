using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Presenter.ConfigurationEditor
{
    public interface IConfigurationEditorPresenterCallbacks
    {
        void OnLoad();
        void OnAddSave();
        void OnAddCancel();
        void OnEditConnectionsTab();
        void OnEditDatabaseTypeChange();
        void OnEditSave();
        void OnEditDelete();
        void OnEditCancel();
        void OnEditLoadDatabaseConnection();
        void OnAddDatabaseTypeSelectedIndexChange();
        void OnAvailableConnectionSelectedIndexChanged();
        void OnAddTestConnectionClick();
        void OnEditTestConnectionClick();
    }
}
