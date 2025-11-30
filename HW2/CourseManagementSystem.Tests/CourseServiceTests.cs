using System;
using System.Collections.Generic;
using Xunit;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

namespace CourseManagementSystem.Tests
{
    public class CourseServiceTests
    {
        [Fact]
        public void AddCourse_ValidCourse_ShouldAddSuccessfully()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");

            // Act
            service.AddCourse(course);
            var allCourses = service.GetAllCourses();

            // Assert
            Assert.Single(allCourses);
            Assert.Contains(course, allCourses);
        }

        [Fact]
        public void AddCourse_NullCourse_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = new CourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddCourse(null));
        }

        [Fact]
        public void AddCourse_DuplicateCourse_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            service.AddCourse(course);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.AddCourse(course));
        }

        [Fact]
        public void RemoveCourse_ExistingCourse_ShouldRemoveSuccessfully()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            service.AddCourse(course);

            // Act
            service.RemoveCourse(course.Id);
            var allCourses = service.GetAllCourses();

            // Assert
            Assert.Empty(allCourses);
        }

        [Fact]
        public void RemoveCourse_NonExistingCourse_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var service = new CourseService();
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => service.RemoveCourse(nonExistingId));
        }

        [Fact]
        public void AssignTeacher_ValidTeacher_ShouldAssignSuccessfully()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            var teacher = new Teacher("John Doe");
            service.AddCourse(course);

            // Act
            service.AssignTeacher(course.Id, teacher);
            var retrievedCourse = service.GetCourseById(course.Id);

            // Assert
            Assert.NotNull(retrievedCourse.Teacher);
            Assert.Equal(teacher.Id, retrievedCourse.Teacher.Id);
            Assert.Equal(teacher.FullName, retrievedCourse.Teacher.FullName);
        }

        [Fact]
        public void AssignTeacher_NullTeacher_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            service.AddCourse(course);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AssignTeacher(course.Id, null));
        }

        [Fact]
        public void AssignTeacher_NonExistingCourse_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var service = new CourseService();
            var teacher = new Teacher("John Doe");
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => service.AssignTeacher(nonExistingId, teacher));
        }

        [Fact]
        public void AddStudent_ValidStudent_ShouldAddSuccessfully()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            var student = new Student("Alice Smith", "alice@example.com");
            service.AddCourse(course);

            // Act
            service.AddStudent(course.Id, student);
            var students = service.GetStudents(course.Id);

            // Assert
            Assert.Single(students);
            Assert.Contains(student, students);
        }

        [Fact]
        public void AddStudent_NullStudent_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            service.AddCourse(course);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.AddStudent(course.Id, null));
        }

        [Fact]
        public void AddStudent_NonExistingCourse_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var service = new CourseService();
            var student = new Student("Alice Smith", "alice@example.com");
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => service.AddStudent(nonExistingId, student));
        }

        [Fact]
        public void AddStudent_DuplicateStudent_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            var student = new Student("Alice Smith", "alice@example.com");
            service.AddCourse(course);
            service.AddStudent(course.Id, student);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => service.AddStudent(course.Id, student));
        }

        [Fact]
        public void GetStudents_ExistingCourse_ShouldReturnStudentsList()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            var student1 = new Student("Alice Smith", "alice@example.com");
            var student2 = new Student("Bob Johnson", "bob@example.com");
            service.AddCourse(course);
            service.AddStudent(course.Id, student1);
            service.AddStudent(course.Id, student2);

            // Act
            var students = service.GetStudents(course.Id);

            // Assert
            Assert.Equal(2, students.Count);
            Assert.Contains(student1, students);
            Assert.Contains(student2, students);
        }

        [Fact]
        public void GetStudents_NonExistingCourse_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var service = new CourseService();
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => service.GetStudents(nonExistingId));
        }

        [Fact]
        public void GetAllCourses_MultipleCourses_ShouldReturnAllCourses()
        {
            // Arrange
            var service = new CourseService();
            var course1 = new OnlineCourse("C# Programming", "Udemy");
            var course2 = new OfflineCourse("Algorithms", "101", "Building A");
            service.AddCourse(course1);
            service.AddCourse(course2);

            // Act
            var allCourses = service.GetAllCourses();

            // Assert
            Assert.Equal(2, allCourses.Count);
            Assert.Contains(course1, allCourses);
            Assert.Contains(course2, allCourses);
        }

        [Fact]
        public void GetCourseById_ExistingCourse_ShouldReturnCourse()
        {
            // Arrange
            var service = new CourseService();
            var course = new OnlineCourse("C# Programming", "Udemy");
            service.AddCourse(course);

            // Act
            var retrievedCourse = service.GetCourseById(course.Id);

            // Assert
            Assert.NotNull(retrievedCourse);
            Assert.Equal(course.Id, retrievedCourse.Id);
            Assert.Equal(course.Name, retrievedCourse.Name);
        }

        [Fact]
        public void GetCourseById_NonExistingCourse_ShouldReturnNull()
        {
            // Arrange
            var service = new CourseService();
            var nonExistingId = Guid.NewGuid();

            // Act
            var course = service.GetCourseById(nonExistingId);

            // Assert
            Assert.Null(course);
        }
    }
}