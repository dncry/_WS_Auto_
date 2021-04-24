using NUnit.Framework;
using UnityEngine;
using WS.Auto;

namespace Tests
{
    public class CharExtensionTest
    {
        [Test]
        public void Char0()
        { 
            var result = '0'.ToInt();
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void Char9()
        {
            var result = '9'.ToInt();
            Assert.AreEqual(result, 9);
        }

        [Test]
        public void CharOther()
        {
             var a = 'A'.ToInt();
            Assert.AreEqual(a, 17);
        }
    }
}