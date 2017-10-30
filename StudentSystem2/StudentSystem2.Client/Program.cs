
namespace StudentSystem2.Client
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem2.Data;
    using StudentSystem2.Domain;
    using StudentSystem2.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {

        static void Main()
        {
            using (DbContextStudentSystem db = new DbContextStudentSystem())
            {
                db.Database.Migrate();

                //SeedStudents(db);
                //SeedCourses(db);
                //SeedResourses(db);
                //SeedStudentsToCourses(db);
                //SeedHomeWork(db);
                //PrintStudentsHomeworks(db);
                //PrintCoursesAndResourses(db);
                //PrintCoursesWithMoreThanFiveResourses(db);
                //PrintActiveCourses(db);
                //PrintStudentCoursesAndPrices(db);
                //SeedLicenses(db);
                // PrintCoursesResoursesLicenses(db);
                PrintStudentCoursesResoursesLicenses(db);
                
            }

        }

        private static void PrintStudentCoursesResoursesLicenses(DbContextStudentSystem db)
        {
            var result = db
               .Students
               .Select(s => new
               {
                   StudentName=s.Name,
                   CoursesCount=s.Courses.Count,
                   TotalNumberResourses=s.Courses.Sum(r=> r.Course.Resources.Count),
                   TotalNumberLicenses= s.Courses.Sum(r=>r.Course.Resources.Sum(l=>l.Licenses.Count)),
                   TotalLicenses2= s.Courses.Sum(r=>r.Course.Resources.SelectMany(d=>d.Licenses).Count())
               }).OrderByDescending(c=>c.CoursesCount).ThenByDescending(r=>r.TotalNumberResourses).ThenBy(n=>n.StudentName).ToList();

            foreach (var student in result)
            {
                Console.WriteLine($"Name {student.StudentName}, Courses N {student.CoursesCount}, Res Count {student.TotalNumberResourses}, Lic Count {student.TotalNumberLicenses}, {student.TotalLicenses2}");
            }

            //2.For each student print the name, the count of courses he or she is enrolled, the total number of resources for their courses and the total number of licenses those resources have.Order the results by number of courses(descending), then by number of recourses(descending), then by name(ascending).



        }

        private static void PrintCoursesResoursesLicenses(DbContextStudentSystem db)
        {
            var result = db
                .Courses
                .OrderByDescending(c=>c.Resources.Count)
                .ThenBy(c=>c.Name)
                .Select(c => new
                {
                    c.Name,
                    ResAndLicenseNames = c.Resources.Select(r => new
                    {
                        ResName = r.Name,
                        LicNames = r.Licenses.Select(l => l.Name)
                    }).OrderByDescending(l=>l.LicNames.Count()).ThenBy(r=>r.ResName)
                });

            //1.List all courses with their corresponding resources. Print the course name, the resource name and the names of all the licenses that resource has(if any).Order the courses by resources count(descending), then by course name(ascending). Order resources by licenses count(descending) and then by name(ascending).
            foreach (var course in result.DefaultIfEmpty())
            {
                Console.WriteLine($"Course Name {course.Name}");

                foreach (var resourse in course.ResAndLicenseNames.DefaultIfEmpty())
                {
                    Console.WriteLine($".................Resourse Name {resourse.ResName}");

                    foreach (var licenseNames in resourse.LicNames.DefaultIfEmpty())
                    {
                        Console.WriteLine($"........................License Name {licenseNames}");
                    }

                }


            }

        }

        private static void SeedLicenses(DbContextStudentSystem db)
        {
            var resourses = db.Resources.ToList();
            Random random = new Random();
            foreach (var item in resourses)
            {
                var licensesCount = random.Next(1, 7);

                var licenses = new List<License>();
                for (int i = 0; i < licensesCount; i++)
                {
                    License license = new License
                    {
                        Name = $"License {item.Id} {i}",
                        ResourceId = item.Id
                    };

                    licenses.Add(license);
                }

                db.Licenses.AddRange(licenses);

            }
            db.SaveChanges();
        }

        private static void PrintStudentCoursesAndPrices(DbContextStudentSystem db)
        {
            var result = db.
                Students
                .Select(s => new
                {
                    s.Name,
                    NumbOfCourses = s.Courses.Count(),
                    TotalCoursesPrice = s.Courses.Sum(p => p.Course.Price),
                    AverageCoursePrice = s.Courses.Average(a => a.Course.Price)

                }).OrderByDescending(o => o.TotalCoursesPrice)
                .ThenByDescending(o => o.NumbOfCourses)
                .ThenBy(s => s.Name)
                .ToList();


            foreach (var item in result)
            {
                Console.WriteLine($"Name {item.Name} Number of Courses {item.NumbOfCourses} Total Price {item.TotalCoursesPrice} Average price {item.AverageCoursePrice}");
            }
            //  5.For each student, calculate the number of courses he / she has enrolled in, the total price of these courses and the average price per course for the student.
            //Print the student name, number of courses, total price and average price.Order the results by total price(descending), then by number of courses(descending) and then by the student's name (ascending).

        }

        private static void PrintActiveCourses(DbContextStudentSystem db)
        {
            DateTime givenDate = new DateTime(2017, 7, 7);

            var result = db
                .Courses
                .Where(c => c.StartDate <= givenDate && c.EndDate >= givenDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duratation = c.EndDate.Subtract(c.StartDate),
                    StudentsNumber = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsNumber)
                .ThenBy(c => c.Duratation);

            foreach (var course in result)
            {
                Console.WriteLine($"Name {course.Name} Start at {course.StartDate.ToShortDateString()}- End at {course.EndDate.ToShortDateString()} Duratation {(int)course.Duratation.TotalDays} Days, Students {course.StudentsNumber}");
            }


            //            4.  * List all courses which were active on a given date(choose the date depending on the data seeded to ensure there are results), and for each course count the number of students enrolled.
            //Print the course name, start and end date, course duration(difference between end and start date) and number of students enrolled.Order the results by the number of students enrolled(in descending order), then by duration(descending).

        }

        private static void PrintCoursesWithMoreThanFiveResourses(DbContextStudentSystem db)
        {
            var result = db
                .Courses
                .Where(c => c.Resources.Count > 2)
                .OrderByDescending(c => c.Resources.Count)
                .ThenBy(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    c.Resources.Count,
                    c.Id
                }).ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"Course Name {item.Name} Resourse Count {item.Count} Id-{item.Id} ");
            }



            //3.List all courses with more than 5 resources.Order them by resources count(descending), then by start date(descending). Print only the course name and the resource count.
        }

        private static void PrintCoursesAndResourses(DbContextStudentSystem db)
        {
            var result = db
                .Courses.
                OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    ResInfo = c.Resources
                                 .Select(r => new
                                 {
                                     r.Name,
                                     r.ResourceKind,
                                     r.URL
                                 })

                });

            foreach (var course in result)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine(course.Description);

                foreach (var item in course.ResInfo)
                {
                    Console.WriteLine($"..............{item.Name}---{item.ResourceKind}----{item.URL} ");

                }


            }
            //2.List all courses with their corresponding resources. Print the course name and description and everything for each resource. Order the courses by start date(ascending), then by end date(descending).
        }

        private static void PrintStudentsHomeworks(DbContextStudentSystem db)
        {

            var result = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    s.Id,
                    HomeworkInfo = s
                    .Homeworks
                    .Select(h => new
                    {
                        h.Content,
                        h.ContentType

                    })
                });

            foreach (var student in result)
            {
                Console.WriteLine($"student Name {student.Name} with Id {student.Id}");
                foreach (var homework in student.HomeworkInfo)
                {
                    Console.WriteLine($"Content {homework.Content} Content Type {homework.ContentType}");


                }


            }

            //1.Lists all students and their homework submissions. Print only their names and for each homework -content and content - type.
        }

        private static void SeedHomeWork(DbContextStudentSystem db)
        {
            //var studentsIds = db.Students.Select(s=>s.Id);
            var random = new Random();
            var studentsCount = db.Students.Count();

            // var students = db.Entry(Students).ToList();
            // var stud = db.Entity
            for (int i = 0; i < studentsCount; i++)
            {
                var student = db.Students.Find(i + 1);
                db.Entry(student).Collection(c => c.Courses).Load();
                // var currentStudent = students[i];
                var currentCoursesIds = student
                    .Courses
                    .Select(c => c.CourseId)
                    .ToList();
                for (int j = 0; j < currentCoursesIds.Count; j++)
                {
                    var homeWorksNumber = random.Next(4, 10);
                    var types = Enum.GetValues(typeof(ContentType)).Cast<int>().ToArray();


                    for (int k = 0; k < homeWorksNumber; k++)
                    {
                        var homework = new Homework

                        {
                            Content = $"Homework number {i} {j}",
                            ContentType = (ContentType)random.Next(1, types.Length),
                            CourseId = currentCoursesIds[j],
                            StudentId = student.Id,
                            SumbisionDate = DateTime.Now.AddDays(-random.Next(20, 100))


                        };



                        db.Homeworks.Add(homework);

                    }
                }
            }

            db.SaveChanges();
        }

        private static void SeedStudentsToCourses(DbContextStudentSystem db)
        {
            Random random = new Random();
            var students = db.Students;
            var coursesIds = db.Courses.Select(id => id.Id).ToList();
            var courses = db.Courses.ToList();
            foreach (var student in students)
            {
                var coursesPerStudent = random.Next(2, 6);

                for (int i = 1; i < coursesPerStudent; i++)
                {

                    var courseIdAdd = random.Next(1, coursesIds.Count);

                    bool IsPairAlreadyExist = students
                       .Any(s => s.Id == student.Id && s.Courses.Any(c => c.CourseId == courseIdAdd));

                    if (IsPairAlreadyExist)
                    {
                        i--;
                        continue;
                    }


                    student.Courses.Add(new StudentCourse
                    {
                        CourseId = courseIdAdd
                    });

                    db.SaveChanges();

                }




            }
        }

        private static void SeedResourses(DbContextStudentSystem db)
        {
            Random random = new Random();
            var types = Enum.GetValues(typeof(ResourceType)).Cast<int>().ToArray();

            var CourseIds = db.Courses.Select(k => k.Id).ToList();
            foreach (var courseId in CourseIds)
            {

                var resourseCountPerCourse = random.Next(2, 7);



                for (int i = 1; i < resourseCountPerCourse; i++)
                {


                    Resource resource = new Resource()
                    {
                        Name = $"Resouirse {courseId}-{i}",
                        ResourceKind = (ResourceType)types[random.Next(0, types.Length)],
                        URL = $"http:\\MyResource{random.Next(0, 100)}",
                        CourseId = courseId
                    };

                    db.Resources.Add(resource);
                }
                db.SaveChanges();

            }
        }

        private static void SeedCourses(DbContextStudentSystem db)
        {
            Random random = new Random();
            for (int i = 1; i < 21; i++) //21 number of courses
            {
                Course course = new Course()
                {
                    Name = $"Course{i}",
                    Description = $"Description{random.Next(100, 1000)}",
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - random.Next(1, 5), random.Next(0, 29)),
                    EndDate = DateTime.Now.AddDays(random.Next(1, 31)),
                    Price = random.Next(150, 250)

                };


                db.Courses.Add(course);

            }

            db.SaveChanges();

        }

        private static void SeedStudents(DbContextStudentSystem db)
        {
            Random random = new Random();

            for (int i = 1; i < 41; i++)
            {
                Student student = new Student()
                {
                    Name = $"Student{i}",
                    Birthday = new DateTime((1995 + random.Next(0, 8)), random.Next(1, 13), random.Next(1, 29)),
                    RegistrationDate = new DateTime(2005, random.Next(7, 9), random.Next(1, 29)),
                    PhoneNumber = $"08982433{random.Next(0, 10)}{random.Next(0, 10)}"
                };

                db.Students.Add(student);
            }
            db.SaveChanges();

        }
    }
}
