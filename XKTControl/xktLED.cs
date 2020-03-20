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
    public partial class xktLED : UserControl
    {
        public xktLED()
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


        #region Fields

        //画布
        private Graphics g;

        //画笔
        private Pen p;

        //画刷
        private SolidBrush sb;

        //宽度
        private int width;

        //高度
        private int height;


        private float outWidth = 4.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环宽度")]
        public float OutWidth
        {
            get { return outWidth; }
            set
            {
                if (value <= 0 || value > 0.1f * this.Width)
                {
                    return;
                }
                outWidth = value;           
                this.Invalidate();
            }
        }


        private float outGap = 3.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环间隙")]
        public float OutGap
        {
            get { return outGap; }
            set
            {
                if (value <= 0 || value > 0.1f * this.Width || inGap <= value)
                {
                    return;
                }
                outGap = value;

               

                this.Invalidate();
            }
        }

        private float inGap = 8.0f;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内环间隙")]
        public float InGap
        {
            get { return inGap; }
            set
            {
                if (value <= 0 || value <= outGap)
                {
                    return;
                }
                inGap = value;          
               this.Invalidate();
            }
        }


        private Color color1 = Color.Gray;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("第一种颜色")]
        public Color Color1
        {
            get { return color1; }
            set
            {
                color1 = value;
                this.Invalidate();
            }
        }



        private Color color2 = Color.LimeGreen;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("第二种颜色")]
        public Color Color2
        {
            get { return color2; }
            set
            {
                color2 = value;
                this.Invalidate();
            }
        }

        private Color color3 = Color.Red;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("第三种颜色")]
        public Color Color3
        {
            get { return color3; }
            set
            {
                color3 = value;
                this.Invalidate();
            }
        }

        private Color color4 = Color.DarkGoldenrod;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("第四种颜色")]
        public Color Color4
        {
            get { return color4; }
            set
            {
                color4 = value;
                this.Invalidate();
            }
        }

        private Color color5 = Color.Blue;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("第五种颜色")]
        public Color Color5
        {
            get { return color5; }
            set
            {
                color5 = value;
                this.Invalidate();
            }
        }

        private int currentValue = 0;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("当前值")]
        public int CurrentValue
        {
            get { return currentValue; }
            set
            {
                if (value > 4 || value < 0)
                {
                    return;
                }
                currentValue = value;           
                this.Invalidate();
            }
        }


        #endregion


        #region Override

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            g = e.Graphics;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            this.width = this.Width;

            this.height = this.Height;

            //特殊情况处理
            if (inGap > 0.5f * this.width || inGap > 0.5f * this.height)
            {
                return;
            }

            Color currentColor = GetCurrentColor();

            //绘制外环

            p = new Pen(currentColor, outWidth);

            RectangleF rec = new RectangleF(outGap, outGap, this.width - 2 * outGap, this.height - 2 * outGap);

            g.DrawEllipse(p, rec);

            //绘制内圆

            sb = new SolidBrush(currentColor);

            rec = new RectangleF(inGap, inGap, this.width - 2 * inGap, this.height - 2 * inGap);

            g.FillEllipse(sb, rec);



        }


        #endregion


        #region Private Methods
        /// <summary>
        /// 返回当前颜色
        /// </summary>
        /// <returns></returns>
        private Color GetCurrentColor()
        {
            List<Color> ColorList = new List<Color>();
            ColorList.Add(color1);
            ColorList.Add(color2);
            ColorList.Add(color3);
            ColorList.Add(color4);
            ColorList.Add(color5);
            return ColorList[currentValue];
        }

        #endregion


    }
}
