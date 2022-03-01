using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface ISettingService
    {
        public ResponseDto GetSetting();
        public ResponseDto EditSetting(SettingDto settingDto);
        public ResponseDto AboutUs();
        public ResponseDto AddAboutUs(SettingDto settingDto);
    }
}
