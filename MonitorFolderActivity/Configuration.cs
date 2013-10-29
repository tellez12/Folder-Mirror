using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MonitorFolderActivity
{
    [Serializable()]
    public class Configuration
    {

        public string Name { get; set; } // Configuration Name
        public string MonitoredFolder { get; set; }
        public string TargetFolder { get; set; }

        [XmlIgnore]        
        private static List<Configuration> _configurations;


        [XmlIgnore]        
        public static List<Configuration> Configurations
        {
            get {
                if (_configurations == null)
                    _configurations = Configuration.GetConfiguration();
                
                return _configurations; }
            set { _configurations = value; }
        }

        private static List<Configuration> GetConfiguration()
        {
            List<Configuration> ConfList;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Configuration>));
                StreamReader reader = new StreamReader("configurations.xml");
                ConfList = (List<Configuration>)serializer.Deserialize(reader);
                reader.Close();      

            }
            catch (Exception)
            {
                ConfList = new List<Configuration>();
                ConfList.Add(new Configuration { Name = "", MonitoredFolder = "", TargetFolder = "" });            
                SaveConfiguration();
              
            }
            

            return ConfList;
        }


        public static bool SaveConfiguration()
        {
         
            bool saved;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Configuration>));
                TextWriter writer = new StreamWriter("configurations.xml");
                using (writer)
                {
                    serializer.Serialize(writer, Configurations);
                    writer.Close();
                    
                }
                saved = true;
            }
            catch (Exception)
            {
                saved = false;

            }
            return saved;
        }        
        

        

    }
}
