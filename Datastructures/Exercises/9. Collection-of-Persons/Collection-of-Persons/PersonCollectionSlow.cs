using System;
using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private Dictionary<string, Person> persons;
    private int count;

    public PersonCollectionSlow()
    {
        this.Persons = new Dictionary<string, Person>();
    }

    public Dictionary<string, Person> Persons { get; set; }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.Persons.ContainsKey(email))
        {
            return false;
        }

        this.Persons.Add(email, new Person
        {
            Age = age,
            Email = email,
            Name = name,
            Town = town
        });

        this.count++;

        return true;
    }

    public int Count => this.count;

    public Person FindPerson(string email)
    {
        if (this.Persons.ContainsKey(email))
        {
            return this.Persons[email];
        }

        return null;
    }

    public bool DeletePerson(string email)
    {
        if (this.Persons.ContainsKey(email))
        {
            this.Persons.Remove(email);
            this.count--;
            return true;
        }

        return false;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.Persons
            .Where(p => p.Key.Contains("@" + emailDomain))
            .OrderBy(p=>p.Value.Email)
            .Select(p => p.Value);

    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.Persons
            .Where(p => p.Value.Name == name && p.Value.Town == town)
            .OrderBy(p => p.Value.Email)
            .Select(p => p.Value);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.Persons
            .Where(p => p.Value.Age >= startAge && p.Value.Age <= endAge)
            .OrderBy(p => p.Value.Age)
            .ThenBy(p => p.Value.Email)
            .Select(p => p.Value);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        return this.Persons
                .Where(p => p.Value.Age >= startAge 
                    && p.Value.Age <= endAge 
                    && string.Equals(p.Value.Town, town))
                .OrderBy(p => p.Value.Age)
                .ThenBy(p => p.Value.Email)
                .Select(p => p.Value);
    }
}
