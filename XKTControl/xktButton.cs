using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XKTControl
{
    public partial class xktButton : Button
    {
        public xktButton()
        {
            InitializeComponent();
            this.Size = new Size(100, 32);
        }

        public xktButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        #region Enum 枚举图标类型

        public enum ButtonPresetImage
        {
            None,

            Check,

            Close,

            Cancel,

            Back,

            Down,

            Go,

            Up,

            Folder,

            Refresh,

            Setting,

            FolderOpen,

            DocumentDelete,

            Document,

            DocumentEdit,

            Info,

            DocumentAdd,

            Global,

            Calculator,

            Calendar,

            Printer


        }

        #endregion


        #region Field 属性

        private ButtonPresetImage buttonType;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("显示的图片样式")]
        public ButtonPresetImage ButtonType
        {
            get { return buttonType; }
            set
            {
                buttonType = value;

                switch (buttonType)
                {
                    case ButtonPresetImage.None:
                        this.Image = null;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        break;
                    case ButtonPresetImage.Check:
                        this.Image = Properties.Resources.check;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Close:
                        this.Image = Properties.Resources.close;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Cancel:
                        this.Image = Properties.Resources.cancel;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Back:
                        this.Image = Properties.Resources.back;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Down:
                        this.Image = Properties.Resources.down;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Go:
                        this.Image = Properties.Resources.go;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Up:
                        this.Image = Properties.Resources.up;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Folder:
                        this.Image = Properties.Resources.folder;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Refresh:
                        this.Image = Properties.Resources.refresh;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Setting:
                        this.Image = Properties.Resources.setting;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.FolderOpen:
                        this.Image = Properties.Resources.folder_open;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.DocumentDelete:
                        this.Image = Properties.Resources.document_delete;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Document:
                        this.Image = Properties.Resources.document;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.DocumentEdit:
                        this.Image = Properties.Resources.document_edit;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Info:
                        this.Image = Properties.Resources.info;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.DocumentAdd:
                        this.Image = Properties.Resources.document_add;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Global:
                        this.Image = Properties.Resources.web;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Calculator:
                        this.Image = Properties.Resources.calculator;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Calendar:
                        this.Image = Properties.Resources.calendar;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    case ButtonPresetImage.Printer:
                        this.Image = Properties.Resources.printer;
                        this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

    }
}
