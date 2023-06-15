
namespace SuziVAT
{
    class Obligation
    {
        public string start { get; set; }
        public string end { get; set; }
        public string due { get; set; }
        public string status { get; set; }
        public string periodKey { get; set; }
        public string received { get; set; }
    }

    class Obligations
    {
        public Obligation[] obligations { get; set; }
    }
}
