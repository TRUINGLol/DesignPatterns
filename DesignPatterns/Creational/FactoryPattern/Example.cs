﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.FactoryPattern.Example;

public class Point
{
    private double x, y;

    protected Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Initializes a point from either cartesian or polar
    /// </summary>
    /// <param name="a">x if cartesian, rho if polar</param>
    /// <param name="b">y if cartesian, theta if polar</param>
    /// <param name="cs"></param>
    public Point(
      double a,
      double b, // names do not communicate intent
      CoordinateSystem cs = CoordinateSystem.Cartesian)
    {
        switch (cs)
        {
            case CoordinateSystem.Polar:
                x = a * Math.Cos(b);
                y = a * Math.Sin(b);
                break;

            default:
                x = a;
                y = b;
                break;
        }

        // steps to add a new system
        // 1. augment CoordinateSystem
        // 2. change ctor
    }

    // factory property

    public static Point Origin => new Point(0, 0);

    // singleton field
    public static Point Origin2 = new Point(0, 0);

    // factory method

    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        //...
        return null;
    }

    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    // make it lazy
    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }
    }
}

internal class PointFactory
{
    public static Point NewCartesianPoint(float x, float y)
    {
        return new Point(x, y); // needs to be public
    }
}

internal class Demo
{
    private static void Main(string[] args)
    {
        var p1 = new Point(2, 3, Point.CoordinateSystem.Cartesian);
        var origin = Point.Origin;

        var p2 = Point.Factory.NewCartesianPoint(1, 2);
    }
}