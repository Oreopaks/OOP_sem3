using System;
using System.Collections.Generic;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для управления курсами
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Добавить курс в систему
        /// </summary>
        void AddCourse(Course course);

        /// <summary>
        /// Удалить курс из системы
        /// </summary>
        void RemoveCourse(Guid courseId);

        /// <summary>
        /// Назначить преподавателя на курс
        /// </summary>
        void AssignTeacher(Guid courseId, Teacher teacher);

        /// <summary>
        /// Добавить студента на курс
        /// </summary>
        void AddStudent(Guid courseId, Student student);

        /// <summary>
        /// Получить список студентов курса
        /// </summary>
        List<Student> GetStudents(Guid courseId);

        /// <summary>
        /// Получить все курсы
        /// </summary>
        List<Course> GetAllCourses();

        /// <summary>
        /// Получить курс по ID
        /// </summary>
        Course GetCourseById(Guid courseId);
    }
}