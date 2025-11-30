using System;
using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

namespace CourseManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления курсами и преподавателями ===\n");

            // Инициализация сервисов
            ICourseService courseService = new CourseService();
            ITeacherService teacherService = new TeacherService(courseService);

            try
            {
                // 1. Создание преподавателей
                Console.WriteLine("1. Создание преподавателей...");
                var teacher1 = new Teacher("Иванов Иван Иванович");
                var teacher2 = new Teacher("Петрова Мария Сергеевна");
                
                teacherService.AddTeacher(teacher1);
                teacherService.AddTeacher(teacher2);
                Console.WriteLine($"   ✓ {teacher1.FullName}");
                Console.WriteLine($"   ✓ {teacher2.FullName}\n");

                // 2. Создание курсов
                Console.WriteLine("2. Создание курсов...");
                var onlineCourse1 = new OnlineCourse("Программирование на C#", "Microsoft Learn");
                var onlineCourse2 = new OnlineCourse("Основы баз данных", "Coursera");
                var offlineCourse1 = new OfflineCourse("Алгоритмы и структуры данных", "301", "Главный корпус");
                
                courseService.AddCourse(onlineCourse1);
                courseService.AddCourse(onlineCourse2);
                courseService.AddCourse(offlineCourse1);
                Console.WriteLine($"   ✓ {onlineCourse1.Name} ({onlineCourse1.GetCourseType()})");
                Console.WriteLine($"   ✓ {onlineCourse2.Name} ({onlineCourse2.GetCourseType()})");
                Console.WriteLine($"   ✓ {offlineCourse1.Name} ({offlineCourse1.GetCourseType()})\n");

                // 3. Назначение преподавателей на курсы
                Console.WriteLine("3. Назначение преподавателей на курсы...");
                courseService.AssignTeacher(onlineCourse1.Id, teacher1);
                courseService.AssignTeacher(onlineCourse2.Id, teacher1);
                courseService.AssignTeacher(offlineCourse1.Id, teacher2);
                Console.WriteLine($"   ✓ {teacher1.FullName} → {onlineCourse1.Name}");
                Console.WriteLine($"   ✓ {teacher1.FullName} → {onlineCourse2.Name}");
                Console.WriteLine($"   ✓ {teacher2.FullName} → {offlineCourse1.Name}\n");

                // 4. Создание и добавление студентов
                Console.WriteLine("4. Добавление студентов на курсы...");
                var student1 = new Student("Сидоров Петр Алексеевич", "sidorov@example.com");
                var student2 = new Student("Козлова Анна Викторовна", "kozlova@example.com");
                var student3 = new Student("Смирнов Дмитрий Игоревич", "smirnov@example.com");

                courseService.AddStudent(onlineCourse1.Id, student1);
                courseService.AddStudent(onlineCourse1.Id, student2);
                courseService.AddStudent(onlineCourse2.Id, student1);
                courseService.AddStudent(offlineCourse1.Id, student3);
                Console.WriteLine($"   ✓ {student1.FullName} → {onlineCourse1.Name}");
                Console.WriteLine($"   ✓ {student2.FullName} → {onlineCourse1.Name}");
                Console.WriteLine($"   ✓ {student1.FullName} → {onlineCourse2.Name}");
                Console.WriteLine($"   ✓ {student3.FullName} → {offlineCourse1.Name}\n");

                // 5. Вывод информации о курсах
                Console.WriteLine("5. Все курсы в системе:");
                var allCourses = courseService.GetAllCourses();
                foreach (var course in allCourses)
                {
                    Console.WriteLine($"   • {course}");
                }
                Console.WriteLine();

                // 6. Вывод курсов конкретного преподавателя
                Console.WriteLine($"6. Курсы преподавателя {teacher1.FullName}:");
                var teacher1Courses = teacherService.GetCoursesByTeacher(teacher1.Id);
                foreach (var course in teacher1Courses)
                {
                    Console.WriteLine($"   • {course.Name} ({course.GetCourseType()})");
                }
                Console.WriteLine();

                // 7. Вывод студентов конкретного курса
                Console.WriteLine($"7. Студенты курса '{onlineCourse1.Name}':");
                var courseStudents = courseService.GetStudents(onlineCourse1.Id);
                foreach (var student in courseStudents)
                {
                    Console.WriteLine($"   • {student}");
                }
                Console.WriteLine();

                // 8. Удаление курса
                Console.WriteLine("8. Удаление курса...");
                courseService.RemoveCourse(onlineCourse2.Id);
                Console.WriteLine($"   ✓ Курс '{onlineCourse2.Name}' удален\n");

                Console.WriteLine("9. Курсы после удаления:");
                allCourses = courseService.GetAllCourses();
                foreach (var course in allCourses)
                {
                    Console.WriteLine($"   • {course.Name}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Ошибка: {ex.Message}");
            }

            Console.WriteLine("\n=== Программа завершена ===");
            Console.ReadKey();
        }
    }
}