using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ce103_hw5_snake_dll;

namespace ce103_hw5_snake_test
{
    [TestClass]
    public class UnitTest1
    {
        Class1 class1 = new Class1();

        [TestMethod]
        public void collisionSnake_test1()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;

            Assert.AreEqual(false, class1.collisionSnake(6, 19, snakeXY, 4, 0));
        }
        [TestMethod]
        public void collisionSnake_test2()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;

            Assert.AreEqual(true, class1.collisionSnake(40, 10, snakeXY, 8, 0));
        }
        [TestMethod]
        public void collisionSnake_test3()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;

            Assert.AreEqual(false, class1.collisionSnake(38, 16, snakeXY, 22, 1));
        }
        [TestMethod]
        public void eatFood_test1()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            Assert.AreEqual(false, class1.eatFood(snakeXY, foodXY));
        }
        [TestMethod]
        public void eatFood_test2()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 33;
            snakeXY[1, 0] = 10;
            int[] foodXY = { 5, 5 };
            foodXY[0] = 33;
            foodXY[1] = 10;
            Assert.AreEqual(true, class1.eatFood(snakeXY, foodXY));
        }
        [TestMethod]
        public void eatFood_test3()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 48;
            snakeXY[1, 0] = 23;
            int[] foodXY = { 5, 5 };
            foodXY[0] = 48;
            foodXY[1] = 23;
            Assert.AreEqual(true, class1.eatFood(snakeXY, foodXY));
        }
        [TestMethod]
        public void collision_detection_test1()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;
            Assert.AreEqual(0, class1.collisionDetection(snakeXY, 80, 25, 7));
        }
        [TestMethod]
        public void collision_detection_test2()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 1;
            snakeXY[1, 0] = 1;
            Assert.AreEqual(1, class1.collisionDetection(snakeXY, 80, 25, 7));
        }
        [TestMethod]
        public void collision_detection_test3()
        {
            int[,] snakeXY = new int[2, 310];
            snakeXY[0, 0] = 1;
            snakeXY[1, 0] = 1;
            Assert.AreEqual(1, class1.collisionDetection(snakeXY, 42, 32, 4));
        }
    }
}
