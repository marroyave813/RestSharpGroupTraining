using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RestSharpGroupTraining.Model.XmlModel
{
	[XmlRoot(ElementName = "slide")]
	public class Slide
	{
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}
}
