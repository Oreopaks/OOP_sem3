using System;
using System.Collections.Generic;
using Xunit;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

namespace CourseManagementSystem.Tests
{
    public class TeacherServiceTests
    {
        [Fact]
        public void AddTeacher_ValidTeacher_ShouldAddSuccessfully()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher = new Teacher("John Doe");

            // Act
            teacherService.AddTeacher(teacher);
            var allTeachers = teacherService.GetAllTeachers();

            // Assert
            Assert.Single(allTeachers);
            Assert.Contains(teacher, allTeachers);
        }

        [Fact]
        public void AddTeacher_NullTeacher_ShouldThrowArgumentNullException()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => teacherService.AddTeacher(null));
        }

        [Fact]
        public void AddTeacher_DuplicateTeacher_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher = new Teacher("John Doe");
            teacherService.AddTeacher(teacher);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => teacherService.AddTeacher(teacher));
        }

        [Fact]
        public void GetTeacherById_ExistingTeacher_ShouldReturnTeacher()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher = new Teacher("John Doe");
            teacherService.AddTeacher(teacher);

            // Act
            var retrievedTeacher = teacherService.GetTeacherById(teacher.Id);

            // Assert
            Assert.NotNull(retrievedTeacher);
            Assert.Equal(teacher.Id, retrievedTeacher.Id);
            Assert.Equal(teacher.FullName, retrievedTeacher.FullName);
        }

        [Fact]
        public void GetTeacherById_NonExistingTeacher_ShouldReturnNull()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var nonExistingId = Guid.NewGuid();

            // Act
            var teacher = teacherService.GetTeacherById(nonExistingId);

            // Assert
            Assert.Null(teacher);
        }

        [Fact]
        public void GetCoursesByTeacher_TeacherWithCourses_ShouldReturnCourses()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher = new Teacher("John Doe");
            var course1 = new OnlineCourse("C# Programming", "Udemy");
            var course2 = new OfflineCourse("Algorithms", "101", "Building A");
            
            teacherService.AddTeacher(teacher);
            courseService.AddCourse(course1);
            courseService.AddCourse(course2);
            courseService.AssignTeacher(course1.Id, teacher);
            courseService.AssignTeacher(course2.Id, teacher);

            // Act
            var teacherCourses = teacherService.GetCoursesByTeacher(teacher.Id);

            // Assert
            Assert.Equal(2, teacherCourses.Count);
            Assert.Contains(course1, teacherCourses);
            Assert.Contains(course2, teacherCourses);
        }

        [Fact]
        public void GetCoursesByTeacher_TeacherWithNoCourses_ShouldReturnEmptyList()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher = new Teacher("John Doe");
            teacherService.AddTeacher(teacher);

            // Act
            var teacherCourses = teacherService.GetCoursesByTeacher(teacher.Id);

            // Assert
            Assert.Empty(teacherCourses);
        }

        [Fact]
        public void GetCoursesByTeacher_NonExistingTeacher_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => teacherService.GetCoursesByTeacher(nonExistingId));
        }

        [Fact]
        public void GetCoursesByTeacher_OnlyReturnCoursesForSpecificTeacher()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher1 = new Teacher("John Doe");
            var teacher2 = new Teacher("Jane Smith");
            var course1 = new OnlineCourse("C# Programming", "Udemy");
            var course2 = new OnlineCourse("Python Basics", "Coursera");
            var course3 = new OfflineCourse("Algorithms", "101", "Building A");
            
            teacherService.AddTeacher(teacher1);
            teacherService.AddTeacher(teacher2);
            courseService.AddCourse(course1);
            courseService.AddCourse(course2);
            courseService.AddCourse(course3);
            courseService.AssignTeacher(course1.Id, teacher1);
            courseService.AssignTeacher(course2.Id, teacher2);
            courseService.AssignTeacher(course3.Id, teacher1);

            // Act
            var teacher1Courses = teacherService.GetCoursesByTeacher(teacher1.Id);
            var teacher2Courses = teacherService.GetCoursesByTeacher(teacher2.Id);

            // Assert
            Assert.Equal(2, teacher1Courses.Count);
            Assert.Single(teacher2Courses);
            Assert.Contains(course1, teacher1Courses);
            Assert.Contains(course3, teacher1Courses);
            Assert.DoesNotContain(course2, teacher1Courses);
            Assert.Contains(course2, teacher2Courses);
        }

        [Fact]
        public void GetAllTeachers_MultipleTeachers_ShouldReturnAllTeachers()
        {
            // Arrange
            var courseService = new CourseService();
            var teacherService = new TeacherService(courseService);
            var teacher1 = new Teacher("John Doe");
            var teacher2 = new Teacher("Jane Smith");
            teacherService.AddTeacher(teacher1);
            teacherService.AddTeacher(teacher2);

            // Act
            var allTeachers = teacherService.GetAllTeachers();

            // Assert
            Assert.Equal(2, allTeachers.Count);
            Assert.Contains(teacher1, allTeachers);
            Assert.Contains(teacher2, allTeachers);
        }

        [Fact]
        public void Constructor_NullCourseService_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new TeacherService(null));
        }
    }
}