using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Delegates;
using Sede_Checker.Entities.DTO;
using Sede_Checker.Entities.Settings;
using Sede_Checker.Enums;
using Sede_Checker.Implementation.Services.FileService;
using Sede_Checker.Help;
using Sede_Checker.Implementation.Captcha.RuCaptcha;
using Sede_Checker.Implementation.Services.MailService;
using Sede_Checker.Implementation.Services.Proxy;
using Sede_Checker.Implementation.Services.Storage;
using Sede_Checker.Implementation.Services.Tasks;
using Sede_Checker.Implementation.StepsResolvers.Alicante;
using Sede_Checker.Implementation.StepsResolvers.Barcelona;
using Sede_Checker.Implementation.StepsResolvers.Constitution;
using Sede_Checker.Implementation.StepsResolvers.IllesBalears;
using Sede_Checker.Implementation.StepsResolvers.Madrid;
using Sede_Checker.Implementation.StepsResolvers.Murcia;
using Sede_Checker.Implementation.StepsResolvers.Girona;
using Timer = System.Threading.Timer;
using System.Collections.Generic;

namespace Sede_Checker
{
    public partial class Main : Form
    {
        private readonly Timer _processTasksTimer;
        private readonly Timer _processConstitutuionTasksTimer;
        private readonly SedeProxyService _ps;

        private readonly IStorageService<SedeAppDto> _storage;

        private int _activeProxyesCount = 1;
        private LoggerConsole _lc;
        private IPostMailer _m;
        private RuCaptchaForm _rc;

        private IFileSystemService f;

        private readonly ILogger _logger;

        private readonly IRecaptchaV2Resolver _recaptcha;

        private IStepsController _controller;

        private IStepsController _cController;

        private readonly TasksDataService _tds;

        private readonly SeaSettings _settings;
        
        public Main(SeaSettings settings)
        {
            _settings = settings;

            InitializeComponent();

            _lc = new LoggerConsole {
                MdiParent = this
            };

            _logger = _lc;

            _storage = new JsonStorageService<SedeAppDto>(SeaVariables.JsonStorageDb);
            _tds = new TasksDataService(_storage);
            _recaptcha = new RuCaptchaResolver(SeaVariables.ReCaptchaApiKey, SeaVariables.SitaCaptchaUrl, _logger);

            _ps = new SedeProxyService(_logger);
            _ps.OnProxyCallback += UpdateActiveProxyesCount;
            _ps.LaunchCheckAdressesState();

            f = new FileService(_logger);

            InitializeUiForms();

            InitializeScripts();

            var callback = new TimerCallback(LaunchTasksProccesing);
            var rand = new Random();
            var randTime = rand.Next(this._settings.TaskProccesingMinDueTimeout * 1000, this._settings.TaskProccesingMaxTimeout * 1000);
            _processTasksTimer = new Timer(callback, null, randTime, randTime);

            var cb = new TimerCallback(LaunchConstitutionTasksProccesing);
            var cRand = new Random();
            var cRandTime = cRand.Next(this._settings.ConstitutionTaskProcessingMinDueTimeout * 1000, this._settings.ConstitutionTaskProcessingMaxTimeout * 1000);
            _processConstitutuionTasksTimer = new Timer(cb, null, cRandTime, cRandTime) ;
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializeUiForms()
        {
            _rc = new RuCaptchaForm(_recaptcha, _logger)
            {
                MdiParent = this
            };

            var t = new TaskForm(_storage)
            {
                MdiParent = this
            };

            var _ctf = new ConstitutionTaskForm(_storage)
            {
                MdiParent = this
            };

            foreach (var f in MdiChildren)
                f.Show();
        }

        public void LaunchTasksProccesing(object obj)
        {   
            var tls = new TasksDataService(_storage);

            var data = tls.GetInProgressTasks();

            _logger.Info($"Initialize tasks processing -> available {data.Length} task(s)...");

            foreach (var u in data)
            {
                var a = _ps.GetAvailibleAdress();

                if (a != null)
                {
                    switch (u.ProcedureRegion)
                    {
                        case "Illes Balears":
                            _controller = new IllesBalearsController(u, a, _logger, _recaptcha, this._settings);
                            break;
                        case "Madrid":
                            _controller = new MadridController(u, a, _logger, _recaptcha, this._settings);
                            break;
                        case "Alicante":
                            _controller = new AlicanteController(u, a, _logger, _recaptcha, this._settings);
                            break;
                        case "Barcelona":
                            _controller = new BarcelonaController(u, a, _logger, _recaptcha, this._settings);
                            break;
                        case "Murcia":
                            _controller = new MurciaController(u, a, _logger, _recaptcha, this._settings);
                            break;
                        case "Girona":
                            _controller = new GironaController(u, a, _logger, _recaptcha, this._settings);
                            break;
                        default:
                            _controller = null;
                            break;
                    }

                    if (!ReferenceEquals(_controller, null))
                    {
                        _logger.Info("Controller has been initialized -> Launch task...");

                        _controller.OnStepsControllerMailCallback += ControllerMailerCallback;
                        _controller.OnStepsControllerCitaStatusCallback += CitaStatusCallback;
                        _controller.SolveStepsAsync();
                    }
                }
                else
                {
                    _logger.Warning(
                        "Sorry, but we cant't launch chrome now, we need more proxy servers to correct work, try again later");
                }
            }

            var rand = new Random();
            var randTime = rand.Next(this._settings.TaskProccesingMinDueTimeout * 1000, this._settings.TaskProccesingMaxTimeout * 1000);
            _processTasksTimer.Change(randTime, randTime);
        }

        public void LaunchConstitutionTasksProccesing(object obj)
        {
            var t = new ConstitutionTaskDataService(_storage);

            var data = t.GetInProgressTasks();

            _logger.Info($"Initialize tasks processing -> available {data.Length} task(s)...");

            foreach(var u in data)
            {
                var a = _ps.GetAvailibleAdress();
                if (a != null)
                {
                    _cController = new ConstitutionController(u, a, _logger, _recaptcha, this._settings, this._settings.SedeScripts);
                    _cController.SolveStepsAsync();
                }
            }
        }


        private void UpdateActiveProxyesCount(object sender, ProxyCallbackResult eventArgs)
        {
            if (_activeProxyesCount < _ps.NeadableProxyesToWork)
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        tsslProxyValue.Text = _activeProxyesCount.ToString();
                        tsslProxyValue.ForeColor = Color.Red;
                    }));
                }
                else
                {
                    tsslProxyValue.Text = _activeProxyesCount.ToString();
                    tsslProxyValue.ForeColor = Color.Red;
                }
            }
            else
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        tsslProxyValue.Text = _activeProxyesCount.ToString();
                        tsslProxyValue.ForeColor = Color.Green;
                    }));
                }
                else
                {
                    tsslProxyValue.Text = _activeProxyesCount.ToString();
                    tsslProxyValue.ForeColor = Color.Green;
                }
            }
            _activeProxyesCount++;
        }

        private void ControllerMailerCallback(object sender, StepsControllerMailCallbackEventArgs eventArgs)
        {
            if (ReferenceEquals(_m, null))
                _m = new MailService(_logger, _settings.MailServiceSettings);
            var attachments = new [] {eventArgs.Attachment};
            if (_m.SendReport(eventArgs.User, attachments)) 
                _logger.Info("Report was successfully sent!");
            else
                _logger.Warning("Can't send messages to selected users, look messages higher to see more info!");
        }

        private void CitaStatusCallback(object sender, StepsControllerCitaStatusCallbackEventArgs eventArgs)
        {
            var c = eventArgs.Data;

            c.CitaDateTime = DateTime.Now;

            if (eventArgs.CitaNumber.Equals(string.Empty))
            {
                c.CitaNotes = "Error occured: empty or invalid data about cita number from page";
                c.TaskStatus = TaskStatus.EXCEPTION;
            }
            else
            {
                c.CitaIdentificationNumber = eventArgs.CitaNumber;
                c.TaskStatus = TaskStatus.DONE;
                c.CitaNotes = $"Sita for user {c.CustomerNameAndSurname} received {c.CitaDateTime}";
            }

            _tds.AddOrUpdate(c);
        }

        private void InitializeScripts()
        {
            var _s = new Dictionary<string, string>();
            foreach(KeyValuePair<string, string> kvp in _settings.SedeScripts.Scripts)
            {
                _s.Add(kvp.Key, f.Load(kvp.Value));
            }
            
            _settings.SedeScripts.Scripts = _s;
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void topToBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var f in MdiChildren)
                f.Close();
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void openDataDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", SeaVariables.RootDirectoryPath);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this,
                    $"Are you sure want close the application?",
                    "Action confirmation",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s = new SeaSettingsUi(this._settings);
            s.Show();
        }
    }
}