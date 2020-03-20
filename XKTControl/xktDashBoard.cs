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
    public partial class xktDashBoard : UserControl
    {
        public xktDashBoard()
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

        private float leftAngle = 168.75f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("左半部分的角度：0°-270°之间")]
        public float LeftAngle
        {
            get { return leftAngle; }
            set
            {

                if (value > 270.0f || value < 0.0f)
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



        private float range = 160.0f;

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

        private float centerRadius = 12.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("中心圆半径")]
        public float CenterRadius
        {
            get { return centerRadius; }
            set
            {
                if (value <= 0.0f)
                {
                    return;
                }

                centerRadius = value;

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

            //设置画布属性
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

            //获取圆心位置
            Point center = GetCenterPoint();

            //绘制外环

            g.RotateTransform(0.0f);

            p = new Pen(leftColor, outThickness);

            g.DrawArc(p, new Rectangle(10, 10, center.X * 2 - 20, center.Y * 2 - 20), -225.0f, leftAngle);

            p = new Pen(rightColor, outThickness);

            g.DrawArc(p, new Rectangle(10, 10, center.X * 2 - 20, center.Y * 2 - 20), -225.0f + leftAngle, 270.0f - leftAngle);

            g.TranslateTransform(center.X, center.Y);

            //画刻度

            g.RotateTransform(-135.0f);

            for (int i = 0; i < 9; i++)
            {
                if (33.75f * i > leftAngle)
                {
                    sb = new SolidBrush(rightColor);
                }
                else
                {
                    sb = new SolidBrush(leftColor);
                }

                g.FillRectangle(sb, new RectangleF(-2.0f, center.Y * (-1.0f) + 5.0f, 4.0f, 12.0f));

                g.RotateTransform(33.75f);
            }

            //绘制刻度值
            //33.75+270.0
            g.RotateTransform(-303.75f);

            //快速、斜着的刻度值

            //for (int i = 0; i < 9; i++)
            //{
            //    g.DrawString((range / 8 * i).ToString(), scaleFont, new SolidBrush(textColor), -8.0f, center.Y * (-1.0f) + 25.0f);

            //    g.RotateTransform(33.75f);
            //}

            //复杂一点、横着的刻度值

            //宽度为60，高度为20

            g.RotateTransform(135.0f);

            for (int i = 0; i < 9; i++)
            {

                float angle = -225.0f + 33.75f * i;

                double x1 = Math.Cos(angle * Math.PI / 180);

                double y1 = Math.Sin(angle * Math.PI / 180);

                float x = Convert.ToSingle(center.X * outScale * x1);

                float y = Convert.ToSingle(center.Y * outScale * y1);

               StringFormat sf = new StringFormat();

                if (i == 4)
                {
                    x = x - 30;
                    sf.Alignment = StringAlignment.Center;
                }
                else if (i > 4)
                {
                    x = x - 60;
                    sf.Alignment = StringAlignment.Far;
                }
                else if(i<4)
                {
                    sf.Alignment = StringAlignment.Near;
                }
                RectangleF rec = new RectangleF(x, y, 60, 20);

                if (range % 8 == 0)
                {
                    g.DrawString((range / 8 * i).ToString(), scaleFont, new SolidBrush(textColor), rec, sf);
                }
                else
                {
                    g.DrawString((range / 8 * i).ToString("f1"), scaleFont, new SolidBrush(textColor), rec, sf);
                }
            }

            //画内圆

            g.FillEllipse(new SolidBrush(leftColor), new RectangleF(-centerRadius, -centerRadius, centerRadius * 2, centerRadius * 2));

            //画内圆实际值

            p = new Pen(leftColor, inThickness);

            float sweepangle = currentValue / range * 270.0f;

            g.DrawArc(p, new RectangleF(center.X * inScale * (-1.0f), center.Y * inScale * (-1.0f), center.X * 2 * inScale, center.Y * 2 * inScale), -225.0f, sweepangle);


            //画指针

            g.RotateTransform(-135.0f);

            g.RotateTransform(sweepangle);

            p = new Pen(leftColor, 3.0f);

            PointF endPoint = new PointF(-1.5f, (center.Y * inScale + inThickness * 0.5f) * (-1.0f));

            g.DrawLine(p, new PointF(0, 0), endPoint);

            //写标识数组
            if (isTextShow)
            {
                g.RotateTransform(sweepangle*(-1.0f));

                g.RotateTransform(135.0f);

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;

                string val = textPrefix + currentValue.ToString() + " " + unit;

                RectangleF rec = new RectangleF(center.X * (-1.0f), this.height * textShowScale - center.Y, center.X * 2, this.height * (1.0f - textShowScale));

                g.DrawString(val, textShowFont, new SolidBrush(textShowColor), rec, sf);

            }


        }

        #endregion

        #region Private Methods

        private Point GetCenterPoint()
        {
            if (this.Height > this.Width)
            {
                return new Point(this.Width / 2, this.Width / 2);
            }
            else
            {
                return new Point(this.Height / 2, this.Height / 2);
            }
        }

        #endregion

    }
}
