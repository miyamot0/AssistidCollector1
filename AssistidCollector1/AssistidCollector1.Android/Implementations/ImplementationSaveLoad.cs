using AssistidCollector1.Interfaces;
using System.IO;
using AssistidCollector1.Droid.Implementations;
using Xamarin.Forms;
using Android.Content;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(ImplementationSaveLoad))]
namespace AssistidCollector1.Droid.Implementations
{
    public class ImplementationSaveLoad : InterfaceSaveLoad
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public bool FileExists(string filename)
        {
            return File.Exists(CreatePathToFile(filename));
        }

        public string LoadFile(string filename)
        {
            var path = CreatePathToFile(filename);

            using (StreamReader sr = File.OpenText(path))
            {
                return sr.ReadToEnd();
            }
        }

        public void SaveFile(string filename, string text)
        {
            var path = CreatePathToFile(filename);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(text);
            }
        }

        string CreatePathToFile(string filename)
        {
            var docsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, filename);
        }
        
        public void InstallLocationFile(string filename)
        {
            var path = CreatePathToFile(filename);
            
            Intent promptInstall = new Intent(Intent.ActionView);
            promptInstall.SetDataAndType(Uri.Parse(path), "application/vnd.android.package-archive");
            promptInstall.SetFlags(ActivityFlags.GrantReadUriPermission);
            promptInstall.SetFlags(ActivityFlags.NewTask);
            promptInstall.SetFlags(ActivityFlags.ClearTop);

            //var intent = new Intent(Intent.ActionView);
            //global::Android.Net.Uri pdfFile = global::Android.Net.Uri.FromFile(new Java.IO.File(path));
            //intent.SetDataAndType(pdfFile, "application/pdf");
            //intent.SetFlags(ActivityFlags.GrantReadUriPermission);
            //intent.SetFlags(ActivityFlags.NewTask);
            //intent.SetFlags(ActivityFlags.ClearWhenTaskReset);

            //Context context = Android.App.Application.Context;

            Forms.Context.StartActivity(promptInstall);
        }
    }
}