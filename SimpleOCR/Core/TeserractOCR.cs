using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SimpleOCR.Core
{
    public class TeserractOCR
    {
        public static string ConvertImageToText(Bitmap image)
        {
            var sb = new StringBuilder();

            var imageByte = ToByteArray(image, ImageFormat.Bmp);

            var text = ParseText(imageByte, "eng");
            return text;
        }

        private static string ParseText(byte[] imageFile, string lang)
        {
            string output = string.Empty;
            var tempOutputFile = Path.GetTempPath() + Guid.NewGuid();
            var tempImageFile = Path.GetTempFileName();

            var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            var tesseractPath = solutionDirectory + @"\tesseract-master.1153";

            try
            {
                File.WriteAllBytes(tempImageFile, imageFile);

                var info = new ProcessStartInfo
                {
                    WorkingDirectory = tesseractPath,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments =
                    "/c tesseract.exe " +
                    tempImageFile + " " +
                    tempOutputFile +
                    " -l " + string.Join("+", lang)
                };

                // Start tesseract.
                Process process = Process.Start(info);
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    // Exit code: success.
                    output = File.ReadAllText(tempOutputFile + ".txt");
                }
                else
                {
                    throw new Exception("Error. Tesseract stopped with an error code = " + process.ExitCode);
                }
            }
            finally
            {
                File.Delete(tempImageFile);
                File.Delete(tempOutputFile + ".txt");
            }
            return output;
        }

        private static byte[] ToByteArray(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}