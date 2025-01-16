using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EIMS
{
    public partial class EmployeeDepartmentForm : Form
    {
        private int edId = -1;
        private DataTable dtEmployees;
        private DataTable dtDepartments;
        public EmployeeDepartmentForm()
        {
            InitializeComponent();
        }

        private void AdjustListViewColumns(ListView listView)
        {
            // 获取ListView的可用宽度（减去垂直滚动条的宽度）
            int availableWidth = listView.ClientSize.Width;

            // 如果ListView没有显示垂直滚动条，则不需要减去滚动条宽度
            if (listView.Items.Count * listView.Font.Height > listView.ClientSize.Height)
            {
                availableWidth -= SystemInformation.VerticalScrollBarWidth;
            }

            // 计算所有列的宽度总和
            int totalWidth = 0;
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                int maxWidth = 0;

                // 计算列头宽度
                int headerWidth = TextRenderer.MeasureText(listView.Columns[i].Text, listView.Font).Width;
                maxWidth = Math.Max(maxWidth, headerWidth);

                // 计算每列中所有项的宽度
                foreach (ListViewItem item in listView.Items)
                {
                    int itemWidth = TextRenderer.MeasureText(item.SubItems[i].Text, listView.Font).Width;
                    maxWidth = Math.Max(maxWidth, itemWidth);
                }

                // 设置列宽为最大值，并增加10像素作为边距
                listView.Columns[i].Width = maxWidth + 10;
                totalWidth += listView.Columns[i].Width;
            }

            // 如果列的宽度总和小于可用宽度，将剩余宽度分配给最后一列
            if (totalWidth < availableWidth)
            {
                listView.Columns[listView.Columns.Count - 1].Width += availableWidth - totalWidth;
            }
        }

        private void DataBindInfo()
        {
            string query = @"
                SELECT
                    ed.ed_id,
                    ed.emp_no,
                    CONCAT(ei.first_name, ei.last_name) AS employee_name,
                    ed.dept_no,
                    di.dept_name,
                    ed.ed_entrydate,
                    ed.ed_leavedate,
                    ed.ed_status
                FROM
                    employee_department ed
                JOIN
                    employee_info ei ON ed.emp_no = ei.emp_no
                JOIN
                    department_info di ON ed.dept_no = di.dept_no;";
            DataTable dt = DataAccessLayer.ExecuteQuery(query);
            lvEmployeeDepartment.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr["ed_id"].ToString());
                item.SubItems.Add(dr["emp_no"].ToString());
                item.SubItems.Add(dr["employee_name"].ToString());
                item.SubItems.Add(dr["dept_no"].ToString());
                item.SubItems.Add(dr["dept_name"].ToString());

                if(string.IsNullOrEmpty(dr["ed_entrydate"].ToString()))
                {
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems.Add(DateTime.Parse(dr["ed_entrydate"].ToString()).ToString("yyyy-MM-dd"));
                }
                if (string.IsNullOrEmpty(dr["ed_leavedate"].ToString()))
                {
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems.Add(DateTime.Parse(dr["ed_leavedate"].ToString()).ToString("yyyy-MM-dd"));
                }

                switch (dr["ed_status"].ToString())
                {
                    case "1":
                        item.SubItems.Add("录属");
                        break;
                    case "2":
                        item.SubItems.Add("离开");
                        break;
                    default:
                        item.SubItems.Add("未知");
                        break;
                }
                lvEmployeeDepartment.Items.Add(item);
            }
            AdjustListViewColumns(lvEmployeeDepartment);

            query = "SELECT emp_no, CONCAT(first_name, last_name) AS employee_name FROM employee_info";
            dtEmployees = DataAccessLayer.ExecuteQuery(query);
            cbbEmpNo.DataSource = dtEmployees;
            cbbEmpNo.DisplayMember = "emp_no";
            cbbEmpNo.ValueMember = "emp_no";

            cbbEmpName.DataSource = dtEmployees;
            cbbEmpName.DisplayMember = "employee_name";
            cbbEmpName.ValueMember = "emp_no";

            query = "SELECT dept_no, dept_name FROM department_info";
            dtDepartments = DataAccessLayer.ExecuteQuery(query);
            cbbDeptNo.DataSource = dtDepartments;
            cbbDeptNo.DisplayMember = "dept_no";
            cbbDeptNo.ValueMember = "dept_no";

            cbbDeptName.DataSource = dtDepartments;
            cbbDeptName.DisplayMember = "dept_name";
            cbbDeptName.ValueMember = "dept_no";
            
            cbbEmpNo.SelectedIndex = -1;
            cbbEmpName.SelectedIndex = -1;
            cbbDeptNo.SelectedIndex = -1;
            cbbDeptName.SelectedIndex = -1;
        }

        private void ClearInfo()
        {
            cbbEmpNo.SelectedIndex = -1;
            cbbEmpName.SelectedIndex = -1;
            cbbDeptNo.SelectedIndex = -1;
            cbbDeptName.SelectedIndex = -1;
            rbEntry.Checked = true;
            edId = -1;
            lblOperation.Text = "添加";
            cbbEmpNo.Enabled = true;
            cbbEmpName.Enabled = true;
            cbbDeptNo.Enabled = true;
            cbbDeptName.Enabled = true;
        }

        private void EmployeeDepartmentForm_Load(object sender, EventArgs e)
        {
            DataBindInfo();
        }

        private void lvEmployeeDepartment_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvEmployeeDepartment.SelectedItems.Count > 0)
            {
                ListViewItem item = lvEmployeeDepartment.SelectedItems[0];
                edId = int.Parse(item.SubItems[0].Text);
                cbbEmpNo.SelectedValue = item.SubItems[1].Text;
                cbbEmpName.SelectedValue = item.SubItems[1].Text;
                cbbDeptNo.SelectedValue = item.SubItems[3].Text;
                cbbDeptName.SelectedValue = item.SubItems[3].Text;
                if(item.SubItems[7].Text == "录属")
                {
                    rbEntry.Checked = true;
                }
                else
                {
                    rbLeave.Checked = true;
                }
                lblOperation.Text = "修改";
                cbbEmpNo.Enabled = false;
                cbbEmpName.Enabled = false;
                cbbDeptNo.Enabled = false;
                cbbDeptName.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int empNo = int.Parse(cbbEmpNo.SelectedValue.ToString());
            int deptNo = int.Parse(cbbDeptNo.SelectedValue.ToString());
            string entryDate = DateTime.Now.ToString("yyyy-MM-dd");
            string leaveDate = DateTime.Now.ToString("yyyy-MM-dd");
            int edStatus = rbEntry.Checked ? 1 : 2;

            if (empNo == -1)
            {
                lblNote.Text = "部门名称不能为空！";
                lblNote.ForeColor = Color.Red;
                cbbEmpNo.Focus();
                return;
            }
            else if (deptNo == -1)
            {
                lblNote.Text = "部门编号不能为空！";
                lblNote.ForeColor = Color.Red;
                cbbDeptNo.Focus();
                return;
            }

            if (lblOperation.Text == "添加")
            {
                string query;
                if (edStatus == 1)
                {
                    query = $@"
                        INSERT INTO employee_department (emp_no, dept_no, ed_entrydate, ed_status)
                        VALUES ({empNo}, {deptNo}, '{entryDate}', {edStatus});";
                }
                else if (edStatus == 2)
                {
                    query = $@"
                        INSERT INTO employee_department (emp_no, dept_no, ed_leavedate, ed_status)
                        VALUES ({empNo}, {deptNo}, '{leaveDate}', {edStatus});";
                }
                else
                {
                    lblNote.Text = "状态值无效！";
                    lblNote.ForeColor = Color.Red;
                    return;
                }
                int i = DataAccessLayer.ExecuteNonQuery(query);
                if (i > 0)
                {
                    lblNote.Text = "添加成功！";
                    lblNote.ForeColor = Color.Green;
                    DataBindInfo();
                    ClearInfo();
                }
                else
                {
                    lblNote.Text = "添加失败！";
                    lblNote.ForeColor = Color.Red;
                }
            }
            else if (lblOperation.Text == "修改")
            {
                string query;
                if (edStatus == 1)
                {
                    query = $@"
                        UPDATE employee_department
                        SET dept_no = {deptNo}, ed_entrydate = '{entryDate}', ed_status = {edStatus}
                        WHERE emp_no = {empNo} and dept_no = {deptNo};";
                }
                else if (edStatus == 2)
                {
                    query = $@"
                        UPDATE employee_department
                        SET dept_no = {deptNo}, ed_leavedate = '{leaveDate}', ed_status = {edStatus}
                        WHERE emp_no = {empNo} and dept_no = {deptNo};";
                }
                else
                {
                    lblNote.Text = "状态值无效！";
                    lblNote.ForeColor = Color.Red;
                    return;
                }
                int i = DataAccessLayer.ExecuteNonQuery(query);
                if (i > 0)
                {
                    lblNote.Text = "修改成功！";
                    lblNote.ForeColor = Color.Green;
                    DataBindInfo();
                    ClearInfo();
                }
                else
                {
                    lblNote.Text = "修改失败！";
                    lblNote.ForeColor = Color.Red;
                }
            }
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            ClearInfo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (edId == -1)
            {
                lblNote.Text = "请选择要删除的员工部门录属信息！";
                lblNote.ForeColor = Color.Red;
                return;
            }
            string query = $"delete from employee_department where ed_id={edId};";
            int i = DataAccessLayer.ExecuteNonQuery(query);
            if (i > 0)
            {
                lblNote.Text = "删除成功！";
                lblNote.ForeColor = Color.Green;
                DataBindInfo();
                ClearInfo();
            }
            else
            {
                lblNote.Text = "删除失败！";
                lblNote.ForeColor = Color.Red;
            }
        }

        private void cbbEmpNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbEmpNo.SelectedIndex == -1) return;
            if (cbbEmpNo.SelectedValue is DataRowView rowView)
            {
                cbbEmpName.SelectedValue = rowView["emp_no"].ToString();
            }
            else
            {
                cbbEmpName.SelectedValue = cbbEmpNo.SelectedValue;
            }
        }

        private void cbbEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbEmpName.SelectedIndex == -1) return;
            if (cbbEmpName.SelectedValue is DataRowView rowView)
            {
                cbbEmpNo.SelectedValue = rowView["emp_no"].ToString();
            }
            else
            {
                cbbEmpNo.SelectedValue = cbbEmpName.SelectedValue;
            }
        }

        private void cbbDeptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDeptNo.SelectedIndex == -1) return;
            if (cbbDeptNo.SelectedValue is DataRowView rowView)
            {
                cbbDeptName.SelectedValue = rowView["dept_no"].ToString();
            }
            else
            {
                cbbDeptName.SelectedValue = cbbDeptNo.SelectedValue;
            }
        }

        private void cbbDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDeptName.SelectedIndex == -1) return;
            if (cbbDeptName.SelectedValue is DataRowView rowView)
            {
                cbbDeptNo.SelectedValue = rowView["dept_no"].ToString();
            }
            else
            {
                cbbDeptNo.SelectedValue = cbbDeptName.SelectedValue;
            }
        }
    }
}
