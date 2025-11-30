using System;

namespace CourseManagementSystem.Models
{
    /// <summary>
    /// Класс студента
    /// </summary>
    public class Student
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public Student()
        {
            Id = Guid.NewGuid();
        }

        public Student(string fullName, string email) : this()
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("ФИО студента не может быть пустым", nameof(fullName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email студента не может быть пустым", nameof(email));

            FullName = fullName;
            Email = email;
        }

        public override string ToString()
        {
            return $"Студент: {FullName} (Email: {Email})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Student student)
                return Id == student.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}