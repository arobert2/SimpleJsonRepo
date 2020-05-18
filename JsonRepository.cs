using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace SimpleJsonRepo
{
    /// <summary>
    /// JsonRepository object type for simple JSON files
    /// </summary>
    /// <typeparam name="T">Object type to track.</typeparam>
    public class JsonRepository<T> where T : new()
    {
        private T _modelObject;                                         //Model you wish to track
        public T ModelObject { get { return _modelObject; } }           //Public read exposure of Model
        public string Path { get; private set; }
        /// <summary>
        /// Create a new Json Repo object.
        /// </summary>
        /// <param name="path">Path to repo</param>
        public JsonRepository(string path)
        {
            Path = path;
            _modelObject = new T();
            LoadModel();
        }
        /// <summary>
        /// Load or create a new JSON file.
        /// </summary>
        private void LoadModel()
        {
            if (!File.Exists(Path))
            {
                Console.WriteLine("File not found, File will be created when ModelObject is instantiated");
                return;
            }

            using(var sr = new StreamReader(Path))
            {
                dynamic model = JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(T));
                _modelObject = model;
            }
        }
        /// <summary>
        /// Update the ModelObject and save to file.
        /// </summary>
        /// <param name="updateObject">ModelObject</param>
        /// <returns>Success less than 1</returns>
        public short Update(T updateObject)
        {
            _modelObject = updateObject;
            return Save();
        }
        /// <summary>
        /// Save the file
        /// </summary>
        /// <returns>Success less than 1</returns>
        private short Save()
        {
            try
            {
                File.WriteAllText(Path, JsonConvert.SerializeObject(ModelObject));
                return 0;
            } catch
            {
                return 1;
            }
        }
    }
}
