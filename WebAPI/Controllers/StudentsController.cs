using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles =RolesDto.Role_Admin)]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudenService _studentService;

        public StudentsController(IStudenService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Students
        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                var response = await _studentService.GetAllStudent();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return ErrorResponse.ReturnErrorResponse(ex.Message);
            }

        }
        [HttpPost("AddStudent")]
        public async Task<ActionResult> AddStundent([FromBody]StudentDTO studentDTO)
        {
            try
            {
                var response = await _studentService.AddStudent(studentDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return ErrorResponse.ReturnErrorResponse(ex.Message);
            }
        }
        [HttpGet("GetStudentById/{studentId}")]
        public async Task<ActionResult> GetStudentById(int studentId)
        {
            try
            {
                var response = await _studentService.GetStudentById(studentId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return ErrorResponse.ReturnErrorResponse(ex.Message);
            }
        }
        [HttpPut("UpdateStudent")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentDTO updateStudentDTO)
        {
            try
            {
                var response = await _studentService.UpdateStudent(updateStudentDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return ErrorResponse.ReturnErrorResponse(ex.Message);
            }
        }
            [HttpDelete("DeleteStundent")]
            public async Task<ActionResult> DeleteStundent([FromBody] DeleteStudentDTO deleteStudentDTO)
            {
                try
                {
                    var response = await _studentService.DeleteStudent(deleteStudentDTO);
                    return Ok(response);
                }
                catch (Exception ex)
                {

                    return ErrorResponse.ReturnErrorResponse(ex.Message);
                }
            }
    }
}

