using NUnit.Framework;
using UnityEngine;
using WS.Auto;

namespace Tests
{
    public class StringExtensionTest
    {
        [Test]
        public void ToColor()
        {
            var color1 = "#a".ToColor();
            Assert.AreEqual(Color.white, color1);

            var color2 = "#000000".ToColor();
            Assert.AreEqual(Color.black, color2);

            var color3 = "#FFFFFF".ToColor();
            Assert.AreEqual(Color.white, color3);

            var color4 = "#0000FF".ToColor();
            Assert.AreEqual(Color.blue, color4);

            //Color.RGBToHSV(color4 , );
        }

        [Test]
        public void ToChildPath()
        {
            var a = new GameObject();
            var b = new GameObject();
            var c = new GameObject();

            a.name = "A";
            b.name = "B";
            c.name = "C";

            c.transform.parent = b.transform;
            b.transform.parent = a.transform;

            var testPath1 = "C".ToChildPath(a.transform);
            Assert.AreEqual("B/C", testPath1);

            var testPath2 = "A".ToChildPath(a.transform);
            Assert.AreEqual(null, testPath2);

            var testPath3 = "B".ToChildPath(a.transform);
            Assert.AreEqual("B", testPath3);

            var testPath4 = "C".ToChildPath(null);
            Assert.AreEqual(null, testPath4);

            var testPath5 = "D".ToChildPath(a.transform);
            Assert.AreEqual(null, testPath5);
        }

        [Test]
        public void ToChildTransform()
        {
            var a = new GameObject();
            var b = new GameObject();
            var c = new GameObject();

            a.name = "A";
            b.name = "B";
            c.name = "C";

            c.transform.parent = b.transform;
            b.transform.parent = a.transform;

            var testPath1 = "C".ToChildTransform(a.transform);
            Assert.AreEqual(c.transform, testPath1);

            var testPath2 = "B".ToChildTransform(a.transform);
            Assert.AreEqual(b.transform, testPath2);

            var testPath3 = "A".ToChildTransform(a.transform);
            Assert.AreEqual(null, testPath3);

            var testPath4 = "C".ToChildTransform(null);
            Assert.AreEqual(null, testPath4);
            
            var testPath5 = "D".ToChildTransform(a.transform);
            Assert.AreEqual(null, testPath5);
        }
    }
}