using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EIMS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // 在窗体加载时，读取已保存的用户名和密码
            LoadCredentials();

            // 绑定文本框的 KeyDown 事件
            txtUsername.KeyDown += TextBox_KeyDown;
            txtPassword.KeyDown += TextBox_KeyDown;
        }

        // 保存用户名和密码
        private void SaveCredentials(string username, string password)
        {
            // 保存用户名
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["username"] != null)
            {
                config.AppSettings.Settings["username"].Value = username;
            }
            else
            {
                config.AppSettings.Settings.Add("username", username);
            }

            // 保存密码（注意安全性）
            if (config.AppSettings.Settings["password"] != null)
            {
                config.AppSettings.Settings["password"].Value = password;
            }
            else
            {
                config.AppSettings.Settings.Add("password", password);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        // 读取已保存的用户名和密码
        private void LoadCredentials()
        {
            string savedUsername = ConfigurationManager.AppSettings["username"];
            string savedPassword = ConfigurationManager.AppSettings["password"];

            if (!string.IsNullOrEmpty(savedUsername))
            {
                txtUsername.Text = savedUsername;
            }

            if (!string.IsNullOrEmpty(savedPassword))
            {
                txtPassword.Text = savedPassword;
            }
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                // 调用登录按钮的点击事件
                btnLogin_Click(sender, e);
            }
        }

        // 检查输入是否合法（只允许字母、数字和下划线）
        private bool IsInputValid(string input)
        {
            // 正则表达式：只允许字母、数字和下划线
            string pattern = @"^[a-zA-Z0-9_]+$";
            return Regex.IsMatch(input, pattern);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if(string.IsNullOrEmpty(username))
            {
                lblNote.Text = "用户名不能为空";
                txtUsername.Focus();
            } else if (string.IsNullOrEmpty(password))
            {
                lblNote.Text = "密码不能为空";
                txtUsername.Focus();
            }
            else
            {
                if (!IsInputValid(username) || !IsInputValid(password))
                {
                    lblNote.Text = "用户名和密码只能包含字母、数字和下划线";
                    return;
                }
                string query = $"SELECT * FROM admin_info WHERE username='{username}' AND password='{password}'";
                DataTable dt = DataAccessLayer.ExecuteQuery(query);
                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("对不起，用户名不存在！");
                }
                else if (dt.Rows[0]["password"].ToString() != password)
                {
                    MessageBox.Show("对不起，密码不正确！");
                }
                else
                {
                    MainForm mainForm = new MainForm();
                    SaveCredentials(username, password);
                    mainForm.Show();
                    this.Hide();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Application.Exit();
        }
    }
}
