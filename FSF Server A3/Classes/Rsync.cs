using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSync
{
    class RSyncCall
    {
        private Form form;
        private Button button;
        private TextBox outputBox;
        private String exeName;
        private String arguments;
        private String dryArguments;
        private ProgressBar progressBar;
        private ProgressBar progressTotal;
        private Button cancelButton;

        private Control outputDisplaySize;
        private Control transferRate;

        private ArrayList controlsToDisable = new ArrayList();

        private long totalSize = 0;

        public RSyncCall(Form form, Button button, TextBox outputBox, ProgressBar progressTotal, ProgressBar progressBar, FileInfo exeName, String ip, String rsyncRemoteDir, DirectoryInfo localDir, Control transferRate)
            : this(form, button, outputBox, progressTotal, progressBar, exeName, ip, rsyncRemoteDir, localDir, null, transferRate)
        {
        }

        public RSyncCall(Form form, Button button, TextBox outputBox, ProgressBar progressTotal, ProgressBar progressBar, FileInfo exeName, String ip, String rsyncRemoteDir, DirectoryInfo localDir, Control outputDisplaySize, Control transferRate)
        {
            this.form = form;
            this.button = button;
            this.outputBox = outputBox;
            this.progressTotal = progressTotal;
            this.progressBar = progressBar;
            this.exeName = exeName.FullName;
            this.outputDisplaySize = outputDisplaySize;
            this.transferRate = transferRate;
            //"-r -v -z --progress --size-only --chmod=ugo=rwX \"127.0.0.1::RSYNCSERVER\" \"/TESTRSYNC_CLIENT\"");  
            /*
            Il est recommandé d'utiliser :
            --size-only car la date de modification des fichiers sous Windows n'est pas toujours fiable.
            --chmod=ugo=rwX est important sinon vous ne pourrez pas relire les fichiers dans la destination (droits NTFS verrouillés sans cette option)
            */
            String rsyncLocalDir = "/cygdrive/" + localDir.FullName.Replace(":\\", "/").Replace('\\', '/');
            this.arguments = "-vza --partial --inplace --progress --delete --bwlimit=0 --chmod=ugo=rwX '" + ip + "::" + rsyncRemoteDir + "' '" + rsyncLocalDir + "'";
            this.dryArguments = "-vzan --stats --partial --inplace --progress --delete --chmod=ugo=rwX '" + ip + "::" + rsyncRemoteDir + "' '" + rsyncLocalDir + "'";
        }

        public void start()
        {
            disableControls();

            getInfo();
            outputBox.Clear();
            button.Visible = false;
            cancelButton = new Button();
            cancelButton.Size = button.Size;
            cancelButton.Location = button.Location;
            cancelButton.FlatStyle = FlatStyle.System;
            cancelButton.Text = "Abandonner";
            cancelButton.Click += button_Cancel;
            button.Parent.Controls.Add(cancelButton);
            new Thread(execute).Start();
        }

        public void setTotalSize(Control c)
        {
            outputDisplaySize = c;
            getInfo();
        }

        public void addControlToDisable(Control control)
        {
            controlsToDisable.Add(control);
        }

        private void disableControls()
        {
            foreach (Control c in controlsToDisable)
            {
                c.Enabled = false;
            }
        }

        private void enableControls()
        {
            foreach (Control c in controlsToDisable)
            {
                c.Enabled = true;
            }
            totalSize = 0;
        }

        private void button_Cancel(object sender, EventArgs e)
        {
            killProcess("rsync");
            button.Visible = true;
            button.Parent.Controls.Remove(cancelButton);
            outputBox.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Mise à jour arrêtée !" + Environment.NewLine);
        }

        private void killProcess(String processName)
        {
            foreach (System.Diagnostics.Process ziProc in System.Diagnostics.Process.GetProcesses())
            {
                if (ziProc.ProcessName == processName)
                {
                    try
                    {
                        ziProc.Kill();
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        //On fait rien process fantome
                    }
                }
            }
            enableControls();
        }

        private void execute()
        {
            long startTime = DateTime.Now.Ticks;
            long last = 0;
            long downloaded = 0;
            Process process;
            ProcessStartInfo processStartInfo;
            processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = arguments;
            processStartInfo.FileName = exeName;

            process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender, DataReceivedEventArgs e)
                {
                    form.Invoke((MethodInvoker)delegate()
                    {
                        if (e.Data != null && e.Data.IndexOf("B/s") > -1)
                        {
                            String[] a = e.Data.Split(new char[] { ' ' });
                            foreach (String s in a)
                            {
                                if (s.IndexOf("%") > -1)
                                {
                                    progressBar.Value = Int32.Parse(s.Substring(0, s.Length - 1));
                                }
                                if (s.IndexOf("B/s") > -1 && transferRate != null)
                                {
                                    transferRate.Text = s;
                                }
                                if (s.IndexOf(",") > -1)
                                {
                                    try
                                    {
                                        last = long.Parse(s.Replace(",", ""));
                                        double dl = Convert.ToDouble(((double)(downloaded + last)) / totalSize);
                                        progressTotal.Value = (int)(dl * 100);
                                    }
                                    catch (System.FormatException)
                                    {
                                        downloaded = downloaded + last;
                                        last = 0;
                                    }
                                    catch (System.DivideByZeroException)
                                    {
                                    }
                                    catch (System.ArgumentOutOfRangeException)
                                    {
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (e.Data != null && e.Data.Length > 0)
                                outputBox.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + e.Data + Environment.NewLine);
                        }
                    });
                }
            );

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();

            form.Invoke((MethodInvoker)delegate()
            {
                button.Visible = true;
                button.Parent.Controls.Remove(cancelButton);
                outputBox.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Mise à jour terminée !" + Environment.NewLine);
                enableControls();
                getInfo();
                progressBar.Value = 0;
                progressTotal.Value = 0;
                transferRate.Text = "0.00Kb/s";

            });
        }

        public void getInfo()
        {
            new Thread(getTInfo).Start();
        }

        private void getTInfo()
        {
            if (outputDisplaySize == null) return;

            Process process;
            ProcessStartInfo processStartInfo;
            processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = dryArguments;
            processStartInfo.FileName = exeName;

            process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate(object sender, DataReceivedEventArgs e)
                {
                    form.Invoke((MethodInvoker)delegate()
                    {
                        if (e.Data != null)
                        {
                            if (e.Data.StartsWith("Total transferred file size: "))
                            {
                                String[] a = e.Data.Split(new char[] { ' ' });
                                foreach (String s in a)
                                {
                                    if (s.IndexOf(",") > -1)
                                    {
                                        totalSize = long.Parse(s.Replace(",", ""));
                                    }
                                }

                                outputDisplaySize.Text = String.Format("{0:0.000} Mo", ((float)totalSize / 1000000));
                            }
                        }
                    });
                }
            );

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
        }
    }
}