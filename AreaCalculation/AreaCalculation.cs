using System;
using System.Collections.Generic;

/*
 * В основу данной библиотеки положено предположение, что заказчику вряд ли потребуется библиотека, единственной
 * функцией которой является статическое вычисление площади фигур. Поэтому в данной библиотеке фигуры реализованы
 * как объекты, которые можно "нанести" на систему координат и работать непосредственно на ней.
 * Вычисление площади происходит путём создания объекта фигуры нужного типа и вызова метода GetArea().
 * Если площать фигуры после вычисления не менялась, получить её повторно без вычислений можно через свойство Area.
 * Новые классы фигур должны быть унаследованы от класса Figure.
 */

namespace AreaCalculation
{
    public struct Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public struct Line
    {
        public double Length { get; set; }
        public Point P1;
        public Point P2;

        public Line(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
            Length = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        public Line(double length)
        {
            Length = length;
            P1 = default;
            P2 = default;
        }
    }

    public abstract class Figure
    {
        public double Area { get; protected set; }
    }

    public class Circle : Figure
    {
        // line.length - радиус круга
        // line.p1 - точка центра круга
        public Line Line;

        public Circle(double radius)
        {
            Line.Length = radius;
        }

        public Circle(Line line)
        {
            Line = line;
        }

        public double GetArea()
        {
            Area = Math.Round(Math.PI * Line.Length * Line.Length, 4);
            return Area;
        }
    }

    public class Triangle : Figure
    {
        public Line L1, L2, L3;

        public Triangle(Line l1, Line l2, Line l3)
        {
            if (l1.Length >= l2.Length + l3.Length || l2.Length >= l1.Length + l3.Length ||
                l3.Length >= l2.Length + l1.Length)
                throw new ArgumentException("Некорректная длина сторон треугольника.");

            L1 = l1;
            L2 = l2;
            L3 = l3;
        }

        public Triangle(double length1, double length2, double length3)
        {
            var l1 = new Line(length1);
            var l2 = new Line(length2);
            var l3 = new Line(length3);
            if (l1.Length >= l2.Length + l3.Length || l2.Length >= l1.Length + l3.Length ||
                l3.Length >= l2.Length + l1.Length)
                throw new ArgumentException("Некорректная длина сторон треугольника.");

            L1 = l1;
            L2 = l2;
            L3 = l3;
        }

        public Triangle(Point p1, Point p2, Point p3)
        {
            var l1 = new Line(p1, p2);
            var l2 = new Line(p2, p3);
            var l3 = new Line(p3, p1);
            if (l1.Length >= l2.Length + l3.Length || l2.Length >= l1.Length + l3.Length ||
                l3.Length >= l2.Length + l1.Length)
                throw new ArgumentException("Некорректные координаты вершин треугольника.");

            L1 = l1;
            L2 = l2;
            L3 = l3;
        }

        public double GetArea()
        {
            var p = (L1.Length + L2.Length + L3.Length) / 2;
            Area = Math.Sqrt(Math.Round(p * (p - L1.Length) * (p - L2.Length) * (p - L3.Length), 4));
            return Area;
        }

        public bool IsRightTriangle()
        {
            var eps = 1E-10D; // Точность сравнения чисел с плавающей точкой
            if (L1.Length > L2.Length && L1.Length > L3.Length)
                return Math.Abs(L1.Length * L1.Length - (L2.Length * L2.Length + L3.Length * L3.Length)) < eps;

            if (L2.Length > L1.Length && L2.Length > L3.Length)
                return Math.Abs(L2.Length * L2.Length - (L1.Length * L1.Length + L3.Length * L3.Length)) < eps;

            if (L3.Length > L1.Length && L3.Length > L2.Length)
                return Math.Abs(L3.Length * L3.Length - (L1.Length * L1.Length + L2.Length * L2.Length)) < eps;

            return false;
        }
    }

    public class UnknownFigure : Figure
    {
        public List<Line> Lines = new List<Line>();

        public UnknownFigure(params Point[] points)
        {
            for (var i = 0; i < points.Length; i++)
                if (i < points.Length - 1)
                    Lines.Add(new Line(points[i], points[i + 1]));
                else
                    Lines.Add(new Line(points[i], points[0]));
        }
        
        public double GetArea()
        {
            double sum1 = 0;
            double sum2 = 0;
            for (var i = 0; i < Lines.Count - 1; i++)
            {
                sum1 += Lines[i].P1.X * Lines[i].P2.Y;
                sum2 += Lines[i].P2.X * Lines[i].P1.Y;
            }

            Area = Math.Abs(sum1 - sum2 + Lines[^1].P1.X * Lines[0].P1.Y - Lines[0].P1.X * Lines[^1].P1.Y) / 2;
            return Area;
        }
    }
}