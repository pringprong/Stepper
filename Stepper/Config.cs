using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Stepper
{
	[Serializable] public sealed class Config
	{

		public string DefaultSourceFolder { get; set; }
		public string DefaultDestinationFolder { get; set; }
		[XmlIgnore] public string notSerialized { get; set; }

		private static Config config_instance = new Config();
		

		private Config() {
			DefaultSourceFolder = "C:\\Games\\StepMania 5\\Songs\\Test";
			DefaultDestinationFolder = "C:\\Games\\StepMania 5\\Songs\\Test";
		}

		public static Config Instance
		{
			get
			{
				return config_instance;
			}
		}

		public static void Save()
		{
			using (var stream = new FileStream("C://Users//Public//Documents//xml.xml", FileMode.Create))
			{
				var XML = new XmlSerializer(typeof(Config));
				XML.Serialize(stream, config_instance);
			}
		}

		public static void Load()
		{
			Config temp_config;
			using (var stream = new FileStream("C://Users//Public//Documents//xml.xml", FileMode.Open))
			{
				var XML = new XmlSerializer(typeof(Config));
				temp_config = (Config)XML.Deserialize(stream);
			}
			config_instance.DefaultDestinationFolder = temp_config.DefaultDestinationFolder;
			config_instance.DefaultSourceFolder = temp_config.DefaultSourceFolder;
		}
	}
}
