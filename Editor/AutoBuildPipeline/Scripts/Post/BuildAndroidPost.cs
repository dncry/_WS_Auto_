/*----------------------------------------------------------------
** Creator：Dncry
** Time：2024年11月19日 星期二 09:58
----------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor.Android;
using UnityEngine;

namespace WS.Auto
{
    public class BuildAndroidPost : IPostGenerateGradleAndroidProject
    {
        private static readonly XNamespace AndroidNamespace = "http://schemas.android.com/apk/res/android";

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            // Path.Combine(path, "../gradle.properties");

            var launcherManifestPath = Path.Combine(path, "../launcher/src/main/AndroidManifest.xml");

            Debug.Log($"######################### {launcherManifestPath} #####################");

            ProcessAndroidManifest(launcherManifestPath);
        }

        public int callbackOrder
        {
            get { return int.MaxValue; }
        }

        private static void ProcessAndroidManifest(string path)
        {
            var manifestPath = path;

            XDocument manifest;
            try
            {
                manifest = XDocument.Load(manifestPath);
            }
#pragma warning disable 0168
            catch (IOException exception)
#pragma warning restore 0168
            {
                Debug.LogWarning("AndroidManifest.xml is missing.");
                return;
            }

            // Get the `manifest` element.
            var elementManifest = manifest.Element("manifest");
            if (elementManifest == null)
            {
                Debug.LogWarning("AndroidManifest.xml is invalid.");
                return;
            }

            DeleteManifestPackage(elementManifest);

            var elementApplication = elementManifest.Element("application");
            if (elementApplication == null)
            {
                Debug.LogWarning("AndroidManifest.xml is invalid.");
                return;
            }

            var metaDataElements = elementApplication.Descendants()
                .Where(element => element.Name.LocalName.Equals("meta-data"));

            // Save the updated manifest file.
            manifest.Save(manifestPath);
        }


        private static void DeleteManifestPackage(XElement elementApplication)
        {
#if !AUTO_FIX_API_35
            return;
#endif

            var p = elementApplication.Attribute("package");

            Debug.Log($"######################### {p} #####################");
            if (p != null)
            {
                p.Remove();
            }
        }

        /// <summary>
        /// Creates and returns a <c>meta-data</c> element with the given name and value. 
        /// </summary>
        private static XElement CreateMetaDataElement(string name, object value)
        {
            var metaData = new XElement("meta-data");
            metaData.Add(new XAttribute(AndroidNamespace + "name", name));
            metaData.Add(new XAttribute(AndroidNamespace + "value", value));

            return metaData;
        }

        /// <summary>
        /// Looks through all the given meta-data elements to check if the required one exists. Returns <c>null</c> if it doesn't exist.
        /// </summary>
        private static XElement GetMetaDataElement(IEnumerable<XElement> metaDataElements, string metaDataName)
        {
            foreach (var metaDataElement in metaDataElements)
            {
                var attributes = metaDataElement.Attributes();
                if (attributes.Any(attribute => attribute.Name.Namespace.Equals(AndroidNamespace)
                                                && attribute.Name.LocalName.Equals("name")
                                                && attribute.Value.Equals(metaDataName)))
                {
                    return metaDataElement;
                }
            }

            return null;
        }
    }
}