namespace EIMS
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.客户信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.员工信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.员工信息管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.员工信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部门信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部门信息管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.员工部分录属管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隶属管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCustomerInfoManage = new System.Windows.Forms.ToolStripButton();
            this.btnEmployeeInfoManage = new System.Windows.Forms.ToolStripButton();
            this.btnDepartmentInfoManage = new System.Windows.Forms.ToolStripButton();
            this.btnEmployeeDepartmentManage = new System.Windows.Forms.ToolStripButton();
            this.btnEmployeeSearch = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.客户信息管理ToolStripMenuItem,
            this.员工信息管理ToolStripMenuItem,
            this.部门信息管理ToolStripMenuItem,
            this.员工部分录属管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(887, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 客户信息管理ToolStripMenuItem
            // 
            this.客户信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.信息管理ToolStripMenuItem});
            this.客户信息管理ToolStripMenuItem.Name = "客户信息管理ToolStripMenuItem";
            this.客户信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 27);
            this.客户信息管理ToolStripMenuItem.Text = "客户管理";
            // 
            // 信息管理ToolStripMenuItem
            // 
            this.信息管理ToolStripMenuItem.Image = global::EIMS.Properties.Resources.customer1;
            this.信息管理ToolStripMenuItem.Name = "信息管理ToolStripMenuItem";
            this.信息管理ToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.信息管理ToolStripMenuItem.Text = "客户信息管理";
            this.信息管理ToolStripMenuItem.Click += new System.EventHandler(this.OpenCustomerInfoForm);
            // 
            // 员工信息管理ToolStripMenuItem
            // 
            this.员工信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.员工信息管理ToolStripMenuItem1,
            this.员工信息查询ToolStripMenuItem});
            this.员工信息管理ToolStripMenuItem.Name = "员工信息管理ToolStripMenuItem";
            this.员工信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 27);
            this.员工信息管理ToolStripMenuItem.Text = "员工管理";
            // 
            // 员工信息管理ToolStripMenuItem1
            // 
            this.员工信息管理ToolStripMenuItem1.Image = global::EIMS.Properties.Resources.employee2;
            this.员工信息管理ToolStripMenuItem1.Name = "员工信息管理ToolStripMenuItem1";
            this.员工信息管理ToolStripMenuItem1.Size = new System.Drawing.Size(224, 28);
            this.员工信息管理ToolStripMenuItem1.Text = "员工信息管理";
            this.员工信息管理ToolStripMenuItem1.Click += new System.EventHandler(this.OpenEmployeeInfoForm);
            // 
            // 员工信息查询ToolStripMenuItem
            // 
            this.员工信息查询ToolStripMenuItem.Image = global::EIMS.Properties.Resources.search;
            this.员工信息查询ToolStripMenuItem.Name = "员工信息查询ToolStripMenuItem";
            this.员工信息查询ToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.员工信息查询ToolStripMenuItem.Text = "员工信息查询";
            this.员工信息查询ToolStripMenuItem.Click += new System.EventHandler(this.OpenEmployeeSearchForm);
            // 
            // 部门信息管理ToolStripMenuItem
            // 
            this.部门信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.部门信息管理ToolStripMenuItem1});
            this.部门信息管理ToolStripMenuItem.Name = "部门信息管理ToolStripMenuItem";
            this.部门信息管理ToolStripMenuItem.Size = new System.Drawing.Size(92, 27);
            this.部门信息管理ToolStripMenuItem.Text = "部门管理";
            // 
            // 部门信息管理ToolStripMenuItem1
            // 
            this.部门信息管理ToolStripMenuItem1.Image = global::EIMS.Properties.Resources.department;
            this.部门信息管理ToolStripMenuItem1.Name = "部门信息管理ToolStripMenuItem1";
            this.部门信息管理ToolStripMenuItem1.Size = new System.Drawing.Size(224, 28);
            this.部门信息管理ToolStripMenuItem1.Text = "部门信息管理";
            this.部门信息管理ToolStripMenuItem1.Click += new System.EventHandler(this.OpenDepartmentInfoForm);
            // 
            // 员工部分录属管理ToolStripMenuItem
            // 
            this.员工部分录属管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.隶属管理ToolStripMenuItem});
            this.员工部分录属管理ToolStripMenuItem.Name = "员工部分录属管理ToolStripMenuItem";
            this.员工部分录属管理ToolStripMenuItem.Size = new System.Drawing.Size(126, 27);
            this.员工部分录属管理ToolStripMenuItem.Text = "员工部门管理";
            this.员工部分录属管理ToolStripMenuItem.Click += new System.EventHandler(this.OpenEmployeeDepartmentForm);
            // 
            // 隶属管理ToolStripMenuItem
            // 
            this.隶属管理ToolStripMenuItem.Image = global::EIMS.Properties.Resources.relation;
            this.隶属管理ToolStripMenuItem.Name = "隶属管理ToolStripMenuItem";
            this.隶属管理ToolStripMenuItem.Size = new System.Drawing.Size(224, 28);
            this.隶属管理ToolStripMenuItem.Text = "隶属管理";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCustomerInfoManage,
            this.btnEmployeeInfoManage,
            this.btnDepartmentInfoManage,
            this.btnEmployeeDepartmentManage,
            this.btnEmployeeSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(887, 47);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCustomerInfoManage
            // 
            this.btnCustomerInfoManage.Image = global::EIMS.Properties.Resources.customer;
            this.btnCustomerInfoManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCustomerInfoManage.Name = "btnCustomerInfoManage";
            this.btnCustomerInfoManage.Size = new System.Drawing.Size(103, 44);
            this.btnCustomerInfoManage.Text = "客户信息管理";
            this.btnCustomerInfoManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCustomerInfoManage.Click += new System.EventHandler(this.OpenCustomerInfoForm);
            // 
            // btnEmployeeInfoManage
            // 
            this.btnEmployeeInfoManage.Image = global::EIMS.Properties.Resources.employee2;
            this.btnEmployeeInfoManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmployeeInfoManage.Name = "btnEmployeeInfoManage";
            this.btnEmployeeInfoManage.Size = new System.Drawing.Size(103, 44);
            this.btnEmployeeInfoManage.Text = "员工信息管理";
            this.btnEmployeeInfoManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEmployeeInfoManage.Click += new System.EventHandler(this.OpenEmployeeInfoForm);
            // 
            // btnDepartmentInfoManage
            // 
            this.btnDepartmentInfoManage.Image = global::EIMS.Properties.Resources.department;
            this.btnDepartmentInfoManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDepartmentInfoManage.Name = "btnDepartmentInfoManage";
            this.btnDepartmentInfoManage.Size = new System.Drawing.Size(103, 44);
            this.btnDepartmentInfoManage.Text = "部门信息管理";
            this.btnDepartmentInfoManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDepartmentInfoManage.Click += new System.EventHandler(this.OpenDepartmentInfoForm);
            // 
            // btnEmployeeDepartmentManage
            // 
            this.btnEmployeeDepartmentManage.Image = global::EIMS.Properties.Resources.relation;
            this.btnEmployeeDepartmentManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmployeeDepartmentManage.Name = "btnEmployeeDepartmentManage";
            this.btnEmployeeDepartmentManage.Size = new System.Drawing.Size(133, 44);
            this.btnEmployeeDepartmentManage.Text = "员工部门录属管理";
            this.btnEmployeeDepartmentManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEmployeeDepartmentManage.Click += new System.EventHandler(this.OpenEmployeeDepartmentForm);
            // 
            // btnEmployeeSearch
            // 
            this.btnEmployeeSearch.Image = global::EIMS.Properties.Resources.search;
            this.btnEmployeeSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmployeeSearch.Name = "btnEmployeeSearch";
            this.btnEmployeeSearch.Size = new System.Drawing.Size(103, 44);
            this.btnEmployeeSearch.Text = "员工信息查询";
            this.btnEmployeeSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEmployeeSearch.Click += new System.EventHandler(this.OpenEmployeeSearchForm);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::EIMS.Properties.Resources.main_bg;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(887, 473);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 551);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统主界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 客户信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 员工信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部门信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 员工部分录属管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCustomerInfoManage;
        private System.Windows.Forms.ToolStripButton btnEmployeeInfoManage;
        private System.Windows.Forms.ToolStripButton btnDepartmentInfoManage;
        private System.Windows.Forms.ToolStripButton btnEmployeeDepartmentManage;
        private System.Windows.Forms.ToolStripButton btnEmployeeSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem 信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 员工信息管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 员工信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部门信息管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 隶属管理ToolStripMenuItem;
    }
}