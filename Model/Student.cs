using System.Data;
using Microsoft.Data.Sqlite;
using RepoDb.Attributes;
using RepoDb.Attributes.Parameter.Sqlite;

namespace FC.WebApi.SQLite.Model
{
    /*
     * CREATE TABLE IF NOT EXISTS [Student]
     (
	    RoleNo INTEGER PRIMARY KEY AUTOINCREMENT,
	    FirstName TEXT,LastName TEXT, DateOfBirth DATETIME,
	    Gender INTEGER, PlaceOfBirth TEXT
     );
     */
    public class Student
    {
        public long RoleNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public string? PlaceOfBirth { get; set; }

    }

    public enum Gender
    {
        Male,
        Female,
        NA
    }
    
    /// <summary>
    /// CREATE TABLE IF NOT EXISTS [Person]
    /// (
    ///    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ///    Name TEXT, Age INTEGER, CreatedDateUtc DATETIME
    /// );
    /// </summary>
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
    
}
