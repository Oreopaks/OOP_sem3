using System;

namespace CourseManagementSystem.Models
{
    /// <summary>
    /// Класс онлайн-курса с указанием платформы
    /// </summary>
    public class OnlineCourse : Course
    {
        public string PlatformName { get; set; }

        public OnlineCourse() : base()
        {
        }

        public OnlineCourse(string name, string platformName) : base(name)
        {
            PlatformName = platformName ?? throw new ArgumentNullException(nameof(platformName));
        }

        public override string GetCourseType()
        {
            return "Онлайн-курс";
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Платформа: {PlatformName}";
        }
    }
}