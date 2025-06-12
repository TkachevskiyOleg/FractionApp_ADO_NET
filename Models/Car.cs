namespace _24._04._2024_Lab.Models
{
    public class Car
    {
        public string Name { get; set; } = "";
        public string Color { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public int Year { get; set; }
        public double EngineVolume { get; set; }

        public Car() { }

        public Car(string name, string color, string manufacturer, int year, double engineVolume)
        {
            Name = name;
            Color = color;
            Manufacturer = manufacturer;
            Year = year;
            EngineVolume = engineVolume;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Color: {Color}, Manufacturer: {Manufacturer}, Year: {Year}, Engine Volume: {EngineVolume}L";
        }

        public string ToFileString()
        {
            return $"{Name};{Color};{Manufacturer};{Year};{EngineVolume}";
        }

        public static Car FromFileString(string line)
        {
            var parts = line.Split(';');
            if (parts.Length != 5) throw new FormatException("Invalid car data format");

            return new Car(
                parts[0],
                parts[1],
                parts[2],
                int.Parse(parts[3]),
                double.Parse(parts[4])
            );
        }
    }
}
