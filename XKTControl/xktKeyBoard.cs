using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XKTControl
{
    public enum KeyBoardCharType
    {
        CHAR = 1,
        NUMBER = 2
    }

    public partial class xktKeyBoard : UserControl
    {
        public xktKeyBoard()
        {
            InitializeComponent();

            EventHandle(this);
        }


        #region Fileds

        private KeyBoardCharType charType = KeyBoardCharType.CHAR;

        public KeyBoardCharType CharType
        {
            get { return charType; }
            set
            {
                charType = value;

                if (charType == KeyBoardCharType.CHAR)
                {
                    if (lbl_NumChar.Text.ToLower() == "abc.")
                    {
                        ChangeShow(this);
                    }
                }
                else
                {
                    if (lbl_NumChar.Text.ToLower() == "?123")
                    {
                        ChangeShow(this);
                    }
                }
            }

        }

        #endregion

        #region Event


        [Browsable(true), Category("自定义事件"), Description("按键点击事件")]
        public event EventHandler KeyClick;


        [Browsable(true), Category("自定义事件"), Description("回车点击事件")]
        public event EventHandler EnterClick;


        [Browsable(true), Category("自定义事件"), Description("删除点击事件")]
        public event EventHandler BackspaceClick;

        [Browsable(true), Category("自定义事件"), Description("关闭点击事件")]
        public event EventHandler CloseClick;





        #endregion

        #region KeyDown Event
        private void KeyDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Label lbl)
            {
                if (string.IsNullOrEmpty(lbl.Text))
                {
                    return;
                }
                //切换大写
                if (lbl.Text == "CAP")
                {
                    ToUpperOrLower(this, true);
                    lbl.Text = "cap";
                }
                //切换小写
                else if (lbl.Text == "cap")
                {
                    ToUpperOrLower(this, false);
                    lbl.Text = "CAP";
                }
                //切换字符或数字
                else if (lbl.Text == "?123" || lbl.Text == "abc.")
                {
                    ChangeShow(this);
                }
                else if (lbl.Text == "空格")
                {
                    SendKeys.Send(" ");
                }
                else if (lbl.Text.ToLower() == "shift")
                {
                    SendKeys.Send("+");
                    if (lbl.Text == "shift")
                    {
                        lbl.Text = "SHIFT";
                    }
                    else
                    {
                        lbl.Text = "shift";
                    }
                }
                else if (lbl.Text == "删除")
                {
                    SendKeys.Send("{BACKSPACE}");
                    BackspaceClick?.Invoke(sender, e);
                }
                else if (lbl.Text == "回车")
                {
                    SendKeys.Send("{ENTER}");
                    EnterClick?.Invoke(sender, e);
                }
                else if (lbl.Text == "关闭")
                {
                    CloseClick?.Invoke(this, e);
                }
                else
                {
                    string str = "{" + lbl.Text + "}";
                    SendKeys.Send(str);
                    KeyClick?.Invoke(sender, e);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 统一绑定事件
        /// </summary>
        /// <param name="ctl"></param>
        private void EventHandle(Control ctl)
        {
            if (ctl.HasChildren)
            {
                foreach (Control item in ctl.Controls)
                {
                    if (item is Label lbl)
                    {
                        lbl.MouseDown += KeyDown_MouseDown;
                    }
                    else if (item.HasChildren)
                    {
                        EventHandle(item);
                    }
                }
            }
        }


        private void ToUpperOrLower(Control ctl, bool bln)
        {
            if (ctl.HasChildren)
            {
                foreach (Control item in ctl.Controls)
                {
                    if (item is Label lbl)
                    {
                        if (lbl.Text == "abc." || lbl.Text.ToLower() == "shift")
                        {
                            return;
                        }

                        lbl.Text = bln ? lbl.Text.ToUpper() : lbl.Text.ToLower();
                    }
                    else if (item.HasChildren)
                    {
                        ToUpperOrLower(item, bln);
                    }
                }

            }
        }

        /// <summary>
        /// 切换所有Lable的Tag和Text
        /// </summary>
        /// <param name="ctl"></param>
        private void ChangeShow(Control ctl)
        {
            if (ctl.HasChildren)
            {
                foreach (Control item in ctl.Controls)
                {
                    if (item is Label lbl)
                    {
                        string strTag = lbl.Text;

                        lbl.Text = lbl.Tag.ToString();
                        lbl.Tag = strTag;
                    }
                    else if (item.HasChildren)
                    {
                        ChangeShow(item);
                    }
                }
            }

        }

        #endregion
    }
}
