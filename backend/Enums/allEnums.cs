using System;
using System.ComponentModel;

namespace backend.Enums
{
    public enum BloodType
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }

    public static class BloodTypeExtensions
    {
        public static string ToDatabaseString(this BloodType bloodType)
        {
            return bloodType switch
            {
                BloodType.A_Positive => "A+",
                BloodType.A_Negative => "A-",
                BloodType.B_Positive => "B+",
                BloodType.B_Negative => "B-",
                BloodType.AB_Positive => "AB+",
                BloodType.AB_Negative => "AB-",
                BloodType.O_Positive => "O+",
                BloodType.O_Negative => "O-",
                _ => throw new ArgumentOutOfRangeException(nameof(bloodType), bloodType, null)
            };
        }

        public static BloodType FromDatabaseString(string bloodType)
        {
            return bloodType switch
            {
                "A+" => BloodType.A_Positive,
                "A-" => BloodType.A_Negative,
                "B+" => BloodType.B_Positive,
                "B-" => BloodType.B_Negative,
                "AB+" => BloodType.AB_Positive,
                "AB-" => BloodType.AB_Negative,
                "O+" => BloodType.O_Positive,
                "O-" => BloodType.O_Negative,
                _ => throw new ArgumentException("Invalid blood type", nameof(bloodType))
            };
        }
    }


    public enum Status
    {
        D,
        P,
    }

    public enum Gender
    {
        M,
        F
    }

    public enum UserRole
    {
        A,
        U
    }

    public enum DocumentType
    {
        P,
        C
    }
}