using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHelperV2
{
    internal class MessageHandler
    {
        public void LoadErrorPopUp(string title, string message)
        {
            MessageBoxIcon icon = MessageBoxIcon.Error;

            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        public bool ConfirmationPopUp(string title, string message)
        {
            DialogResult result;
            result = MessageBox.Show(message, title, MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
