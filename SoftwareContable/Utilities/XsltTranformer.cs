using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using SoftwareContable.Extensions;
using SoftwareContable.Models;

namespace SoftwareContable.Utilities
{
    public static class XsltTranformer
    {
        public static string GetHtmlFromXslt<TModel>(this TModel model, string xsltFilePath, Dictionary<string, object> parameters = null)
            where TModel : IModel
        {
            var serializedModel = model.Serialize();
            var xsltOutput = new StringBuilder();

            using (var writer = XmlWriter.Create(xsltOutput))
            {
                var xslTransfomer = new XslCompiledTransform();
                var xsltArguments = new XsltArgumentList();
                var xmlDocument = new XmlDocument();

                if (parameters != null)
                {
                    foreach (var paramter in parameters)
                    {
                        xsltArguments.AddParam(paramter.Key, string.Empty, paramter.Value);
                    }
                }

                xslTransfomer.Load(xsltFilePath);

                xmlDocument.LoadXml(serializedModel);

                xslTransfomer.Transform(xmlDocument, xsltArguments, writer);
            }

            return xsltOutput.ToString();
        }
    }
}