using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchIt.Models
{
    public class Group<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
    }

    public class BranchOrdersView
    {
        public int branchId;
        public string branchName;
        public string branchCity;
        public DateTime orderDate;
    }

    public class BranchCustomerView
    {
        public int BranchId;
        public string branchName;
        public string branchCity;
        public string firstName;
        public string lastName;
        public DateTime birthDate;
    }

    public class MovieDirectorView
    {
        public string Title;
        public string Description;
        public Genre Genre;
        public double Price;
        public string Image;
        public string DirectorName;
        public double Length;
        public double Rating;
        public DateTime ReleaseDate;
        public string Trailer;
    }
}