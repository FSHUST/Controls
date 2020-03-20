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
    [DefaultEvent("SwitchChanged")]
    public partial class xktSwtich : UserControl
    {
        public xktSwtich()
        {
            InitializeComponent();

            //设置控件样式
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.MouseClick += XktSwtich_MouseClick;

        }

        private void XktSwtich_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                switchStatus = !switchStatus;
            }
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


        private float outWidth = 3.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环宽度")]
        public float OutWidth
        {
            get { return outWidth; }
            set {
                if (value <= 0)
                {
                    return;
                }
                outWidth = value;
                this.Invalidate();
            }
        }

        private float outGap = 15.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("外环间隙")]
        public float OutGap
        {
            get { return outGap; }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                outGap = value;
                this.Invalidate();
            }
        }

        private float inGap = 19.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内环间隙")]
        public float InGap
        {
            get { return inGap; }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                inGap = value;
                this.Invalidate();
            }
        }

        private Color circleColor = Color.DimGray;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("内圆背景色")]
        public Color CircleColor
        {
            get { return circleColor; }
            set
            {
                circleColor = value;
                this.Invalidate();
            }
        }

        private Color toggleColor = Color.Black;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关背景色")]
        public Color ToggleColor
        {
            get { return toggleColor; }
            set
            {
                toggleColor = value;
                this.Invalidate();
            }
        }

        private float toggleWidth = 15.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关宽度")]
        public float ToggleWidth
        {
            get { return toggleWidth; }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                toggleWidth = value;
                this.Invalidate();
            }
        }

        private float toggleGap= 6.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关间隙")]
        public float ToggleGap
        {
            get { return toggleGap; }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                toggleGap = value;
                this.Invalidate();
            }
        }


        private Color toggleforeColor = Color.FromArgb(255,128,0);
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关圆点背景色")]
        public Color ToggleforeColor
        {
            get { return toggleforeColor; }
            set
            {
                toggleforeColor = value;
                this.Invalidate();
            }
        }

        private float toggleforeHeight = 20.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关圆点高度")]
        public float ToggleforeHeight
        {
            get { return toggleforeHeight; }
            set
            {
                if (value <= 0)
                {
                    return;
                }
                toggleforeHeight = value;
                this.Invalidate();
            }
        }

        private float toggleforeGap =4.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关圆点间隙")]
        public float ToggleforeGap
        {
            get { return toggleforeGap; }
            set
            {
                if (value <= 0||value>=toggleWidth*0.5f)
                {
                    return;
                }
                toggleforeGap = value;
                this.Invalidate();
            }
        }

        private bool switchStatus = false;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("开关状态")]
        public bool SwitchStatus
        {
            get { return switchStatus; }
            set
            {
                switchStatus = value;
                this.Invalidate();
                this.SwitchChanged?.Invoke(this, null);
            }
        }

        #endregion

        #region Event
        [Browsable(true)]
        [Category("自定义事件")]
        [Description("切换触发事件")]
        public event EventHandler SwitchChanged;

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
            if (outGap > 0.5f * this.width || outGap > 0.5f * this.height)
            {
                return;
            }

            Point centerPoint = GetCenterPoint();
            
            //绘制外环
            p = new Pen(circleColor, outWidth);
            RectangleF rec = new RectangleF(outGap, outGap, (centerPoint.X - outGap) * 2, (centerPoint.X - outGap) * 2);

            g.DrawEllipse(p, rec);

            //绘制内圆
            sb = new SolidBrush(circleColor);

            rec = new RectangleF(inGap, inGap, (centerPoint.X - inGap) * 2, (centerPoint.X - inGap) * 2);

            g.FillEllipse(sb, rec);

            g.TranslateTransform(centerPoint.X, centerPoint.Y);


            if (switchStatus)
            {
                g.RotateTransform(36.0f);
            }
            else
            {
                g.RotateTransform(-36.0f);
            }

            rec = new RectangleF(-toggleWidth * 0.5f, toggleGap - centerPoint.Y, toggleWidth, (centerPoint.Y - toggleGap) * 2);

            g.FillRectangle(new SolidBrush(toggleColor), rec);

            rec = new RectangleF(-toggleWidth * 0.5f + toggleforeGap, toggleGap - centerPoint.Y + toggleforeGap, toggleWidth - 2 * toggleforeGap, toggleforeHeight);

            g.FillEllipse(new SolidBrush(toggleforeColor), rec);

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
