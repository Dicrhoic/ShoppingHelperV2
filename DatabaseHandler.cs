using IronPdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;

namespace ShoppingHelperV2
{
    internal class DatabaseHandler : ShoppingClasses
    {
        public List<Item> wishListDB = new();
        public List<Item> cartDB = new();
        public Form origin = Application.OpenForms["Form1"];
        public DatabaseHandler()
        {
            Debug.WriteLine("DatabaseHandler Called");
        }

        public void InitlializeDatabase()
        {   
            string dir = Directory.GetCurrentDirectory();
            string dbFileName = "";
            string cartFileName = "";
            string? v = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (v is not null)
            {
                dbFileName = Path.Combine(v, @"Database\listDB.xml");
                cartFileName = Path.Combine(v, @"Database\cart.xml");
            }
            if (v is null)
            {
                Debug.WriteLine("File could not be found");
            }
            if (File.Exists(dbFileName))
            {
                Debug.WriteLine("That database exists already.");
                         
                origin = Application.OpenForms["Form1"];
                var panel1 = origin.Controls["sidePanel"];
                ComboBox wishListHolder = (ComboBox)panel1.Controls["wishListCB"];
                ReadXMLData(dbFileName, wishListHolder, wishListDB);
                RichTextBox listViewer = (RichTextBox)panel1.Controls["wishListHolder"];
                WriteToDataField(listViewer, wishListDB, "Wish List");
            }
            if (!File.Exists(dbFileName))
            {
                string fileName = "listDB.xml";
                string location = System.IO.Path.Combine(dir, "Database");
                string elementNode = "items";
                CreateXMLFile(location, fileName, elementNode);
            }

            Debug.WriteLine("Checking for cart");
            if(File.Exists(cartFileName))
            {
                Debug.WriteLine($"{cartFileName} exists already.");

                origin = Application.OpenForms["Form1"];
                var panel1 = origin.Controls["cartPanel"];
                //ComboBox wishListHolder = (ComboBox)panel1.Controls["wishListCB"];
                ReadXMLData(cartFileName, cartDB);
                RichTextBox listViewer = (RichTextBox)panel1.Controls["cartList"];
                WriteToDataField(listViewer, cartDB, "Cart");
            }
            if (!File.Exists(cartFileName))
            {
                string fileName = "cart.xml";
                string location = System.IO.Path.Combine(dir, "Database");
                string elementNode = "items";
                CreateXMLFile(location, fileName, elementNode);
            }
        }

        public static void CreateXMLFile(string location, string fileName, string rootElement)
        {

            string pathString = location;
            System.IO.Directory.CreateDirectory(pathString);
            Directory.SetCurrentDirectory(pathString);
            using XmlWriter writer = XmlWriter.Create(fileName);
            writer.WriteStartDocument();
            Debug.WriteLine("writing head");
            writer.WriteWhitespace("\n");
            writer.WriteStartElement(rootElement);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            Debug.WriteLine("Done writing");
        }

        public void ReadXMLData(string fileName, ComboBox cb, List<Item> db)
        {

            Debug.WriteLine($"Openning {fileName}");
            Stopwatch timer = Stopwatch.StartNew();
            XmlDocument doc = new();
            doc.PreserveWhitespace = true;
            MessageHandler messageHandler = new();
            try { doc.Load(fileName); }
            catch (System.IO.FileNotFoundException e)
            {
                string caption = $"Error openning file {fileName}";
                //LoadErrorPopUp(caption, e.Message);
            }
            if (!File.Exists(fileName))
            {
                string caption = "Failed to load database";
                string message = $"Check if db exists at {fileName}";
                messageHandler.LoadErrorPopUp(caption, message);
                return;
            }
            Debug.WriteLine($"{doc.InnerXml}");
            XmlNode root = doc.SelectSingleNode("items");
            if( root is null)
            {
                Debug.WriteLine("Error reading file");
                return;
            }
            Debug.WriteLine($"{doc}");
            try
            {
                XmlNodeList xnList = root.SelectNodes("item");
                int itemID = 0;
                db.Clear();
                var document = XDocument.Load(fileName);
                XElement xmlTree;
                if (document.Root is not null)
                {
                    xmlTree = document.Root;
                }
                //Console.Write(document.ToString());
                document.Descendants().Where(t => string.IsNullOrEmpty(t.Value)).Remove();
                doc.Save(fileName);
                if(xnList != null)
                {
                    foreach (XmlNode xn in xnList)
                    {   
                        Console.WriteLine("Loading data....");
                        string vendor = xn.Attributes["vendor"].Value;
                        string link = xn.Attributes["link"].Value;
                        string name = xn["name"].InnerText;
                        string price = xn["price"].InnerText;
                        string image = xn[name: "image"].InnerText;
                        Debug.WriteLine($"{vendor}, {link}, {name}, {price}, {image}");
                        Item product = new(Int32.Parse(price), name, vendor, link, image);
                        itemID++;
                        db.Add(product);
                        Debug.WriteLine($"index{itemID}");
                    }
                    Debug.WriteLine($"Loaded data {db.Count}");
                }               
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("XPath could not find an item", e.Message);
            }
            timer.Stop();
            TimeSpan timespan = timer.Elapsed;
            cb.DataSource = db.Select(d => d.Name).ToList();
        }

        public void ReadXMLData(string fileName, List<Item> db)
        {

            Debug.WriteLine($"Openning {fileName}");
            XmlDocument doc = new();
            doc.PreserveWhitespace = true;
            MessageHandler messageHandler = new();
            try { doc.Load(fileName); }
            catch (System.IO.FileNotFoundException e)
            {
                string caption = $"Error openning file {fileName}";
                //LoadErrorPopUp(caption, e.Message);
            }
            if (!File.Exists(fileName))
            {
                string caption = "Failed to load database";
                string message = $"Check if db exists at {fileName}";
                messageHandler.LoadErrorPopUp(caption, message);
                return;
            }
            Debug.WriteLine($"{doc.InnerXml}");
            XmlNode root = doc.SelectSingleNode("items");
            if (root is null)
            {
                Debug.WriteLine("Error reading file");
                return;
            }
            Debug.WriteLine($"{doc}");
            try
            {
                XmlNodeList xnList = root.SelectNodes("item");
                int itemID = 0;
                db.Clear();
                var document = XDocument.Load(fileName);
                XElement xmlTree;
                if (document.Root is not null)
                {
                    xmlTree = document.Root;
                }
                //Console.Write(document.ToString());
                document.Descendants().Where(t => string.IsNullOrEmpty(t.Value)).Remove();
                doc.Save(fileName);
                if (xnList != null)
                {
                    foreach (XmlNode xn in xnList)
                    {
                        Console.WriteLine("Loading data....");
                        string vendor = xn.Attributes["vendor"].Value;
                        string link = xn.Attributes["link"].Value;
                        string name = xn["name"].InnerText;
                        string price = xn["price"].InnerText;
                        string image = xn[name: "image"].InnerText;
                        Debug.WriteLine($"{vendor}, {link}, {name}, {price}, {image}");
                        Item product = new(Int32.Parse(price), name, vendor, link, image);
                        itemID++;
                        db.Add(product);
                        Debug.WriteLine($"index{itemID}");
                    }
                    Debug.WriteLine($"Loaded data {db.Count}");
                }
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("XPath could not find an item", e.Message);
            }
        }
        public void WriteToDataField(RichTextBox textBox, List<Item> database, string fileType)
        {
            Debug.WriteLine("Writer to list called");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            string contents = $"{fileType} Items: \n";
            int count = 1;
            foreach (var product in database)
            {
                string cost = "";
                cost = product.price.ToString("c");
                if (product.price == 0)
                {
                    cost = "Item is sold out";
                }

                string item = $"{count}. {product.Name}\nVendor:{product.Vendor}\n" +
                    $"Price: {cost}\nLink: {product.link}\n\n";
                contents += item;
                count++;
            }
            textBox.Text = contents;
        }

        public static void ConvertDBToJSON(List<Item> dataBase)
        {
            MessageHandler messageHandler = new();
            string? v = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string jsonString = "";
            string fileName = "listDB.json";
            if (v != null)
            {
                Debug.WriteLine("Preparing to write to file");
                string path = Path.Combine(v, @"Database\listDB.json");
                if (!File.Exists(path))
                {
                    string caption = "Failed to load database";
                    string message = $"Check if db exists at {path}";
                    messageHandler.LoadErrorPopUp(caption, message);
                    return;
                }

                foreach (var product in dataBase)
                {
                    jsonString += JsonSerializer.Serialize(product);
                }

            }
            File.WriteAllText(fileName, jsonString);
        }

        public void CreateCheckoutFile(List<int> priceList, BackgroundWorker worker)
        {
            if(cartDB.Count > 0)
            {
                ProgressBar progressBarForm;
                progressBarForm = new();
                progressBarForm.Show();
                
                int finalPercentage = (100/cartDB.Count) - 10;
                int count = 0;
                string dir = Directory.GetCurrentDirectory();
                string dest = System.IO.Path.Combine(dir, "Purchases");
                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                }
                Directory.SetCurrentDirectory(dest);

                DateTime currentDT = DateTime.Now;
                DateTime dateOnly = currentDT.Date;
                string fileName = currentDT.ToString("dd-MM-yyyy-HH-mm");
                string title = dateOnly.ToString("D");
                string header = "Purchase for " + title;
                int index = 0;
                string path = fileName + ".html";
                string formTitle = $"Checkout file {path}";
                progressBarForm.SetCaption(formTitle);
                using (FileStream fs = File.Create(path))
                {

                }
                using (StreamWriter sw = File.CreateText(path))
                {
                    HtmlTextWriter htmlTextWriter = new(sw);
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Html);
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Head);
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Title);
                    htmlTextWriter.Write(fileName);
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Style);
                    htmlTextWriter.Write(CSSFormat());
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.H1);
                    htmlTextWriter.Write(header);
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Table);
                    htmlTextWriter.AddAttribute("id", "receipt");
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlTextWriter.AddAttribute("id", "headerRow");

                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                    htmlTextWriter.Write("Item name");
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();

                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                    htmlTextWriter.Write("Vendor");
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();

                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                    htmlTextWriter.Write("Price");
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();

                    htmlTextWriter.RenderEndTag();

                    foreach (var item in cartDB)
                    {
                        string caption = $"Adding {item.Name} to reciept";
                        htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                        htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlTextWriter.WriteBeginTag("a");
                        htmlTextWriter.WriteAttribute("href", item.link);
                        htmlTextWriter.Write(">");
                        htmlTextWriter.Write(item.Name);
                        htmlTextWriter.WriteEndTag("a");
                        htmlTextWriter.RenderEndTag();
                        htmlTextWriter.WriteLine();

                        htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlTextWriter.Write(item.Vendor);
                        htmlTextWriter.RenderEndTag();
                        htmlTextWriter.WriteLine();

                        htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        string priceValue = "Item Sold Out";
                        if (priceList[index] != -1)
                        {
                            priceValue = priceList[index].ToString("c");
                        }
                        htmlTextWriter.Write(priceValue);
                        htmlTextWriter.RenderEndTag();
                        htmlTextWriter.WriteLine();

                        htmlTextWriter.RenderEndTag();
                        index++;
                        count += finalPercentage;
                        progressBarForm.SetCaption(caption);
                        worker.ReportProgress(count);
                        progressBarForm.BarStepUp();
                    }
                    //sum section
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlTextWriter.AddAttribute("id", "totalRow");
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlTextWriter.Write($"Total Cost of {priceList.Count} items");
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlTextWriter.Write("");
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlTextWriter.Write(priceList.Sum().ToString("c"));
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();
                    htmlTextWriter.Close();
                    worker.ReportProgress(100);
                }
                //string message = "Created file " + path + " in " + dest;
                //string caption = "File made";
                ConvertToPDF(path, fileName);
                //OpenFilePrompt(path);
               
            }          
        }

        public async Task<bool> CheckoutFileCreated(List<int> priceList, BackgroundWorker worker)
        {
            ProgressBar progressBarForm;
            progressBarForm = new();
            progressBarForm.Show();

            int finalPercentage = (100 / cartDB.Count) - 10;
            int count = 0;
            string dir = Directory.GetCurrentDirectory();
            string dest = System.IO.Path.Combine(dir, "Purchases");
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }
            Directory.SetCurrentDirectory(dest);

            DateTime currentDT = DateTime.Now;
            DateTime dateOnly = currentDT.Date;
            string fileName = currentDT.ToString("dd-MM-yyyy-HH-mm");
            string title = dateOnly.ToString("D");
            string header = "Purchase for " + title;
            int index = 0;
            string path = fileName + ".html";
            string formTitle = $"Checkout file {path}";
            progressBarForm.SetCaption(formTitle);
            using (FileStream fs = File.Create(path))
            {

            }
            using (StreamWriter sw = File.CreateText(path))
            {
                HtmlTextWriter htmlTextWriter = new(sw);
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Html);
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Head);
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Title);
                htmlTextWriter.Write(fileName);
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Style);
                htmlTextWriter.Write(CSSFormat());
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.H1);
                htmlTextWriter.Write(header);
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Table);
                htmlTextWriter.AddAttribute("id", "receipt");
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlTextWriter.AddAttribute("id", "headerRow");

                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlTextWriter.Write("Item name");
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.WriteLine();

                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlTextWriter.Write("Vendor");
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.WriteLine();

                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlTextWriter.Write("Price");
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.WriteLine();

                htmlTextWriter.RenderEndTag();

                foreach (var item in cartDB)
                {
                    string caption = $"Adding {item.Name} to reciept";
                    Debug.WriteLine(caption);
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlTextWriter.WriteBeginTag("a");
                    htmlTextWriter.WriteAttribute("href", item.link);
                    htmlTextWriter.Write(">");
                    htmlTextWriter.Write(item.Name);
                    htmlTextWriter.WriteEndTag("a");
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();

                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlTextWriter.Write(item.Vendor);
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();

                    htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    string priceValue = "Item Sold Out";
                    if (priceList[index] != -1)
                    {
                        priceValue = priceList[index].ToString("c");
                    }
                    htmlTextWriter.Write(priceValue);
                    htmlTextWriter.RenderEndTag();
                    htmlTextWriter.WriteLine();

                    htmlTextWriter.RenderEndTag();
                    index++;
                    count += finalPercentage;
                    progressBarForm.SetCaption(caption);
                    worker.ReportProgress(count);
                    await Task.Delay(50);
                    progressBarForm.BarStepUp();
                }
                //sum section
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlTextWriter.AddAttribute("id", "totalRow");
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlTextWriter.Write($"Total Cost of {priceList.Count} items");
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.WriteLine();
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlTextWriter.Write("");
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.WriteLine();
                htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                htmlTextWriter.Write(priceList.Sum().ToString("c"));
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.RenderEndTag();
                htmlTextWriter.WriteLine();
                htmlTextWriter.Close();
                worker.ReportProgress(100);
                progressBarForm.Close();
                ConvertToPDF(path, fileName);
                OpenFilePrompt(path);
                await Task.Delay(200);
            }
                return true;
        }

        private string CSSFormat()
        {
            string format = " h1 {\r\n    " +
                "        text-align: left;\r\n" +
                "            font-family: 'Trebuchet MS', 'Lucida Sans Unicode'," +
                " 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;\r\n" +
                "        }\r\n\r\n        " +
                "h2 {\r\n            " +
                "text-align: left;\r\n            " +
                "font-family: Georgia, 'Times New Roman', Times, serif;\r\n" +
                "        }\r\n\r\n" +
                "        h3 {\r\n" +
                "            text-align: right;\r\n\r\n" +
                "        }\r\n\r\n        " +
                "#receipt {\r\n            " +
                "font-family: \"ヒラギノ角ゴ Pro W3\", sans-serif;\r\n            " +
                "border-collapse: collapse;\r\n            " +
                "width: 100%;\r\n        }\r\n\r\n        " +
                "td,\r\n        th {\r\n            " +
                "border: 1px solid #ddd;\r\n            " +
                "padding: 8px;\r\n        }\r\n\r\n        " +
                "tr:nth-child(even) {\r\n            " +
                "background-color: #f2f2f2;\r\n        " +
                "}\r\n\r\n        tr:hover {\r\n            " +
                "background-color: #ddd;\r\n        }\r\n\r\n        " +
                "#customers th {\r\n            " +
                "padding-top: 12px;\r\n            " +
                "padding-bottom: 12px;\r\n            " +
                "text-align: left;\r\n            " +
                "background-color: #04AA6D;\r\n            " +
                "color: white;\r\n        }\r\n        " +
                "#totalRow\r\n        {\r\n            " +
                "background-color: #294269;\r\n            " +
                "color: white;\r\n        }\r\n        " +
                "#headerRow\r\n        {\r\n            " +
                "background-color: #3c63a3;\r\n            " +
                "color: white;\r\n        }";
            return format;
        }

        public void OpenFilePrompt(string path)
        {
            string message = "Would you like to open the file " + path + " ?";
            string caption = "File:" + path + " made";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = path;
                psi.UseShellExecute = true;
                Process.Start(psi);
            }
        }

        public void ConvertToPDF(string xmlFile, string outputName)
        {
            var Renderer = new IronPdf.ChromePdfRenderer();
            Renderer.RenderingOptions.MarginTop = 50;  //millimeters
            Renderer.RenderingOptions.MarginBottom = 50;
            Renderer.RenderingOptions.CssMediaType = IronPdf.Rendering.PdfCssMediaType.Print;
            Renderer.RenderingOptions.TextHeader = new TextHeaderFooter()
            {
                CenterText = "{pdf-title}",
                DrawDividerLine = true,
                FontSize = 16
            };
            Renderer.RenderingOptions.TextFooter = new TextHeaderFooter()
            {
                LeftText = "{date} {time}",
                RightText = "Page {page} of {total-pages}",
                DrawDividerLine = true,
                FontSize = 14
            };

            var PDF = Renderer.RenderHtmlFileAsPdf(xmlFile);
            PDF.SaveAs($"{outputName}.pdf");
        }

        public string CheckoutRecieptDirectory()
        {
            string dir = Directory.GetCurrentDirectory();
            string dest = System.IO.Path.Combine(dir, "Purchases");
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }
            Directory.SetCurrentDirectory(dest);
            return dest;

        }
    }
}
