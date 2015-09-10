using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private int count;

    public PersonCollection()
    {
        this.PersonsByEmail = new Dictionary<string, Person>();
        this.PersonByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        this.PersonsByNameAmdTown = new Dictionary<string, SortedSet<Person>>();
        this.PersonsByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.PersonsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public Dictionary<string, Person> PersonsByEmail { get; set; }
    public Dictionary<string, SortedSet<Person>> PersonByEmailDomain { get; set; }
    public Dictionary<string, SortedSet<Person>> PersonsByNameAmdTown { get; set; }
    public OrderedDictionary<int, SortedSet<Person>> PersonsByAge { get; set; }
    public Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> PersonsByTownAndAge { get; set; }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.PersonsByEmail.ContainsKey(email))
        {
            return false;
        }

        var newPerson = new Person
        {
            Age = age,
            Email = email,
            Name = name,
            Town = town
        };

        this.PersonsByEmail.Add(newPerson.Email, newPerson);
        var domain = email.Substring(email.IndexOf("@") + 1);
        if (this.PersonByEmailDomain.ContainsKey(domain))
        {
            this.PersonByEmailDomain[domain].Add(newPerson);
        }
        else
        {
            this.PersonByEmailDomain.Add(domain, new SortedSet<Person>() { {newPerson} });
        }

        var nameTownKey = $"{newPerson.Name}{newPerson.Town}";
        if (this.PersonsByNameAmdTown.ContainsKey(nameTownKey))
        {
            this.PersonsByNameAmdTown[nameTownKey].Add(newPerson);
        }
        else
        {
            this.PersonsByNameAmdTown.Add(nameTownKey, new SortedSet<Person>() { {newPerson} });
        }

        if (this.PersonsByAge.ContainsKey(newPerson.Age))
        {
            this.PersonsByAge[newPerson.Age].Add(newPerson);
        }
        else
        {
            this.PersonsByAge.Add(newPerson.Age, new SortedSet<Person>() { {newPerson} });
        }

        if (this.PersonsByTownAndAge.ContainsKey(newPerson.Town))
        {
            if (this.PersonsByTownAndAge[newPerson.Town].ContainsKey(newPerson.Age))
            {
                this.PersonsByTownAndAge[newPerson.Town][newPerson.Age].Add(newPerson);
            }
            else
            {
                this.PersonsByTownAndAge[newPerson.Town].Add(newPerson.Age, new SortedSet<Person>() { {newPerson} });
            }
        }
        else
        {
            this.PersonsByTownAndAge.Add(newPerson.Town, new OrderedDictionary<int, SortedSet<Person>>() { {newPerson.Age, new SortedSet<Person>() { {newPerson} } } });
        }

        this.count++;
        return true;
    }

    public int Count => this.count;

    public Person FindPerson(string email)
    {
        if (this.PersonsByEmail.ContainsKey(email))
        {
            return this.PersonsByEmail[email];
        }

        return null;
    }

    public bool DeletePerson(string email)
    {
        Person person;
        this.PersonsByEmail.TryGetValue(email, out person);
        if (person == null)
        {
            return false;
        }

        this.PersonsByEmail.Remove(person.Email);
        var domain = person.Email.Substring(person.Email.IndexOf("@") + 1);
        this.PersonByEmailDomain[domain].Remove(person);
        this.PersonsByAge[person.Age].Remove(person);
        this.PersonsByNameAmdTown[$"{person.Name}{person.Town}"].Remove(person);
        this.PersonsByTownAndAge[person.Town][person.Age].Remove(person);

        this.count--;
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        SortedSet<Person> persons;
        this.PersonByEmailDomain.TryGetValue(emailDomain, out persons);

        if (persons != null)
        {
            return persons;
        }

        return new List<Person>();
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        SortedSet<Person> persons;
        this.PersonsByNameAmdTown.TryGetValue($"{name}{town}", out persons);

        if (persons != null)
        {
            return persons;
        }

        return new List<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.PersonsByAge
            .Range(startAge, true, endAge, true)
            .SelectMany(p=>p.Value);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (this.PersonsByTownAndAge.ContainsKey(town))
        {
            return this.PersonsByTownAndAge[town]
                .Range(startAge, true, endAge, true)
                .SelectMany(p => p.Value);
        }
        else
        {
            return new List<Person>();
        }
    }
}
