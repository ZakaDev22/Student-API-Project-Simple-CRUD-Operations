using Student_API_Project_v1___Get_ALL.Model;

namespace Student_API_Project_v1___Get_ALL.DataSimulation
{
    public class clsStudentsData
    {
        public static readonly List<clsStudents> StudentsList = new List<clsStudents>
        {
            new clsStudents {Id =1,Name = "Zaka", Age =22,Grade = 90},
              new clsStudents {Id =2,Name = "Hamza", Age =27,Grade = 90},
               new clsStudents {Id =3,Name = "ziko", Age =22,Grade = 99},
                new clsStudents {Id =4,Name = "Omar", Age =20,Grade = 90},
                 new clsStudents {Id =5,Name = "SS", Age =22,Grade = 88},
                  new clsStudents {Id =6,Name = "Test", Age =19,Grade = 49}
        };
    }
}
