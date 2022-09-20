using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;

namespace ShoppingHelperV2
{
    internal class ItemHandler : ShoppingClasses
    {
        public void WriteToLocalDB(List<Item> dataBase, 
            Item product, string fileName, string dbName)
        {
            Debug.WriteLine("Called");
          MessageHandler handler = new();
            string message = $"Would you like to add {product.Name} to your {dbName}?";
            string title = $"Adding item to {dbName} confirmation";
            var answer = handler.ConfirmationPopUp(title, message);
            Debug.WriteLine(answer);
            if(answer)
            {
                WriteToDB(product, fileName);
            }
        }

        public static void WriteToDB(Item product, string fileName)
        {
            Debug.WriteLine("Writer called");
            MessageHandler messageHandler = new();
            string pathEnd = @"Database\" + fileName;
            //string? v = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), pathEnd);
            string? v = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          
            if (v != null)
            {
                Debug.WriteLine("Preparing to write to file");
                string path = Path.Combine(v, fileName);
                XmlDocument doc = new()
                {
                    PreserveWhitespace = true
                };
                if(!File.Exists(path))
                {
                    string caption = "Failed to load database";
                    string message = $"Check if db exists at {path}";
                    messageHandler.LoadErrorPopUp(caption, message);
                    return;
                }
                try { doc.Load(path); }
                catch (System.IO.FileNotFoundException e)
                {
                    string caption = "Failed to load database";
                    messageHandler.LoadErrorPopUp(caption, e.Message);
                }
                try
                {
                    XmlNode? root = doc.SelectSingleNode("items");
                    if (root is not null)
                    {
                        using XmlWriter xmlwriter = root.CreateNavigator().AppendChild();

                        Debug.WriteLine("Appending...");
                        xmlwriter.WriteStartElement("item");
                        xmlwriter.WriteAttributeString("link", product.link);
                        xmlwriter.WriteAttributeString("vendor", product.Vendor);
                        xmlwriter.WriteWhitespace("\n\t");
                        xmlwriter.WriteElementString("name", product.Name);
                        xmlwriter.WriteWhitespace("\n\t");
                        xmlwriter.WriteElementString("image", product.image);
                        xmlwriter.WriteWhitespace("\n\t");
                        xmlwriter.WriteElementString("price", product.price.ToString());
                        xmlwriter.WriteWhitespace("\n\t");
                        xmlwriter.WriteEndElement();
                        xmlwriter.WriteWhitespace("\n");
                    }
                    Debug.WriteLine("Supposingly appended");
                    doc.Save(path);
                    string message = $"Added {product.Name} to wishlist!";
                    string caption = "Item added successfully";
                    
                }
                catch (Exception ex)
                {
                    string caption = "Failed to write to database";
                    messageHandler.LoadErrorPopUp(caption, ex.Message);
                }

            }
        }       
    }
}
