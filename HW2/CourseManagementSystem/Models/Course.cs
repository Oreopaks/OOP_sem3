using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Models
{
    /// <summary>
    /// Абстрактный базовый класс для всех типов курсов
    /// </summary>
    public abstract class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        protected Course()
        {
            Id = Guid.NewGuid();
            Students = new List<Student>();
        }

        protected Course(string name) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Абстрактный метод для получения типа курса
        /// </summary>
        public abstract string GetCourseType();

        /// <summary>
        /// Добавить студента на курс
        /// </summary>
        public virtual void AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (Students.Any(s => s.Id == student.Id))
                throw new InvalidOperationException($"Студент {student.FullName} уже записан на курс");

            Students.Add(student);
        }

        /// <summary>
        /// Удалить студента с курса
        /// </summary>
        public virtual void RemoveStudent(Guid studentId)
        {
            var student = Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                Students.Remove(student);
            }
        }

        public override string ToString()
        {
            var teacherName = Teacher != null ? Teacher.FullName : "Не назначен";
            return $"{GetCourseType()}: {Name} (Преподаватель: {teacherName}, Студентов: {Students.Count})";
        }
    }
}