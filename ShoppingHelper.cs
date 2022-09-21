using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ShoppingHelperV2
{
    internal class ShoppingHelper
    {
        public MessageHandler msgHndler = new();
        ItemHandler itemHndler = new();  
        public static readonly HttpClient client = new();
        private string htmlData = "";
        readonly string surugaya = "www.suruga-ya.jp";
        public Form origin = Application.OpenForms["Form1"];
        public ShoppingClasses.Item? currentItem = null;
        public async Task<bool> WebPageIsValid(string passedURL)
        {
            Debug.WriteLine("Task is running");
            bool responseRecieved = Uri.TryCreate(passedURL, UriKind.Absolute, out Uri myUri);
            if (responseRecieved)
            {
                try
                {
                    string responseBody = await client.GetStringAsync(passedURL);
                    Debug.WriteLine("Link {0} is valid", passedURL);
                    htmlData = responseBody;
                    return true;
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("Exception caught\nMessage :{0} ", e.Message);
                    return false;
                }
            }
            string caption = "Failed to load data from URL";
            string message = $"Please check that {passedURL} has been entered correctly";
            msgHndler.LoadErrorPopUp(caption, message);
            return false;
        }

        public void LayoutAnalysis()
        {
            Debug.WriteLine("Running");
            origin = Application.OpenForms["Form1"];
            if (origin != null)
            {
                Debug.WriteLine($"Form1 contents: {origin.Controls} could be found");
                foreach (Control control in origin.Controls)
                {
                    var home = control.Controls;
                    Debug.WriteLine($"Inner 1 Control: {control.Name} could be found");
                    foreach (Control control1 in home)
                    {
                        Debug.WriteLine($"Inner 2 Control: {control1.Name} could be found");
                    }
                   
                }

            }
        }

        public void DisplayProduct(ShoppingClasses.Item item)
        {   
            origin = Application.OpenForms["Form1"];
            var panel1 = origin.Controls["productPanel"];
            PictureBox imageHolder = (PictureBox)panel1.Controls["productImage"];
            Label priceHolder = (Label)panel1.Controls["priceTag"];
            priceHolder.Text = item.price.ToString("c");
            if(item.price == 0)
            {
                priceHolder.Text = "Item is sold out";
            }
            Debug.WriteLine($"Price retrieved: {item.price}");
            var infoHolder = panel1.Controls["productInfo"];
            string productInfo = item.Name;
            infoHolder.Text = productInfo;
            string imageLink = item.image;
            imageHolder.Load(imageLink);
        }

        public async Task<bool> RetrieveDataFromURI(string passedURL)
        {
            Debug.WriteLine($"Retrieving data from {passedURL}");
            var task = await (WebPageIsValid(passedURL));
            if (task)
            {
                Debug.WriteLine(SiteRequestor(passedURL));
                if (string.Compare(SiteRequestor(passedURL), surugaya) == 0)
                {
                    try
                    {
                        var item = SurugayaGrab(htmlData, passedURL);
                        DisplayProduct(item);
                        currentItem = item;
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            return false;
        }

        public async Task<bool> ProductRetrievedFromURI(string passedURL)
        {
            Debug.WriteLine($"Retrieving data from {passedURL}");
            var task = await (WebPageIsValid(passedURL));
            if (task)
            {
                Debug.WriteLine(SiteRequestor(passedURL));
                if (string.Compare(SiteRequestor(passedURL), surugaya) == 0)
                {
                    try
                    {
                        ShoppingClasses.Item item = SurugayaGrab(htmlData, passedURL);
                        DisplayProduct(item);
                        currentItem = item;
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            return false;
        }

        public static string SiteRequestor(string query)
        {
            string begin = @"s:{1}\/{2}";
            string end = @"\.jp{1}";
            Match idx1 = Regex.Match(query, begin);
            string trim1 = query.Substring(8, (query.Length - (idx1.Index + 8)));
            Match idx3 = Regex.Match(trim1, end);
            //const int StartIndex = 0;
            string trim2 = trim1[..(idx3.Index + 3)];
            return trim2;
        }

        public ShoppingClasses.Item SurugayaGrab(string passedBody, string passedURL)
        {

            string link = passedURL;
            Encoding utf8 = new UTF8Encoding(true);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            string htmlCode = passedBody;
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlCode);
            string cost = ItemIsAvaliable(doc);

            Byte[] eB = utf8.GetBytes(doc.DocumentNode.SelectSingleNode("//h1[@id='item_title']").InnerText);
            string product = doc.DocumentNode.SelectSingleNode("//h1[@id='item_title']").InnerText;
            string value = "";
            string soldOut = "Item is Sold Out";
            int price = 0;
            if (string.Compare(cost, soldOut) != 0)
            {
                for (int i = 0; i < cost.Length; i++)
                {
                    if (Char.IsNumber(cost[i]))
                    {
                        value += cost[i];
                    }
                }
                price = Int32.Parse(value);
            }
            else
            {
                value = soldOut;
            }

            string imageHolder = doc.DocumentNode
                      .SelectNodes("//img[@class='img-fluid main-pro-img']").First()
                        .Attributes["src"].Value;       
            string cleanText = String.Concat(product.Where(c => !Char.IsWhiteSpace(c)));
            string unicodeString = cleanText;
            Console.WriteLine(cleanText);
            Debug.WriteLine($"Cost: {value}");
            
            
            
            string vendor = "Surugaya";
            ShoppingClasses.Item newProduct = new(price, cleanText, vendor, link, imageHolder);
            return newProduct;
        }

        public string ItemIsAvaliable(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            var doc = htmlDoc;
            string value = "Item is Sold Out";
            var priceNode = doc.DocumentNode
                     .SelectSingleNode("//span[@class='text-price-detail price-buy']");
            if (priceNode != null)
            {
                value = doc.DocumentNode
                     .SelectSingleNode("//span[@class='text-price-detail price-buy']").InnerText;

            }
            return value;
        }

        public void addToCart(ShoppingClasses.Item product)
        {
            //cartDB.Add(product);

            //WriteToDB(product, "cart.xml");
            //cartItems.Add(product.Name);
            //updateList(cartList, cartItems);
            //copyList(cartDB, purchaseList);
            //cartPrice();
        }

        public async Task<List<int>> UpdatedItemPrices(List<ShoppingClasses.Item> items, BackgroundWorker worker)
        {
            ProgressBar progressBarForm;
            progressBarForm = new();      
            int finalPercentage = (100 / items.Count);
            int count = 0;
            List<int> prices = new List<int>();         
            string caption = $"Checking for prices {items.Count}";
            Debug.WriteLine(caption);
            Debug.WriteLine("Running");
            progressBarForm.Text = caption;
            progressBarForm.SetCaption("");
            progressBarForm.Show();
            foreach (var item in items)
            {
                var task = await GetPriceSurugaya(item.link);
                int cost = task;
                string message = $"{item.Name} costs {cost.ToString("c")}";
                progressBarForm.SetCaption(message);
                Debug.WriteLine(cost);
                prices.Add(cost);
                count += finalPercentage;
                progressBarForm.UpdateBarProgress(count);
                worker.ReportProgress(count);
            }
            worker.ReportProgress(100);
            progressBarForm.UpdateBarProgress(100);
            progressBarForm.Close();
            Debug.WriteLine("Ran");
            return prices;
        }

        public async Task<int> GetPriceSurugaya(string passedURL)
        {
            int price = -1;
            var task = await (WebPageIsValid(passedURL));
            if (task)
            {
                Debug.WriteLine(SiteRequestor(passedURL));
                if (string.Compare(SiteRequestor(passedURL), surugaya) == 0)
                {     
                    var item = SurugayaGrab(htmlData, passedURL);
                    if(item.price != 0)
                    {
                        price = item.price;
                    }
                                 
                }
            }
            return price;

        }
    }
}
