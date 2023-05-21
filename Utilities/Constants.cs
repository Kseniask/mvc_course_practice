using System;
using Vidly.Models;

namespace Vidly.Utilities
{
    public static class Constants
    {
        public static readonly List<Customer> Customers =
            new List<Customer> {
                new Customer { Id = 1, Name = "John Smith"},
                new Customer { Id = 2, Name = "Ana Bobkins"}
            };

    }
}

