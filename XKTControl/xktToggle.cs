using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace XKTControl
{
    [DefaultEvent("CheckedChanged")]
    public partial class xktToggle : UserControl
    {
        public xktToggle()
        {
            InitializeComponent();

            //设置控件样式
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.MouseDown += XktToggle_MouseDown;

        }


        #region Fileds

        private Graphics g;

        private int width;

        private int height;


        public enum SwitchType
        {
            //椭圆
            Ellipse,

            //矩形
            Rectangle,
        }


        private bool _checked = false;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("是否选中")]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;

                this.Invalidate();

                //触发一个事件
                this.CheckedChanged?.Invoke(this,null);
            }
        }

        private Color falseColor = Color.FromArgb(180, 180, 180);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("没有选中时的颜色")]
        public Color FalseColor
        {
            get { return falseColor; }
            set
            {
                falseColor = value;

                this.Invalidate();
            }
        }


        private Color trueColor = Color.FromArgb(73, 119, 232);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("选中时的颜色")]
        public Color TrueColor
        {
            get { return trueColor; }
            set
            {
                trueColor = value;

                this.Invalidate();
            }
        }


        private string falseText = "关闭";

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("没有选中时的文本")]
        public string FalseText
        {
            get { return falseText; }
            set
            {
                falseText = value;

                this.Invalidate();
            }
        }

        private string trueText = "打开";

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("选中时的文本")]
        public string TrueText
        {
            get { return trueText; }
            set
            {
                trueText = value;

                this.Invalidate();
            }
        }

        private SwitchType switchtype = SwitchType.Ellipse;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("切换样式")]
        public SwitchType Switchtype

        {
            get { return switchtype; }
            set
            {
                switchtype = value;
                this.Invalidate();
            }
        }


        private int circleDiameter = 10;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("小圆形直径")]
        public int CircleDiameter
        {
            get { return circleDiameter; }
            set
            {
                if (value >= this.Width || value >= this.Height || value <= 0)
                {
                    return;
                }

                circleDiameter = value;
                this.Invalidate();
            }
        }


        private int circlePosition = 10;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("小圆形位置")]
        public int CirclePosition
        {
            get { return circlePosition; }
            set
            {
                if (value >= this.Width || value >= this.Height || value <= 0)
                {
                    return;
                }

                circlePosition = value;
                this.Invalidate();
            }
        }


        private Color sliderColor = Color.White;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("滑块颜色")]
        public Color SliderColor
        {
            get { return sliderColor; }
            set
            {
                sliderColor = value;

                this.Invalidate();
            }
        }

        #endregion

        #region Event


        [Browsable(true)]
        [Category("自定义事件")]
        [Description("选中改变事件")]
        public event EventHandler CheckedChanged;

        private void XktToggle_MouseDown(object sender, MouseEventArgs e)
        {
            Checked = !Checked;
        }


        #endregion

        #region Override
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            this.width = this.Width;
            this.height = this.Height;

            //矩形样式绘制
            if (this.switchtype == SwitchType.Rectangle)
            {
                //确定填充色
                Color fillColor = this._checked ? trueColor : falseColor;

                //画带圆弧的矩形
                GraphicsPath path = new GraphicsPath();

                int diameter = 5;

                path.AddArc(0, 0, diameter, diameter, 180f, 90f);

                path.AddArc(this.width - diameter, 0, diameter, diameter, 270f, 90f);

                path.AddArc(this.width - diameter, this.height - diameter, diameter, diameter, 0f, 90f);

                path.AddArc(0, this.height - diameter, diameter, diameter, 90f, 90f);

                g.FillPath(new SolidBrush(fillColor), path);

                string strText = this._checked ? trueText : falseText;

                if (_checked)
                {
                    path = new GraphicsPath();

                    int sliderwidth = this.height - 4;

                    path.AddArc(this.width - sliderwidth - 2, 2, diameter, diameter, 180f, 90f);

                    path.AddArc(this.width - diameter - 2, 2, diameter, diameter, 270f, 90f);

                    path.AddArc(this.width - diameter - 2, this.height - diameter - 2, diameter, diameter, 0f, 90f);

                    path.AddArc(this.width - sliderwidth - 2, this.height - diameter - 2, diameter, diameter, 90f, 90f);

                    g.FillPath(new SolidBrush(sliderColor), path);


                    if (string.IsNullOrEmpty(strText))
                    {
                        //绘制小圆形
                        g.DrawEllipse(new Pen(sliderColor, 2), new RectangleF(circlePosition, (this.height - circleDiameter) * 0.5f, circleDiameter, circleDiameter));
                    }
                    else
                    {
                        //绘制文本
                        Rectangle rec = new Rectangle(0, 0, this.width - sliderwidth - 2, this.height);

                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString(strText, Font, new SolidBrush(sliderColor), rec, sf);

                    }
                }
                else
                {
                    path = new GraphicsPath();

                    int sliderwidth = this.height - 4;

                    path.AddArc(2, 2, diameter, diameter, 180f, 90f);

                    path.AddArc(sliderwidth - diameter + 2, 2, diameter, diameter, 270f, 90f);

                    path.AddArc(sliderwidth - diameter + 2, sliderwidth - diameter + 2, diameter, diameter, 0f, 90f);

                    path.AddArc(2, sliderwidth - diameter + 2, diameter, diameter, 90f, 90f);

                    g.FillPath(new SolidBrush(sliderColor), path);


                    if (string.IsNullOrEmpty(strText))
                    {
                        //绘制小圆形
                        g.DrawEllipse(new Pen(sliderColor, 2), new RectangleF(this.width - circlePosition - circleDiameter, (this.height - circleDiameter) * 0.5f, circleDiameter, circleDiameter));
                    }
                    else
                    {
                        //绘制文本
                        Rectangle rec = new Rectangle(sliderwidth + 2, 0, this.width - sliderwidth - 2, this.height);

                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString(strText, Font, new SolidBrush(sliderColor), rec, sf);

                    }


                }
            }

            //椭圆样式绘制
            else if (this.switchtype == SwitchType.Ellipse)
            {
                //确定填充色
                Color fillColor = this._checked ? trueColor : falseColor;

                //画带圆弧的矩形
                GraphicsPath path = new GraphicsPath();

                path.AddArc(1, 1, this.height-2, this.height-2, 90f, 180f);

                path.AddArc(this.width - (this.height - 2)-1, 1, this.height - 2, this.height - 2, 270f, 180f);

                g.FillPath(new SolidBrush(fillColor), path);

                string strText = this._checked ? trueText : falseText;

                if (_checked)
                {
                    int circlewidth = this.height - 6;

                    g.FillEllipse( new SolidBrush(sliderColor),new Rectangle( this.width - circlewidth - 3, 3, circlewidth, circlewidth));

                    if (string.IsNullOrEmpty(strText))
                    {       
                        //绘制小圆形
                        g.DrawEllipse(new Pen(sliderColor, 2), new RectangleF(circlePosition, (this.height - circleDiameter) * 0.5f, circleDiameter, circleDiameter));

                    }
                    else
                    {
                        //绘制文本
                        Rectangle rec = new Rectangle(0, 0, this.width - circlewidth - 3, this.height);

                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString(strText, Font, new SolidBrush(sliderColor), rec, sf);
                    }
                }
                else
                {
                    int circlewidth = this.height - 6;

                    g.FillEllipse(new SolidBrush(sliderColor), new Rectangle(3, 3, circlewidth, circlewidth));

                    if (string.IsNullOrEmpty(strText))
                    {
                        //绘制小圆形
                        g.DrawEllipse(new Pen(sliderColor, 2), new RectangleF(this.width - circlePosition - circleDiameter, (this.height - circleDiameter) * 0.5f, circleDiameter, circleDiameter));

                    }
                    else
                    {
                        //绘制文本
                        Rectangle rec = new Rectangle(circlewidth+3, 0, this.width - circlewidth - 3, this.height);

                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString(strText, Font, new SolidBrush(sliderColor), rec, sf);
                    }
                }

            }
        }

        #endregion


    }
}
