using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;



namespace JoHeSupporter
{
    public partial class GUI : Form
    {

        #region class_init
        public methods methods;
        public param param;
        // Vesaatz der Position in Titelleiste
        
        public GUI(methods _methods)
        {
            // damit die Form klein genug werden kann
            this.MinimumSize = new System.Drawing.Size(16, 5);
            this.Size = new System.Drawing.Size(16, 5);


            methods = _methods;
            param = methods._Param;

            // setze CustomIcon, wenn vorhanden - ansonnsten nimm das Default
            try
            {
                this.BackgroundImage = Image.FromFile
                (AppDomain.CurrentDomain.BaseDirectory
                + param.AppCfg_CustomIconFile);

            }
            catch (Exception)
            {
                this.BackgroundImage = global::JoHeSupporter.Properties.Resources.Icon;
            }




            InitializeComponent();

            this.ShowInTaskbar = false;
            // ToolTip für den Button
            ToolTip GuiToolTip = new ToolTip();
            GuiToolTip.SetToolTip(this, "JoHeSupporter\n[Shift] + Maus -> Bewegen\nRechtsklick -> Menü");
            
            

            // Active Window Change event definieren
            dele = new WinEventDelegate(WinEventProc);
            //IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_OBJECT_LOCATIONCHANGE, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
            
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_OBJECT_LOCATIONCHANGE, IntPtr.Zero, dele, 0, 0,
                WINEVENT_OUTOFCONTEXT 
                //WINEVENT_SKIPOWNPROCESS | 
                //  EVENT_SYSTEM_MENUSTART | 
                //  EVENT_SYSTEM_MENUPOPUPSTART | 
                //  EVENT_SYSTEM_CONTEXTHELPSTART | 
                //    EVENT_SYSTEM_SCROLLINGSTART //| 
                //EVENT_SYSTEM_DESKTOPSWITCH
                );

            // Größe des Icons nach dem Wert der XML Datei anpassen.
            this.ClientSize = new Size(param.AppCfg_IconSize, param.AppCfg_IconSize);
         

        }
        

        #endregion class_init

        #region GuiMovement
        WinEventDelegate dele = null;

                
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);


        // Events definieren
        private const uint WINEVENT_OUTOFCONTEXT = 0;  // Events are ASYNC
        private const uint EVENT_SYSTEM_FOREGROUND = 3;
        private const uint EVENT_OBJECT_LOCATIONCHANGE = 0x800B;

        private const uint WINEVENT_SKIPOWNPROCESS = 0x0002; // Don't call back for events on installer's process
        private const uint EVENT_SYSTEM_MENUSTART = 0x0004;
        private const uint EVENT_SYSTEM_MENUPOPUPSTART = 0x0006;

        private const uint EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C;
        private const uint EVENT_SYSTEM_SCROLLINGSTART = 0x0012;
        private const uint EVENT_SYSTEM_DESKTOPSWITCH = 0x0020;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        IntPtr handleActiveWindow = IntPtr.Zero;
        IntPtr handleLastWindow = IntPtr.Zero;

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            //IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handleActiveWindow = GetForegroundWindow();

            if (GetWindowText(handleActiveWindow, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private string GetWindowTitle(IntPtr _handle)
        {
            const int nChars = 256;
            //IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
          
            if (GetWindowText(_handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

      


        private int[] GetActiveWindowPos()
        {
            //IntPtr handle = IntPtr.Zero;            
            handleActiveWindow = GetForegroundWindow();
            RECT rct = new RECT();
            GetWindowRect(handleActiveWindow, ref rct);

            int[] wnd = new int[] { rct.Left, rct.Right, rct.Top };
            return wnd;
        }

        private int[] GetWindowPos(IntPtr _handle)
        {
            //IntPtr handle = IntPtr.Zero;            
            //handleActiveWindow = GetForegroundWindow();
            RECT rct = new RECT();
            GetWindowRect(_handle, ref rct);

            int[] wnd = new int[] { rct.Left, rct.Right, rct.Top };
            return wnd;
        }

        

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {

            if (eventType == EVENT_SYSTEM_FOREGROUND || eventType == EVENT_OBJECT_LOCATIONCHANGE)
                       {
            //  if (ActiveWindowChanged != null)
            // {





            // Letzen Handle vor Fensterwechsel behalten.
            if (handleActiveWindow != GetForegroundWindow())
            {
                handleLastWindow = handleActiveWindow;
                handleActiveWindow = GetForegroundWindow();
            }

            // this.label1.Text = GetWindowTitle(handleLastWindow);


            string _windTitle = GetActiveWindowTitle();
                    //  this.label1.Text = _windTitle;

                    // Bei eigenen Fenstern nicht verschieben
                    if (_windTitle != "JoHeSupporter" && _windTitle != "" && _windTitle != null)
                    {
                        int[] wnd = GetActiveWindowPos();
                       int leftPos = ((wnd[1] - wnd[0]) / 100 * param.PosOffset) + wnd[0];

                    // Versuch icon rechts an zu binden
                    int rightPos = wnd[1] - 175;
                    this.Left = rightPos;


                    //
                    if (leftPos >= 20)
                    {
                        //this.Left = leftPos;
                    }
                    this.Top = wnd[2];
                    this.TopMost = true;
                 //   Console.WriteLine("Title:" + _windTitle + "; + Left: " + leftPos);
                    }
                // }
                
            }

        }
        
        
        // Right Mouse Button
        // public const int WM_NCRBUTTONDOWN = 0x00A4;
        // LeftMouse Button
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion GuiMovement

        #region Gui_Click_and_Menu

        private void GUI_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            //if (e.Button == MouseButtons.Left && e.Button == MouseButtons.Right)
           if ((Control.ModifierKeys == Keys.Shift))
       //     if ((Control.MouseButtons & MouseButtons.Right)== MouseButtons.Right)
            {
                
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                // Button wieder an die Top Posistion des letzten Fensters verschieben
                int[] wnd = GetWindowPos(handleLastWindow);
                //int[] wnd = GetActiveWindowPos();
                this.Top = wnd[2];


                //Offset aktualisieren
                //posOffset
                int _laenge = wnd[1] - wnd[0];
                int _pos = this.Left - wnd[0];

                try
                {
                    param.PosOffset= (_pos / _laenge) * 100;
                } // eventuelle division durch 0 abfangen
                catch (Exception) { }
                
                float _temp = (float)_pos / (float)_laenge * 100;
                
                // posOffset nur anpassen, wenn ergebniss nicht 0 und kleiner 90 Prozent ist.
                if ((int)_temp !=0 && (int)_temp <90)
                {
                    param.PosOffset = (int)_temp;
                }
                
                
                // label2.Text = "Länge: " + _laenge + "\n _Pos: " + _pos + " \n proz: " + posOffset + "; ";


            }
           /// --> Hier wird der Mausklick behandelt
            else if (e.Button == MouseButtons.Left) {
                //     string _windTitle = GetActiveWindowTitle();
                //   label1.Text = _windTitle + "\r\n" + GetActiveWindowXPos().ToString();

                //  label1.Text = "Klicked";
                //MessageBox.Show("Geklickt :-)","s");
                //       methods.saveScreenshot();
                //         methods.sendMailTest();

                // eigene methode dafür aufrufen:
                clickevent();

            } else if (e.Button == MouseButtons.Right)      // Kontextmenü bei rechter Maustaste
            {
                contextMenuStrip1.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
            }

          

        }



        #endregion Gui_Click_and_Menu

        #region clicked
        /// <summary>
        /// startet die Methoden, die aufgerufen werden sollen wenn der Button gedrückt wird.
        /// </summary>
        private void clickevent()
        {
            //MessageBox.Show("Geklickt :-)","s");
            methods.saveScreenshot();
            UserDialog userDialog = new UserDialog(methods, param);

            
            userDialog.Show();

            //string mail = methods.getUserMail();
           // MessageBox.Show("Mail: " + param.UserMail);
            //methods.sendMailTest();
        }

        #endregion clicked

        #region Kontextmenüs

        private void info_Click(object sender, EventArgs e)
        {

            MessageBox.Show(@"
                JoHeSupporter - Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + @" 
                © 2017 Hehn Johannes 
                Lizenz gülltig bis: " + methods._LicenseExpireDate + 
@"

Klicken um eine Supportmeldung per Mail zu erstellen. 

[Shift] gedrückt halten um das Icon mit der Maus zu verschieben.

----------------------------------------------------------------

[SKGL Lizenzinfos] :

Copyright (c) 2011-2012, Artem Los

All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

TSOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 'AS IS' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
", "JoHeSupporter");

        }

        private void exit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(sender.ToString());
            try
            {
                System.Environment.Exit(0);
                //Application.Exit();
                
            } catch ( Exception ex)
                { 
                MessageBox.Show("ERROR: " + ex.ToString());
            }
            
            
        }
        #endregion Kontextmenüs
    }
}
