using System;
using Xunit;

namespace AreaCalculation.Tests
{
    public class FiguresTests
    {
        [Fact]
        public void CircleTests()
        {
            var circle = new Circle(5);
            var circle2 = new Circle(new Line(12));
            Assert.Equal(78.5398, circle.GetArea());
            Assert.Equal(452.3893, circle2.GetArea());
        }

        [Fact]
        public void TriangleTests()
        {
            var p1 = new Point(3, 4);
            var p2 = new Point(5, 11);
            var p3 = new Point(12, 8);
            var triangle = new Triangle(3, 4, 5);
            var triangle2 = new Triangle(new Point(2, 4), new Point(3, -8), new Point(1, 2));
            var triangle3 = new UnknownFigure(new Point(2, 4), new Point(3, -8), new Point(1, 2));
            var triangle4 = new Triangle(new Point(1.82, 1.56), new Point(1.82, 7.3), new Point(3.5, 1.56));
            var triangle5 = new Triangle(p1, p2, p3);
            var triangle6 = new UnknownFigure(p1, p2, p3);
            var triangle7 = new Triangle(new Line(p1, p2), new Line(p2, p3), new Line(p3, p1));
            Assert.Equal(6, triangle.GetArea());
            Assert.Equal(7, triangle2.GetArea(), 5);
            Assert.Equal(7, triangle3.GetArea());
            Assert.Equal(27.5, triangle5.GetArea());
            Assert.Equal(27.5, triangle6.GetArea());
            Assert.Equal(27.5, triangle7.GetArea());
            Assert.True(triangle.IsRightTriangle());
            Assert.True(triangle4.IsRightTriangle());
            Assert.Throws<ArgumentException>(() => new Triangle(3, 5, 9));
            Assert.Throws<ArgumentException>(() => new Triangle(new Point(0, 5), new Point(0, 0), new Point(0, -5)));
            Assert.Throws<ArgumentException>(() => new Triangle(new Line(5), new Line(7), new Line(12)));
        }

        [Fact]
        public void UnknownFigureTests()
        {
            var p1 = new Point(3, 4);
            var p2 = new Point(5, 11);
            var p3 = new Point(12, 8);
            var p4 = new Point(9, 5);
            var p5 = new Point(5, 6);
            var uf = new UnknownFigure(p1, p2, p3, p4, p5);
            Assert.Equal(30, uf.GetArea());
        }
    }
}