using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IParamedicalServicesService
    {
        public ResponseDto GetParamedicalServices();
        public ResponseDto GetParamedicalServiceById(int paramedicalServiceId);
        public ResponseDto AddParamedicalService(ParamedicalServiceDto paramedicalServiceDto);
        public ResponseDto EditParamedicalService(ParamedicalServiceDto paramedicalServiceDto);
        public ResponseDto DeleteParamedicalService(int paramedicalServiceId);
    }
}
