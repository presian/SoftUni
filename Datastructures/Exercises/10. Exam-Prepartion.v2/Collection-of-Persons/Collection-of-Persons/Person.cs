﻿using System;

public class Person : IComparable<Person>
{
    public string Email { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Town { get; set; }


    public int CompareTo(Person other)
    {
        var compare = this.Email.CompareTo(other.Email);
        return compare;
    }
}
