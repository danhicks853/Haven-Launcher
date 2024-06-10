using System.Diagnostics;
using System.IO.Compression;
using Microsoft.Win32;
using System.Reflection;
using static System.Net.WebRequestMethods;
using static Haven_Launcher.HavenLauncher;
using Haven_Launcher.Properties;
using System.Drawing.Text;
using System.Configuration;
using System.IO;
using static System.Windows.Forms.LinkLabel;
using System.Text.RegularExpressions;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;

namespace Haven_Launcher
{
    public partial class HavenLauncher : Form
    {
        static string GAME = "legion";
        static string CLIENTDIRECTORY = "";
        static string REG_PATH = "Software\\Haven";
        static string REG_KEY = "";
        static string WEBVIEW_SOURCE = "";
        static string MINIMAL_CLIENT_URI = "";
        static string CLIENT_URI = "";
        static string ADDON_URI = "";
        static string CONNECT_CONFIG = "";
        static string CONNECT_CONFIG_LINE = "";
        static string SERVER_IP = "129.159.85.2";
        static string GET_PATCH = "false";
        public HavenLauncher()
        {
            InitializeComponent();
            webView21.DefaultBackgroundColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
        }
        private void HavenLauncher_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.legion;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            GAME = "legion";
            btnPlay.Text = "Play Legion!";
            CLIENTDIRECTORY = "";
            REG_KEY = "LegionDirectory";
            WEBVIEW_SOURCE = "http://legionhaven.com/changelog.html";
            MINIMAL_CLIENT_URI = "http://legionhaven.com/minimalclient.zip";
            CLIENT_URI = "http://legionhaven.com/client.zip";
            ADDON_URI = "http://legionhaven.com/addons/";
            InitializeExpansion();
            CONNECT_CONFIG = "legionhack";
            CONNECT_CONFIG_LINE = $"SET portal \"{SERVER_IP}\"";
        }
        private async void InitializeExpansion() { 
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate($"{ WEBVIEW_SOURCE}");
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync($"{ADDON_URI}/addons.txt");
                lbxAddons.Items.Clear();
                lbxAddons.Items.AddRange(content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));
            }
            string registryKeyName = REG_KEY;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REG_PATH))
            {
                if (key == null || key.GetValue(REG_KEY) == null)
                {
                    SaveDirectoryToRegistry();
                    using (RegistryKey keynew = Registry.CurrentUser.OpenSubKey(REG_PATH))
                    {
                        CLIENTDIRECTORY = keynew.GetValue(REG_KEY).ToString();
                        bool clientExists = CheckClientDirectory();
                        if (clientExists)
                        {
                            SetupControlsClientPresent();
                        }
                        else
                        {
                            SetupControlsNoClient();
                        }
                    }
                }
                else
                {
                    CLIENTDIRECTORY = key.GetValue(REG_KEY).ToString();
                    bool clientExists = CheckClientDirectory();
                    if (clientExists)
                    {
                        SetupControlsClientPresent();
                    }
                    else
                    {
                        SetupControlsNoClient();
                    }
                }
            }
        }
        private void SetupControlsNoClient()
        {
            btnPlay.Visible = false;
            btnCancel.Visible = false;
            btnInstall.Visible = true;
            btnInstallAddon.Visible = false;
            lblProgress.Visible = false;
            lbxAddons.Visible = false;
            btnModifySettings.Visible = true;
            progressBar.Visible = false;

        }
        private void SetupControlsClientPresent()
        {
            btnPlay.Visible = true;
            btnCancel.Visible = false;
            btnInstall.Visible = false;
            btnInstallAddon.Visible = true;
            lblProgress.Visible = false;
            lbxAddons.Visible = true;
            btnModifySettings.Visible = true;
            progressBar.Visible = false;

        }
        private void SetupControlsDownloadInProgress()
        {
            btnPlay.Visible = false;
            btnCancel.Visible = true;
            btnInstall.Visible = false;
            btnInstallAddon.Visible = false;
            lblProgress.Visible = true;
            lbxAddons.Visible = false;
            btnModifySettings.Visible = false;
            progressBar.Visible = true;

        }
        private void SaveDirectoryToRegistry()
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Select a directory to save as Client Directory";
                folderBrowser.UseDescriptionForTitle = true;
                folderBrowser.ShowNewFolderButton = true;
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowser.SelectedPath;
                    string registryPath = REG_PATH;
                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(REG_PATH))
                    {
                        key.SetValue(REG_KEY, selectedPath);
                    }
                }
            }
        }
        private void modifyConfig(string path, string configuration)
        {
            if (System.IO.File.Exists(path))
            {
                var lines = System.IO.File.ReadAllLines(path).ToList();
                if (!(lines.Any(line => line.Contains(CONNECT_CONFIG_LINE))))
                {
                    DialogResult reconfigure =  MessageBox.Show("This client is not configured for Haven. Reconfigure?", "Configure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (reconfigure == DialogResult.Yes)
                    {
                        try{System.IO.File.Delete(path);}catch (System.IO.FileNotFoundException){}
                        System.IO.File.WriteAllText(path, configuration);
                    } else
                    {
                        MessageBox.Show("Configuration not updated.");
                    }
                }
                bool lineExists = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (Regex.IsMatch(lines[i], "SET portal") || Regex.IsMatch(lines[i], "set realmlist"))
                    {
                        if (lines[i] != configuration)
                        {
                            lines.RemoveAt(i);
                            lines.Insert(i, configuration);
                        }
                        lineExists = true;
                        break;
                    }
                }
                if (!lineExists)
                {
                    lines.Add(configuration);
                }
                System.IO.File.WriteAllLines(path, lines);
            } else
            {
                System.IO.File.WriteAllText(path, configuration);
            }
        }

        private bool CheckClientDirectory()
        {
            if (System.IO.File.Exists($"{CLIENTDIRECTORY}\\wow.exe"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (GAME == "wrath")
            {
                modifyConfig($"{CLIENTDIRECTORY}\\data\\enUS\\realmlist.wtf", $"set realmlist {SERVER_IP}");
            } else
            {
                modifyConfig($"{CLIENTDIRECTORY}\\wtf\\HG-735.wtf", $"SET portal {SERVER_IP}");
                modifyConfig($"{CLIENTDIRECTORY}\\wtf\\config.wtf", $"SET portal {SERVER_IP}");
            }
            
            bool fileExists = System.IO.File.Exists($"{CLIENTDIRECTORY}\\wow.exe");
            if (fileExists)
            {
                if(GET_PATCH == "true")
                {
                    if (System.IO.File.Exists($"{CLIENTDIRECTORY}\\data\\Patch-A.mpq"))
                    {} else
                    {
                        new System.Net.WebClient().DownloadFile("https://legionhaven.com/wotlk/Patch-A.MPQ", $"{CLIENTDIRECTORY}\\data\\Patch-A.mpq");

                    }
                }
                Process.Start($"{CLIENTDIRECTORY}\\wow.exe");
            } else
            {
                MessageBox.Show("Client not found. Please choose the directory that contains wow.exe.");
                SaveDirectoryToRegistry();
            }
        }
        private async void btnInstall_Click(object sender, EventArgs e)
        {
            SetupControlsDownloadInProgress();
            string Uri = "";
            string destinationPath = "client.zip";
            if (MINIMAL_CLIENT_URI != null) {
                DialogResult client = MessageBox.Show("Download the full client? Click 'No' for the minimal client.", "Client Type", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (client == DialogResult.Yes)
                {
                    Uri = CLIENT_URI;
                }
                else if (client == DialogResult.No)
                {
                    Uri = MINIMAL_CLIENT_URI;
                    MessageBox.Show("After launching the game for the first time, please be patient. The minimal client takes a long time to initialize.");
                }
            } else
            {
                Uri = CLIENT_URI;
            }
            lblProgress.Text = "Starting download...";
            progressBar.Value = 0;
            DownloadClient downloadClient = new DownloadClient();
            bool result = await downloadClient.DownloadClientAsync
                (
                    Uri,
                    $"{CLIENTDIRECTORY}\\client.zip",
                    lblProgress,
                    progressBar,
                    btnCancel,
                    CLIENTDIRECTORY
                );
            if (result)
            {
                SetupControlsClientPresent();
            }
            else
            {
                SetupControlsNoClient();
            }
        }
        public class DownloadClient
        {
            private bool isCanceled;
            public async Task<bool> DownloadClientAsync(string url, string destinationPath, Label lblProgress, ProgressBar progressBar, Button btnCancel, string extractpath)
            {
                isCanceled = false;
                using (HttpClient httpClient = new HttpClient())
                {
                    long totalBytes = -1;
                    long existingBytes = 0;
                    FileInfo fileInfo = new FileInfo(destinationPath);
                    if (fileInfo.Exists)
                    {
                        existingBytes = fileInfo.Length;
                    }
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    if (existingBytes > 0)
                    {
                        request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(existingBytes, null);
                    }
                    using (HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        totalBytes = response.Content.Headers.ContentLength ?? -1L;
                        totalBytes += existingBytes;
                        long totalRead = existingBytes;
                        byte[] buffer = new byte[8192];
                        var downloadTask = response.Content.ReadAsStreamAsync();
                        btnCancel.Click += (s, e) => isCanceled = true;
                        using (var fileStream = new FileStream(destinationPath, FileMode.Append, FileAccess.Write, FileShare.None, buffer.Length, true))
                        {
                            var stream = await downloadTask;
                            int bytesRead;
                            DateTime lastUpdate = DateTime.Now;
                            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                if (isCanceled)
                                {
                                    return false;
                                }
                                await fileStream.WriteAsync(buffer, 0, bytesRead);
                                totalRead += bytesRead;
                                if (DateTime.Now - lastUpdate >= TimeSpan.FromSeconds(1))
                                {
                                    int progressPercentage = (int)((totalRead * 100) / totalBytes);
                                    lblProgress.Invoke(new Action(() => lblProgress.Text = $"{progressPercentage}% {(int)(totalRead / 1000000)} MB /{(int)(totalBytes / 1000000)} MB"));
                                    progressBar.Invoke(new Action(() => progressBar.Value = progressPercentage));
                                    lastUpdate = DateTime.Now;
                                }
                            }
                        }
                    }
                }
                Directory.CreateDirectory(extractpath);
                string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempPath);
                try
                {
                    ZipFile.ExtractToDirectory(destinationPath, tempPath);

                    foreach (var tempFile in Directory.GetFiles(tempPath, "*", SearchOption.AllDirectories))
                    {
                        string relativePath = tempFile.Substring(tempPath.Length + 1);
                        string destinationFile = Path.Combine(extractpath, relativePath);
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));
                        System.IO.File.Copy(tempFile, destinationFile, true);
                    }
                }
                finally
                {
                    Directory.Delete(tempPath, true);
                }
                System.IO.File.Delete(destinationPath);
                return true;
            }
        }
        private async void GetAddon()
        {
            Process[] processes = Process.GetProcessesByName("wow.exe");
            if (processes.Length > 0)
            {
                MessageBox.Show("Close all running wow clients before attempting to install Addons.");
            }
            else
            {
                string addon = lbxAddons.SelectedItem.ToString();
                DownloadClient downloadClient = new DownloadClient();
                bool result = await downloadClient.DownloadClientAsync
                    (
                        $"{ADDON_URI}/{addon}.zip",
                        $"{CLIENTDIRECTORY}\\Interface\\Addons\\{addon}.zip",
                        lblProgress,
                        progressBar,
                        btnCancel,
                        $"{CLIENTDIRECTORY}\\Interface\\Addons\\"
                    );
            }

        }

        private void btnInstallAddon_Click(object sender, EventArgs e)
        {
            GetAddon();
        }

        private void btnModifySettings_Click(object sender, EventArgs e)
        {
            
            if (CheckClientDirectory())
            {
                DialogResult confirm = MessageBox.Show("You are about to move your World of Warcraft Installation. Continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    DialogResult result = MessageBox.Show("Do you want to remove the current installation after moving?", "Move or Copy?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    string oldClient = CLIENTDIRECTORY;
                    SaveDirectoryToRegistry();
                    string registryKeyName = "ClientDirectory";
                    using (RegistryKey keynew = Registry.CurrentUser.OpenSubKey(@"Software\Haven"))
                    {
                        CLIENTDIRECTORY = keynew.GetValue(registryKeyName).ToString();
                    }
                    Process.Start("robocopy", $"{oldClient} {CLIENTDIRECTORY}").WaitForExit();
                    if (result == DialogResult.Yes)
                    {
                        Directory.Delete(oldClient);
                    }
                }

                else
                {
                    MessageBox.Show("No Changes have been made.");
                }
            } else
            {
                SaveDirectoryToRegistry();
                string registryKeyName = "ClientDirectory";
                using (RegistryKey keynew = Registry.CurrentUser.OpenSubKey(@"Software\Haven"))
                {
                    CLIENTDIRECTORY = keynew.GetValue(registryKeyName).ToString();
                }
                if (CheckClientDirectory())
                {
                    SetupControlsClientPresent();
                } else
                {
                    SetupControlsNoClient();
                }
                MessageBox.Show("Client Directory Changed");
            }
        }
        private void btnLegion_Click(object sender, EventArgs e)
        {
            GAME = "legion";
            this.BackgroundImage = Properties.Resources.legion;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            btnPlay.Text = "Play Legion!";
            CLIENTDIRECTORY = "";
            REG_KEY = "LegionDirectory";
            WEBVIEW_SOURCE = "http://legionhaven.com/changelog.html";
            MINIMAL_CLIENT_URI = "http://legionhaven.com/minimalclient.zip";
            CLIENT_URI = "http://legionhaven.com/client.zip";
            ADDON_URI = "http://legionhaven.com/addons/";
            InitializeExpansion();
        }
        private void btnWotlk_Click(object sender, EventArgs e)
        {
            GAME = "wrath";
            this.BackgroundImage = Properties.Resources.wrath;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            btnPlay.Text = "Play Wrath of the Lich King!";
            btnInstall.Text = "Install Wrath!";
            CLIENTDIRECTORY = "";
            REG_KEY = "WotlkDirectory";
            WEBVIEW_SOURCE = "http://legionhaven.com/wotlk/changelog.html";
            MINIMAL_CLIENT_URI = null;
            CLIENT_URI = "http://legionhaven.com/wotlk/client.zip";
            ADDON_URI = "http://legionhaven.com/wotlk/addons/";
            InitializeExpansion();
            GET_PATCH = "true";
        }
    }
}
