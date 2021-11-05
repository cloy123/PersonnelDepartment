using System;

namespace PersonnelDepartment
{
    public class Worker
    {
        public string Name { get; } = "";
        public string SecondName { get; } = "";
        public string Patronymic { get; } = "";
        public string SecondNameAndInitials
        { get
            {
                if (SecondName.Length > 0 && Name.Length > 0 && Patronymic.Length > 0)
                    return $"{SecondName} {Name[0]}.{Patronymic[0]}.";
                else
                    return "";
            } 
        }
        public string Position { get; } = "";
        public DateTime EmploymentDate { get; }
        public int Experience { get
            {
                if(DateTime.Now.Month < EmploymentDate.Month)
                {
                    return DateTime.Now.Year - EmploymentDate.Year - 1;
                }
                else if(DateTime.Now.Month == EmploymentDate.Month)
                {
                    if (DateTime.Now.Day < EmploymentDate.Day)
                    {
                        return DateTime.Now.Year - EmploymentDate.Year - 1;
                    }
                    else
                    {
                        return DateTime.Now.Year - EmploymentDate.Year;
                    }
                }
                else
                {
                    return DateTime.Now.Year - EmploymentDate.Year;
                }
            } 
        }

        public Worker(string name, string secondName, string patronymic, string position, DateTime employmentDate)
        {
            if(employmentDate == null)
            {
                throw new ArgumentNullException("В аргумент employmentDate передано null");
            }
            Name = name;
            SecondName = secondName;
            Patronymic = patronymic;
            Position = position;
            EmploymentDate = employmentDate;
        }

        public Worker Clone()
        {
            return new Worker(this.Name, this.SecondName, this.Patronymic, this.Position, new DateTime(EmploymentDate.Year, EmploymentDate.Month, EmploymentDate.Day) );
        }
    }
}
