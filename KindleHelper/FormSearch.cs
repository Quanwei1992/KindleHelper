using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using GrapchLibrary;
using KindleHelper.lib;
using KindleHelper.Plugin.Interface;
using libZhuishu;
namespace KindleHelper
{
    public partial class FormSearch : Form
    {
        int a, b;
        GrapchHelper c;
        public FormSearch()
        {
            InitializeComponent();
            a = Width;
            b = Height;
            
        }

        private void Method(object text)
        {
            try
            {
                var resultss = LibZhuiShu.fuzzySearch(text as string, 0, 100);
                if (resultss == null || resultss.Length < 1)
                {
                    MessageBox.Show("没有找到:" + text as string);
                    return;
                }
                form_result.ShowResult(resultss);
            }
            catch
            {

            }
        }
        FormSearchResult form_result = new FormSearchResult();
        private void btn_search_Click(object sender, EventArgs e)
        {  
            if (FormBookDetail.str!="")
            {
                    Thread t = new Thread(new ParameterizedThreadStart(Method), 0);
                    t.IsBackground = true;
                    if (!t.IsAlive)
                    {
                        t.Start(FormBookDetail.str);
                    }
            }
            else
            {
                Method(textbox_search.Text);
            } 
        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox_search.Text)) return;
            listbox_autocomplate.Items.Clear();
            RunAsync(()=> {
                var words = LibZhuiShu.autoComplate(textbox_search.Text);
                RunInMainthread(()=> {
                    if (words != null && words.Length > 0) {
                        listbox_autocomplate.Items.AddRange(words);
                        listbox_autocomplate.Visible = true;
                    } else {
                        listbox_autocomplate.Visible = false;
                    }
                });
            });


        }

        private void listbox_autocomplate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listbox_autocomplate.SelectedItem == null) return;

            textbox_search.Text = listbox_autocomplate.SelectedItem.ToString();
            listbox_autocomplate.Visible = false;
            btn_search_Click(sender,e);
        }
        PluginHelper helper = new PluginHelper();
        
        private void FormSearch_Load(object sender, EventArgs e)
        {
            if (File.Exists(f.filename))
            {             
                if (f.read())
                {
                    func();
                }
                else
                {
                    func2();
                }
            }
            else
            {
                func2();
            }
        }
        private void func()
        {
            Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            AssemblyDescriptionAttribute asmdis = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyDescriptionAttribute));
            AssemblyCopyrightAttribute asmcpr = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            AssemblyCompanyAttribute asmcpn = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute));
            this.Text += " V" + asmdis.Description + (IsAdministrator() ? "(管理员）" : "");
            button2_Click(null,null);
            new testlib.Class1();
        }
        private void func2()
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("必须以管理员身份运行！");
                this.Close();
            }
            else
            {
                testlib2.Form1 f = new testlib2.Form1();
                f.ShowDialog();
                if (f.isreg)
                {
                    func();
                }
            }
        }
        void RunAsync(Action action)
        {
            ((Action)(delegate () {
                action.Invoke();
            })).BeginInvoke(null, null);
        }

        void RunInMainthread(Action action)
        {
            this.BeginInvoke((Action)(delegate () {
                action.Invoke();
            }));
        }

        private void P(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            c = new GrapchHelper(g);
            c.Draw(b, a);
            g.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> pluginpath = helper.FindPlugin();
            pluginpath = helper.DeleteInvalidPlungin(pluginpath);
            foreach (string filename in pluginpath)
            {
                try
                {
                    //获取文件名
                    string asmfile = filename;
                    string asmname = Path.GetFileNameWithoutExtension(asmfile);
                    if (asmname != string.Empty)
                    {
                        // 利用反射,构造DLL文件的实例
                        Assembly asm = Assembly.LoadFile(asmfile);
                        //利用反射,从程序集(DLL)中,提取类,并把此类实例化
                        Type[] t = asm.GetExportedTypes();
                        foreach (Type type in t)
                        {
                            if (type.GetInterface("IPlugin") != null)
                            {
                                IPlugin show = (IPlugin)Activator.CreateInstance(type);
                                listBox1.Items.Add(show);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    loglibrary.LogHelper.Error(ex);
                    loglibrary.LogHelper.Flush();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
        private object[] ConvrtResultToObject(IPlugin r)
        {
            try
            {
                object result = r.run();
                object result2 = r.getresult();
                return new[] { result, result2 };
            }
            catch (Exception ee)
            {
                loglibrary.LogHelper.Error(ee);
                loglibrary.LogHelper.Flush();
            }
            return null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var r = (IPlugin)listBox1.SelectedItem;
                var watch = r.Watch();
                listBox2.Items.Add(r.Show());
                r.ShowForm();
                if (r.IsNotNeedToShowAsDialog())
                {
                 r.ShowFormAsDialog();
                }
                listBox2.Items.Add(r.version());
                listBox2.Items.AddRange(ConvrtResultToObject(r));
                listBox2.Items.Add("耗时："+watch.Elapsed.ToString());
                listBox2.Items.Add("计时器周期："+watch.ElapsedTicks);
            }
            catch (Exception ee)
            {
                loglibrary.LogHelper.Error(ee);
                loglibrary.LogHelper.Flush();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Enabled = false;
            listBox1.Items.Clear();
            button2_Click(sender, e);
            listBox1.Enabled = true;
        }
        public bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e); 
            a = Width;
            b = Height;
            Graphics g = Graphics.FromHwnd(Handle);
            c = new GrapchHelper(g);
            c.Draw(b, a);
            g.Dispose();
            c.Dispose();         
            foreach (Control item in Controls)
            {
                if (item!=this)
                {
                    item.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                }
            }
        }
        testlib2.Form1 f = new testlib2.Form1();
        private void textbox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(char)Keys.Enter)
            {
                btn_search_Click(sender, e);
            }
        }

        private void FormSearch_Click(object sender, EventArgs e)
        {
            listbox_autocomplate.Hide();
        }
        private void write(object o)
        {
            f.write();
        }
        private void l(object sender, FormClosingEventArgs e)
        {
            Thread t = new Thread(new ParameterizedThreadStart(write));
            if (!t.IsAlive)
            {
                t.Start(new object());
            }
        }
    }
}
