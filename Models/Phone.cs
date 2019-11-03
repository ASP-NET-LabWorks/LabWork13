﻿using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork13.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Фамилия"), MaxLength(30)]
        public string LastName { get; set; }

        [Required, Display(Name = "Имя"), MaxLength(30)]
        public string FirstName { get; set; }

        [Required, Display(Name = "Отчество"), MaxLength(30)]
        public string MiddleName { get; set; }

        [Required, Display(Name = "Дата рождения"), DataType(DataType.Date)]
        [DateOfBirthValidator(MaxAge = 120)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Телефонный номер"), MaxLength(10), Index(IsUnique = true)]
        public string Number { get; set; }
    }

    public class PhoneDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
    }

    public class DateOfBirthValidator : ValidationAttribute
    {
        public int MaxAge { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var date = DateTime.Parse(value.ToString());
                var currentDate = DateTime.Now;
                if (date < currentDate && date > currentDate.AddYears(-MaxAge))
                    return ValidationResult.Success;
                throw new Exception();
            }
            catch
            {
                return new ValidationResult("Недопустимая дата рождения");
            }
        }
    }
}