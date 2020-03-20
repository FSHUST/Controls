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
    public partial class xktDialPlate : UserControl
    {
        public xktDialPlate()
        {
            InitializeComponent();

            //设置控件样式
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }


        #region Fileds

        private Graphics g;

        private Pen p;

        private SolidBrush sb;

        private int width;

        private int height;

        private Color leftColor = Color.FromArgb(113, 152, 54);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环左边颜色")]
        public Color LeftColor
        {
            get { return leftColor; }
            set
            {
                leftColor = value;

                this.Invalidate();
            }
        }


        private Color rightColor = Color.FromArgb(187, 187, 187);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环右边颜色")]
        public Color RightColor
        {
            get { return rightColor; }
            set
            {
                rightColor = value;

                this.Invalidate();
            }
        }

        private Color textColor = Color.Black;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("刻度颜色")]
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;

                this.Invalidate();
            }
        }

        private float leftAngle = 120f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("左半部分的角度：0°-180°之间")]
        public float LeftAngle
        {
            get { return leftAngle; }
            set
            {

                if (value > 180.0f || value < 0.0f)
                {
                    return;
                }

                leftAngle = value;

                this.Invalidate();
            }
        }


        private float inScale = 0.5f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内环圆所占比例：0 - 1 之间")]
        public float InScale
        {
            get { return inScale; }
            set
            {

                if (value > 1.0f || value < 0.0f)
                {
                    return;
                }

                inScale = value;

                this.Invalidate();
            }
        }



        private float outScale = 0.8f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("刻度圆所占比例：0 - 1 之间")]
        public float OutScale
        {
            get { return outScale; }
            set
            {

                if (value > 1.0f || value < 0.0f)
                {
                    return;
                }

                outScale = value;

                this.Invalidate();
            }
        }

        private float textShowScale = 0.88f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("实时数据显示高度所占比例：0 - 1 之间，值越小越靠上")]
        public float TextShowScale
        {
            get { return textShowScale; }
            set
            {

                if (value > 1.0f || value < 0.0f)
                {
                    return;
                }

                textShowScale = value;

                this.Invalidate();
            }
        }



        private float range = 180.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("仪表显示量程")]
        public float Range
        {
            get { return range; }
            set
            {

                if (value <= 0.0f)
                {
                    return;
                }

                range = value;

                this.Invalidate();
            }
        }

        private int inThickness = 12;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内环宽度")]
        public int InThickness
        {
            get { return inThickness; }
            set
            {

                if (value <= 0)
                {
                    return;
                }

                inThickness = value;

                this.Invalidate();
            }
        }


        private int outThickness = 5;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环宽度")]
        public int OutThickness
        {
            get { return outThickness; }
            set
            {

                if (value <= 0)
                {
                    return;
                }

                outThickness = value;

                this.Invalidate();
            }
        }

        private float currentValue = 100.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("实时值显示")]
        public float CurrentValue
        {
            get { return currentValue; }
            set
            {

                if (value <= 0.0f || value > range)
                {
                    return;
                }

                currentValue = value;

                this.Invalidate();
            }
        }


        private string textPrefix = "实际温度：";

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("显示前缀")]
        public string TextPrefix
        {
            get { return textPrefix; }
            set
            {
                textPrefix = value;

                this.Invalidate();
            }
        }

        private string unit = "℃";

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("显示前缀")]
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;

                this.Invalidate();
            }
        }


        private Font scaleFont = new Font(new FontFamily("微软雅黑"), 8.0f);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("刻度字体")]
        public Font ScaleFont
        {
            get { return scaleFont; }
            set
            {
                scaleFont = value;

                this.Invalidate();
            }
        }


        private Font textShowFont = new Font(new FontFamily("微软雅黑"), 10.0f);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("显示字体")]
        public Font TxtShowFont
        {
            get { return textShowFont; }
            set
            {
                textShowFont = value;

                this.Invalidate();
            }
        }

        private bool isTextShow = true;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("是否显示实时值")]
        public bool IsTextShow
        {
            get { return isTextShow; }
            set
            {
                isTextShow = value;

                this.Invalidate();
            }
        }

        private Color textShowColor = Color.Black;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("实时值显示颜色")]
        public Color TextShowColor
        {
            get { return textShowColor; }
            set
            {
                textShowColor = value;

                this.Invalidate();
            }
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

            //特殊情况处理
            if (this.width <= 20 || this.height <= 20)
            {
                return;
            }

            if (this.height < this.width * 0.5f)
            {
                return;
            }

            //画外环
            p = new Pen(leftColor, outThickness);

            g.DrawArc(p, new RectangleF(10, 10, this.width - 20, this.width - 20), -180.0f, leftAngle);

            p = new Pen(rightColor, outThickness);

            g.DrawArc(p, new RectangleF(10, 10, this.width - 20, this.width - 20), -180.0f+leftAngle,180.0f- leftAngle);

            //画刻度

            g.TranslateTransform(this.width * 0.5f, this.width * 0.5f);

            g.RotateTransform(-90.0f);

            for (int i = 0; i < 7; i++)
            {
                if (30 * i > leftAngle)
                {
                    sb = new SolidBrush(rightColor);
                }
                else
                {
                    sb = new SolidBrush(leftColor);
                }
                g.FillRectangle(sb, new RectangleF(-3.0f, (this.width * 0.5f - 10.0f + outThickness * 0.5f + 2.0f) * (-1.0f), 6.0f, outThickness + 4.0f));

                g.RotateTransform(30.0f);
            }

            //画刻度值

            g.RotateTransform(-210.0f);

            g.RotateTransform(90.0f);

            for (int i = 0; i < 7; i++)
            {
                float angle = -180.0f + 30.0f * i;

                double x1 = Math.Cos(angle * Math.PI / 180);

                double y1 = Math.Sin(angle * Math.PI / 180);

                float x =Convert.ToSingle( this.width * outScale * 0.5f * x1);

                float y = Convert.ToSingle(this.width * outScale * 0.5f * y1);

                StringFormat sf = new StringFormat();

                if (i == 3)
                {
                    x = x - 30;
                    sf.Alignment = StringAlignment.Center;
                }
                else if (i > 3)
                {
                    x = x - 60;
                    sf.Alignment = StringAlignment.Far;
                }
                else if (i < 3)
                {
                    sf.Alignment = StringAlignment.Near;
                }

                RectangleF rec = new RectangleF(x, y, 60, 20);

                sb = new SolidBrush(textColor);

                if (range % 6 == 0)
                {
                    g.DrawString((range / 6 * i).ToString(), scaleFont, sb, rec, sf);
                }
                else
                {
                    g.DrawString((range / 6 * i).ToString("f1"), scaleFont, sb, rec, sf);
                }

            }

            //画实际值圆弧

            p = new Pen(leftColor, inThickness);

            float sweepangle = currentValue / range * 180.0f;

            float xx = this.width * 0.5f * inScale * (-1.0f);

            float yy = this.width * 0.5f * inScale * (-1.0f);

            g.DrawArc(p, new RectangleF(xx, yy, this.width * inScale, this.width * inScale), -180.0f, sweepangle);

            //绘制TextShow

            if (isTextShow)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;

                RectangleF rec = new RectangleF(this.width * (-0.5f), this.height * textShowScale - 0.5f * this.width, this.width, this.height * (1.0f - textShowScale));

                string val = textPrefix + currentValue.ToString() + " " + unit;

                g.DrawString(val, textShowFont, new SolidBrush(textShowColor), rec, sf);
            }


        }
        #endregion


    }
}
