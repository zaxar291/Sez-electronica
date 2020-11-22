using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Delegates;
using Sede_Checker.Entities.Converters;
using Sede_Checker.Implementation.Captcha.RuCaptcha.Enums;

namespace Sede_Checker
{
    public partial class RuCaptchaForm : Form
    {
        private const double MinBalance = 20;

        private readonly SedeRuCaptchaConverter _c;

        private readonly IRecaptchaV2Resolver _recaptcha;
        public List<IRecaptchaV2ResolverTask> CaptchaResolver;

        private ILogger _l;

        public RuCaptchaForm(IRecaptchaV2Resolver resolverTask, ILogger logger)
        {
            InitializeComponent();
            _c = new SedeRuCaptchaConverter();
            this._l = logger;

            _recaptcha = resolverTask;
            _recaptcha.OnServiceStatusInfoCallback += _recaptcha_OnServiceStatusInfoCallback;
            _recaptcha.OnRecaptchaV2TaskCallback += _recaptcha_OnRecaptchaV2TaskCallback;
        }

        private void _recaptcha_OnRecaptchaV2TaskCallback(object sender, RecaptchaV2TaskResultEventArgs eventArgs)
        {   
            UpdateTableData();
        }

        private void _recaptcha_OnServiceStatusInfoCallback(object sender, CaptchaServiceInfoEventArgs eventArgs)
        {
            try
            {
                var t = $"{Math.Round(eventArgs.Balance, 2)} RUB";
                var mb = eventArgs.Balance < MinBalance; //Balance status

                if (ssRuCaptcha.InvokeRequired)
                {
                    ssRuCaptcha.BeginInvoke(new MethodInvoker(() =>
                    {
                        tsslBalanceValue.ForeColor = mb ? Color.DarkRed : Color.Green;
                        tsslBalanceValue.Text = mb ? $"{t} (We need to replenish the balance)" : t;
                    }));
                }
            }
            catch (Exception e)
            {
                this._l.Exception(e);
            }
        }

        public void UpdateTableStyle()
        {
            try
            {
                if (dgwRucaptcha.Rows.Count.Equals(0)) return;

                const int captchStatus = 0;

                foreach (DataGridViewRow row in dgwRucaptcha.Rows)
                {
                    var r = row.Cells[captchStatus].Value.ToString();

                    var font = "Microsoft Sans Serif";
                    var fontSize = 8;

                    switch ((RuCaptchaTaskStatus)Enum.Parse(typeof(RuCaptchaTaskStatus), r))
                    {
                        case RuCaptchaTaskStatus.Unknow:
                            row.DefaultCellStyle.BackColor = Color.Gray;
                            row.DefaultCellStyle.ForeColor = Color.White;
                            break;
                        case RuCaptchaTaskStatus.NotReady:
                            row.DefaultCellStyle.BackColor = Color.Gold;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                            break;
                        case RuCaptchaTaskStatus.Solved:
                            row.DefaultCellStyle.BackColor = Color.Green;
                            row.DefaultCellStyle.ForeColor = Color.White;
                            break;
                        case RuCaptchaTaskStatus.Unsolvable:
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception e)
            {
                this._l.Exception(e);
            }
        }

        public void UpdateTableData()
        {
            try
            {
                if (ReferenceEquals(_recaptcha, null)) return;

                var tsk = _recaptcha.Tasks.OrderByDescending(el => el.CreatedDateTime).ToArray();
                var d = _c.ConvertToUiEntity(tsk);

                if (ReferenceEquals(d, null) || d.Length.Equals(0)) return;

                dgwRucaptcha.Invoke(new MethodInvoker(() =>
                {
                    dgwRucaptcha.AutoGenerateColumns = false;
                    dgwRucaptcha.DataSource = d;
                }));
            }
            catch (Exception e)
            {
                this._l.Exception(e);
            }
        }

        private void dgwRucaptcha_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            UpdateTableStyle();
        }
    }
}