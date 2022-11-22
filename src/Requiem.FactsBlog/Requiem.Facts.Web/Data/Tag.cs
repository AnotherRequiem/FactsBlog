﻿namespace Requiem.Facts.Web.Data
{
    public class Tag : Identity
    {
        public string Name { get; set; }

        public ICollection<Fact> Facts { get; set; }

        public int Number { get; set; }
    }
}
