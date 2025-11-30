using System;
using System.Collections.Generic;
using System.Linq;
using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Services
{
    /// <summary>
    /// Сервис для управления преподавателями
    /// </summary>
    public class TeacherService : ITeacherService
    {
        private readonly List<Teacher> _teachers;
        private readonly ICourseService _courseService;

        public TeacherService(ICourseService courseService)
        {
            _teachers = new List<Teacher>();
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
        }

        public void AddTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            if (_teachers.Any(t => t.Id == teacher.Id))
                throw new InvalidOperationException($"Преподаватель с ID {teacher.Id} уже существует");

            _teachers.Add(teacher);
        }

        public Teacher GetTeacherById(Guid teacherId)
        {
            return _teachers.FirstOrDefault(t => t.Id == teacherId);
        }

        public List<Course> GetCoursesByTeacher(Guid teacherId)
        {
            var teacher = GetTeacherById(teacherId);
            if (teacher == null)
                throw new KeyNotFoundException($"Преподаватель с ID {teacherId} не найден");

            var allCourses = _courseService.GetAllCourses();
            return allCourses.Where(c => c.Teacher != null && c.Teacher.Id == teacherId).ToList();
        }

        public List<Teacher> GetAllTeachers()
        {
            return new List<Teacher>(_teachers);
        }
    }
}