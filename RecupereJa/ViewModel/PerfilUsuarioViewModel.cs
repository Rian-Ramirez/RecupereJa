namespace RecupereJa.ViewModel
{
    public class UsuarioViewModel
    {
            public string Name { get; set; }
            public string Position { get; set; }
            public string Location { get; set; }
            public string PhotoUrl { get; set; }
            public double Rating { get; set; }

            public string Phone { get; set; }
            public string Email { get; set; }
            public string Website { get; set; }
            public string Address { get; set; }

            public DateTime Birthday { get; set; }
            public string Gender { get; set; }

            public List<string> Skills { get; set; }
            public List<Workplace> Workplaces { get; set; }

        public class Workplace
        {
            public string Company { get; set; }
            public string Address { get; set; }
        }

    }
}
