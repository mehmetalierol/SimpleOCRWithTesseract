using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleOCR.Core
{
    public class HelperMethods
    {
        public static Window GetWindow(string PageUId)
        {
            Window window = null;
            foreach (Window item in App.Current.Windows)
            {
                if (item.Uid == PageUId.ToString())
                {
                    window = item;
                    break;
                }
            }
            return window;
        }

        public static void ReadFromScreenForOCR()
        {
            var window = GetWindow(WindowList.SelectionWindow.ToString());
            PresentationSource MainWindowPresentationSource = PresentationSource.FromVisual(window);
            Matrix m = MainWindowPresentationSource.CompositionTarget.TransformToDevice;
            var dpiWidthFactor = m.M11;
            var dpiHeightFactor = m.M22;

            var screen = SystemParameters.WorkArea;

            System.Drawing.Size tempSize = new System.Drawing.Size
            {
                Height = (int)(window.Height * dpiHeightFactor) - 10,
                Width = (int)(window.Width * dpiWidthFactor) - 10
            };

            Bitmap bmp = new Bitmap((int)tempSize.Width, (int)tempSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen((int)(window.Left * dpiWidthFactor) + 5, (int)(window.Top * dpiHeightFactor) + 5, 5, 5, tempSize);

                IntPtr intptr = bmp.GetHbitmap();
                var sourcem = Imaging.CreateBitmapSourceFromHBitmap(intptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                var OCRResult = TeserractOCR.ConvertImageToText(bmp);

                MessageBox.Show(OCRResult, "Aşağıdaki Metin Panoya Kopyalandı!");

                Clipboard.SetText(OCRResult);
            }
        }
    }
}
