using Microsoft.AspNetCore.Mvc;
using Student_API_Project_v1___Get_ALL.DataSimulation;
using Student_API_Project_v1___Get_ALL.Model;
using System.Data;

namespace Student_API_Project_v1___Get_ALL.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<clsStudents>> GetAllStudents()
        {
            if (clsStudentsData.StudentsList.Count == 0)
            {
                return NotFound("Not Students Found!");
            }

            return Ok(clsStudentsData.StudentsList);
        }

        [HttpGet("Passed", Name = "GetAllPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<clsStudents>> GetAllStudentPassed()
        {
            if (clsStudentsData.StudentsList.Count == 0)
            {
                return NotFound("Not Students Found!");
            }

            var PassedStudent = clsStudentsData.StudentsList.Where(x => x.Grade > 50);
            return Ok(PassedStudent);
        }

        [HttpGet("AVG", Name = "GetStudentsAVGGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetStudentsAVGGrade()
        {
            //clsStudentsData.StudentsList.Clear();
            if (clsStudentsData.StudentsList.Count == 0)
            {
                return NotFound("Not Students Found!");
            }

            var AVGGrade = clsStudentsData.StudentsList.Average(x => x.Grade);
            return Ok(AVGGrade);
        }

        [HttpGet("GetStudentByID/{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsStudents> GetStudentByID(int ID)
        {
            if (ID <= 0)
            {
                return BadRequest($"Bad Request With ID {ID}!");
            }

            var student = clsStudentsData.StudentsList.FirstOrDefault(x => x.Id == ID);
            if (student == null)
            {
                return NotFound("No Student Found!");
            }

            return Ok(student);
        }

        [HttpPost("AddNewStudent", Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsStudents> AddNewStudent(clsStudents NewStudent)
        {
            if (NewStudent == null || string.IsNullOrEmpty(NewStudent.Name) || NewStudent.Age < 0 || NewStudent.Grade < 0)
            {
                return BadRequest("Bad Request Informations");
            }

            NewStudent.Id = clsStudentsData.StudentsList.Max(x => x.Id) > 0 ? clsStudentsData.StudentsList.Max(x => x.Id) + 1 : 1;
            clsStudentsData.StudentsList.Add(NewStudent);

            return CreatedAtRoute("GetStudentByID", new { id = NewStudent.Id }, NewStudent);
        }

        [HttpDelete("DeleteStudentByID/{ID}", Name = "DeleteStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsStudents> DeleteStudentByID(int ID)
        {
            if (ID <= 0)
            {
                return BadRequest($"Bad Request With ID {ID}!");
            }

            var student = clsStudentsData.StudentsList.FirstOrDefault(x => x.Id == ID);
            if (student == null)
            {
                return NotFound("No Student Found!");
            }

            clsStudentsData.StudentsList.Remove(student);

            return Ok($"Student With ID {ID} Has ben Deleted.");
        }

        [HttpPut("UpdateStudentByID/{ID}", Name = "UpdateStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsStudents> UpdateStudentByID(int ID, clsStudents UpdatedStudent)
        {
            if (ID <= 0 || string.IsNullOrEmpty(UpdatedStudent.Name) || UpdatedStudent.Age <= 0 || UpdatedStudent.Grade < 0)
            {
                return BadRequest($"Bad Request With ID {ID}!");
            }

            var student = clsStudentsData.StudentsList.FirstOrDefault(x => x.Id == ID);
            if (student == null)
            {
                return NotFound("No Student Found!");
            }

            student.Age = UpdatedStudent.Age;
            student.Name = UpdatedStudent.Name;
            student.Grade = UpdatedStudent.Grade;

            return Ok(student);
        }

    }
}
