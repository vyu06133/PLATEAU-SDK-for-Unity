using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PLATEAU.RoadAdjust.RoadNetworkToMesh
{
    /// <summary>
    /// <see cref="RnmContour"/>を複数保持して1つのメッシュに相当するものです。
    /// </summary>
    [Serializable]
    internal class RnmContourMesh : IEnumerable<RnmContour>
    {
        [SerializeField] private List<RnmContour> contours = new();
        [SerializeField] private RoadReproduceSource[] sourceObjects;

        public RoadReproduceSource[] SourceObjects => sourceObjects;

        public RnmContourMesh(IEnumerable<RoadReproduceSource> sourceObjects) { this.sourceObjects = sourceObjects.ToArray(); }

        public RnmContourMesh(IEnumerable<RoadReproduceSource> sourceObjects, IEnumerable<RnmContour> contours)
            : this(sourceObjects)
        {
            this.contours = contours.ToList();
        }

        public RnmContourMesh(IEnumerable<RoadReproduceSource> sourceObjects, RnmContour contour)
            : this(sourceObjects)
        {
            this.contours = new List<RnmContour> { contour };
        }

        public int Count => contours.Count;
        public RnmContour this[int index] => contours[index];
        public void Add(RnmContour c) => contours.Add(c);

        public void AddRange(RnmContourMesh c)
        {
            foreach (var contour in c.contours) Add(contour);
        }

        public IEnumerator<RnmContour> GetEnumerator() => contours.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary> <see cref="RnmContourMesh"/>を複数保持します。 </summary>
    [Serializable]
    internal class RnmContourMeshList : IEnumerable<RnmContourMesh>
    {
        [SerializeField] private List<RnmContourMesh> meshes = new();
        public int Count => meshes.Count;

        public RnmContourMeshList(){}

        public RnmContourMeshList(IEnumerable<RnmContourMesh> contourMeshes)
        {
            this.meshes = contourMeshes.ToList();
        }
        
        public RnmContourMesh this[int index] => meshes[index];
        public void Add(RnmContourMesh c) => meshes.Add(c);

        public void AddRange(RnmContourMeshList c)
        {
            foreach (var cMesh in c.meshes) Add(cMesh);
        }
        

        public IEnumerator<RnmContourMesh> GetEnumerator() => meshes.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}