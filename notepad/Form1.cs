using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace notepad
{
    public partial class Form1 : Form
    {
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        string filename;
        public Form1()
        {
            InitializeComponent();
        }

        private void save(bool saveas = false)
        {
            if (filename == null || saveas)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
                filename = saveFileDialog1.FileName;
                this.Text = "Notepad - " + filename;
            }
            try
            {
                System.IO.File.WriteAllText(filename, textbox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when saving file: " + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            filename = openFileDialog1.FileName;
            this.Text = "Notepad - " + filename;
            string text = System.IO.File.ReadAllText(filename);
            textbox.Text = text;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private void copy(object sender, EventArgs e)
        {
            textbox.Copy();
        }

        private void paste(object sender, EventArgs e)
        {
            textbox.Paste();
        }

        private void cut(object sender, EventArgs e)
        {
            textbox.Cut();
        }

        private void selectAll(object sender, EventArgs e)
        {
            textbox.SelectAll();
        }
    }
}
