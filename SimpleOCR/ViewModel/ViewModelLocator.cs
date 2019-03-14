using SimpleOCR.Core;

namespace SimpleOCR.ViewModel
{
    public class ViewModelLocator
    {
        public MainWindowVM MainWindowVM => Kernel.Instance.GetType<MainWindowVM>();
        public HelperWindowVM HelperWindowVM => Kernel.Instance.GetType<HelperWindowVM>();
    }
}
