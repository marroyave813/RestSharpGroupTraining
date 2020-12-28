using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RestSharpGroupTraining.Model.XmlModel
{
	[XmlRoot(ElementName = "item")]
	public class Item
	{
		[XmlElement(ElementName = "em")]
		public string Em { get; set; }
	}
}
