using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RestSharpGroupTraining.Model.XmlModel
{
	[XmlRoot(ElementName = "slideshow")]
	public class Slideshow
	{
		[XmlElement(ElementName = "slide")]
		public List<Slide> Slide { get; set; }
		[XmlAttribute(AttributeName = "title")]
		public string Title { get; set; }
		[XmlAttribute(AttributeName = "date")]
		public string Date { get; set; }
		[XmlAttribute(AttributeName = "author")]
		public string Author { get; set; }
	}
}
