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

namespace Spammer
{
    public partial class Form1 : Form
    {

        bool startOrClose = true;
        bool checkGrowtopia = false;

        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x0101;
        public TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr Hwnd, int msg, int param, int lparam);

        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();


        public Form1(){
            InitializeComponent();
            intervalTextBox.Value = 1000;
            processtextTimer.Start();
            checkGrowtopia =  growCheck();
        }

        public void Spam(IntPtr Handle, string Text)
        {
            PostMessage(Handle, WM_KEYDOWN, (IntPtr)(Keys.Enter), IntPtr.Zero);
            PostMessage(Handle, WM_KEYUP, (IntPtr)(Keys.Enter), IntPtr.Zero);
            for (int i = 0; i < Text.Length; i++)
            {
                Keys keys1;
                switch (Text[i].ToString())
                {
                    case " ":
                        keys1 = Keys.Space;
                        break;
                    case "`":
                        keys1 = (Keys)0xC0;
                        break;
                    case ".":
                        keys1 = (Keys)0x6E;
                        break;
                    case ",":
                        keys1 = (Keys)0xBC;
                        break;
                    case "/":
                        keys1 = (Keys)0x6F;
                        break;
                    case @"":
                        keys1 = (Keys)0xDC;
                        break;
                    case "+":
                        keys1 = (Keys)0x6B;
                        break;
                    case "-":
                        keys1 = (Keys)0x6D;
                        break;
                    case ":":
                        keys1 = (Keys)0xBA;
                        break;
                    case ";":
                        keys1 = (Keys)0xBA;
                        break;
                    case "[":
                        keys1 = (Keys)0xDB;
                        break;
                    case "]":
                        keys1 = (Keys)0xDD;
                        break;
                        
                    case "I":
                        keys1 = (Keys)0x49;
                        break;
                    case "İ":
                        keys1 = (Keys)0x49;
                        break;

                    case "|":
                        keys1 = (Keys)0xDC;
                        break;

                    case "\t":
                    case "\n":
                        keys1 = Keys.Space;
                        break;
                    
                    default:
                        keys1 = (Keys)converter.ConvertFromString(Text[i].ToString().ToUpper());
                        break;
                }

                PostMessage(Handle, WM_KEYDOWN, (IntPtr)Keys.CapsLock, IntPtr.Zero);
                PostMessage(Handle, WM_KEYDOWN, (IntPtr)keys1, IntPtr.Zero);
                PostMessage(Handle, WM_KEYUP, (IntPtr)keys1, IntPtr.Zero);
            }
            PostMessage(Handle, WM_KEYDOWN, (IntPtr)Keys.Enter, IntPtr.Zero);
            PostMessage(Handle, WM_KEYUP, (IntPtr)Keys.Enter, IntPtr.Zero);
        }

        private void timer1_Tick(object sender, EventArgs e){

            try {
                string windowTitle1 = "growtopia";
                IntPtr winTitle = Process.GetProcessesByName(windowTitle1)[0].MainWindowHandle;
                string spamText = messageTextBox.Text;
                Spam(winTitle, spamText.ToUpper());
            }catch(Exception ex){
                stopSpammer();
                MessageBox.Show(ex.Message);
            }
            

        }

        private void startSpammer() {
            timer1.Start();
            startBotButton.Text = "Close";
            startBotButton.BackColor = Color.Thistle;
            startBotButton.ForeColor = Color.Black;
            startOrClose = !startOrClose;
        }
        private void stopSpammer() {
            timer1.Stop();
            startBotButton.Text = "Start";
            startBotButton.BackColor = Color.FromArgb(23, 23, 23);
            startBotButton.ForeColor = Color.FromArgb(1,255, 128, 255);
            startBotButton.BackColor = Color.FromArgb(23, 23, 23);
            startOrClose = !startOrClose;
        }

        private void startBotButton_Click(object sender, EventArgs e){
            checkGrowtopia = growCheck();
            if (checkGrowtopia){
                if (intervalTextBox.Value <= 0)
                {
                    MessageBox.Show("Please enter interval!");
                    return;
                }

                timer1.Interval = (int)intervalTextBox.Value;
                if (startOrClose){
                    startSpammer();
                }
                else{
                    stopSpammer();
                }
                
            }else {
                MessageBox.Show("Please OPEN Growtopia!");
            }
            

            
        }

        private void processtextTimer_Tick(object sender, EventArgs e){
            checkGrowtopia = growCheck();
        }

        private bool growCheck() {

            try {
                string windowTitle1 = "growtopia";
                IntPtr winTitle = Process.GetProcessesByName(windowTitle1)[0].MainWindowHandle;
                processText.Text = "Growtopia ONLINE";
                processText.ForeColor = Color.FromArgb(255, 128, 255);
                return true;
            }
            catch (Exception ex) {
                processText.Text = "Growtopia OFFLINE";
                processText.ForeColor = Color.Purple;
                return false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e){
            specialMover(e);
        }

        private void specialMover(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            specialMover(e);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            specialMover(e);
        }
    }
}
