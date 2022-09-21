using System.Diagnostics;

namespace ShoppingHelperV2
{
    public partial class Form1 : Form
    {
        ShoppingHelper shoppingHelper = new();
        MessageHandler messageHandler = new();
        DatabaseHandler databaseHandler = new();    
        PanelHelper? panelHelper;

        public Form1()
        {
            InitializeComponent();
        }

        public void InitializeFormLayout()
        {
            panelHelper = new(searchPanel.Width, searchPanel.Height, commentPanel.Width
                , commentPanel.Height, cartPanel.Width, cartPanel.Height, sidePanel.Width,
                sidePanel.Height, productPanel.Width, productPanel.Height);
            Debug.WriteLine($"{productPanel.Width}, {productPanel.Height}");
            panelHelper.PrintStoredSizes();
            //panelHelper.AdjustPanel(sidePanel, 0, 0);
            //panelHelper.AdjustPanel(productPanel, 0, 0);
            GetPanelSizes();
        }

        public void GetPanelSizes()
        {
            //sidePanel.Hide();
            //productPanel.Hide();
        }

        private void LoadItemBtn_Click(object sender, EventArgs e)
        {

            if (urlInputTB.Text != "")
            {
                var task = shoppingHelper.RetrieveDataFromURI(urlInputTB.Text);
                //shoppingHelper.LayoutAnalysis();
                if(task != null)
                {
                   
                }
               
            }
            else
            {
                string caption = "Error Loading Item";
                string message = "Nothing was entered in the link search";
                messageHandler.LoadErrorPopUp(caption, message);
            }
        }

        private void InitializeForm(object sender, EventArgs e)
        {
            InitializeFormLayout();
            databaseHandler.InitlializeDatabase();
            int dbCount = databaseHandler.wishListDB.Count;
            Debug.WriteLine(dbCount);
            searchPanelOptn.Checked = true;
            productOptn.Checked = true;
            wishlistOptn.Checked = true;
        }

        private void ProductOptn_Click(object sender, EventArgs e)
        {
            switch(productOptn.Checked)
            {
                case false:
                    productOptn.Checked = true;
                    productPanel.Show();
                    panelHelper.AdjustPanel(productPanel, panelHelper.productPanelW, panelHelper.productPanelH);
                    break;
                case true:
                    productOptn.Checked = false;
                    productPanel.Hide();
                    panelHelper.AdjustPanel(productPanel, 0, 0);
                    break;
            }
        }

        private async void AddItemBtn_Click(object sender, EventArgs e)
        {
            //https://www.suruga-ya.jp/product/detail/GL286369

            if (urlInputTB.Text != "")
            {
                var task = await shoppingHelper.ProductRetrievedFromURI(urlInputTB.Text);
                shoppingHelper.LayoutAnalysis();
                if (task) 
                {
                    if (shoppingHelper.currentItem != null)
                    {
                        ItemHandler handler = new();
                        Debug.WriteLine("Calling handler.....");
                        handler.WriteToLocalDB(databaseHandler.wishListDB, shoppingHelper.currentItem, "listDB.xml", "Wish List");
                        
                    }
                    if (shoppingHelper.currentItem == null)
                    {
                        Debug.WriteLine("Null item");
                    }

                }
            }
            else
            {
                string caption = "Error Loading Item";
                string message = "Nothing was entered in the link search";
                messageHandler.LoadErrorPopUp(caption, message);
            }
        }

        private void WishlistOptnClicked(object sender, EventArgs e)
        {
            switch (wishlistOptn.Checked)
            {
                case false:
                    wishlistOptn.Checked = true;
                    sidePanel.Show();
                    panelHelper.AdjustPanel(sidePanel, panelHelper.sidePanelW, panelHelper.sidePanelH);
                    break;
                case true:
                    wishlistOptn.Checked = false;
                    sidePanel.Hide();
                    panelHelper.AdjustPanel(sidePanel, 0, 0);
                    break;
            }
        }

        private async void WishlistLinkedClicked(object sender, LinkClickedEventArgs e)
        {
            string? url = e.LinkText;
            Debug.WriteLine($"Link clicked {url}");
            if(e.LinkText is not null)
            {
                var task = await shoppingHelper.ProductRetrievedFromURI(e.LinkText);
                if(task)
                {
                    var taskA = shoppingHelper.RetrieveDataFromURI(e.LinkText);
                    if(taskA is null)
                    {

                    }
                }
            }
           
        }

        private async void LoadProductImage(object sender, EventArgs e)
        {
            var product = shoppingHelper.currentItem;
            if(product is not null)
            {
                var AuthTask = await shoppingHelper.WebPageIsValid(product.link);
                if (AuthTask)
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo(product.link) { UseShellExecute = true });
                }
            }
        }

        private void DisplayProductLink(object sender, EventArgs e)
        {
            var product = shoppingHelper.currentItem;
            if (product is not null)
            {
                ToolTip toolTip1 = new ToolTip();
                toolTip1.SetToolTip(this.productImage, product.link);
            }
        
        }

        private void SearchPanelOptnClicked(object sender, EventArgs e)
        {
            switch (searchPanelOptn.Checked)
            {
                case false:
                    searchPanelOptn.Checked = true;            
                    panelHelper.AdjustPanel(searchPanel, panelHelper.searchPanelW, panelHelper.searchPanelH);
                    //panelHelper.AdjustPanel(commentPanel, panelHelper.commentPanelW, panelHelper.commentPanelH);
                    break;
                case true:
                    searchPanelOptn.Checked = false;
                    panelHelper.AdjustPanel(searchPanel, 0, 0);
                    //panelHelper.AdjustPanel(commentPanel, 0, 0);
                    break;
            }
        }

        private async void addToCartBtn_Click(object sender, EventArgs e)
        {
            var product = shoppingHelper.currentItem;
            string link = urlInputTB.Text;
            if (link is not null && product is null)
            {
                if(ItemIsLoaded(link))
                {
                    ItemHandler handler = new();
                    var item = shoppingHelper.currentItem;
                    if(item is not null)
                    {
                        handler.WriteToLocalDB(databaseHandler.cartDB, item, "cart.xml", "Cart");
                    }                
                }
            }
            if (product is not null)
            {
                if (ItemIsLoaded(product))
                {
                    ItemHandler handler = new();
                    handler.WriteToLocalDB(databaseHandler.cartDB, product, "cart.xml", "Cart");
                }
            }
            else
            {
                Debug.WriteLine("Booleans failed");
                return;
            }
           


        }

        private bool ItemIsLoaded(ShoppingClasses.Item product)
        {
            if (product is null)
            {
                string caption = "Error Loading Item";
                string message = "Check if an item was loaded in the product panel";
                messageHandler.LoadErrorPopUp(caption, message);
                if (!productOptn.Checked)
                {
                    productOptn.PerformClick();
                }
                return false;
            }
            return true;
        }
        private bool ItemIsLoaded(string link)
        {
            var task = shoppingHelper.RetrieveDataFromURI(link);
            if (task != null)
            {
                return true;
            }
            return false;
        }

        private void RmvWLBtn_Click(object sender, EventArgs e)
        {
            
            int dbCount = databaseHandler.wishListDB.Count;
            if(dbCount != 0)
            {
                //shoppingHelper.UpdatedItemPrices(databaseHandler.wishListDB);
            }
           
        }

        private async void checkOutBtn_Click(object sender, EventArgs e)
        {
            int dbCount = databaseHandler.wishListDB.Count;
            if (dbCount != 0)
            {   
                var list = await shoppingHelper.UpdatedItemPrices(databaseHandler.wishListDB, backgroundWorker1);
                if(list is not null)
                {
                    //databaseHandler.CreateCheckoutFile(list, backgroundWorker1);
                    var task = await databaseHandler.CheckoutFileCreated(list, backgroundWorker1);
                    if(task)
                    {
                        Debug.WriteLine("Created file");
                    }
                }
                //shoppingHelper.UpdatedItemPrices(databaseHandler.wishListDB);
            }
        }

        private void UpdateProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Debug.WriteLine($"Progress changed: {e.ProgressPercentage}");
            var progressPercentage = e.ProgressPercentage;
            Form bar = Application.OpenForms["ProgressBar"];
            if(bar is not null)
            {
                var barForm = (ProgressBar)bar;
                barForm.UpdateBarProgress(e.ProgressPercentage);
                
            }
        }
    }
}