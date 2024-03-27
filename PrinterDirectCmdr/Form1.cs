using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace PrinterDirectCmdr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cfgfile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "config.xml");
            // load config
            if (File.Exists(cfgfile))
            {
                XMLConfigClass cfg = new XMLConfigClass(cfgfile);
                eSelPrinter.Text = cfg.ReadString("printer", "selected", "");
                eDocName.Text = cfg.ReadString("printer", "job","Net-RAW-Doc");
                eCmd.Text = cfg.ReadString("cmd", "cmd", "");
                cbAutoClear.Checked = cfg.ReadBool("cmd", "clear", false);
                alog("Loaded config from: " + cfgfile);
            }
            // load seq.
            string seqf = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sequence.txt");
            if (File.Exists(seqf))
            {
                eCmdSeq.Text = File.ReadAllText(seqf);
                alog("Loaded sequence from file: " + seqf);
            }
            CreateCommandMenu();
        }

        private string cfgfile;
        StringList commandList = new StringList();

        // ------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------
        ToolStripMenuItem CreateCommandMenuItem(ToolStripMenuItem root, string[] keys, int lineid)
        {
            if (root == null) { throw new Exception("Root node can't be null"); }
            
            keys = keys.Where((v, x) => x != 0).ToArray();

            // find item
            ToolStripMenuItem mi = null;
            foreach(ToolStripMenuItem mii in root.DropDownItems)
            {
                if (mii.Text == keys[0])
                {
                    mi = mii;
                }
            }
            // if not found menu in this level, create one
            if (mi == null)
            {
                mi = new ToolStripMenuItem();
                mi.Text = keys[0];
                mi.Tag = (int)-1;
                // Add MenuItem item to root
                root.DropDownItems.Add(mi);
                // if keys.Len>1
                if (keys.Length > 1)
                {
                    mi = CreateCommandMenuItem(mi, keys, lineid);
                }
                else
                {
                    mi.Tag = (int)lineid;
                }
            } else
            {
                mi = CreateCommandMenuItem(mi, keys, lineid);
            }

            return mi;
        }

        string GetMenuItemNamePath(ToolStripMenuItem mi)
        {
            string s = "";
            ToolStripMenuItem m=mi;
            while (m != null)
            {
                s = "|"+ m.Text+s;
                //if (m.OwnerItem != null)
                //{
                    m = (ToolStripMenuItem)m.OwnerItem;
                //} else
                //{

                //}
            }

            return s.Substring(1);
        }

        void CreateCommandMenu()
        {
            string fp = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "commands.txt");
            if (!File.Exists(fp)) return;

            
            commandList.NameValueSeparator = "=";
            commandList.Add(File.ReadAllLines(fp));

            try
            {
                for (int i = 0; i < commandList.Count; i++)
                {
                    string key = commandList.GetName(i);
                    //string val = commandList.GetValue(i);
                    string[] keys;

                    if (key.Contains("|"))
                    {
                        keys = key.Split(new char[] { '|' });
                    }
                    else
                    {
                        keys = new string[1] { key };
                    }

                    ToolStripMenuItem mi = null;
                    // find or add root
                    for (int j = 0; j < cmCommands.Items.Count; j++)
                    {
                        if (cmCommands.Items[j].Text == keys[0])
                        {
                            mi = (ToolStripMenuItem)cmCommands.Items[j];
                        }
                    }

                    // if found root item with that name
                    if (mi != null)
                    {
                        mi = CreateCommandMenuItem(mi, keys, i);
                    }
                    else
                    {
                        mi = new ToolStripMenuItem();
                        mi.Text = keys[0];
                        mi.Tag = i;
                        cmCommands.Items.Add(mi);

                        if (keys.Length > 1)
                        {
                            mi = CreateCommandMenuItem(mi, keys, i);
                        }
                    }

                    mi.Click += new EventHandler(miCommands_Item_OnClick);

                }
            } catch(Exception e)
            {
                alog("------!!! Create Command Menu failed !!!------");
                alog("Fix your commands.txt file. Command item can't have that same name in that same level in menu.");
                alog("Or file syntax is invalid.");
                alog("Error: "+e.Message, true);
            }
        }

        private void miCommands_Item_OnClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = sender as ToolStripMenuItem;

            int tagid = (int)tsi.Tag;

            alog("EVENT: Cliecked tag_line_id: " + tagid.ToString() + " PATH=" + GetMenuItemNamePath(tsi), true);
            alog("Command text: >>> " + GetCommandTextFromMenuItem(tagid) + " <<<", true);
        }

        string GetCommandTextFromMenuItem(int tag_line_id)
        {
            if (tag_line_id < 0 || tag_line_id >= commandList.Count) return "";
            return commandList.GetValue(tag_line_id);
        }
        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------------------

        /// <summary>
        /// Convert Hex string (2-digit hex number) to char. 
        /// May throw exception if format is invalid (not-hex or have more or less that 2 digits)
        /// </summary>
        /// <param name="x">String, hexadecimal 2 digit</param>
        /// <returns>Char</returns>
        char HexStringToChar(string x)
        {
            if (x.Length != 2)
            {
                throw new FormatException("HEX value have to exactly 2 characters. (Got: "+x.Length.ToString()+" `"+x+"`).");
            }
            try
            {
                byte b = byte.Parse(x, System.Globalization.NumberStyles.HexNumber);
                return (char)b; // just cast byte to character
            } catch(Exception ee)
            {
                alog("Failed to convert HEX string »" + x + "« to byte: " + ee.Message);
                throw ee;
            }
        }

        /// <summary>
        /// Return a ASCII character/code name from passed char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        string GetASCIICodeString(char c)
        {
            
            // low ASCII codes (0..31)
            string[] low = { "NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", "BS", "HT", "LF", "VT", "FF", "CR", "SO", "SI", "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", "SUB", "ESC", "FS", "GS", "RS", "US" };
            if (c < '\x32')
            {
                byte b = (byte)c;
                return low[b];
            } else if (c > '\x7e')
            {
                // delete
                if (c == '\x7f') return "DEL";
                // other just return in int format
                byte b = (byte)c;
                return b.ToString("000");
            }
            return "" + c;
        }

        /// <summary>
        /// Convert RAW command string to human-readable format
        /// </summary>
        /// <param name="s">RAW command</param>
        /// <returns></returns>
        string HumanFormatCommandString(string s)
        {
            if (s == null) return "";
            if (s.Length < 1) return "";

            string o = "";
            foreach(char c in s) { 
                if (c < '\x20')
                {
                    o += "<" + GetASCIICodeString(c) + ">";
                } else if (c > '\x7e')
                {
                    o += "<" + GetASCIICodeString(c) + ">";
                } else
                {
                    o += c;
                }

            }
            return o;
        }

        /// <summary>
        /// Convert RAW command string to HEX string (each char is coinverted to 2digit-HEX value)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        string HEXArrayCommandString(string s)
        {
            if (s == null) return "";
            if (s.Length < 1) return "";

            string o = "";
            foreach (char c in s)
            {
                byte b = (byte)c;
                o += b.ToString("X2")+" ";
            }
            return o.Trim();
        }

        /// <summary>
        /// Convert string with raw command to raw string format (convert string hex codes to characters)
        /// </summary>
        /// <param name="incmd"></param>
        /// <param name="stripEndCRLF"></param>
        /// <returns></returns>
        string ProcessCmdString(string incmd, bool stripEndCRLF=false)
        {
            string ostr = "";

            if (incmd.Length < 1) { return ""; }

            // strip end CRLF
            if (stripEndCRLF && incmd.EndsWith(Environment.NewLine, StringComparison.OrdinalIgnoreCase))
            {
                incmd = incmd.Substring(0, incmd.Length - Environment.NewLine.Length);
            }

            int pos = 0;

            try
            {
                bool cancelhex = false;
                do
                {
                    char c = incmd[pos];
                    if (c == '\\' && !cancelhex)
                    {
                        if (incmd[pos + 1] == 'x')
                        {
                            if ((pos+3) > (incmd.Length) )
                            {
                                throw new Exception("Error processing command, backslash must be escaped by another one or followed by `x` then by 2 hexadecimal digits of character code!");
                            }
                            string hex = incmd.Substring(pos + 2, 2);
                            ostr += HexStringToChar(hex);
                            pos += 3;
                        }
                        else
                        {
                            cancelhex = true;
                            //ostr += c;
                        }
                    }
                    else
                    {
                        ostr += c;
                        cancelhex = false;
                    }
                    pos++;
                } while (pos < incmd.Length);
            } catch(Exception ee)
            {
                alog("Processing CMD failed at position " + pos.ToString() + " » " + incmd[pos] + " «: " + ee.Message);
                ostr = "";
            }

            return ostr;
        }

        /// <summary>
        /// Open printer to raw writting, or return ok if already opened.
        /// Return: 0=opened, 1=unable to open printer, 2=unable to start doc, 3=unable to start page
        /// </summary>
        /// <returns></returns>
        int openPrinter()
        {
            if (!rawPrintOut.PrinterIsOpen)
            {
                if (eSelPrinter.Text.Trim().Length < 5)
                {
                    alog("No printer selected.");
                    return 2;
                }
                alog("Opening printer: " + eSelPrinter.Text);
                alog("Job document name: " + eDocName.Text);
                int re = rawPrintOut.OpenPrinter(eSelPrinter.Text, eDocName.Text);
                if (re!=0)
                {
                    alog("!!!!!! Unable to open printer !!!!!!");
                    switch(re)
                    {
                        case 0: alog("Opened. ??"); break;
                            case 1: alog("Unable to open printer."); break;
                            case 2: alog("Unable to start document."); break;
                            case 3: alog("Unable to start page."); break;
                    }
                    alog("Win32LastError: " + rawPrintOut.Win32LastErrorCode.ToString());
                    return re;
                }
                else
                {
                    alog("Printer opened.");
                    return 0;
                }
            } else
            {
                alog("Printer alredy opened: " + rawPrintOut.PrinterName);
            }
            return 0;
        }

        /// <summary>
        /// Close printer
        /// </summary>
        void closePrinter()
        {
            if (rawPrintOut.PrinterIsOpen)
            {
                int re = rawPrintOut.ClosePrinter();
                alog("Close printer resuly: 0x" + re.ToString("X8") + " (win32Error=" + rawPrintOut.Win32LastErrorCode.ToString() + ").");
            }
        }

        /// <summary>
        /// Run sequence
        /// </summary>
        void runSequence(bool testOnly=false)
        {
            int re;
            
            alog("Validating sequence...");
            // validate lines
            bool errs=false;
            for (int i = 0; i < eCmdSeq.Lines.Length; i++)
            {
                string s1 = eCmdSeq.Lines[i];
                alog("checking line[" + i.ToString() + "]...");
                
                try
                {
                    string s2 = ProcessCmdString(s1);
                    string s3 = HumanFormatCommandString(s2);
                    string s4 = HEXArrayCommandString(s2);
                    alog("            » " + s3 + " «", true);
                    alog("            » " + s4 + " «", true);

                }
                catch (Exception e)
                {
                    alog("Error processing line[" + i.ToString() + "]: » " + s1 + " «");
                    alog(e.Message, true);
                    errs = true;
                }
            }

            alog("Validating sequence done.");

            if (errs)
            {
                alog("Errors in sequence detected. STOP.");
                return;
            }

            if (testOnly) 
            return;

            bRunCommands.Enabled = eCmdSeq.Enabled = false;

            re = openPrinter();
            if (re != 0) {
                alog("Sequence: Unable to open printer. STOP. (error: "+re.ToString()+").");
                bRunCommands.Enabled = eCmdSeq.Enabled = true;
                return;
            }
            
            for (int i = 0; i < eCmdSeq.Lines.Length; i++)
            {
                string s1 = eCmdSeq.Lines[i];
                string s2 = ProcessCmdString(s1);
                string s3 = HumanFormatCommandString(s2);
                string s4 = HEXArrayCommandString(s2);
                alog("Sending line[" + i.ToString() + "]:");
                alog("            » " + s3 + " «", true);
                //alog("Sending line[" + i.ToString() + "] » " + s4 + " «");
                alog("            » " + s4 + " «", true);
                int r = rawPrintOut.SendStringToPrinter(s2);
                if (r!=0)
                {
                    alog("ERROR sending line[" + i.ToString() + "] - ERROR.");
                    alog("Win32LastError: " + rawPrintOut.Win32LastErrorCode.ToString());
                    break;
                }
                Application.DoEvents();
                Application.DoEvents();
            }

            closePrinter();

            bRunCommands.Enabled = eCmdSeq.Enabled = true;

        }

        // --------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------



        void alog(string text, bool noTime=false, bool noNewLine=false)
        {
            eLog.AppendText((noTime ? "" : DateTime.Now.ToString("HH:mm:ss.fff: ")) + text + (noNewLine ? "" : Environment.NewLine));
        }

        private void bSelPrinter_Click(object sender, EventArgs e)
        {
            using (FormPrinterList f = new FormPrinterList()) 
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    eSelPrinter.Text = f.SelectedPrinter;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XMLConfigClass cfg = new XMLConfigClass(cfgfile);
            cfg.WriteString("printer","selected",eSelPrinter.Text);
            cfg.WriteString("printer","job",eDocName.Text);
            cfg.WriteString("cmd", "cmd",eCmd.Text);
            cfg.WriteBool("cmd","clear",cbAutoClear.Checked);
            cfg.Save();
            // save sequence to file
            string seqf = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sequence.txt");
            File.WriteAllText(seqf, eCmdSeq.Text);
            // save log to file
            string logfile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "log.txt");
            if (File.Exists(logfile))
            {
                FileInfo filog = new FileInfo(logfile);
                int lnc = eLog.Lines.Length;
                List<string> lns = new List<string>(File.ReadAllLines(logfile));
                lns.AddRange(eLog.Lines);

                if (filog.Length > 2097152)
                {
                    File.WriteAllLines(logfile, lns.GetRange(lnc, lns.Count).ToArray());
                }
                else
                {
                    File.WriteAllLines(logfile, lns.ToArray());
                }
            } else
            {
                File.WriteAllLines(logfile, eLog.Lines);
            }
        }

        // --------------------
        PrinterHelpers.RawPrinterHelper rawPrintOut = new PrinterHelpers.RawPrinterHelper();
        // --------------------

        private void bExecCmd_Click(object sender, EventArgs e)
        {
            string sc = eCmd.Text;
            if (sc.Length < 1) return;

            alog("STRING CMD: » " + sc + " «");
            string cmd = ProcessCmdString(sc);
            alog("CMD: » " + HumanFormatCommandString(cmd) + " «");
            alog("CMDHEX: » " + HEXArrayCommandString(cmd) + " «");
            if (rawPrintOut.PrinterIsOpen)
            {
                alog("Sending command... ");
                int r = rawPrintOut.SendStringToPrinter(cmd);
                alog("Command sent, result: "+r.ToString()+" (0=OK).");
                if (cbAutoClear.Checked)
                {
                    eCmd.Text = "";
                }
            }
        }

        private void eCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = e.Handled = true;
                bExecCmd.PerformClick();
            }
        }

        private void bOpenPrinter_Click(object sender, EventArgs e)
        {
            openPrinter();
        }

        private void bClosePrinter_Click(object sender, EventArgs e)
        {
            closePrinter();
        }

        private void eCmdSeq_TextChanged(object sender, EventArgs e)
        {
            lblLineCount.Text = "Lines: "+eCmdSeq.Lines.Length.ToString();
        }

        private void bRunCommands_Click(object sender, EventArgs e)
        {
            lblLineCount.Text = "Lines: " + eCmdSeq.Lines.Length.ToString();
            Application.DoEvents();
            Application.DoEvents();
            runSequence(); 

        }

        private void bTestSeq_Click(object sender, EventArgs e)
        {
            runSequence(true);
        }

        private void bNewSeq_Click(object sender, EventArgs e)
        {
            eCmdSeq.Clear();
        }

        private void bOpenSeq_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                eCmdSeq.Text = File.ReadAllText(dlgOpen.FileName);
            }
        }

        private void bSaveSeq_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlgSave.FileName, eCmdSeq.Text);
            }
        }

        private void bCmdMenu_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            cmCommands.Show(ptLowerLeft);
        }
    }
}
