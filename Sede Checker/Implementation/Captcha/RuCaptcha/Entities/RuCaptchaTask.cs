using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Implementation.Captcha.RuCaptcha.Enums;
using System;

namespace Sede_Checker.Implementation.Captcha.RuCaptcha.Entities
{
    public class RuCaptchaTask : IRecaptchaV2ResolverTask
    {
        public RuCaptchaTask(string requestId)
        {
            RequestId = requestId;
            CreatedDateTime = DateTime.Now;
        }

        public string RequestId { get; }

        public string RequestUrl { set; get; } 

        public string CaptchaSolution {

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this._captchaSolution = value;
                    this.Status = RuCaptchaTaskStatus.Solved;
                    this.SolvedDateTime = DateTime.Now;
                }
            }
            get => this. _captchaSolution;
        }

        public RuCaptchaTaskStatus Status { set; get; }

        public DateTime CreatedDateTime { get; }

        public DateTime SolvedDateTime { private set; get; }
        
        private string _captchaSolution;
    }
}
