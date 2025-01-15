using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EIMS
{
    public partial class CustomerInfoForm : Form
    {
        private int customer_id;
        public CustomerInfoForm()
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
            string query = "select * from customer_info;";
            DataTable dt = DataAccessLayer.ExecuteQuery(query);
            lvCustomerInfo.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem item = new ListViewItem(dr["customer_id"].ToString());
                item.SubItems.Add(dr["customer_name"].ToString());
                item.SubItems.Add(dr["company"].ToString());
                item.SubItems.Add(dr["sex"].ToString());
                item.SubItems.Add(dr["age"].ToString());
                item.SubItems.Add(dr["telephone"].ToString());
                item.SubItems.Add(dr["address"].ToString());
                lvCustomerInfo.Items.Add(item);
            }
            AdjustListViewColumns(lvCustomerInfo);
        }

        private void ClearInfo()
        {
            txtName.Text = "";
            txtCompany.Text = "";
            txtTelephone.Text = "";
            txtAddress.Text = "";
            nudAge.Value = 0;
            cbbSex.SelectedIndex = 0;
            customer_id = -1;
            lblOperation.Text = "添加";
        }

        private void CustomerInfoForm_Load(object sender, System.EventArgs e)
        {
            DataBindInfo();
        }

        private void lvCustomerInfo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvCustomerInfo.SelectedItems.Count > 0)
            {
                ListViewItem item = lvCustomerInfo.SelectedItems[0];
                customer_id = int.Parse(item.SubItems[0].Text);
                txtName.Text = item.SubItems[1].Text;
                txtCompany.Text = item.SubItems[2].Text;
                cbbSex.SelectedIndex = item.SubItems[3].Text == "男" ? 0 : 1;
                nudAge.Value = int.Parse(item.SubItems[4].Text);
                txtTelephone.Text = item.SubItems[5].Text;
                txtAddress.Text = item.SubItems[6].Text;
                lblOperation.Text = "修改";
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string name = txtName.Text;
            string company = txtCompany.Text;
            string telephone = txtTelephone.Text;
            string address = txtAddress.Text;
            int age = (int)nudAge.Value;
            string sex = cbbSex.SelectedItem.ToString();

            if (string.IsNullOrEmpty(name))
            {
                lblNote.Text = "姓名不能为空！";
                lblNote.ForeColor = Color.Red;
                txtName.Focus();
                return;
            }

            if (lblOperation.Text == "添加")
            {
                string query = $"insert into customer_info values(null, '{name}', '{company}'," +
                    $" '{sex}', {age}, '{telephone}', '{address}');";
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
                string query = $"update customer_info set customer_name='{name}', company='{company}'," +
                    $" sex='{sex}', age={age}, telephone='{telephone}', address='{address}' where customer_id=" +
                    $"{customer_id};";
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
            if(customer_id == -1)
            {
                lblNote.Text = "请选择要删除的客户信息！";
                lblNote.ForeColor = Color.Red;
                return;
            }
            string query = $"delete from customer_info where customer_id={customer_id};";
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
