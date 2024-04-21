namespace OwlOS_Utility
{
    partial class FAQControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.AnimatorNS.Animation animation1 = new Guna.UI2.AnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAQControl));
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            guna2Transition1 = new Guna.UI2.WinForms.Guna2Transition();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(label4);
            guna2Panel1.Controls.Add(label3);
            guna2Panel1.Controls.Add(label2);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Transition1.SetDecoration(guna2Panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(868, 423);
            guna2Panel1.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            guna2Transition1.SetDecoration(label4, Guna.UI2.AnimatorNS.DecorationType.None);
            label4.Font = new Font("Verdana", 9F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(167, 263);
            label4.Name = "label4";
            label4.Size = new Size(534, 14);
            label4.TabIndex = 42;
            label4.Text = "If you have any other questions, ask on chat or use support channel on our discord";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            guna2Transition1.SetDecoration(label3, Guna.UI2.AnimatorNS.DecorationType.None);
            label3.Font = new Font("Verdana", 9F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(186, 156);
            label3.Name = "label3";
            label3.Size = new Size(497, 42);
            label3.TabIndex = 41;
            label3.Text = "Q: Can I play faceit?\r\nA: Yes, but you need to enable DEP in Services tab. If you are on Windows 11\r\nYou will also need to enable secure boot and TPM";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            guna2Transition1.SetDecoration(label2, Guna.UI2.AnimatorNS.DecorationType.None);
            label2.Font = new Font("Verdana", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(32, 89);
            label2.Name = "label2";
            label2.Size = new Size(804, 42);
            label2.TabIndex = 40;
            label2.Text = resources.GetString("label2.Text");
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            guna2Transition1.SetDecoration(label1, Guna.UI2.AnimatorNS.DecorationType.None);
            label1.Font = new Font("Verdana", 9F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(214, 32);
            label1.Name = "label1";
            label1.Size = new Size(440, 28);
            label1.TabIndex = 39;
            label1.Text = "Q: Why can't i change the screen resolution?\r\nA: If you want to change screen resolution you will need GPU drivers";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // guna2Transition1
            // 
            guna2Transition1.AnimationType = Guna.UI2.AnimatorNS.AnimationType.Transparent;
            guna2Transition1.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = (PointF)resources.GetObject("animation1.BlindCoeff");
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = (PointF)resources.GetObject("animation1.MosaicCoeff");
            animation1.MosaicShift = (PointF)resources.GetObject("animation1.MosaicShift");
            animation1.MosaicSize = 0;
            animation1.Padding = new Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = (PointF)resources.GetObject("animation1.ScaleCoeff");
            animation1.SlideCoeff = (PointF)resources.GetObject("animation1.SlideCoeff");
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 1F;
            guna2Transition1.DefaultAnimation = animation1;
            guna2Transition1.Interval = 3;
            // 
            // FAQControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(32, 32, 32);
            Controls.Add(guna2Panel1);
            guna2Transition1.SetDecoration(this, Guna.UI2.AnimatorNS.DecorationType.None);
            Name = "FAQControl";
            Size = new Size(868, 423);
            Load += UpdatesControl_Load;
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Transition guna2Transition1;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
    }
}
