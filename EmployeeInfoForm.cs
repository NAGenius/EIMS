using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EIMS
{
    public partial class EmployeeInfoForm : Form
    {
        private int empNo;
        public EmployeeInfoForm()
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
            string query = "select * from employee_info;";
            DataTable dt = DataAccessLayer.ExecuteQuery(query);
            lvEmployeeInfo.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr["emp_no"].ToString());
                item.SubItems.Add(dr["first_name"].ToString());
                item.SubItems.Add(dr["last_name"].ToString());

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
        }

        private void ClearInfo()
        {
            txtEmpNo.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtTelephone.Text = "";
            txtAddress.Text = "";
            rbMan.Checked = true;
            dtpHireDate.Value = new DateTime(2025, 1, 1);
            dtpBirthday.Value = new DateTime(2000, 1, 1);
            empNo = -1;
            lblOperation.Text = "添加";
            txtEmpNo.ReadOnly = false;
        }

        private void EmployeeInfoForm_Load(object sender, EventArgs e)
        {
            DataBindInfo();
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
                if(item.SubItems[3].Text == "男")
                {
                    rbMan.Checked = true;
                }
                else
                {
                    rbWoman.Checked = true;
                }
                dtpHireDate.Value = DateTime.ParseExact(item.SubItems[4].Text, "yyyy-MM-dd", null);
                dtpBirthday.Value = DateTime.ParseExact(item.SubItems[5].Text, "yyyy-MM-dd", null);
                txtAddress.Text = item.SubItems[6].Text;
                txtTelephone.Text = item.SubItems[7].Text;
                lblOperation.Text = "修改";
                txtEmpNo.ReadOnly = true;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string telephone = txtTelephone.Text;
            string address = txtAddress.Text;
            int gender = rbMan.Checked ? 1 : 0;
            string hireDate = dtpHireDate.Value.ToString("yyyy-MM-dd");
            string birthday = dtpBirthday.Value.ToString("yyyy-MM-dd");
            empNo = int.Parse(txtEmpNo.Text);

            if(empNo == -1)
            {
                lblNote.Text = "请选择要操作的用户信息！";
                lblNote.ForeColor = Color.Red;
                return;
            }
            else if (string.IsNullOrEmpty(firstName))
            {
                lblNote.Text = "姓名不能为空！";
                lblNote.ForeColor = Color.Red;
                txtFirstName.Focus();
                return;
            } 
            else if(string.IsNullOrEmpty(lastName))
            {
                lblNote.Text = "姓氏不能为空！";
                lblNote.ForeColor = Color.Red;
                txtLastName.Focus();
                return;
            } 
            else if(string.IsNullOrEmpty(hireDate))
            {
                lblNote.Text = "入职日期不能为空！";
                lblNote.ForeColor = Color.Red;
                dtpHireDate.Focus();
                return;
            }

            if (lblOperation.Text == "添加")
            {
                string query = $"insert into employee_info values({empNo}, '{firstName}', '{lastName}'," +
                    $" {gender}, '{hireDate}', '{birthday}', '{address}', '{telephone}');";
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
                string query = $"update employee_info set first_name='{firstName}', last_name='{lastName}'," +
                    $" gender={gender}, hire_date='{hireDate}', birthday='{birthday}'," +
                    $" telephone='{telephone}', address='{address}' where emp_no={empNo};";
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
                DataBindInfo();
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
