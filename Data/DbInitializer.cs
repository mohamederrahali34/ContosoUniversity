using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
             new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2005-09-01")},
             new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2005-09-01")},
             new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Nino",LastName="olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")},



            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

           

            var instructors = new Instructor[]
            {
                new Instructor {FirstMidName="Kim",LastName="Abercrombie",HireDate=DateTime.Parse("1995-03-11")},
                new Instructor {FirstMidName="Mohamed",LastName="Errahali",HireDate=DateTime.Parse("1993-03-11")},
                  new Instructor {FirstMidName="Abdelilah",LastName="Errahali2",HireDate=DateTime.Parse("1952-03-11")},
                    new Instructor {FirstMidName="Abdessalam",LastName="Errahali3",HireDate=DateTime.Parse("1963-03-11")}
            };

            foreach (Instructor inst in instructors)
            {
                context.Instructors.Add(inst);
            }
            context.SaveChanges();


            var departments = new Department[]
            {
                new Department{Name="English",Budget=350000,StartDate=DateTime.Parse("2007-07-01")
                ,InstructorID=instructors.Single(i=>i.LastName=="Abercrombie").ID}
            };

            foreach (Department dept in departments)
            {
                context.Departments.Add(dept);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{Title="Chemistry",Credits=3,DepartmentID=departments.Single(s=> s.Name=="English").DepartmentID},
                     new Course{Title="Microeconomics",Credits=3,DepartmentID=departments.Single(s=> s.Name=="English").DepartmentID},
            new Course{Title="Calculus",Credits=3,DepartmentID=departments.Single(s=> s.Name=="English").DepartmentID},
            new Course{Title="Trigonometry",Credits=3,DepartmentID=departments.Single(s=> s.Name=="English").DepartmentID},
            new Course{Title="Composition",Credits=3,DepartmentID=departments.Single(s=> s.Name=="English").DepartmentID},
            new Course{Title="Literature",Credits=3,DepartmentID=departments.Single(s=> s.Name=="English").DepartmentID},

            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignements = new OfficeAssignment[]
            {
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i => i.LastName == "Abercrombie").ID ,Location="Smith 17"
                },
                new OfficeAssignment
                {
                    InstructorID=instructors.Single(i => i.LastName == "Errahali").ID ,Location="Rabat"
                },
                 new OfficeAssignment
                {
                    InstructorID=instructors.Single(i => i.LastName == "Errahali3").ID ,Location="Casablanca"
                }
        };

            foreach (OfficeAssignment o in officeAssignements)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment
                {
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                },
                new CourseAssignment
                {
                    CourseID =courses.Single(c => c.Title == "Composition").CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Errahali").ID
                }
            };

            foreach (CourseAssignment ci in courseInstructors)
            {
                context.CourseAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID = students.Single(s => s.LastName=="Alexander").ID,
                CourseID=courses.Single(c=> c.Title == "Chemistry").CourseID,Grade=Grade.A},
            new Enrollment{StudentID = students.Single(s => s.LastName=="Alonso").ID,
                CourseID=courses.Single(c=> c.Title == "Calculus").CourseID,Grade=Grade.B},
            new Enrollment{StudentID = students.Single(s => s.LastName=="Li").ID,
                CourseID=courses.Single(c=> c.Title == "Literature").CourseID,Grade=Grade.C},
             new Enrollment{StudentID = students.Single(s => s.LastName=="Norman").ID,
                CourseID=courses.Single(c=> c.Title == "Composition").CourseID,Grade=Grade.D},
            };
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s => s.Student.ID == e.StudentID && s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
                context.SaveChanges();
            }
            context.SaveChanges();


           


        }
       
    }
}
