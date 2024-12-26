using System.IO;
using Exiled.API.Features;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CustomHint
{
    public class FileDotNet
    {
        /*
        I added summaries to the methods.
        If you do not like this just remove it
        what it does is, it shows what it does
        when references in file, might help you
        working with this later.
        -Saskyc.
        */

        /// <summary>
        /// Gets the path of file you are searching for.
        /// </summary>
        /// <param name="fileName">The file you are searching for.</param>
        /// <returns></returns>
        public static string GetPath(string fileName)
        {
            return Path.Combine(Paths.Configs, "CustomHint", fileName);
        }

        /// <summary>
        /// If the file is missing, executes SaveFile method otherwisely returns text.
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="fileName">The file you are making sure to load.</param>
        /// <param name="objectDefault">Default object - string, int, bool etc.</param>
        /// <returns></returns>
        public static object LoadFile<F>(string fileName, object objectDefault)
        {
            /*TODO: Test this
            Made this method to always return content of file and create it before it returns if not existing.
            -saskyc*/

            if (!File.Exists(fileName))
                SaveFile(fileName, objectDefault);

            IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

            return deserializer.Deserialize<F>(File.ReadAllText(fileName));
        }

        /// <summary>
        /// SaveFile method creates file by serializing the default object, in this case value text.
        /// </summary>
        /// <param name="fileName">File name you want to save.</param>
        /// <param name="text">Value you want to add, can be int, float, bool etc.</param>
        public static void SaveFile(string fileName, object text)
        {
            string directory = Path.GetDirectoryName(fileName);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            File.WriteAllText(fileName, serializer.Serialize(text));
        }
    }
}
