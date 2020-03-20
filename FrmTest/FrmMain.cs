using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XKTControl;

namespace FrmTest
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void TextBox1_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!KeyBoardExited(tb))
            {
                xktKeyBoard keyboard = new xktKeyBoard();
                keyboard.Location = new Point(tb.Location.X, tb.Location.Y + 10);
                keyboard.Size = new Size(420, 200);
                keyboard.CloseClick += Keyboard_CloseClick;
                tb.Parent.Controls.Add(keyboard);
            }
        }

        private void Keyboard_CloseClick(object sender, EventArgs e)
        {
            xktKeyBoard kb = sender as xktKeyBoard;
            kb.Parent.Controls.Remove(kb);
        }

        private bool KeyBoardExited(Control ctl)
        {
            foreach (Control item in ctl.Parent.Controls)
            {
                if (item is xktKeyBoard kb)
                {
                    kb.BringToFront();
                    return true;
                }
            }
            return false;
        }
    }
}
