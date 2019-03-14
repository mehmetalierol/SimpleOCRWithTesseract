using SimpleOCR.Command;
using SimpleOCR.Core;
using System.Windows;
using System.Windows.Input;

namespace SimpleOCR.ViewModel
{
    public class HelperWindowVM
    {
        RelayCommand _windowLoadCommand, _btnCatchCommand;
        public HelperWindowVM()
        {

        }

        public ICommand WindowLoadCommand
        {
            get
            {
                if (_windowLoadCommand == null)
                {
                    _windowLoadCommand = new RelayCommand(param => this.WindowLoaded());
                }
                return _windowLoadCommand;
            }
        }

        public ICommand BtnCatchCommand
        {
            get
            {
                if (_btnCatchCommand == null)
                {
                    _btnCatchCommand = new RelayCommand(param => this.BtnCatch());
                }
                return _btnCatchCommand;
            }
        }

        void BtnCatch()
        {
            HelperMethods.ReadFromScreenForOCR();
        }

        void WindowLoaded()
        {
            var window = HelperMethods.GetWindow(WindowList.HelperWindow.ToString());
            var screen = SystemParameters.WorkArea;
            window.Left = screen.Right - window.Width;
            window.Top = screen.Bottom - window.Height;
        }
    }
}
