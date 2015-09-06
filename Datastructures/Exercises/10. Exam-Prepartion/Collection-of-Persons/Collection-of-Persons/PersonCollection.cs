using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private int conunt;
    private Dictionary<string, Person> EmailToPerson { get; set; }
    private Dictionary<string, OrderedDictionary<string, Person>> TownToPerson { get; set; }
    private Dictionary <string, SortedDictionary<string, Person>> EmailDomainToPersons{ get; set; }
    private Dictionary<string, OrderedMultiDictionary<string, Person>> TownToPersonByNames { get; set; }
    private OrderedDictionary<int, OrderedDictionary<string, Person>> AgeToPerson { get; set; } 
    private OrderedDictionary<int, OrderedMultiDictionary<string, Person>> TownToPersonByAge { get; set; }


    public PersonCollection()
    {
        this.EmailToPerson = new Dictionary<string, Person>();
        this.TownToPerson = new Dictionary<string, OrderedDictionary<string, Person>>();
        this.AgeToPerson = new OrderedDictionary<int, OrderedDictionary<string, Person>>();

        this.EmailDomainToPersons = new Dictionary<string, SortedDictionary<string, Person>>();
        this.TownToPersonByNames = new Dictionary<string, OrderedMultiDictionary<string, Person>>();
        this.TownToPersonByAge = new OrderedDictionary<int, OrderedMultiDictionary<string, Person>>();
        this.conunt = 0;
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        var newPerson = new Person
        {
            Email = email,
            Age = age,
            Name = name,
            Town = town
        };

        if (!this.EmailToPerson.ContainsKey(newPerson.Email))
        {
            this.EmailToPerson.Add(newPerson.Email, newPerson);

            if (this.TownToPerson.ContainsKey(newPerson.Town))
            {
                this.TownToPerson[newPerson.Town].Add(newPerson.Email, newPerson);
            }
            else
            {
                this.TownToPerson.Add(newPerson.Town, new OrderedDictionary<string, Person>() { { newPerson.Email, newPerson } });
            }

            if (this.AgeToPerson.ContainsKey(newPerson.Age))
            {
                this.AgeToPerson[newPerson.Age].Add(newPerson.Email, newPerson);
            }
            else
            {
                this.AgeToPerson.Add(newPerson.Age, new OrderedDictionary<string, Person>() { { newPerson.Email, newPerson } });
            }

            var emailDomain = "@" + email.Split(new[] {"@"}, StringSplitOptions.RemoveEmptyEntries)[1];
            if (this.EmailDomainToPersons.ContainsKey(emailDomain))
            {
                this.EmailDomainToPersons[emailDomain].Add(newPerson.Email, newPerson);
            }
            else
            {
                this.EmailDomainToPersons.Add(emailDomain, new SortedDictionary<string, Person>(){ { newPerson.Email, newPerson}});
            }

            if (this.TownToPersonByNames.ContainsKey(newPerson.Name))
            {
                this.TownToPersonByNames[newPerson.Name].Add(newPerson.Town, newPerson);
            }
            else
            {
                this.TownToPersonByNames.Add(newPerson.Name, new OrderedMultiDictionary<string, Person>(true) { {newPerson.Town, newPerson} });
            }

            if (this.TownToPersonByAge.ContainsKey(newPerson.Age))
            {
                this.TownToPersonByAge[newPerson.Age].Add(newPerson.Town, newPerson);
            }
            else
            {
                this.TownToPersonByAge.Add(newPerson.Age, new OrderedMultiDictionary<string, Person>(true) { {newPerson.Town, newPerson} });
            }

            this.conunt++;
            return true;
        }

        return false;
    }

    public int Count => this.conunt;

    public Person FindPerson(string email)
    {
        Person finded;
        this.EmailToPerson.TryGetValue(email, out finded);
        return finded;
    }

    public bool DeletePerson(string email)
    {
        var deleted = this.FindPerson(email);
        if (deleted !=null)
        {
            this.EmailToPerson.Remove(email);
            this.TownToPerson[deleted.Town].Remove(deleted.Email);
            this.AgeToPerson[deleted.Age].Remove(deleted.Email);
            var emailDomain = "@" + deleted.Email.Split(new[] {"@"}, StringSplitOptions.RemoveEmptyEntries)[1];
            this.EmailDomainToPersons[emailDomain].Remove(deleted.Email);
            this.TownToPersonByAge[deleted.Age].Remove(deleted.Town, deleted);
            this.TownToPersonByNames[deleted.Name].Remove(deleted.Town, deleted);
            this.conunt--;

            return true;
        }

        return false;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        var key = "@" + emailDomain;
        if (this.EmailDomainToPersons.ContainsKey(key))
        {
            return this.EmailDomainToPersons[key].Values;
        }
        else
        {
            return new List<Person>();
        }
        
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        if (this.TownToPersonByNames.ContainsKey(name))
        {
            return this.TownToPersonByNames[name][town].OrderBy(p => p.Email);
        }
        else
        {
            return new List<Person>();
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
            return this.AgeToPerson
            .Range(startAge, true, endAge, true)
            .Values
            .SelectMany(i => i.Values);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        return this.TownToPersonByAge.Range(startAge, true, endAge, true)
            .SelectMany(p => p.Value[town])
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);
    }
}
