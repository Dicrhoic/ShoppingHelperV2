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
                WriteToDB(product, fileName, dataBase, dbName);
            }
        }

        public static void WriteToDB(Item product, string fileName, List<Item> database, string dbName)
        {
            Debug.WriteLine("Writer called");
            MessageHandler messageHandler = new();
            string pathEnd = @"Database\" + fileName;
            //string? v = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), pathEnd);
            string? v = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          
            if (v != null)
            {
                string truePath = @"" + fileName;
                Debug.WriteLine("Preparing to write to file");
                string path = Path.Combine(v, pathEnd);
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
                    database.Add(product);
                }
                catch (Exception ex)
                {
                    string caption = "Failed to write to database";
                    messageHandler.LoadErrorPopUp(caption, ex.Message);
                }

            }
            Form origin = Application.OpenForms["Form1"];
            if (origin != null)
            {
                DatabaseHandler databaseHandler = new DatabaseHandler();
                switch (dbName)
                {
                    case "Wish List":
                        var panel1 = origin.Controls["sidePanel"];
                        RichTextBox listViewer = (RichTextBox)panel1.Controls["wishListHolder"];
                        databaseHandler.WriteToDataField(listViewer, database, dbName);
                        Debug.WriteLine("Updated?");
                        break;
                    case "Cart":
                        var panel2 = origin.Controls["cartPanel"];
                        RichTextBox listViewer2 = (RichTextBox)panel2.Controls["cartList"];
                        databaseHandler.WriteToDataField(listViewer2, database, dbName);
                        break;
                }
            }
           
           
        }       
    }
}
