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
    /// <summary>
    /// 管道左、右的转向类型
    /// </summary>
    public enum PipeTurnDirection
    {
        Up = 1,
        Down,
        Left,
        Right,
        None
    }

    /// <summary>
    /// 管道的样式，水平还是数值
    /// </summary>
    public enum DirectionStyle
    {
        Horizontal = 1,
        Vertical
    }


    public partial class xktFlowControl : UserControl
    {
        public xktFlowControl()
        {
            InitializeComponent();

            //设置控件样式
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.mytimer = new Timer();
            mytimer.Interval = 50;
            this.mytimer.Tick += Mytimer_Tick;
        }

        private void Mytimer_Tick(object sender, EventArgs e)
        {
            this.startOffset = this.startOffset - this.moveSpeed;

            if (this.startOffset > this.pipeLength + this.gapLength || this.startOffset < (this.pipeLength + this.gapLength) * (-1))
            {
                this.startOffset = 0.0f;
            }

            this.Invalidate();
        }

        private Graphics g;

        private Pen p;

        private float startOffset = 0.0f;

        private Timer mytimer;

        #region Fileds

        private int pipeWidth = 5;

        [Browsable(true), Category("自定义属性"), Description("流动条宽度")]
        public int PipeWidth
        {
            get { return pipeWidth; }
            set
            {
                this.pipeWidth = value;

                base.Invalidate();
            }
        }

        private Color colorCenter = Color.DodgerBlue;

        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件的流动颜色")]
        public Color ColorCenter
        {
            get
            {
                return this.colorCenter;
            }
            set
            {
                this.colorCenter = value;
                base.Invalidate();
            }
        }

        private Color borderColor = Color.DimGray;

        [Browsable(true), Category("自定义属性"), Description("获取或设置管道边线颜色")]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                p = new Pen(value, 1.0f);
                base.Invalidate();
            }
        }

        private Color edgeColor = Color.DimGray;

        [Browsable(true), Category("自定义属性"), Description("获取或设置管道边缘颜色")]
        public Color EdgeColor
        {
            get
            {
                return this.edgeColor;
            }
            set
            {
                this.edgeColor = value;
                base.Invalidate();
            }
        }

        private Color centerColor = Color.LightGray;

        [Browsable(true), Category("自定义属性"), Description("获取或设置管道控件的中心颜色")]
        public Color LineCenterColor
        {
            get
            {
                return this.centerColor;
            }
            set
            {
                this.centerColor = value;
                base.Invalidate();
            }
        }


        private PipeTurnDirection pipeTurnLeft = PipeTurnDirection.None;

        [Browsable(true), Category("自定义属性"), Description("左管道的转向类型")]
        public PipeTurnDirection PipeTurnLeft
        {
            get
            {
                return this.pipeTurnLeft;
            }
            set
            {
                this.pipeTurnLeft = value;
                base.Invalidate();
            }
        }

        private PipeTurnDirection pipeTurnRight = PipeTurnDirection.None;

        [Browsable(true), Category("自定义属性"), Description("右管道的转向类型")]
        public PipeTurnDirection PipeTurnRight
        {
            get
            {
                return this.pipeTurnRight;
            }
            set
            {
                this.pipeTurnRight = value;
                base.Invalidate();
            }
        }

        private DirectionStyle pipeLineStyle = DirectionStyle.Horizontal;

        [Browsable(true), Category("自定义属性"), Description("设置管道是横向的还是纵向的")]
        public DirectionStyle PipeLineStyle
        {
            get
            {
                return this.pipeLineStyle;
            }
            set
            {
                this.pipeLineStyle = value;
                base.Invalidate();
            }
        }

        private bool isActive = false;

        [Browsable(true), Category("自定义属性"), DefaultValue(false), Description("获取或设置管道线是否激活液体显示")]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
                this.mytimer.Enabled = value;
                base.Invalidate();
            }
        }


        private float moveSpeed = 0.3f;

        [Browsable(true), Category("自定义属性"), Description("管道线液体流动的速度，0为静止，正数为正向流动，负数为反向流动")]
        public float MoveSpeed
        {
            get
            {
                return this.moveSpeed;
            }
            set
            {
                this.moveSpeed = value;
                base.Invalidate();
            }
        }

        private int pipeLength = 5;

        [Browsable(true), Category("自定义属性"), Description("流动条长度")]
        public int PipeLength
        {
            get
            {
                return this.pipeLength;
            }
            set
            {
                this.pipeLength = value;
                base.Invalidate();
            }
        }

        private int gapLength = 5;

        [Browsable(true), Category("自定义属性"), Description("间隙长度")]
        public int GapLength
        {
            get
            {
                return this.gapLength;
            }
            set
            {
                this.gapLength = value;
                base.Invalidate();
            }
        }


        #endregion

        #region Override

        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            p = new Pen(this.borderColor, 1.0f);

            ColorBlend colorBlend = new ColorBlend();

            //渐变线的比例
            colorBlend.Positions = new float[]
                {
                    0.0f,0.5f,1.0f
                };
            colorBlend.Colors = new Color[]
                {
                    this.edgeColor,this.centerColor,this.edgeColor
                };
            //水平管道
            if (this.pipeLineStyle == DirectionStyle.Horizontal)
            {
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, this.Height), edgeColor, edgeColor);

                linearGradientBrush.InterpolationColors = colorBlend;
                //绘制左部分
                switch (this.pipeTurnLeft)
                {
                    case PipeTurnDirection.Up:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(0, this.Height * (-1), this.Height * 2, this.Height * 2), 90.0f, 90.0f);
                        break;
                    case PipeTurnDirection.Down:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(0, 0, this.Height * 2, this.Height * 2), 180.0f, 90.0f);
                        break;
                    default:
                        this.PaintRectangle(g, linearGradientBrush, p, new Rectangle(0, 0, this.Height, this.Height));
                        break;
                }


                //绘制右部分
                switch (this.pipeTurnRight)
                {
                    case PipeTurnDirection.Up:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(this.Width - this.Height * 2, this.Height * (-1), this.Height * 2, this.Height * 2), 0.0f, 90.0f);
                        break;
                    case PipeTurnDirection.Down:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(this.Width - this.Height * 2, 0, this.Height * 2, this.Height * 2), 270.0f, 90.0f);
                        break;
                    default:
                        this.PaintRectangle(g, linearGradientBrush, p, new Rectangle(this.Width - this.Height, 0, this.Height, this.Height));
                        break;
                }

                //绘制中间
                if (this.Width > this.Height * 2)
                {
                    this.PaintRectangle(g, linearGradientBrush, p, new Rectangle(this.Height - 1, 0, this.Width - this.Height * 2 + 2, this.Height));
                }

                if (isActive)
                {
                    GraphicsPath graphicsPath = new GraphicsPath();

                    switch (this.pipeTurnLeft)
                    {
                        case PipeTurnDirection.Up:
                            graphicsPath.AddArc(new Rectangle(this.Height / 2, this.Height / 2 * (-1) - 1, this.Height, this.Height), 180.0f, -90.0f);
                            break;
                        case PipeTurnDirection.Down:
                            graphicsPath.AddArc(new Rectangle(this.Height / 2, this.Height / 2, this.Height, this.Height), 180.0f, 90.0f);
                            break;
                        default:
                            graphicsPath.AddLine(0, this.Height / 2, this.Height, this.Height / 2);
                            break;
                    }

                    if (this.Width > this.Height * 2)
                    {
                        graphicsPath.AddLine(base.Height, base.Height / 2, base.Width - base.Height - 1, base.Height / 2);
                    }

                    switch (this.pipeTurnRight)
                    {
                        case PipeTurnDirection.Up:
                            graphicsPath.AddArc(new Rectangle(base.Width - 1 - base.Height * 3 / 2, -base.Height / 2 - 1, base.Height, base.Height), 90f, -90f);
                            break;
                        case PipeTurnDirection.Down:
                            graphicsPath.AddArc(new Rectangle(base.Width - 1 - base.Height * 3 / 2, base.Height / 2, base.Height, base.Height), 270f, 90f);
                            break;
                        default:
                            graphicsPath.AddLine(base.Width - base.Height, base.Height / 2, base.Width - 1, base.Height / 2);
                            break;
                    }

                    //其实就是画虚线，画虚线的关键在于笔和路径

                    Pen pen = new Pen(this.colorCenter, this.pipeWidth);
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[]
                        {
                            pipeLength,gapLength
                        };
                    pen.DashOffset = this.startOffset;
                    g.DrawPath(pen, graphicsPath);

                }
            }
            //竖直管道
            else
            {
                LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(new Point(0, 0), new Point(this.Width, 0), edgeColor, edgeColor);

                linearGradientBrush2.InterpolationColors = colorBlend;
                //绘制上部分
                switch (this.pipeTurnLeft)
                {
                    case PipeTurnDirection.Left:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(this.Width * (-1), 0, this.Width * 2, this.Width * 2), 270.0f, 90.0f);
                        break;
                    case PipeTurnDirection.Right:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(0, 0, this.Width * 2, this.Width * 2), 180.0f, 90.0f);
                        break;
                    default:
                        this.PaintRectangle(g, linearGradientBrush2, p, new Rectangle(0, 0, this.Width, this.Width));
                        break;
                }

                //绘制下部分
                switch (this.pipeTurnRight)
                {
                    case PipeTurnDirection.Left:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(this.Width * (-1), this.Height - this.Width * 2, this.Width * 2, this.Width * 2), 0.0f, 90.0f);
                        break;
                    case PipeTurnDirection.Right:
                        this.PaintEllipse(g, colorBlend, p, new Rectangle(0, this.Height - this.Width * 2, this.Width * 2, this.Width * 2), 90.0f, 90.0f);
                        break;
                    default:
                        this.PaintRectangle(g, linearGradientBrush2, p, new Rectangle(0, this.Height - this.Width, this.Width, this.Width));
                        break;
                }

                //绘制中间
                if (this.Height > this.Width * 2)
                {
                    this.PaintRectangle(g, linearGradientBrush2, p, new Rectangle(0, this.Width - 1, this.Width, this.Height - this.Width * 2 + 2));
                }

                if (isActive)
                {
                    //绘制路径
                    GraphicsPath graphicsPath = new GraphicsPath();

                    switch (this.pipeTurnLeft)
                    {
                        case PipeTurnDirection.Left:
                            graphicsPath.AddArc(new Rectangle(this.Width / 2 * (-1), this.Width / 2 - 1, this.Width, this.Width), 270.0f, 90.0f);
                            break;
                        case PipeTurnDirection.Right:
                            graphicsPath.AddArc(new Rectangle(this.Width / 2, this.Width / 2 - 1, this.Width, this.Width), 270.0f, -90.0f);
                            break;
                        default:
                            graphicsPath.AddLine(this.Width / 2, 0, this.Width / 2, this.Width);
                            break;
                    }

                    if (this.Height > this.Width * 2)
                    {
                        graphicsPath.AddLine(this.Width / 2 - 1, this.Width, this.Width / 2 - 1, this.Height - this.Width - 1);
                    }

                    switch (this.pipeTurnRight)
                    {
                        case PipeTurnDirection.Left:
                            graphicsPath.AddArc(new Rectangle(this.Width / 2 * (-1), this.Height - this.Width / 2 * 3 - 1, this.Width, this.Width), 0f, 90f);
                            break;
                        case PipeTurnDirection.Right:
                            graphicsPath.AddArc(new Rectangle(this.Width / 2, this.Height - this.Width / 2 * 3 - 1, this.Width, this.Width), -180, -90f);
                            break;
                        default:
                            graphicsPath.AddLine(this.Width / 2, this.Height - this.Width, this.Width / 2, this.Height);
                            break;
                    }

                    //其实就是画虚线，画虚线的关键在于笔和路径

                    Pen pen = new Pen(this.colorCenter, this.pipeWidth);
                    pen.DashStyle = DashStyle.Custom;
                    pen.DashPattern = new float[]
                        {
                            pipeLength,gapLength
                        };
                    pen.DashOffset = this.startOffset;
                    g.DrawPath(pen, graphicsPath);

                }
            }

            base.OnPaint(e);
        }



        #endregion

        #region Methods

        /// <summary>
        /// 根据外切矩形绘制内部的扇形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="colorBlend"></param>
        /// <param name="p"></param>
        /// <param name="rect"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        private void PaintEllipse(Graphics g, ColorBlend colorBlend, Pen p, Rectangle rect, float startAngle, float sweepAngle)
        {
            //第一步：创建GraphicsPath
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(rect);

            //第二步：PathGradientBrush
            PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);

            pathGradientBrush.CenterPoint = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            pathGradientBrush.InterpolationColors = colorBlend;

            //第三步：绘制Pipe
            g.FillPie(pathGradientBrush, rect, startAngle, sweepAngle);

            //第四步：绘制边线
            g.DrawArc(p, rect, startAngle, sweepAngle);

        }

        /// <summary>
        /// 根据外切矩形填充渐变色
        /// </summary>
        /// <param name="g"></param>
        /// <param name="brush"></param>
        /// <param name="pen"></param>
        /// <param name="rectangle"></param>
        private void PaintRectangle(Graphics g, Brush brush, Pen pen, Rectangle rectangle)
        {
            //填充矩形
            g.FillRectangle(brush, rectangle);

            switch (this.pipeLineStyle)
            {
                case DirectionStyle.Horizontal:
                    g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y);
                    //g.DrawLine(pen, rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height - 1);
                    break;
                case DirectionStyle.Vertical:
                    g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + rectangle.Height);
                    // g.DrawLine(pen, rectangle.X + rectangle.Width - 1, rectangle.Y, rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height);
                    break;
                default:
                    break;
            }

        }

        #endregion

    }
}
