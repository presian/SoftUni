using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private int count;

    public PersonCollectionSlow()
    {
        Persons = new List<Person>();
    }

    public List<Person> Persons { get; set; } 

    public bool AddPerson(string email, string name, int age, string town)
    {
        var person = new Person
        {
            Age = age,
            Email = email,
            Name = name,
            Town = town
        };
        if (this.Persons.Contains(person))
        {
            return false;
        }

        this.Persons.Add(person);
        this.count++;
        return true;
    }

    public int Count => this.count;

    public Person FindPerson(string email)
    {
        return this.Persons.FirstOrDefault(p => p.Email == email);
    }

    public bool DeletePerson(string email)
    {
        var person = this.Persons.FirstOrDefault(p => p.Email == email);
        if (person == null)
        {
            return false;
        }

        this.Persons.Remove(person);
        this.count--;
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.Persons
            .Where(p => p.Email.Contains(emailDomain))
            .OrderBy(p => p.Email);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.Persons
            .Where(p => p.Name.Equals(name) 
                && p.Town.Equals(town))
            .OrderBy(p => p.Email);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.Persons
            .Where(p => p.Age >= startAge 
                && p.Age <= endAge)
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        return this.Persons
            .Where(p => p.Town.Equals(town)
                        && p.Age >= startAge
                        && p.Age <= endAge);
    }
}
