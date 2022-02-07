﻿using IesSchool.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.IServices
{
    public interface IMobileService
    {
        public bool IsParentExist(string UserName, string Password);
        public ResponseDto Login(string UserName, string Password);
        public ResponseDto GetParentById(int parentId);
        public ResponseDto GetEventsImageGroubedByEventId();
        public ResponseDto GetParentStudents(int parentId);
        public ResponseDto GetStudentsEventsByParentId(int parentId);
        public ResponseDto GetStudentIepsItpsIxps(int studentId);
    }
}
