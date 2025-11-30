using System;
using System.Collections.Generic;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для управления преподавателями
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// Добавить преподавателя в систему
        /// </summary>
        void AddTeacher(Teacher teacher);

        /// <summary>
        /// Получить преподавателя по ID
        /// </summary>
        Teacher GetTeacherById(Guid teacherId);

        /// <summary>
        /// Получить все курсы конкретного преподавателя
        /// </summary>
        List<Course> GetCoursesByTeacher(Guid teacherId);

        /// <summary>
        /// Получить всех преподавателей
        /// </summary>
        List<Teacher> GetAllTeachers();
    }
}