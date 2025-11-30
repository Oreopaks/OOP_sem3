using System;
using System.Collections.Generic;
using System.Linq;
using CourseManagementSystem.Interfaces;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Services
{
    /// <summary>
    /// Сервис для управления курсами
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly List<Course> _courses;

        public CourseService()
        {
            _courses = new List<Course>();
        }

        public void AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (_courses.Any(c => c.Id == course.Id))
                throw new InvalidOperationException($"Курс с ID {course.Id} уже существует");

            _courses.Add(course);
        }

        public void RemoveCourse(Guid courseId)
        {
            var course = GetCourseById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Курс с ID {courseId} не найден");

            _courses.Remove(course);
        }

        public void AssignTeacher(Guid courseId, Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            var course = GetCourseById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Курс с ID {courseId} не найден");

            course.Teacher = teacher;
        }

        public void AddStudent(Guid courseId, Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var course = GetCourseById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Курс с ID {courseId} не найден");

            course.AddStudent(student);
        }

        public List<Student> GetStudents(Guid courseId)
        {
            var course = GetCourseById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Курс с ID {courseId} не найден");

            return new List<Student>(course.Students);
        }

        public List<Course> GetAllCourses()
        {
            return new List<Course>(_courses);
        }

        public Course GetCourseById(Guid courseId)
        {
            return _courses.FirstOrDefault(c => c.Id == courseId);
        }
    }
}