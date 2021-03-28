using OpenKh.Common;
using OpenKh.Kh2;
using System.IO;
using Xunit;

namespace OpenKh.Tests.kh2
{
    public class CollisionTests
    {
        private const string FileName = "kh2/res/map-collision.coct";

        [Fact]
        public void IsValid() => File.OpenRead(FileName).Using(stream =>
            Coct.IsValid(stream));

        [Fact]
        public void ReadCollision() => File.OpenRead(FileName).Using(stream =>
        {
            var collision = Coct.Read(stream);

            Assert.Equal(188, collision.Nodes.Count);
            Assert.Equal(7, collision.Nodes[5].Child1);
            Assert.Equal(6, collision.Nodes[5].Child2);
            Assert.Equal(6, collision.Nodes[5].Child2);
            Assert.Equal(-4551, collision.Nodes[5].BoundingBox.Minimum.X);

            Assert.Equal(1, collision.Nodes[5].Meshes.Count);

            Assert.Equal(99, collision.Nodes[6].Meshes[0].BoundingBox.Minimum.Y);
            Assert.Equal(-3449, collision.Nodes[6].Meshes[0].BoundingBox.Maximum.X);
            Assert.Equal(1, collision.Nodes[6].Meshes[0].Collisions.Count);

            Assert.Equal(24, collision.Nodes[7].Meshes[0].Collisions[0].Vertex1);
            Assert.Equal(25, collision.Nodes[7].Meshes[0].Collisions[0].Vertex2);
            Assert.Equal(14, collision.Nodes[7].Meshes[0].Collisions[0].Vertex3);
            Assert.Equal(18, collision.Nodes[7].Meshes[0].Collisions[0].Vertex4);

            Assert.Equal(549, collision.VertexList.Count);
        });

        [Fact]
        public void WriteCollision() => File.OpenRead(FileName).Using(x =>
            Helpers.AssertStream(x, inStream =>
            {
                var outStream = new MemoryStream();
                Coct.Read(inStream).Write(outStream);

                return outStream;
            }));

        //[Theory]
        //[InlineData(Bar.EntryType.MapCollision)]
        //[InlineData(Bar.EntryType.MapCollision2)]
        //[InlineData(Bar.EntryType.CameraCollision)]
        //// [InlineData(Bar.EntryType.LightData)] BoundingBoxIndex = 32767
        //public void PreserveCollision(Bar.EntryType type) =>
        //    Helpers.AssertAllBarEntries(@"D:\Hacking\KH2\export_fm", type, stream =>
        //    {
        //        var outStream = new MemoryStream();
        //        Coct.Read(stream).Write(outStream);
        //        return outStream;
        //    });
    }
}
