using SimpleOCR.Command;
using SimpleOCR.Core;
using System.Windows.Input;

namespace SimpleOCR.ViewModel
{
    public class MainWindowVM
    {
        RelayCommand _windowMouseDown, _windowLoadCommand, _windowKeyDownCommand;

        public MainWindowVM()
        {
            
        }

        public ICommand WindowKeyDownCommand
        {
            get
            {
                if (_windowKeyDownCommand == null)
                {
                    _windowKeyDownCommand = new RelayCommand(param => this.WindowKeyDownEvent((KeyEventArgs)param));
                }
                return _windowKeyDownCommand;
            }
        }

        public ICommand WindowMouseDownCommand
        {
            get
            {
                if(_windowMouseDown == null)
                {
                    _windowMouseDown = new RelayCommand(param => this.WindowMouseDown((MouseEventArgs)param));
                }
                return _windowMouseDown;
            }
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

        void WindowMouseDown(MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var window = HelperMethods.GetWindow(WindowList.SelectionWindow.ToString());
                window.DragMove();
            }
        }

        void WindowKeyDownEvent(KeyEventArgs e)
        {
            if(e.Key == Key.T && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                HelperMethods.ReadFromScreenForOCR();
            }
        }

        void WindowLoaded()
        {
            var helperWindow = new HelperWindow();
            helperWindow.Show();

            var window = HelperMethods.GetWindow(WindowList.SelectionWindow.ToString());
            window.Activate();
        }
    }
}
