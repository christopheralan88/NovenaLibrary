﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovenaLibrary.Presenter.LogIn
{
    public interface ILogInPresenterCallbacks
    {
        void OnLoad();
        void OnOk();
    }
}
