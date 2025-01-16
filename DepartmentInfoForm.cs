using Google.Protobuf.WellKnownTypes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EIMS
{
    public partial class DepartmentInfoForm : Form
    {
        private int deptNo = -1;
        public DepartmentInfoForm()
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
            string query = "select * from department_info;";
            DataTable dt = DataAccessLayer.ExecuteQuery(query);
            lvDepartmentInfo.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr["dept_no"].ToString());
                item.SubItems.Add(dr["dept_name"].ToString());
                item.SubItems.Add(dr["dept_peoplecount"].ToString());
                lvDepartmentInfo.Items.Add(item);
            }
            AdjustListViewColumns(lvDepartmentInfo);
        }

        private void ClearInfo()
        {
            txtDeptNo.Text = "";
            txtDeptName.Text = "";
            txtDeptPeopleCount.Text = "0";
            deptNo = -1;
            lblOperation.Text = "添加";
            txtDeptNo.ReadOnly = false;
        }

        private void DepartmentInfoForm_Load(object sender, EventArgs e)
        {
            DataBindInfo();
        }

        private void lvDepartmentInfo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvDepartmentInfo.SelectedItems.Count > 0)
            {
                ListViewItem item = lvDepartmentInfo.SelectedItems[0];
                deptNo = int.Parse(item.SubItems[0].Text);
                txtDeptNo.Text = item.SubItems[0].Text;
                txtDeptName.Text = item.SubItems[1].Text;
                txtDeptPeopleCount.Text = item.SubItems[2].Text;
                lblOperation.Text = "修改";
                txtDeptNo.ReadOnly = true;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string deptName = txtDeptName.Text;
            int deptPeopleCount = int.Parse(txtDeptPeopleCount.Text);
            deptNo = int.Parse(txtDeptNo.Text);

            if (deptNo == -1)
            {
                lblNote.Text = "请选择要操作的部门信息！";
                lblNote.ForeColor = Color.Red;
                return;
            }
            else if (string.IsNullOrEmpty(deptName))
            {
                lblNote.Text = "部门名称不能为空！";
                lblNote.ForeColor = Color.Red;
                txtDeptName.Focus();
                return;
            }

            if (lblOperation.Text == "添加")
            {
                string query = $"insert into department_info values({deptNo}, '{deptName}', {deptPeopleCount});";
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
                string query = $"update department_info set dept_name='{deptName}' where dept_no={deptNo};";
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
            if (deptNo == -1)
            {
                lblNote.Text = "请选择要删除的部门信息！";
                lblNote.ForeColor = Color.Red;
                return;
            }
            string query = $"delete from department_info where dept_no={deptNo};";
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
