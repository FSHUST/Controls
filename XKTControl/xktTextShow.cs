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
    [DefaultEvent("TextShowClick")]
    public partial class xktTextShow : UserControl
    {
        public xktTextShow()
        {
            InitializeComponent();
        }

        #region Fields

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("绑定变量名称")]
        public string VarName { get; set; }

        private string varValue = "0.0";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置显示数值")]
        public string VarValue
        {
            get { return varValue; }
            set
            {
                varValue = value;
                this.lbl_data.Text = varValue;
            }
        }


        private string unit = "Mpa";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置显示单位")]
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                this.lbl_Unit.Text = unit;
            }
        }


        private float textScale = 0.6f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置文本宽度所占比例")]
        public float TextScale
        {
            get { return textScale; }
            set
            {
                textScale = value;
                this.MainLayout.ColumnStyles[0].Width = textScale * this.Width;
                this.MainLayout.ColumnStyles[1].Width =this.Width- textScale * this.Width;
            }
        }

        private Font textFont = new Font("微软雅黑", 10.5f, FontStyle.Bold);

        public Font TextFont
        {
            get { return textFont; }
            set { textFont = value;

                this.lbl_data.Font = this.lbl_Unit.Font = this.textFont;
            }
        }


        #endregion


        //创建委托
        public delegate void BtnClickDelegate(object sender, EventArgs e);


        //创建事件
        [Browsable(true)]
        [Category("自定义事件")]
        [Description("文本双击触发事件")]
        public event BtnClickDelegate TextShowClick;

        private void Lbl_data_DoubleClick(object sender, EventArgs e)
        {
            TextShowClick?.Invoke(this, new EventArgs());
        }
    }
}
