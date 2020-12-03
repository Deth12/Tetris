using NUnit.Framework;
using UnityEngine;
using Tetris.Controllers;

namespace Tetris.Tests
{
    public class GridTests
    {
        [Test]
        [TestCase(1,2, ExpectedResult = true)]
        [TestCase(9,9, ExpectedResult = true)]
        [TestCase(10,10, ExpectedResult = false)]
        [TestCase(0,0, ExpectedResult = true)]
        [TestCase(5,6, ExpectedResult = true)]
        [TestCase(100,50, ExpectedResult = false)]
        public bool Grid_IsValidPositionTest(int x, int y)
        {
            GridController gridController = new GridController();

            var root = GetBlockPartSample(0, 0);
            var child = GetBlockPartSample(x, y);
            child.SetParent(root);
            
            return gridController.IsValidPositionOnGrid(root);
        }
        
        [TestCase(1,2, 4, 6, true)]
        [TestCase(5,6, 5,6, false)]
        [TestCase(1,1, 7,8, true)]
        [TestCase(1,2, 8,7, true)]
        public void Grid_UpdateGridTest(int x1, int y1, int x2, int y2, bool result)
        {
            GridController gridController = new GridController();

            var root1 = GetBlockPartSample(0, 0);
            var child1 = GetBlockPartSample(x1, y1);
            child1.SetParent(root1);
            
            gridController.UpdateGrid(root1);
            
            var root2 = GetBlockPartSample(0, 0);
            var child2 = GetBlockPartSample(x2, y2);
            child2.SetParent(root2);
            
            Assert.AreEqual(result, gridController.IsValidPositionOnGrid(root2));
        }

        private Transform GetBlockPartSample(int x, int y)
        {
            var spawnPos = new Vector2(x, y);
            var spawnRot = Quaternion.identity;
            return GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), spawnPos, spawnRot).transform;
        }
    }
}
