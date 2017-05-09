using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolUI
{
    class Writer
    {
        public static async Task writeToFile(ContainerClass container)
        {
            string path = "C:/Users/Japh/Desktop/" + "hello.txt";
            //path = "C:/Users/Sabrina/Dropbox/Emnefiler/Krak/Output.txt";
            path = "C:/Users/Japh/Desktop/" + "hello.txt";

            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(path, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, "Hello world!");

            Task newt = new Task(() => {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Hello World");
                sw.WriteLine("Hello World");
                sw.WriteLine("Hello World");
                sw.WriteLine("Hello World");
                sw.Dispose();
                fs.Dispose();
            });
            newt.Start();

        }
    }
}
