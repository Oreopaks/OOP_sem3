using System;

namespace CourseManagementSystem.Models
{
    /// <summary>
    /// Класс преподавателя
    /// </summary>
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public Teacher()
        {
            Id = Guid.NewGuid();
        }

        public Teacher(string fullName) : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО преподавателя не может быть пустым", nameof(fullName));

            FullName = fullName;
        }

        public override string ToString()
        {
            return $"Преподаватель: {FullName} (ID: {Id})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Teacher teacher)
                return Id == teacher.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}