using Ninject.Modules;
using SimpleOCR.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOCR.Core
{
    public class SimpleOCRModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<MainWindowVM>().ToSelf().InSingletonScope();
        }
    }
}
