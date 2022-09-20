namespace ShoppingHelperV2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchPanelOptn = new System.Windows.Forms.ToolStripMenuItem();
            this.wishlistOptn = new System.Windows.Forms.ToolStripMenuItem();
            this.productOptn = new System.Windows.Forms.ToolStripMenuItem();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.addToCartBtn = new System.Windows.Forms.Button();
            this.rmvWLBtn = new System.Windows.Forms.Button();
            this.wishListHolder = new System.Windows.Forms.RichTextBox();
            this.wishListCB = new System.Windows.Forms.ComboBox();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.commentPanel = new System.Windows.Forms.Panel();
            this.loadItemBtn = new System.Windows.Forms.Button();
            this.addItemBtn = new System.Windows.Forms.Button();
            this.urlInputTB = new System.Windows.Forms.TextBox();
            this.cartPanel = new System.Windows.Forms.Panel();
            this.checkOutBtn = new System.Windows.Forms.Button();
            this.cartList = new System.Windows.Forms.RichTextBox();
            this.productPanel = new System.Windows.Forms.Panel();
            this.priceTag = new System.Windows.Forms.Label();
            this.productInfo = new System.Windows.Forms.RichTextBox();
            this.productImage = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.cartPanel.SuspendLayout();
            this.productPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1902, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchPanelOptn,
            this.wishlistOptn,
            this.productOptn});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // searchPanelOptn
            // 
            this.searchPanelOptn.Name = "searchPanelOptn";
            this.searchPanelOptn.Size = new System.Drawing.Size(148, 22);
            this.searchPanelOptn.Text = "Search Panel";
            this.searchPanelOptn.Click += new System.EventHandler(this.SearchPanelOptnClicked);
            // 
            // wishlistOptn
            // 
            this.wishlistOptn.Name = "wishlistOptn";
            this.wishlistOptn.Size = new System.Drawing.Size(148, 22);
            this.wishlistOptn.Text = "Wishlist Panel";
            this.wishlistOptn.Click += new System.EventHandler(this.WishlistOptnClicked);
            // 
            // productOptn
            // 
            this.productOptn.Name = "productOptn";
            this.productOptn.Size = new System.Drawing.Size(148, 22);
            this.productOptn.Text = "Product Panel";
            this.productOptn.Click += new System.EventHandler(this.ProductOptn_Click);
            // 
            // sidePanel
            // 
            this.sidePanel.Controls.Add(this.addToCartBtn);
            this.sidePanel.Controls.Add(this.rmvWLBtn);
            this.sidePanel.Controls.Add(this.wishListHolder);
            this.sidePanel.Controls.Add(this.wishListCB);
            this.sidePanel.Controls.Add(this.filterPanel);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 24);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(440, 1037);
            this.sidePanel.TabIndex = 1;
            // 
            // addToCartBtn
            // 
            this.addToCartBtn.Location = new System.Drawing.Point(311, 63);
            this.addToCartBtn.Name = "addToCartBtn";
            this.addToCartBtn.Size = new System.Drawing.Size(112, 37);
            this.addToCartBtn.TabIndex = 3;
            this.addToCartBtn.Text = "Add to Cart";
            this.addToCartBtn.UseVisualStyleBackColor = true;
            this.addToCartBtn.Click += new System.EventHandler(this.addToCartBtn_Click);
            // 
            // rmvWLBtn
            // 
            this.rmvWLBtn.Location = new System.Drawing.Point(12, 63);
            this.rmvWLBtn.Name = "rmvWLBtn";
            this.rmvWLBtn.Size = new System.Drawing.Size(112, 37);
            this.rmvWLBtn.TabIndex = 0;
            this.rmvWLBtn.Text = "Remove";
            this.rmvWLBtn.UseVisualStyleBackColor = true;
            this.rmvWLBtn.Click += new System.EventHandler(this.RmvWLBtn_Click);
            // 
            // wishListHolder
            // 
            this.wishListHolder.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wishListHolder.Location = new System.Drawing.Point(12, 106);
            this.wishListHolder.Name = "wishListHolder";
            this.wishListHolder.ReadOnly = true;
            this.wishListHolder.Size = new System.Drawing.Size(411, 847);
            this.wishListHolder.TabIndex = 2;
            this.wishListHolder.Text = "";
            this.wishListHolder.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.WishlistLinkedClicked);
            // 
            // wishListCB
            // 
            this.wishListCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wishListCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wishListCB.FormattingEnabled = true;
            this.wishListCB.Location = new System.Drawing.Point(12, 959);
            this.wishListCB.Name = "wishListCB";
            this.wishListCB.Size = new System.Drawing.Size(413, 25);
            this.wishListCB.TabIndex = 1;
            // 
            // filterPanel
            // 
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(440, 57);
            this.filterPanel.TabIndex = 0;
            // 
            // searchPanel
            // 
            this.searchPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchPanel.Controls.Add(this.commentPanel);
            this.searchPanel.Controls.Add(this.loadItemBtn);
            this.searchPanel.Controls.Add(this.addItemBtn);
            this.searchPanel.Controls.Add(this.urlInputTB);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(440, 24);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1462, 120);
            this.searchPanel.TabIndex = 2;
            // 
            // commentPanel
            // 
            this.commentPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.commentPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.commentPanel.Location = new System.Drawing.Point(0, 58);
            this.commentPanel.Name = "commentPanel";
            this.commentPanel.Size = new System.Drawing.Size(1462, 62);
            this.commentPanel.TabIndex = 3;
            // 
            // loadItemBtn
            // 
            this.loadItemBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loadItemBtn.Location = new System.Drawing.Point(948, 18);
            this.loadItemBtn.Name = "loadItemBtn";
            this.loadItemBtn.Size = new System.Drawing.Size(195, 31);
            this.loadItemBtn.TabIndex = 2;
            this.loadItemBtn.Text = "Load Item";
            this.loadItemBtn.UseVisualStyleBackColor = true;
            this.loadItemBtn.Click += new System.EventHandler(this.LoadItemBtn_Click);
            // 
            // addItemBtn
            // 
            this.addItemBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addItemBtn.Location = new System.Drawing.Point(1173, 18);
            this.addItemBtn.Name = "addItemBtn";
            this.addItemBtn.Size = new System.Drawing.Size(223, 31);
            this.addItemBtn.TabIndex = 1;
            this.addItemBtn.Text = "Add Item to Wishlist";
            this.addItemBtn.UseVisualStyleBackColor = true;
            this.addItemBtn.Click += new System.EventHandler(this.AddItemBtn_Click);
            // 
            // urlInputTB
            // 
            this.urlInputTB.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.urlInputTB.Location = new System.Drawing.Point(25, 18);
            this.urlInputTB.Name = "urlInputTB";
            this.urlInputTB.PlaceholderText = "Enter an accepted URL link here";
            this.urlInputTB.Size = new System.Drawing.Size(902, 33);
            this.urlInputTB.TabIndex = 0;
            // 
            // cartPanel
            // 
            this.cartPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cartPanel.Controls.Add(this.checkOutBtn);
            this.cartPanel.Controls.Add(this.cartList);
            this.cartPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.cartPanel.Location = new System.Drawing.Point(440, 144);
            this.cartPanel.Name = "cartPanel";
            this.cartPanel.Size = new System.Drawing.Size(813, 917);
            this.cartPanel.TabIndex = 4;
            // 
            // checkOutBtn
            // 
            this.checkOutBtn.Location = new System.Drawing.Point(625, 10);
            this.checkOutBtn.Name = "checkOutBtn";
            this.checkOutBtn.Size = new System.Drawing.Size(168, 37);
            this.checkOutBtn.TabIndex = 4;
            this.checkOutBtn.Text = "Create Checkout File";
            this.checkOutBtn.UseVisualStyleBackColor = true;
            this.checkOutBtn.Click += new System.EventHandler(this.checkOutBtn_Click);
            // 
            // cartList
            // 
            this.cartList.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cartList.Location = new System.Drawing.Point(25, 90);
            this.cartList.Name = "cartList";
            this.cartList.ReadOnly = true;
            this.cartList.Size = new System.Drawing.Size(768, 743);
            this.cartList.TabIndex = 3;
            this.cartList.Text = "";
            // 
            // productPanel
            // 
            this.productPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.productPanel.Controls.Add(this.priceTag);
            this.productPanel.Controls.Add(this.productInfo);
            this.productPanel.Controls.Add(this.productImage);
            this.productPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.productPanel.Location = new System.Drawing.Point(1370, 144);
            this.productPanel.Name = "productPanel";
            this.productPanel.Size = new System.Drawing.Size(532, 917);
            this.productPanel.TabIndex = 5;
            // 
            // priceTag
            // 
            this.priceTag.AutoSize = true;
            this.priceTag.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.priceTag.Location = new System.Drawing.Point(18, 123);
            this.priceTag.Name = "priceTag";
            this.priceTag.Size = new System.Drawing.Size(0, 17);
            this.priceTag.TabIndex = 2;
            // 
            // productInfo
            // 
            this.productInfo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.productInfo.Location = new System.Drawing.Point(18, 6);
            this.productInfo.Name = "productInfo";
            this.productInfo.ReadOnly = true;
            this.productInfo.Size = new System.Drawing.Size(502, 96);
            this.productInfo.TabIndex = 1;
            this.productInfo.Text = "";
            // 
            // productImage
            // 
            this.productImage.Location = new System.Drawing.Point(18, 169);
            this.productImage.Name = "productImage";
            this.productImage.Size = new System.Drawing.Size(502, 664);
            this.productImage.TabIndex = 0;
            this.productImage.TabStop = false;
            this.productImage.Click += new System.EventHandler(this.LoadProductImage);
            this.productImage.MouseHover += new System.EventHandler(this.DisplayProductLink);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1061);
            this.Controls.Add(this.productPanel);
            this.Controls.Add(this.cartPanel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InitializeForm);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.cartPanel.ResumeLayout(false);
            this.productPanel.ResumeLayout(false);
            this.productPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem windowToolStripMenuItem;
        private Panel sidePanel;
        private Panel searchPanel;
        private Panel cartPanel;
        private Panel productPanel;
        private Panel filterPanel;
        private TextBox urlInputTB;
        private Button loadItemBtn;
        private Button addItemBtn;
        private RichTextBox productInfo;
        private PictureBox productImage;
        private ToolStripMenuItem searchPanelOptn;
        private ToolStripMenuItem wishlistOptn;
        private ToolStripMenuItem productOptn;
        private ComboBox wishListCB;
        private RichTextBox wishListHolder;
        private Label priceTag;
        private RichTextBox cartList;
        private Button rmvWLBtn;
        private Button addToCartBtn;
        private Panel commentPanel;
        private Button checkOutBtn;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}