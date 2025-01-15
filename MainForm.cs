using System.Windows.Forms;

namespace EIMS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 释放资源
                this.Dispose();
                // 不能只关闭自身（还有登录窗体） this.Close()无法退出
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OpenCustomerInfoForm(object sender, System.EventArgs e)
        {
            // 打开客户信息管理窗体
            CustomerInfoForm customerInfoForm = new CustomerInfoForm();
            customerInfoForm.Show();
        }

        private void OpenDepartmentInfoForm(object sender, System.EventArgs e)
        {
            // 打开部门信息管理窗体
            DepartmentInfoForm departmentInfoForm = new DepartmentInfoForm();
            departmentInfoForm.Show();
        }

        private void OpenEmployeeInfoForm(object sender, System.EventArgs e)
        {
            // 打开员工信息管理窗体
            EmployeeInfoForm employeeInfoForm = new EmployeeInfoForm();
            employeeInfoForm.Show();
        }

        private void OpenEmployeeDepartmentForm(object sender, System.EventArgs e)
        {
            // 打开员工部门隶属管理窗体
            EmployeeDepartmentForm employeeDepartmentForm = new EmployeeDepartmentForm();
            employeeDepartmentForm.Show();
        }

        private void OpenEmployeeSearchForm(object sender, System.EventArgs e)
        {
            // 打开员工职位管理窗体
            EmployeeSearchForm employeeSearchForm = new EmployeeSearchForm();
            employeeSearchForm.Show();
        }
    }
}
