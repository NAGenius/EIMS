using System.Data;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace EIMS
{
    public partial class EmployeeSearchForm : Form
    {
        private string commonQuery = @"
                SELECT
                    ei.emp_no,
                    ei.first_name,
                    ei.last_name,
                    di.dept_name,
                    ei.gender,
                    ei.hire_date,
                    ei.birthday,
                    ei.address,
                    ei.telephone
                FROM
                    employee_info ei
                LEFT JOIN
                    employee_department ed ON ei.emp_no = ed.emp_no
                LEFT JOIN
                    department_info di ON ed.dept_no = di.dept_no;";
        private int empNo;
        private DataTable dtEmployees;
        public EmployeeSearchForm()
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

        private int DataBindInfo(string query)
        {
            DataTable dt = DataAccessLayer.ExecuteQuery(query);
            lvEmployeeInfo.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr["emp_no"].ToString());
                item.SubItems.Add(dr["first_name"].ToString());
                item.SubItems.Add(dr["last_name"].ToString());
                item.SubItems.Add(dr["dept_name"].ToString());

                switch (dr["gender"].ToString())
                {
                    case "1":
                        item.SubItems.Add("男");
                        break;
                    case "0":
                        item.SubItems.Add("女");
                        break;
                    default:
                        break;
                }

                item.SubItems.Add(DateTime.Parse(dr["hire_date"].ToString()).ToString("yyyy-MM-dd"));
                item.SubItems.Add(DateTime.Parse(dr["birthday"].ToString()).ToString("yyyy-MM-dd"));
                item.SubItems.Add(dr["address"].ToString());
                item.SubItems.Add(dr["telephone"].ToString());
                lvEmployeeInfo.Items.Add(item);
            }
            AdjustListViewColumns(lvEmployeeInfo);

            query = "SELECT dept_name FROM department_info";
            dtEmployees = DataAccessLayer.ExecuteQuery(query);
            cbbDeptName.DataSource = dtEmployees;
            cbbDeptName.DisplayMember = "dept_name";
            cbbDeptName.ValueMember = "dept_name";

            cbbDeptName.SelectedIndex = -1;

            return dt.Rows.Count;
        }

        private void ClearInfo()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmpNo.Text = "";
            cbbDeptName.SelectedIndex = -1;
            txtTelephone.Text = "";
            txtAddress.Text = "";
            rbAll.Checked = true;
            dtpBirthday.Value = new DateTime(2000, 1, 1);
            dtpBirthday2.Value = DateTime.Now;
            dtpHireDate.Value = new DateTime(2000, 1, 1);
            dtpHireDate2.Value = DateTime.Now;
            empNo = -1;
            lblOperation.Text = "查询";
            btnSearch.Text = "查询";
            txtEmpNo.ReadOnly = false;
            cbbDeptName.Enabled = true;
        }

        private void EmployeeSearchForm_Load(object sender, EventArgs e)
        {
            DataBindInfo(commonQuery);
        }


        private void lvEmployeeInfo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvEmployeeInfo.SelectedItems.Count > 0)
            {
                ListViewItem item = lvEmployeeInfo.SelectedItems[0];
                empNo = int.Parse(item.SubItems[0].Text);
                txtEmpNo.Text = item.SubItems[0].Text;
                txtFirstName.Text = item.SubItems[1].Text;
                txtLastName.Text = item.SubItems[2].Text;
                cbbDeptName.SelectedValue = item.SubItems[3].Text;
                if(item.SubItems[4].Text == "男")
                {
                    rbMan.Checked = true;
                }
                else
                {
                    rbWoman.Checked = true;
                }
                dtpHireDate.Value = DateTime.ParseExact(item.SubItems[5].Text, "yyyy-MM-dd", null);
                dtpBirthday.Value = DateTime.ParseExact(item.SubItems[6].Text, "yyyy-MM-dd", null);
                txtAddress.Text = item.SubItems[7].Text;
                txtTelephone.Text = item.SubItems[8].Text;
                lblOperation.Text = "修改";
                btnSearch.Text = "修改";
                txtEmpNo.ReadOnly = true;
                cbbDeptName.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string deptName = cbbDeptName.Text;
            string telephone = txtTelephone.Text;
            string address = txtAddress.Text;
            int gender = rbMan.Checked ? 1 : (rbWoman.Checked ? 0 : -1);
            string hireDate = dtpHireDate.Value.ToString("yyyy-MM-dd");
            string hireDate2 = dtpHireDate2.Value.ToString("yyyy-MM-dd");
            string birthday = dtpBirthday.Value.ToString("yyyy-MM-dd");
            string birthday2 = dtpBirthday2.Value.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(txtEmpNo.Text))
            {
                empNo = -1;
            }
            else
            {
                empNo = int.Parse(txtEmpNo.Text);
            }

            if (lblOperation.Text == "查询")
            {
                string query = @"
                    SELECT
                        ei.emp_no,
                        ei.first_name,
                        ei.last_name,
                        di.dept_name,
                        ei.gender,
                        ei.hire_date,
                        ei.birthday,
                        ei.address,
                        ei.telephone
                    FROM
                        employee_info ei
                    LEFT JOIN
                        employee_department ed ON ei.emp_no = ed.emp_no
                    LEFT JOIN
                        department_info di ON ed.dept_no = di.dept_no
                    WHERE
                        1 = 1"; // 初始条件，确保后续条件可以拼接
                // 添加查询条件
                if (empNo > 0) // 如果工号有效
                {
                    query += $" AND ei.emp_no = {empNo}";
                }
                if (!string.IsNullOrEmpty(firstName))
                {
                    query += $" AND ei.first_name LIKE '%{firstName}%'";
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    query += $" AND ei.last_name LIKE '%{lastName}%'";
                }
                if (!string.IsNullOrEmpty(deptName))
                {
                    query += $" AND di.dept_name LIKE '%{deptName}%'";
                }
                if (!string.IsNullOrEmpty(telephone))
                {
                    query += $" AND ei.telephone LIKE '%{telephone}%'";
                }
                if (!string.IsNullOrEmpty(address))
                {
                    query += $" AND ei.address LIKE '%{address}%'";
                }
                if (gender != -1) // 如果选择了性别
                {
                    query += $" AND ei.gender = {gender}";
                }
                if (!string.IsNullOrEmpty(hireDate))
                {
                    query += $" AND ei.hire_date BETWEEN '{hireDate}' AND '{hireDate2}'";
                }
                if (!string.IsNullOrEmpty(birthday))
                {
                    query += $" AND ei.birthday BETWEEN '{birthday}' AND '{birthday2}'";
                }
                query += ";";

                int i = DataBindInfo(query);
                if (i > 0)
                {
                    lblNote.Text = "查询成功！";
                    lblNote.ForeColor = Color.Green;
                    ClearInfo();
                }
                else
                {
                    lblNote.Text = "查询失败！";
                    lblNote.ForeColor = Color.Red;
                }
            }
            else if (lblOperation.Text == "修改")
            {
                string query = $"update employee_info set first_name='{firstName}', last_name='{lastName}'," +
                    $" gender={gender}, hire_date='{hireDate}', birthday='{birthday}'," +
                    $" telephone='{telephone}', address='{address}' where emp_no={empNo};";
                int i = DataAccessLayer.ExecuteNonQuery(query);
                if (i > 0)
                {
                    lblNote.Text = "修改成功！";
                    lblNote.ForeColor = Color.Green;
                    DataBindInfo(commonQuery);
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
            if (empNo == -1)
            {
                lblNote.Text = "请选择要删除的用户信息！";
                lblNote.ForeColor = Color.Red;
                return;
            }
            string query = $"delete from employee_info where emp_no={empNo};";
            int i = DataAccessLayer.ExecuteNonQuery(query);
            if (i > 0)
            {
                lblNote.Text = "删除成功！";
                lblNote.ForeColor = Color.Green;
                DataBindInfo(commonQuery);
                ClearInfo();
            }
            else
            {
                lblNote.Text = "删除失败！";
                lblNote.ForeColor = Color.Red;
            }
        }
    }
}
