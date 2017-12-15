using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp6
{

    class Program
    {
        static void Main(string[] args)
        {
            // обєкт для серіалізації
            Forms.Directory direct = new Forms.Directory();
            Console.WriteLine("object create");
 
            // створюєм обєкт BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();
            // получаєм потік куди будем записувати серіалізований обєкт
            using (FileStream fs = new FileStream("direct.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, direct);
 
                Console.WriteLine("object serialized");
            }
 
            // десеріалізація з файла direct.dat
            using (FileStream fs = new FileStream("direct.dat", FileMode.OpenOrCreate))
            {
                Forms.Directory newPerson = (Forms.Directory)formatter.Deserialize(fs);
 
                Console.WriteLine("object deserialized");
                
            }
 
            Console.ReadLine();


            // обєкт для серіалізації
            Forms.File file = new Forms.File();
            Console.WriteLine("object create");

            // створюєм обєкт BinaryFormatter
            BinaryFormatter formatter1 = new BinaryFormatter();
            // получаєм потік куди будем записувати серіалізований обєкт
            using (FileStream fs = new FileStream("file.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, file);

                Console.WriteLine("object1 serialized");
            }

            // десеріалізація з файла file.dat
            using (FileStream fs = new FileStream("file.dat", FileMode.OpenOrCreate))
            {
                Forms.File newPerson = (Forms.File)formatter.Deserialize(fs);

                Console.WriteLine("object1 deserialized");
                
            }

            Console.ReadLine();

        }
    }
    public class Forms
    {
        [Serializable]
        public class Directory
        {
            public Directory[] SubFolders { get; set; }
            public File[] Files { get; set; }
            public string Name { get; set; }
        }
        [Serializable]
        public class File
        {
            public byte[] Data { get; set; }
            public string Name { get; set; }
        }

        private IEnumerable<Directory> GetDirectoriesR(string root)
        {
            foreach (var dir in System.IO.Directory.GetDirectories(root))
            {
                var dirInfo = new DirectoryInfo(dir);
                var directory = new Directory
                {
                    Name = dirInfo.Name,
                    Files = GetFilesR(dir).ToArray(),
                    SubFolders = GetDirectoriesR(dir).ToArray()
                };
                yield return directory;
            }
        }

        private IEnumerable<File> GetFilesR(string dir)
        {
            foreach (var file in System.IO.Directory.GetFiles(dir))
            {
                var fInfo = new FileInfo(file);

                yield return new File
                {
                    Data = System.IO.File.ReadAllBytes(file),
                    Name = fInfo.Name
                };
            }
        }
    }
    

   
}

