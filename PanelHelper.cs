using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperV2
{
    internal class PanelHelper
    {
        public int searchPanelW;
        public int searchPanelH;
        public int commentPanelW;
        public int commentPanelH;
        public int cartPanelW;
        public int cartPanelH;
        public int productPanelW;
        public int productPanelH;
        public int sidePanelW;
        public int sidePanelH;


        public PanelHelper(int searchPanelW, int searchPanelH, int commentPanelW,
            int commentPanelH, int cartPanelW, int cartPanelH, int sidePanelW,
            int sidePanelH, int productPanelW, int productPanelH)
        {
            this.searchPanelW = searchPanelW;
            this.searchPanelH = searchPanelH;
            this.commentPanelW = commentPanelW;
            this.commentPanelH = commentPanelH;
            this.cartPanelW = cartPanelW;
            this.cartPanelH = cartPanelH;
            this.sidePanelW = sidePanelW;
            this.sidePanelH = sidePanelH;
            this.productPanelW = productPanelW;
            this.productPanelH = productPanelH;
        }

        public void PrintStoredSizes()
        {
            Debug.WriteLine($"{searchPanelW}, {searchPanelH}\n" +
                $"{commentPanelW}, {commentPanelH}\n" +
                $"{cartPanelW}, {cartPanelH}\n" +
                $"{sidePanelW}, {sidePanelH}\n" +
                $"{productPanelW}, {productPanelH}");
        }

        public void AdjustPanel(Panel panel, int panelW, int panelH)
        {   
            Debug.WriteLine($"Adjust panel {panel.Name} W:{panelW} H:{panelH}");
            panel.Size = new Size(panelW, panelH);
        }
    }
}
