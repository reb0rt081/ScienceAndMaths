﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScienceAndMaths.Configuration.Canals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ScienceAndMaths.Configuration.Converter;
using System.Text.RegularExpressions;

namespace ScienceAndMaths.Configuration.Test
{
    [TestClass]
    public class CanalConfigurationTest
    {
        [TestMethod]
        public void RegularExpressionTest()
        {
            List<int> solutions = new List<int> { 1238, 1812, 1936, 1937 };
            string textToFilter =
                "Valencia was founded as a Roman colony by the consul Decimus Junius Brutus Callaicus in 138 BC and called Valentia Edetanorum. In 714, Moroccan and Arab Moors occupied the city, introducing their language, religion and customs; they implemented improved irrigation systems and the cultivation of new crops as well. Valencia was the capital of the Taifa of Valencia. In 1238 the Christian king James I of Aragon conquered the city and divided the land among the nobles who helped him conquer it, as witnessed in the Llibre del Repartiment. He also created the new Kingdom of Valencia, which had its own laws (Furs), with Valencia as its main city and capital. In the 18th century Philip V of Spain abolished the privileges as punishment to the kingdom of Valencia for aligning with the Habsburg side in the War of the Spanish Succession. Valencia was the capital of Spain when Joseph Bonaparte moved the Court there in the summer of 1812. It also served as the capital between 1936 and 1937, during the Second Spanish Republic.";
            string regexPattern = @"[\d+]{4}";
            Regex regex = new Regex(regexPattern);
            MatchCollection result = regex.Matches(textToFilter);

            for (int count = 0; count < result.Count; count++)
            {
                int year = Convert.ToInt32(result[count].Value);
                Console.WriteLine($"Regex result: {year}");
                Assert.IsTrue(solutions.Contains(year));
            }

            Assert.AreEqual(4, result.Count);            
        }

        [TestMethod]
        public void RectangularSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();            
            canalConfiguration.ConfigId = "TestConfig";
            
            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode"
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.028,
                Slope = 0.001,
                Width = 5.0
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void CombinedRectangularSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode"
            };
            CanalNode midNode = new CanalNode()
            {
                NodeId = "MiddleNode"
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow1 = new CanalArrow()
            {
                ArrowId = "Arrow1",
                Length = 600.0,
                Flow = 2.5486840725260249,
                FromNodeId = initNode.NodeId,
                ToNodeId = midNode.NodeId
            };
            arrow1.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.014,
                Slope = 0.0004,
                Width = 4.0
            };

            CanalArrow arrow2 = new CanalArrow()
            {
                ArrowId = "Arrow2",
                Length = 600.0,
                Flow = 2.5486840725260249,
                FromNodeId = midNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow2.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.014,
                Slope = 0.004,
                Width = 4.0
            };

            canalConfiguration.Arrows = new List<CanalArrow>() { arrow1, arrow2 };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, midNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(3, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(2, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(3, data.CanelEdges.Count);
            Assert.AreEqual(2, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(3, canalConfiguration.Nodes.Count);
            Assert.AreEqual(2, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void RectangularWithSluidgeGateSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            SluiceGateNode initNode = new SluiceGateNode()
            {
                NodeId = "InitNode",
                ContractionCoefficient = 0.8,
                GateOpening = 0.2125,
                RetainedWaterLevel = 2
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.025,
                Slope = 0.0036,
                Width = 6.0
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void RectangularWithSluidgeGateCompositeSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            SluiceGateNode initNode = new SluiceGateNode()
            {
                NodeId = "InitNode",
                ContractionCoefficient = 0.8,
                GateOpening = 0.125,
                RetainedWaterLevel = 2,
                GateWidth = 2
            };

            CanalNode middleNode = new CanalNode()
            {
                NodeId = "MiddleNode"
            };

            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow1 = new CanalArrow()
            {
                ArrowId = "TestArrow1",
                Length = 600.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = middleNode.NodeId
            };
            arrow1.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.014,
                Slope = 0.0004,
                Width = 2.0
            };

            CanalArrow arrow2 = new CanalArrow()
            {
                ArrowId = "TestArrow2",
                Length = 600.0,
                FromNodeId = middleNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow2.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.014,
                Slope = 0.0045,
                Width = 2.0
            };

            canalConfiguration.Arrows = new List<CanalArrow>() { arrow1, arrow2 };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, middleNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(3, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(2, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(3, data.CanelEdges.Count);
            Assert.AreEqual(2, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(3, canalConfiguration.Nodes.Count);
            Assert.AreEqual(2, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void RectangularSerializingWithWaterLevelTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode",
                WaterLevel = 2.9
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.028,
                Slope = 0.001,
                Width = 5.0
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.IsTrue(data.CanelEdges.Any(ce => ce.WaterLevel.HasValue && ce.WaterLevel.Value == 2.9));
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void RectangularSerializingWithWaterLevelAndS1Test()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode"
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode",
                WaterLevel = 2.9
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 900.0,
                Flow = 50.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.028,
                Slope = 0.001,
                Width = 5.0
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.IsTrue(data.CanelEdges.Any(ce => ce.WaterLevel.HasValue && ce.WaterLevel.Value == 2.9));
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
            Assert.AreEqual(0.001, canalConfiguration.Arrows.Single().CanalSection.Slope);
        }

        [TestMethod]
        public void RectangularSerializingWithWaterLevelAndS3Test()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            SluiceGateNode initNode = new SluiceGateNode()
            {
                NodeId = "InitNode",
                ContractionCoefficient = 0.8,
                GateOpening = 0.2125,
                GateWidth = 4.0,
                RetainedWaterLevel = 2
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new RectangularSectionConfiguration()
            {
                Roughness = 0.025,
                Slope = 0.02,
                Width = 6.0
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);

            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration = (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }

        [TestMethod]
        public void TrapezoidSerializingTest()
        {
            //  Arrange
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CanalConfiguration));
            CanalConfiguration canalConfiguration = new CanalConfiguration();
            canalConfiguration.ConfigId = "TestConfig";

            CanalNode initNode = new CanalNode()
            {
                NodeId = "InitNode"
            };
            CanalNode endNode = new CanalNode()
            {
                NodeId = "EndNode"
            };
            CanalArrow arrow = new CanalArrow()
            {
                ArrowId = "TestArrow",
                Length = 500.0,
                FromNodeId = initNode.NodeId,
                ToNodeId = endNode.NodeId
            };
            arrow.CanalSection = new TrapezoidalSectionConfiguration()
            {
                Roughness = 0.028,
                Slope = 0.001,
                Width = 5.0,
                Incline = 0.5
            };
            canalConfiguration.Arrows = new List<CanalArrow>() { arrow };
            canalConfiguration.Nodes = new List<CanalNode>() { initNode, endNode };
            var xml = "";

            //  Act
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, canalConfiguration);
                    xml = sww.ToString(); // Your XML
                }
            }

            //  Assert
            Assert.IsTrue(xml != null);
            CanalConfiguration deserializedConfiguration = null;

            using (StringReader sr = new StringReader(xml))
            {
                deserializedConfiguration =  (CanalConfiguration)xmlSerializer.Deserialize(sr);
            }

            Assert.IsTrue(deserializedConfiguration != null);
            Assert.AreEqual(2, deserializedConfiguration.Nodes.Count);
            Assert.AreEqual(1, deserializedConfiguration.Arrows.Count);

            CanalConfigurationConverter converter = new CanalConfigurationConverter();

            var data = converter.Convert(deserializedConfiguration);

            Assert.IsTrue(data != null);
            Assert.AreEqual(2, data.CanelEdges.Count);
            Assert.AreEqual(1, data.CanalStretches.Count);

            canalConfiguration = converter.ConvertBack(data);

            Assert.IsTrue(canalConfiguration != null);
            Assert.AreEqual(2, canalConfiguration.Nodes.Count);
            Assert.AreEqual(1, canalConfiguration.Arrows.Count);
        }
    }
}
