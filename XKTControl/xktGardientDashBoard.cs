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
    public partial class xktGardientDashBoard : UserControl
    {
        public xktGardientDashBoard()
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

        private Color circleColor1 = Color.FromArgb(33, 80, 33);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环左侧颜色")]
        public Color CircleColor1
        {
            get { return circleColor1; }
            set
            {
                circleColor1 = value;

                this.Invalidate();
            }
        }

        private Color circleColor2 = Color.FromArgb(22, 128, 22);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环中间颜色")]
        public Color CircleColor2
        {
            get { return circleColor2; }
            set
            {
                circleColor2 = value;

                this.Invalidate();
            }
        }

        private Color circleColor3 = Color.FromArgb(20, 181, 20);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环右侧颜色")]
        public Color CircleColor3
        {
            get { return circleColor3; }
            set
            {
                circleColor3 = value;

                this.Invalidate();
            }
        }



        private Color pointColor = Color.Green;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("指针颜色")]
        public Color PointColor
        {
            get { return pointColor; }
            set
            {
                pointColor = value;

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

                if (value < 0.0f || value > range)
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


        private float centerRadius = 6.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("中心半径")]
        public float CenterRadiusnit
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

        private float gapAngle = 2.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("间隙角度")]
        public float GapAngle
        {
            get { return gapAngle; }
            set
            {
                if (value <= 0.0f)
                {
                    return;
                }

                gapAngle = value;

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

            float angle = (180.0f - gapAngle * 2) / 3.0f;

            RectangleF rec = new RectangleF(10, 10, this.width - 20, this.width - 20);


            //第一个圆弧
            p = new Pen(circleColor1, outThickness);

            g.DrawArc(p, rec, -180.0f, angle);

            //第二个圆弧
            p = new Pen(circleColor2, outThickness);

            g.DrawArc(p, rec, -180.0f + angle + gapAngle, angle);

            //第三个圆弧
            p = new Pen(circleColor3, outThickness);

            g.DrawArc(p, rec, -180.0f + angle * 2.0f + gapAngle * 2.0f, angle);


            //绘制刻度

            g.TranslateTransform(this.width * 0.5f, this.width * 0.5f);

            for (int i = 0; i < 4; i++)
            {
                float actualangle = -180.0f + 60.0f * i;

                double x1 = Math.Cos(actualangle * Math.PI / 180);

                double y1 = Math.Sin(actualangle * Math.PI / 180);

                float x = Convert.ToSingle(this.width * outScale * 0.5f * x1);

                float y = Convert.ToSingle(this.width * outScale * 0.5f * y1);

                StringFormat sf = new StringFormat();

                if (i > 1)
                {
                    x = x - 60;
                    sf.Alignment = StringAlignment.Far;
                }
                else
                {
                    sf.Alignment = StringAlignment.Near;
                }

                rec = new RectangleF(x, y, 60, 20);

                sb = new SolidBrush(textColor);

                if (range % 6 == 0)
                {
                    g.DrawString((range / 3 * i).ToString(), scaleFont, sb, rec, sf);
                }
                else
                {
                    g.DrawString((range / 3 * i).ToString("f1"), scaleFont, sb, rec, sf);
                }

            }

            //绘制内圆

            g.FillEllipse(new SolidBrush(pointColor), new RectangleF(-centerRadius, -centerRadius, centerRadius * 2.0f, centerRadius * 2.0f));

            //绘制指针

            p = new Pen(pointColor, 3.0f);

            float sweepangle = currentValue / range * 180.0f;

            float z = this.width * 0.5f * outScale - outThickness * 0.5f - 20.0f;

            g.RotateTransform(90.0f);

            g.RotateTransform(sweepangle);

            g.DrawLine(p, new PointF(0, 0), new PointF(0, z));

            if (isTextShow)
            {
                g.RotateTransform(-sweepangle);

                g.RotateTransform(-90.0f);

                StringFormat sf = new StringFormat();

                sf.Alignment = StringAlignment.Center;

                 rec = new RectangleF(this.width * (-0.5f), this.height * textShowScale - 0.5f * this.width, this.width, this.height * (1.0f - textShowScale));

                string val = textPrefix + currentValue.ToString() + " " + unit+"（"+(currentValue/range*100.0f).ToString("f0")+"%"+"）";

                g.DrawString(val, textShowFont, new SolidBrush(textShowColor), rec, sf);

            }

        }

        #endregion
    }
}
