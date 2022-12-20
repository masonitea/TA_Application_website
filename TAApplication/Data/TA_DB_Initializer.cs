using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAApplication.Areas.Identity.Data;
using TAApplication.Models;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      12 / 10 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    Has three methods for seeding data into the three respective tables in TA_DB. They all use hardcoded data.
*/

namespace TAApplication.Data
{
    public class TA_DB_Initializer
    {
        public static void InitializeApplications(TA_DB context)
        {
            var students = new Application[]
            {
            new Application{FirstName="Krishna", LastName="Patel", uID="u0000001", PhoneNumber="801-123-4567", Address="1234 Valley Way, Salt Lake City, Utah", Degree=Degree.Bachelors, Program="CSE", GPA=4.00, Hours=20,
                    Statement="I really enjoyed the content of my Computer Science classes and would love to help other students with said content. I am willing to put in the time and work required.", EnglishFluency=EnglishFluency.Fluent, Semesters=7, LinkedIn="https://www.linkedin.com/in/krishna-patel-885aa3183/",
                    Resume="NotImplemented", CreationDate=DateTime.Now, ModificationDate=DateTime.Now, Owner="u0000001@utah.edu"},
            new Application{FirstName="Anneswa", LastName="Ghosh", uID="u0000002", PhoneNumber="801-987-6789", Address="589 Avenue Av, Millcreek, Utah", Degree=Degree.Doctorate, Program="CS", GPA=3.97, Hours=18,
                    Statement="I have experience TAing for multiple CS classes and have enjoyed working with the faculty and staff everytime. I have a real passion for helping my fellow students and sharing my knoewledge.", EnglishFluency=EnglishFluency.Native, Semesters=5, LinkedIn="https://www.linkedin.com/in/anneswaghosh/",
                    Resume="NotImplemented", CreationDate=DateTime.Now, ModificationDate=DateTime.Now, Owner="u0000002@utah.edu"},
            new Application{FirstName="Jesse", LastName="Sinclair", uID="u0000003", PhoneNumber="453-213-4567", Address="859 Digital Dr, Sandy, Utah", Degree=Degree.Bachelors, Program="CE", GPA=3.50, Hours=15,
                    Statement="Some of my favorite classes over my education here were my Computer Science classes. I really enjoy working with others and I already have experience with helping other students.", EnglishFluency=EnglishFluency.Adequate, Semesters=6, LinkedIn="https://www.linkedin.com/in/jesse-sinclair-541476123/",
                    Resume="NotImplemented", CreationDate=DateTime.Now, ModificationDate=DateTime.Now, Owner="u0000003@utah.edu"},
            new Application{FirstName="Nhan", LastName="Nguyen", uID="u0000004", PhoneNumber="386-580-6083", Address="509 Bird St, Salt Lake City, Utah ", Degree=Degree.Masters, Program="CS", GPA=3.86, Hours=22,
                    Statement="I think the School of Computing faculty and staff is incredible to work with. I'm willing to put in as much time as needed and I'm always excited to help my fellow students.", EnglishFluency=EnglishFluency.Fluent, Semesters=9, LinkedIn="https://www.linkedin.com/in/nhantrangnguyen/",
                    Resume="NotImplemented", CreationDate=DateTime.Now, ModificationDate=DateTime.Now, Owner="u0000004@utah.edu"},
            new Application{FirstName="Mason", LastName="Austin", uID="u0000005", PhoneNumber="801-453-0863", Address="301 Meadowlark Dr, Alpine, Utah", Degree=Degree.Bachelors, Program="CS", GPA=3.68, Hours=17,
                    Statement="I really like the School of Computing here at the University of Utah and want to help other students by using the knowledge I've gained here.", EnglishFluency=EnglishFluency.Native, Semesters=8, LinkedIn="https://www.linkedin.com/in/mason-austin-9a8684156/",
                    Resume="NotImplemented", CreationDate=DateTime.Now, ModificationDate=DateTime.Now, Owner="u0000005@utah.edu"}
            };
            foreach (Application s in students)
            {
                context.Applications.Add(s);
            }
            context.SaveChanges();
        }

        public static void InitializeEnrollmentData(TA_DB context, IWebHostEnvironment environment)
        {
            var dataPath = environment.WebRootFileProvider.GetFileInfo("data/temp.csv")?.PhysicalPath;

            using (var reader = new StreamReader(dataPath))
            {
                // First line contains a bunch of dates
                var head = reader.ReadLine();
                var dates = head.Split(',');
                DateTime[] enrollmentDates = new DateTime[dates.Length - 1];

                // Build a date time for every date
                int index = 0;
                foreach(string date in dates)
                {
                    // First column is the course, rest are dates
                    if (date.Equals("Course"))
                    {
                        continue;
                    }

                    // Month and day are split by a whitespace
                    string trimmedDate = date.Trim();
                    string[] monthDay = trimmedDate.Split();
                    int day = int.Parse(monthDay[1]);
                    int month = 0;

                    // Convert the month to its numeric value
                    // The month should be only 3 letters
                    switch (monthDay[0].ToUpper())
                    {
                        case "JAN":
                            month = 1;
                            break;
                        case "FEB":
                            month = 2;
                            break;
                        case "MAR":
                            month = 3;
                            break;
                        case "APR":
                            month = 4;
                            break;
                        case "MAY":
                            month = 5;
                            break;
                        case "JUN":
                            month = 6;
                            break;
                        case "JUL":
                            month = 7;
                            break;
                        case "AUG":
                            month = 8;
                            break;
                        case "SEP":
                            month = 9;
                            break;
                        case "OCT":
                            month = 10;
                            break;
                        case "NOV":
                            month = 11;
                            break;
                        case "DEC":
                            month = 12;
                            break;
                        default:
                            throw new InvalidDataException("Error: Cannot parse month " + monthDay[0] + @" in wwwroot//data//temp.csv");
                    }

                    // Create the DateTime for the year 2021
                    enrollmentDates[index] = new DateTime(2021, month, day);
                    index++;
                }

                // Each row represents a course and an enrollment entry for the column date
                List<EnrollmentData> dataEntries = new List<EnrollmentData>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    string courseName = values[0];

                    for(int i = 0; i < enrollmentDates.Length; i++)
                    {
                        dataEntries.Add(new EnrollmentData { Course=courseName, Date=enrollmentDates[i], Enrollment=int.Parse(values[i+1])});
                    }
                }

                // Add all the entries to the context and save
                foreach(EnrollmentData data in dataEntries)
                {
                    context.EnrollmentDatas.Add(data);
                }
                context.SaveChanges();
            }
        }

        public static void InitializeCourses(TA_DB context)
        {
            var courses = new Course[]
            {
            new Course{SemesterOffered=Semester.Spring, YearOffered=2022, Name="Introduction to Computer Programming", Department="CS", Number=1400, Section="001",
                Description="This course is an introduction to the engineering and mathematical skills required to effectively program computers and is designed for students with no prior programming experience. ",
                ProfessorUnid="u1023456", ProfessorName="JOHNSON, DAVID E", OfferedDays="MoWe", OfferedTimes="3:00PM-4:20PM", Location="Web L102", CreditHours=4, Enrollment=200, Note="Needs Extra TAs!" },
            new Course{SemesterOffered=Semester.Spring, YearOffered=2022, Name="Discrete Structures", Department="CS", Number=2100, Section="001",
                Description="Introduction to propositional logic, predicate logic, formal logical arguments, finite sets, functions, relations, inductive proofs, recurrence relations, graphs, probability, and their applications to Computer Science.",
                ProfessorUnid="u0928376", ProfessorName="PARKER, DAISY", OfferedDays="TuTh", OfferedTimes="10:45AM-12:05PM", Location="Web L104", CreditHours=3, Enrollment=150, Note="" },
            new Course{SemesterOffered=Semester.Spring, YearOffered=2022, Name="Discrete Structures", Department="CS", Number=2100, Section="002",
                Description="Introduction to propositional logic, predicate logic, formal logical arguments, finite sets, functions, relations, inductive proofs, recurrence relations, graphs, probability, and their applications to Computer Science.",
                ProfessorUnid="u0928376", ProfessorName="PARKER, DAISY", OfferedDays="TuTh", OfferedTimes="9:40AM-10:30AM", Location="CRCC 205", CreditHours=3, Enrollment=150, Note="" },
            new Course{SemesterOffered=Semester.Spring, YearOffered=2022, Name="Introduction to Algorithms & Data Structures", Department="CS", Number=2420, Section="001",
                Description="This course provides an introduction to the problem of engineering computational efficiency into programs. Students will learn about classical algorithms (including sorting, searching, and graph traversal), data structures (including stacks, queues, linked lists, trees, hash tables, and graphs), and analysis of program space and time requirements. Students will complete extensive programming exercises that require the application of elementary techniques from software engineering.",
                ProfessorUnid="u0689539", ProfessorName="KOPTA, DANIEL M", OfferedDays="MoWe", OfferedTimes="3:00PM-4:20PM", Location="ABS 220", CreditHours=4, Enrollment=250, Note="" },
            new Course{SemesterOffered=Semester.Spring, YearOffered=2022, Name="Models Of Computation", Department="CS", Number=3100, Section="001",
                Description="This course covers different models of computation and how they relate to the understanding and better design of real-world computer programs. As examples, we will study Turing machines that help define the fundamental limits of computing, Push-down Automata that help build language parsers, and Finite Automata that help build string pattern matchers. This course also covers the basics of designing correctly functioning programs, and introduces the use of mathematical logic through Boolean satisfiability methods. The course will involve the use of hands-on programming exercises written at a sufficiently high level of abstraction that the connections between theory and practice are apparent.",
                ProfessorUnid="u0463703", ProfessorName="GOPALAKRISHNAN, GANESH", OfferedDays="MoWe", OfferedTimes="4:35PM-5:55PM", Location="Web L104", CreditHours=3, Enrollment=125, Note="" },
            new Course{SemesterOffered=Semester.Spring, YearOffered=2022, Name="Computer Systems", Department="CS", Number=4400, Section="001",
                Description="Introduction to computer systems from a programmer's point of view. Machine level representations of programs, optimizing program performance, memory hierarchy, linking, exceptional control flow, measuring program performance, virtual memory, concurrent programming with threads, network programming.",
                ProfessorUnid="u0871351", ProfessorName="ZHANG, MU", OfferedDays="MoWe", OfferedTimes="11:50AM-1:10PM", Location="ASB 220", CreditHours=3, Enrollment=100, Note="Only needs one more TA" }
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();
        }

        public static void InitializeAvailabilities(TA_DB TAContext, TAUsersRolesDB IDContext)
        {
            // Only u0000000@utah.edu needs to be initialized
            string userID = "";
            foreach (var user in IDContext.Users)
            {
                if (user.Email == "u0000000@utah.edu")
                {
                    userID = user.Id;
                    break;
                }
            }
            if (userID == "")
            {
                return;
            }

            // 8:00am to noon on Monday and Friday, and noon to 5:00 pm on Tuesday and Thursday.
            // Year and month are irrelevant. I chose May 2000 since May 1st 2000 is Monday.
            for(int i = 0; i < 16; i++) // Monday & Friday, 8 AM to noon
            {
                // Starts at 8:00 am, increases by 15 minutes each loop. Floor to just get the hour.
                double hour = 8 + (0.25 * i);
                hour = Math.Floor(hour);

                // Increases by 15 minute intervals, use the hour to ensure it doesn't go to 60.
                int minutes = (15 * i) - 60*((int)hour-8);

                // Create the availability objects for monday and friday with hours and minutes
                Availability monday = new Availability();
                monday.UserId = userID;
                monday.Slot = new DateTime(2000, 5, 1, (int)hour, minutes, 0);

                Availability friday = new Availability();
                friday.UserId = userID;
                friday.Slot = new DateTime(2000, 5, 5, (int)hour, minutes, 0);

                // Add them to the DB
                TAContext.Add(monday);
                TAContext.Add(friday);
            }
            for (int i = 0; i < 20; i++) // Tuesday & Thursday, Noon to 5 pm
            {
                // Starts at 12:00 pm, increases by 15 minutes each loop. Floor to just get the hour.
                double hour = 12 + (0.25 * i);
                hour = Math.Floor(hour);

                // Increases by 15 minute intervals, use the hour to ensure it doesn't go to 60.
                int minutes = (15 * i) - 60 * ((int)hour - 12);

                // Create the availability objects for tuesday and thursday with hours and minutes
                Availability tuesday = new Availability();
                tuesday.UserId = userID;
                tuesday.Slot = new DateTime(2000, 5, 2, (int)hour, minutes, 0);

                Availability thursday = new Availability();
                thursday.UserId = userID;
                thursday.Slot = new DateTime(2000, 5, 4, (int)hour, minutes, 0);

                // Add them to the DB
                TAContext.Add(tuesday);
                TAContext.Add(thursday);
            }

            TAContext.SaveChanges();
        }
    }
}
