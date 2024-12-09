using System;
using System.Collections.Generic;

public class Road
{
    public double Length { get; private set; }
    public double Width { get; private set; }
    public int Lanes { get; private set; }
    public double TrafficLevel { get; set; }

    public Road(double length, double width, int lanes, double trafficLevel)
    {
        Length = length;
        Width = width;
        Lanes = lanes;
        TrafficLevel = Math.Clamp(trafficLevel, 0.0, 1.0);
    }

    public override string ToString()
    {
        return $"Road: {Length} km, {Width} m, {Lanes} strip, Traffic: {TrafficLevel * 100}%";
    }
}

public interface IDriveable
{
    void Move();
    void Stop();
}

public class Vehicle : IDriveable
{
    public string Type { get; private set; }
    public double Speed { get; set; }
    public double Size { get; private set; }

    public Vehicle(string type, double speed, double size)
    {
        Type = type;
        Speed = speed;
        Size = size;
    }

    public void Move()
    {
        Console.WriteLine($"{Type} moves with speed {Speed} km/h.");
    }

    public void Stop()
    {
        Console.WriteLine($"{Type} stopped.");
    }

    public override string ToString()
    {
        return $"Transport: {Type}, Speed: {Speed} km/h, Size: {Size} m";
    }
}

public class TrafficSimulation
{
    private List<Road> roads;
    private List<Vehicle> vehicles;

    public TrafficSimulation()
    {
        roads = new List<Road>();
        vehicles = new List<Vehicle>();
    }

    public void AddRoad(Road road)
    {
        roads.Add(road);
    }

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void SimulateIntersection()
    {
        Console.WriteLine("Traffic simulation at an intersection...");
        foreach (var road in roads)
        {
            Console.WriteLine(road);
        }

        foreach (var vehicle in vehicles)
        {
            vehicle.Speed *= 1 - roads[0].TrafficLevel;
            vehicle.Move();
        }
    }

    public void OptimizeTraffic()
    {
        Console.WriteLine("Optimization of movement...");
        foreach (var road in roads)
        {
            if (road.TrafficLevel > 0.7)
            {
                Console.WriteLine($"Congestion on the road ({road}). Regulating traffic.");
                road.TrafficLevel -= 0.1;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Road road1 = new Road(5.0, 10.0, 3, 0.8);
        Road road2 = new Road(3.0, 8.0, 2, 0.4);

        Vehicle car = new Vehicle("Car", 60.0, 4.5);
        Vehicle truck = new Vehicle("Truck", 40.0, 10.0);
        Vehicle bus = new Vehicle("Bus", 50.0, 12.0);

        TrafficSimulation simulation = new TrafficSimulation();
        simulation.AddRoad(road1);
        simulation.AddRoad(road2);
        simulation.AddVehicle(car);
        simulation.AddVehicle(truck);
        simulation.AddVehicle(bus);

        simulation.SimulateIntersection();

        simulation.OptimizeTraffic();
        simulation.SimulateIntersection();
    }
}
