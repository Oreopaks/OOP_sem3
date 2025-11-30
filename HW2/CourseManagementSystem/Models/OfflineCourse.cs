using System;

namespace CourseManagementSystem.Models
{
    /// <summary>
    /// Класс офлайн-курса с указанием аудитории и корпуса
    /// </summary>
    public class OfflineCourse : Course
    {
        public string Auditorium { get; set; }
        public string Building { get; set; }

        public OfflineCourse() : base()
        {
        }

        public OfflineCourse(string name, string auditorium, string building) : base(name)
        {
            Auditorium = auditorium ?? throw new ArgumentNullException(nameof(auditorium));
            Building = building ?? throw new ArgumentNullException(nameof(building));
        }

        public override string GetCourseType()
        {
            return "Офлайн-курс";
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Аудитория: {Auditorium}, Корпус: {Building}";
        }
    }
}