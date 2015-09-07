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

    private Dictionary<string, SortedSet<Person>> TownToPersonByNames { get; set; }

    private OrderedDictionary<int, OrderedDictionary<string, Person>> AgeToPerson { get; set; } 
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> TownToPersonByAge { get; set; }


    public PersonCollection()
    {
        this.EmailToPerson = new Dictionary<string, Person>();
        this.TownToPerson = new Dictionary<string, OrderedDictionary<string, Person>>();
        this.AgeToPerson = new OrderedDictionary<int, OrderedDictionary<string, Person>>();

        this.EmailDomainToPersons = new Dictionary<string, SortedDictionary<string, Person>>();
        this.TownToPersonByNames = new Dictionary<string, SortedSet<Person>>();
        this.TownToPersonByAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
        this.conunt = 0;
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        // make new person
        var newPerson = new Person
        {
            Email = email,
            Age = age,
            Name = name,
            Town = town
        };


        // add if not exist
        if (!this.EmailToPerson.ContainsKey(newPerson.Email))
        {
            // add to main dict
            this.EmailToPerson.Add(newPerson.Email, newPerson);

            // add to town_person dict
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

            var nameTownKey = newPerson.Name + newPerson.Town;
            if (this.TownToPersonByNames.ContainsKey(nameTownKey))
            {
                this.TownToPersonByNames[nameTownKey].Add(newPerson);
            }
            else
            {
                this.TownToPersonByNames.Add(nameTownKey, new SortedSet<Person>() {newPerson});
            }

            if (this.TownToPersonByAge.ContainsKey(newPerson.Town))
            {
                if (this.TownToPersonByAge[newPerson.Town].ContainsKey(newPerson.Age))
                {
                    this.TownToPersonByAge[newPerson.Town][newPerson.Age].Add(newPerson);
                }
                else
                {
                    this.TownToPersonByAge[newPerson.Town]
                        .Add(newPerson.Age, new SortedSet<Person>() {newPerson});
                }
            }
            else
            {
                this.TownToPersonByAge.Add(newPerson.Town, new OrderedDictionary<int, SortedSet<Person>>() { {newPerson.Age, new SortedSet<Person>() { newPerson} } });
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
            this.TownToPersonByAge[deleted.Town][deleted.Age].Remove(deleted);
            this.TownToPersonByNames[deleted.Name + deleted.Town].Remove(deleted);
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
        var key = name + town;
        if (this.TownToPersonByNames.ContainsKey(key))
        {
            return this.TownToPersonByNames[key];
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
        OrderedDictionary<int, SortedSet<Person>> townResult;
        this.TownToPersonByAge.TryGetValue(town, out townResult);
        if (townResult != null)
        {
            return townResult
                .Range(startAge, true, endAge, true)
                .SelectMany(p => p.Value);
        }
        return new List<Person>();
    }
}
